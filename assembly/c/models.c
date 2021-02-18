#include <stdbool.h>
#include "items.h"
#include "item_override.h"
#include "loaded_models.h"
#include "misc.h"
#include "mmr.h"
#include "models.h"
#include "objheap.h"
#include "util.h"
#include "z2.h"

#define OBJHEAP_SLOTS (12)
#define OBJHEAP_SIZE  (0x20000)

struct objheap_item g_objheap_items[OBJHEAP_SLOTS] = { 0 };
struct objheap g_objheap = { 0 };

static void scale_top_matrix(f32 scale_factor) {
    f32 *matrix = z2_GetMatrixStackTop();
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            matrix[4*i + j] *= scale_factor;
        }
    }
}

static void set_object_segment(z2_game_t *game, const void *buf) {
    z2_disp_buf_t *xlu = &(game->common.gfx->poly_xlu);
    gSPSegment(xlu->p++, 6, (u32)buf);

    z2_disp_buf_t *opa = &(game->common.gfx->poly_opa);
    gSPSegment(opa->p++, 6, (u32)buf);
}

static void draw_model_low_level(z2_actor_t *actor, z2_game_t *game, s8 graphic_id_minus_1) {
    z2_PreDraw1(actor, game, 0);
    z2_PreDraw2(actor, game, 0);
    z2_BaseDrawGiModel(game, graphic_id_minus_1);
}

static void draw_model(struct model model, z2_actor_t *actor, z2_game_t *game, f32 base_scale) {
    // If both graphic & object are 0, draw nothing.
    if (model.graphic_id == 0 && model.object_id == 0) {
        return;
    }

    struct objheap_item *object = objheap_allocate(&g_objheap, model.object_id, actor->room);
    if (object) {
        // Update RDRAM segment table with object pointer during the draw function.
        // This is required by Moon's Tear (and possibly others), which programatically resolves a
        // segmented address using that table when writing instructions to the display list.
        z2_segment.current_object = (u32)object->buf & 0xFFFFFF;

        set_object_segment(game, object->buf);
        scale_top_matrix(base_scale);
        draw_model_low_level(actor, game, model.graphic_id - 1);
    }
}

/**
 * "Fix" the graphic Id used in the Get-Item table.
 **/
static u8 models_fix_graphic_id(u8 graphic) {
    if (graphic >= 0x80) {
        return (u8)(0x100 - (u16)graphic);
    } else {
        return graphic;
    }
}

/**
 * Get the Get-Item table entry for a specific index, and optionally load relevant entry values
 * into a model structure for drawing.
 **/
static mmr_gi_t * models_prepare_gi_entry(struct model *model, z2_game_t *game, u16 gi_index, bool resolve) {
    if (resolve) {
        gi_index = mmr_GetNewGiIndex(game, 0, gi_index, false);
    }
    mmr_gi_t *entry = mmr_get_gi_entry(gi_index);

    if (model != NULL) {
        u16 gfx, obj;
        if (item_override_get_graphic(gi_index, &gfx, &obj)) {
            model->graphic_id = models_fix_graphic_id((u8)gfx);
            model->object_id = obj;
        } else {
            u8 graphic = models_fix_graphic_id(entry->graphic);
            model->object_id = entry->object;
            model->graphic_id = graphic;
        }
    }

    return entry;
}

/**
 * Load information from the Get-Item table using an index and draw the corresponding model.
 **/
static void models_draw_from_gi_table(z2_actor_t *actor, z2_game_t *game, f32 scale, u16 gi_index) {
    struct model model;
    mmr_gi_t *entry = models_prepare_gi_entry(&model, game, gi_index, true);

    z2_CallSetupDList(z2_game.common.gfx);
    draw_model(model, actor, game, scale);
}

/**
 * Load the actor model information for later reference if not already stored, and return in model
 * parameter.
 **/
static bool models_set_loaded_actor_model(struct model *model, z2_actor_t *actor, z2_game_t *game, u16 gi_index) {
    if (!loaded_models_get_actor_model(model, NULL, actor)) {
        mmr_gi_t *entry = models_prepare_gi_entry(model, game, gi_index, true);
        loaded_models_add_actor_model(*model, entry, actor);
        return true;
    } else {
        return false;
    }
}

