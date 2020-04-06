#include <stdbool.h>
#include "misc.h"
#include "reloc.h"
#include "z2.h"

struct arrow_cycle_state {
    u16 frame_delay;
    s8 magic_cost;
    z2_actor_t *arrow;
};

static struct arrow_cycle_state g_arrow_cycle_state = {
    .frame_delay = 0,
    .magic_cost = 0,
    .arrow = NULL,
};

struct arrow_info {
    u8 item;
    u8 slot;
    u8 icon;
    u8 action;
    s8 magic;
    u16 var;
};

static struct arrow_info g_arrows[4] = {
    { Z2_ITEM_BOW,         Z2_SLOT_BOW,         Z2_ITEM_BOW,             0x9, 0x0, 0x2, },
    { Z2_ITEM_FIRE_ARROW,  Z2_SLOT_FIRE_ARROW,  Z2_ITEM_BOW_FIRE_ARROW,  0xA, 0x4, 0x3, },
    { Z2_ITEM_ICE_ARROW,   Z2_SLOT_ICE_ARROW,   Z2_ITEM_BOW_ICE_ARROW,   0xB, 0x4, 0x4, },
    { Z2_ITEM_LIGHT_ARROW, Z2_SLOT_LIGHT_ARROW, Z2_ITEM_BOW_LIGHT_ARROW, 0xC, 0x8, 0x5, },
};

static const struct arrow_info * arrow_cycle_get_info(u16 variable) {
    for (int i = 0; i < 4; i++) {
        if (g_arrows[i].var == variable) {
            return (const struct arrow_info *)&g_arrows[i];
        }
    }

    return NULL;
}

static u16 arrow_cycle_get_next_arrow_variable(u16 variable) {
    switch (variable) {
        case 2: return 3; // Normal -> Fire
        case 3: return 4; // Fire -> Ice
        case 4: return 5; // Ice -> Light
        case 5: return 2; // Light -> Normal
        default: return variable;
    }
}

static const struct arrow_info * arrow_cycle_get_next_info(u16 variable) {
    // Get magic cost of current arrow type.
    s8 magic_cost = arrow_cycle_get_info(variable)->magic;

    u16 current = variable;
    const struct arrow_info *info;
    for (int i = 0; i < 4; i++) {
        current = arrow_cycle_get_next_arrow_variable(current);
        info = arrow_cycle_get_info(current);

        // Calculate difference in magic cost and ensure that the player has enough magic to switch.
        bool enough_magic = (z2_file.current_magic >= (info->magic - magic_cost));

        if (info != NULL && info->item == z2_file.items[info->slot] && enough_magic) {
            return info;
        }
    }

    return NULL;
}

static z2_actor_t * arrow_cycle_find_arrow(z2_link_t *link, z2_game_t *game) {
    z2_actor_t *attached = link->common.attached_b;
    if (attached != NULL && attached->id == Z2_ACTOR_EN_ARROW && attached->attached_a == &link->common) {
        return attached;
    } else {
        return NULL;
    }
}

/**
 * Call the En_Arrow actor constructor on an existing En_Arrow instance.
 *
 * This will re-copy the data used to draw the arrow trail color, and thus it will appear as it should.
 **/
static bool arrow_cycle_call_arrow_actor_ctor(z2_actor_t *arrow, z2_game_t *game) {
    z2_actor_ovl_table_t *ovl = &z2_actor_ovl_table[Z2_ACTOR_EN_ARROW];
    z2_actor_init_t *init = reloc_resolve_actor_init(ovl);
    if (init != NULL && init->ctor != NULL) {
        init->dtor(arrow, game);
        init->ctor(arrow, game);
        return true;
    } else {
        return false;
    }
}

static s16 * arrow_cycle_get_effect_state(void) {
    return &z2_file.special_arrow_state;
}

static bool arrow_cycle_is_effect_active(void) {
    return z2_file.special_arrow_state != 0;
}

/**
 * Function called on delayed frame to finish processing the arrow cycle.
 **/
