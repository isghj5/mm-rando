#include <stdbool.h>
#include "linheap.h"
#include "loaded_models.h"
#include "misc.h"
#include "mmr.h"
#include "models.h"
#include "util.h"
#include "z2.h"

#define slot_count 8

static struct linheap g_object_heap = {
    .start = NULL,
    .cur = NULL,
    .size = 0x20000,
};

struct loaded_object {
    u16 object_id;
    u8 *buf;
};

static struct loaded_object object_slots[slot_count] = { 0 };

static void load_object_file(u32 object_id, u8 *buf) {
    z2_obj_file_t *entry = &(z2_obj_table[object_id]);
    u32 vrom_start = entry->vrom_start;
    u32 size = entry->vrom_end - vrom_start;
    z2_ReadFile(buf, vrom_start, size);
}

static void load_object(struct loaded_object *object, u32 object_id) {
    object->object_id = object_id;
    load_object_file(object_id, object->buf);
}

static size_t get_object_size(u32 object_id) {
    z2_obj_file_t info = z2_obj_table[object_id];
    return (size_t)(info.vrom_end - info.vrom_start);
}

static struct loaded_object* get_object(u32 object_id) {
    for (int i = 0; i < slot_count; i++) {
        struct loaded_object *object = &(object_slots[i]);
        if (object->object_id == object_id) {
            return object;
        }
        if (object->object_id == 0) {
            size_t objsize = get_object_size(object_id);
            object->buf = linheap_alloc(&g_object_heap, objsize);
            load_object(object, object_id);
            return object;
        }
    }

    return NULL;
}

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
    struct loaded_object *object = get_object(model.object_id);
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
static mmr_gi_t * models_prepare_gi_entry(struct model *model, z2_game_t *game, u32 gi_index, bool resolve) {
    if (resolve) {
        gi_index = mmr_GetNewGiIndex_stub(game, gi_index, false);
    }
    mmr_gi_t *entry = mmr_get_gi_entry(gi_index);

    if (model != NULL) {
        u8 graphic = models_fix_graphic_id(entry->graphic);
        model->object_id = entry->object;
        model->graphic_id = graphic;
    }

    return entry;
}

/**
 * Load information from the Get-Item table using an index and draw the corresponding model.
 **/
static void models_draw_from_gi_table(z2_actor_t *actor, z2_game_t *game, f32 scale, u32 gi_index) {
    struct model model;
    mmr_gi_t *entry = models_prepare_gi_entry(&model, game, gi_index, true);

    z2_CallSetupDList(z2_game.common.gfx);
    draw_model(model, actor, game, scale);
}

/**
 * Load the actor model information for later reference if not already stored, and return in model
 * parameter.
 **/
static bool models_set_loaded_actor_model(struct model *model, z2_actor_t *actor, z2_game_t *game, u32 gi_index) {
    if (!loaded_models_get_actor_model(model, NULL, actor)) {
        mmr_gi_t *entry = models_prepare_gi_entry(model, game, gi_index, true);
        loaded_models_add_actor_model(*model, entry, actor);
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function for drawing Heart Piece actors as their new item.
 **/
void models_draw_heart_piece(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u32 index = actor->variable + 0x80;
        models_draw_from_gi_table(actor, game, 22.0, index);
    } else {
        z2_DrawHeartPiece(actor, game);
    }
}

/**
 * Hook function for drawing Skulltula Token actors as their new item.
 **/
void models_draw_skulltula_token(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.freestanding) {
        u16 chest_flag = (actor->variable & 0xFC) >> 2;
        // Checks if Swamp Spider House scene
        u32 base_index = game->scene_index == 0x27 ? 0x13A : 0x158;
        u32 gi_index = base_index + chest_flag;
        models_draw_from_gi_table(actor, game, 1.0, gi_index);
    } else {
        draw_model_low_level(actor, game, Z2_GRAPHIC_ST_TOKEN - 1);
    }
}

/**
 * Check whether or not a model draws a Stray Fairy.
 **/
static bool models_is_stray_fairy_model(struct model model) {
    return model.graphic_id == 0x4F && model.object_id == 0x13A;
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
        struct model model;
        u32 gi_index = models_get_stray_fairy_gi_index(actor, game);
        models_set_loaded_actor_model(&model, actor, game, gi_index);
        // Check that we are not drawing a stray fairy.
        if (!models_is_stray_fairy_model(model)) {
            // Rotate at the same speed of a Heart Piece actor.
            actor->rot_2.y = (u16)(actor->rot_2.y + 0x3C0);
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
        u32 gi_index = models_get_stray_fairy_gi_index(actor, game);
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
static u32 models_get_heart_container_gi_index(z2_game_t *game) {
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
        u32 index = models_get_heart_container_gi_index(game);
        models_draw_from_gi_table(actor, game, 1.0, index);
        return true;
    } else {
        return false;
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
 **/
void models_draw_boss_remains(z2_actor_t *actor, z2_game_t *game, u32 graphic_id_minus_1) {
    if (MISC_CONFIG.freestanding) {
        draw_model_low_level(actor, game, graphic_id_minus_1);
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
    bool draw = models_should_override_moons_tear_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
        // If the Moon's Tear on display, reposition and rotate.
        if (actor->variable == 0) {
            actor->pos_2.x = 157.0;
            actor->pos_2.y = -32.0;
            actor->pos_2.z = -103.0;
            actor->rot_2.y = (u16)(actor->rot_2.y + 0x3C0);
        }
    }
}

/**
 * Hook function for drawing Moon's Tear actor as its new item.
 **/
bool models_draw_moons_tear(z2_actor_t *actor, z2_game_t *game) {
    bool draw = models_should_override_moons_tear_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
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
    } else {
        return false;
    }
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
    bool draw = models_should_override_seahorse_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
        actor->rot_2.y = (u16)(actor->rot_2.y + 0x3C0);
    }
}

/**
 * Hook function for drawing Seahorse actor as its new item.
 **/
bool models_draw_seahorse(z2_actor_t *actor, z2_game_t *game) {
    bool draw = models_should_override_seahorse_draw(actor, game);
    if (MISC_CONFIG.freestanding && draw) {
        models_draw_from_gi_table(actor, game, 50.0, 0x95);
        return true;
    } else {
        return false;
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
    linheap_clear(&g_object_heap);
    for (size_t i = 0; i < slot_count; i++) {
        object_slots[i].object_id = 0;
        object_slots[i].buf = NULL;
    }
}

/**
 * Initialize object heap.
 **/
void models_init(void) {
    void *alloc = heap_alloc(g_object_heap.size);
    linheap_init(&g_object_heap, alloc);
}