/**
 * Cause model to "float" using rotation value.
 **/
static void models_apply_hover_float(z2_actor_t *actor, f32 base, f32 multiplier) {
    f32 rot = z2_Math_Sins(actor->rot_2.y);
    actor->unk_0xC4 = (rot * multiplier) + base;
}

/**
 * Check if a model should rotate backwards (trap item).
 **/
static bool models_should_rotate_backwards(z2_game_t *game, u16 gi_index) {
    // Only rotate ice traps backwards if Ice Trap Quirks enabled.
    if (MISC_CONFIG.ice_trap_quirks) {
        struct model model;
        mmr_gi_t *entry = models_prepare_gi_entry(&model, game, gi_index, true);
        return entry->item == Z2_ICE_TRAP;
    } else {
        return false;
    }
}

/**
 * Rotate an actor model by a specific amount.
 **/
static void models_rotate(z2_actor_t *actor, z2_game_t *game, u16 gi_index, u16 amount) {
    if (!models_should_rotate_backwards(game, gi_index)) {
        actor->rot_2.y += amount;
    } else {
        actor->rot_2.y -= amount;
    }
}

/**
 * Hook function for drawing Heart Piece actors as their new item.
 **/
void models_draw_heart_piece(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 index = actor->variable + 0x80;
        models_draw_from_gi_table(actor, game, 22.0, index);
    } else {
        z2_DrawHeartPiece(actor, game);
    }
}

/**
 * Hook function for rotating En_Item00 actors (Heart Piece).
 **/
void models_rotate_en_item00(z2_actor_t *actor, z2_game_t *game) {
    // MMR Heart Pieces use masked variable 0x1D or greater.
    if (MISC_CONFIG.freestanding && (actor->variable & 0xFF) >= 0x1D) {
        // Rotate Heart Piece.
        u16 index = actor->variable + 0x80;
        models_rotate(actor, game, index, 0x3C0);
    } else {
        actor->rot_2.y += 0x3C0;
    }
}

/**
 * Get the Get-Item index for a Skulltula Token actor.
 **/
u16 models_get_skulltula_token_gi_index(z2_actor_t *actor, z2_game_t *game) {
    u16 chest_flag = (actor->variable & 0xFC) >> 2;
    // Checks if Swamp Spider House scene
    u16 base_index = game->scene_index == 0x27 ? 0x13A : 0x158;
    u16 gi_index = base_index + chest_flag;
    return gi_index;
}

/**
 * Hook function for drawing Skulltula Token actors as their new item.
 **/
void models_draw_skulltula_token(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 gi_index = models_get_skulltula_token_gi_index(actor, game);
        models_draw_from_gi_table(actor, game, 1.0, gi_index);
    } else {
        draw_model_low_level(actor, game, Z2_GRAPHIC_ST_TOKEN - 1);
    }
}

/**
 * Hook function for rotating Skulltula Token actors.
 **/
void models_rotate_skulltula_token(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 gi_index = models_get_skulltula_token_gi_index(actor, game);
        models_rotate(actor, game, gi_index, 0x38E);
    } else {
        actor->rot_2.y += 0x38E;
    }
}

/**
 * Check whether or not a model draws a Stray Fairy.
 **/
static bool models_is_stray_fairy_model(struct model model) {
    return model.graphic_id == 0x4F;
}

/**
 * Get the Get-Item index for a Stray Fairy.
 **/
static u16 models_get_stray_fairy_gi_index(z2_actor_t *actor, z2_game_t *game) {
    if ((actor->variable & 0xF) == 3) {
        // Clock Town stray fairy
        return 0x3B;
    } else {
        // Dungeon stray fairies
        u16 cur_dungeon_offset = *(u16*)0x801F3F38;
        u16 chest_flag = ((actor->variable & 0xFE00) >> 9) & 0x1F;
        return 0x16D + (cur_dungeon_offset * 0x14) + chest_flag;
    }
}

/**
 * Check if a Stray Fairy actor should be drawn as its Get-Item.
 **/