static void arrow_cycle_handle_frame_delay(z2_link_t *link, z2_game_t *game, z2_actor_t *arrow) {
    s16 *effect_state = arrow_cycle_get_effect_state();
    s16 prev_effect_state = *effect_state;

    const struct arrow_info *cur_info = arrow_cycle_get_info(arrow->variable);
    if (arrow != NULL && cur_info != NULL) {
        z2_actor_t *special = arrow->attached_b;
        if (special != NULL) {
            // Deconstruct and remove special arrow actor.
            z2_ActorRemove(&game->actor_ctxt, special, game);
            arrow->attached_b = NULL;
        }

        // Make sure the game is aware that a special arrow effect is happening when switching
        // from normal arrow -> elemental arrow. Uses value 2 to make sure the magic cost is
        // consumed this frame.
        if (cur_info->item != Z2_ITEM_BOW) {
            *effect_state = 2;
        }

        // Refund magic cost of previous arrow type.
        if (prev_effect_state >= 2 && !z2_file.week_event_inf.infinite_magic) {
            z2_file.current_magic += g_arrow_cycle_state.magic_cost;
        }

        // Set magic cost value to be subtracted when arrow effect state == 2.
        z2_file.arrow_magic_cost = cur_info->magic;
    }
}

/**
 * Handle arrow cycling.
 *
 * Note: A "delay frame" is necessary because we cannot free the special arrow actor immediately.
 * This is due to the double-buffer nature of the game's DLists, where each one is processed every
 * other frame, and if we free the actor immediately the next DList will still contain pointers to
 * data which requires the special arrow actor file to be loaded.
 *
 * Thus, we must set the draw pointer to NULL and let one frame process before freeing the actor.
 **/
void arrow_cycle_handle(z2_link_t *link, z2_game_t *game) {
    // Check if processing arrow cycling on delay frame.
    if (g_arrow_cycle_state.frame_delay >= 1) {
        arrow_cycle_handle_frame_delay(link, game, g_arrow_cycle_state.arrow);
        g_arrow_cycle_state.arrow = NULL;
        g_arrow_cycle_state.frame_delay = 0;
        g_arrow_cycle_state.magic_cost = 0;
        return;
    }

    // Make sure arrow cycling is enabled.
    if (!MISC_CONFIG.arrow_cycle) {
        return;
    }

    // Check if buttons state is normal, otherwise we are possibly in a minigame or on Epona.
    if (z2_file.buttons_state.state != Z2_BUTTONS_STATE_NORMAL) {
        return;
    }

    // Find the arrow currently attached to the player.
    z2_actor_t *arrow = arrow_cycle_find_arrow(link, game);
    if (arrow == NULL) {
        return;
    }

    // Ensure arrow has an appropriate variable (cannot be a deku bubble).
    if (!(2 <= arrow->variable && arrow->variable < 6)) {
        return;
    }

    // Check if R is pressed.
    if (!game->common.input[0].pad_pressed.r) {
        return;
    }
    game->common.input[0].pad_pressed.r = 0;

    // Get info for arrow types.
    const struct arrow_info *cur_info, *next_info;
    cur_info = arrow_cycle_get_info(arrow->variable);
    next_info = arrow_cycle_get_next_info(arrow->variable);

    // Check if there is nothing to cycle to and return early.
    if (cur_info == NULL || next_info == NULL || cur_info->var == next_info->var) {
        z2_PlaySfx(0x4806);
        return;
    }

    // Check if we are switching from normal arrow -> elemental arrow, and if so verify that an
    // existing effect is not active. Otherwise the game may crash by attempting to load the actor
    // code file for one effect while an existing effect is still processing.
    // This also prevents from switching when Lens of Truth is activated.
    if (cur_info->item == Z2_ITEM_BOW && arrow_cycle_is_effect_active()) {
        z2_PlaySfx(0x4806);
        return;
    }

    // Update the existing actor variable.
    arrow->variable = next_info->var;

    // Call arrow actor constructor so that the arrow trail color data is re-copied.
    arrow_cycle_call_arrow_actor_ctor(arrow, game);

    // If found, NULL out special arrow actor's draw function to prevent it from writing to DList.
    z2_actor_t *special = arrow->attached_b;
    if (special != NULL) {
        special->draw_proc = NULL;
    }

    // Update the C button value & texture.
    z2_file.form_button_item[0].button_item[link->item_button] = next_info->icon;
    z2_ReloadButtonTexture(game, link->item_button);

    // Update player fields for new action type.
    link->unk_0x147 = next_info->action;
    link->unk_0x14A = next_info->action;

    // Prepare for finishing cycle next frame.
    g_arrow_cycle_state.arrow = arrow;
    g_arrow_cycle_state.frame_delay++;
    g_arrow_cycle_state.magic_cost = cur_info->magic;
}