static bool models_should_override_stray_fairy_draw(z2_actor_t *actor, z2_game_t *game) {
    u16 flag = actor->variable & 0xF;

    // Check if a Stray Fairy is in a Great Fairy fountain:
    // 1 is used for Stray Fairies in the Great Fairy fountain.
    // 8 is used for animating Stray Fairies when being given to the fountain.
    // Optionally check Great Fairy fountain scene: 0x26
    return (flag != 1) && (flag != 8);
}

/**
 * Hook function called before Stray Fairy actor's main function.
 **/
void models_before_stray_fairy_main(z2_actor_t *actor, z2_game_t *game) {
    // If not a Stray Fairy, rotate like En_Item00 does.
    bool draw = models_should_override_stray_fairy_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
        mmr_gi_t *entry;
        struct model model;
        u16 gi_index = models_get_stray_fairy_gi_index(actor, game);
        models_set_loaded_actor_model(&model, actor, game, gi_index);
        if (loaded_models_get_actor_model(&model, (void**)&entry, actor)) {
            // Check that we are not drawing a stray fairy.
            if (!models_is_stray_fairy_model(model)) {
                // Rotate at the same speed of a Heart Piece actor.
                models_rotate(actor, game, gi_index, 0x3C0);
            }
        }
    }
}

/**
 * Hook function for drawing Stray Fairy actors as their new item.
 *
 * Return true if overriding functionality, false if using original functionality.
 **/
bool models_draw_stray_fairy(z2_actor_t *actor, z2_game_t *game) {
    bool draw = models_should_override_stray_fairy_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
        mmr_gi_t *entry;
        struct model model;
        u16 gi_index = models_get_stray_fairy_gi_index(actor, game);
        models_set_loaded_actor_model(&model, actor, game, gi_index);
        if (!loaded_models_get_actor_model(&model, (void**)&entry, actor)) {
            return false;
        }

        // Check if we are drawing a stray fairy.
        if (models_is_stray_fairy_model(model)) {
            // Update stray fairy actor according to type, and perform original draw.
            z2_en_elforg_t *elforg = (z2_en_elforg_t *)actor;
            u8 fairy_type = entry->type >> 4;
            elforg->color = fairy_type;
            return false;
        } else {
            z2_CallSetupDList(z2_game.common.gfx);
            draw_model(model, actor, game, 25.0);
            return true;
        }
    } else {
        return false;
    }
}

/**
 * Get the Get-Item index for a Heart Container actor.
 **/
static u16 models_get_heart_container_gi_index(z2_game_t *game) {
    // This is a (somewhat) reimplementation of MMR function at: 0x801DC138
    // The original function returns in A2 and A3 to setup calling a different function.
    if (game->scene_index == 0x1F) {
        return 0x11A;
    } else if (game->scene_index == 0x44) {
        return 0x11B;
    } else if (game->scene_index == 0x5F) {
        return 0x11C;
    } else {
        return 0x11D;
    }
}

/**
 * Hook function for drawing Heart Container actors as their new item.
 *
 * Return true if overriding functionality, false if using original functionality.
 **/
bool models_draw_heart_container(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 index = models_get_heart_container_gi_index(game);
        models_draw_from_gi_table(actor, game, 1.0, index);
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function for rotating Heart Container actors.
 **/
void models_rotate_heart_container(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 gi_index = models_get_heart_container_gi_index(game);
        models_rotate(actor, game, gi_index, 0x400);
    } else {
        actor->rot_2.y += 0x400;
    }
}

/**
 * Hook function for replacing original behaviour of the Get-Item draw function for Boss Remains,
 * which wrote the segmented address instruction (for the object) in the function itself, instead
 * of the caller.
 **/
void models_write_boss_remains_object_segment(z2_game_t *game, u32 graphic_id_minus_1) {
    z2_disp_buf_t *opa = &(game->common.gfx->poly_opa);

    // Get index of object, and use it to get the data pointer
    s8 index = z2_GetObjectIndex(&game->obj_ctxt, Z2_OBJECT_BSMASK);

    // Only write segment instruction if object found in game's object list.
    // Otherwise, assume it was set by the caller.
    if (index >= 0) {
        void *data = game->obj_ctxt.obj[index].data;

        // Write segmented address instruction
        gSPSegment(opa->p++, 6, (u32)data);
    }
}

/**
 * Hook function for drawing Boss Remain actors as their new item.
 * Currently draws the item on the Oath to Order check. Will need
 * to be updated if Boss Remains are randomized.
 **/
void models_draw_boss_remains(z2_actor_t *actor, z2_game_t *game, u32 graphic_id_minus_1) {
    if (MISC_CONFIG.freestanding && (actor->attached_a->attached_a == NULL || actor->attached_a->attached_a->id != 0)) {
        models_draw_from_gi_table(actor, game, 1.0, 0x77);
    } else {
        draw_model_low_level(actor, game, graphic_id_minus_1);
    }
}

/**
 * Check whether or not a model draws a Moon's Tear.
 **/
static bool models_is_moons_tear_model(struct model model) {
    return model.graphic_id == 0x5A && model.object_id == 0x1B1;
}

/**
 * Check if a Moon's Tear actor should be drawn as its Get-Item.
 **/
static bool models_should_override_moons_tear_draw(z2_actor_t *actor, z2_game_t *game) {
    // Check if a vanilla Moon's Tear is being drawn.
    struct model model;
    mmr_gi_t *entry = models_prepare_gi_entry(&model, game, 0x96, true);
    return !models_is_moons_tear_model(model);
}

/**
 * Hook function called before a Moon's Tear actor's main function.
 **/
void models_before_moons_tear_main(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        if (models_should_override_moons_tear_draw(actor, game)) {
            // If the Moon's Tear on display, reposition and rotate.
            if (actor->variable == 0) {
                actor->pos_2.x = 157.0;
                actor->pos_2.y = -32.0;
                actor->pos_2.z = -103.0;
                models_rotate(actor, game, 0x96, 0x3C0);
                models_apply_hover_float(actor, 30.0, 18.0);
            }
        }
    }
}

/**
 * Hook function for drawing Moon's Tear actor as its new item.
 **/
bool models_draw_moons_tear(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        if (models_should_override_moons_tear_draw(actor, game)) {
            struct model model;
            bool resolve;

            if (actor->variable == 0) {
                // Moon's Tear on display in observatory (not collectible).
                resolve = false;
            } else {
                // Moon's Tear on ground outside observatory (collectible).
                resolve = true;
            }

            mmr_gi_t *entry = models_prepare_gi_entry(&model, game, 0x96, resolve);
            z2_CallSetupDList(z2_game.common.gfx);
            draw_model(model, actor, game, 1.0);
            return true;
        }
    }

    return false;
}

/**
 * Hook function for drawing Lab Fish Heart Piece actor as its new item.
 **/
bool models_draw_lab_fish_heart_piece(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        models_draw_from_gi_table(actor, game, 25.0, 0x112);
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function for rotating Lab Fish Heart Piece actor.
 **/
void models_rotate_lab_fish_heart_piece(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        models_rotate(actor, game, 0x112, 0x3E8);
    } else {
        actor->rot_2.y += 0x3E8;
    }
}

/**
 * Check whether or not a model draws a Seahorse.
 **/
static bool models_is_seahorse_model(struct model model) {
    return model.graphic_id == 0x63 && model.object_id == 0x1F0;
}

/**
 * Check if a Seahorse actor should be drawn as its Get-Item.
 **/
static bool models_should_override_seahorse_draw(z2_actor_t *actor, z2_game_t *game) {
    // Check if a vanilla Seahorse is being drawn.
    struct model model;
    mmr_gi_t *entry = models_prepare_gi_entry(&model, game, 0x95, true);
    // Ensure that only the fishtank Seahorse is being drawn over.
    bool is_fishtank = actor->variable == 0xFFFF;
    return is_fishtank && !models_is_seahorse_model(model);
}

/**
 * Hook function called before a Seahorse actor's main function.
 **/
void models_before_seahorse_main(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        if (models_should_override_seahorse_draw(actor, game)) {
            models_rotate(actor, game, 0x95, 0x3C0);
            models_apply_hover_float(actor, -1000.0, 1000.0);
        }
    }
}

/**
 * Hook function for drawing Seahorse actor as its new item.
 **/
bool models_draw_seahorse(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        if (models_should_override_seahorse_draw(actor, game)) {
            models_draw_from_gi_table(actor, game, 50.0, 0x95);
            return true;
        }
    }

    return false;
}

void models_draw_shop_inventory(z2_en_girla_t *actor, z2_game_t *game, u32 graphic_id_minus_1) {
    if (MISC_CONFIG.shop_models) {
        models_draw_from_gi_table(&(actor->common), game, 1.0, actor->gi_index);
    } else {
        draw_model_low_level(&(actor->common), game, graphic_id_minus_1);
    }
}

void models_after_actor_dtor(z2_actor_t *actor) {
    if (MISC_CONFIG.freestanding) {
        if (actor->id == Z2_ACTOR_EN_ELFORG) {
            loaded_models_remove_actor_model(actor);
        }
    }
}

/**
 * Reset object heap pointer and clear all loaded object slots.
 **/
void models_clear_object_heap(void) {
    objheap_clear(&g_objheap);
}

/**
 * Initialize object heap.
 **/
void models_init(void) {
    void *alloc = heap_alloc(OBJHEAP_SIZE);
    objheap_init(&g_objheap, alloc, OBJHEAP_SIZE, g_objheap_items, OBJHEAP_SLOTS);
}

struct models_state {
    // Pointer to graphics context, cannot be retrieved from normal pointer during room unload.
    z2_gfx_t *gfx;
    // Pointer to poly_opa, used to check if objheap should finish advance.
    const Gfx *prev_opa;
};

static struct models_state g_state = { 0 };

/**
 * Helper function called after preparing game's display buffers for writing (write pointer set to buffer start).
 * Used to finish advancing objheap when needed.
 **/
void models_after_prepare_display_buffers(z2_gfx_t *gfx) {
    // Apparently gfx pointer cannot be retrieved during room unload, so store in global.
    if (gfx != NULL) {
        g_state.gfx = gfx;
    }
    // Note: This assumes that when the poly_opa buffer pointer has been reset to start, it has already been flushed
    // to RDP. While this is very likely, it is not guaranteed.
    // If alternative Opa buffer has been cleared, both DLists should be rid of pointers to object data in previous room.
    if (g_state.prev_opa != NULL && gfx->poly_opa.buf != g_state.prev_opa) {
        objheap_flush_operation(&g_objheap);
        g_state.prev_opa = NULL;
    }
}

/**
 * Helper function called when unloading previous room, to prepare for finishing advance of objheap in a subsequent frame.
 **/
void models_prepare_after_room_unload(z2_game_t *game) {
    // Note: During frame processing loop, unloads room before drawing actors.
    // Not sure how to get alternative Opa buffer, so get current and check if non-NULL and non-equal (there are only 2).
    g_state.prev_opa = g_state.gfx->poly_opa.buf;

    // Determine operation before finish advancing or reverting.
    // Normally, objects from previously loaded rooms would no longer draw so this isn't an issue, but is required for hack
    // used to draw actors with 0xFF room, so that the pointer can be safely swapped to data of the relevant room.
    s8 cur_room = (s8)game->room_ctxt.rooms[0].idx;
    objheap_handle_room_unload(&g_objheap, cur_room);
}

/**
 * Helper function called when loading next room, to prepare objheap for advancing.
 **/
void models_prepare_before_room_load(z2_room_ctxt_t *room_ctxt, s8 room_index) {
    if ((s8)room_ctxt->rooms[0].idx == -1) {
        // If loading first room in scene, remember room index.
        objheap_init_room(&g_objheap, room_index);
    } else {
        // Safeguard: If previous Opa DList pointer is non-NULL, still waiting to flush advance or revert operation.
        // If attempting to prepare advance before this has happened, prevent flushing advance or revert operations.
        if (g_state.prev_opa) {
            g_state.prev_opa = NULL;
        }
        // If not loading first room in scene, prepare objheap for advance.
        objheap_prepare_advance(&g_objheap, room_index);
    }
}
