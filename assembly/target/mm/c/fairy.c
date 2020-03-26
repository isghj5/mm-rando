#include <stdbool.h>
#include "actor.h"
#include "z2.h"

// Number of fairy_inst entries in table.
#define FAIRY_INST_COUNT 12

struct fairy_inst {
    u16 scene1;
    u16 scene2;
    u16 instance;
    bool used;
};

// Table of known fairy instances, and the scene in which they are normally found.
static struct fairy_inst g_fairy_table[] = {
    { 0x0022, 0xFFFF, 0x4102, false, }, // Milk Road fairy
    { 0x0046, 0x001B, 0x4302, false, }, // Woodfall fairy, Woodfall Temple fairy
    { 0x005C, 0xFFFF, 0x4502, false, }, // Snowhead fairy
    { 0x0016, 0x0018, 0x4702, false, }, // Stone Tower Temple fairy, (assume) Stone Tower Temple Inverted fairy
    { 0x0038, 0xFFFF, 0x4902, false, }, // Zora Cape fairy
    { 0x0021, 0xFFFF, 0x4F02, false, }, // Snowhead Temple fairy
    { 0x0037, 0xFFFF, 0x5702, false, }, // Great Bay fairy
    { 0x0045, 0xFFFF, 0x5B02, false, }, // Southern Swamp fairy
    { 0x0049, 0xFFFF, 0x6702, false, }, // Great Bay Temple fairy
    { 0x0050, 0xFFFF, 0x6D02, false, }, // Mountain Village fairy
    { 0x0058, 0x0059, 0x7302, false, }, // Stone Tower fairy, Stone Tower Inverted fairy
    { 0x0013, 0xFFFF, 0x9302, false, }, // Ikana Canyon fairy
};

// Whether or not Link can interact with a fairy currently.
bool can_interact_with_fairy(z2_game_t *game, z2_link_t *link) {
    // Cannot collect fairy if in Deku flower
    if ((link->action_state3 & Z2_ACTION_STATE3_DEKU_DIVE) != 0)
        return false;

    return z2_CanInteract(game) == 0;
}

// Get the next available fairy instance Id, and mark as "used" for this scene.
bool get_next_fairy_instance(u16 *inst, z2_game_t *game) {
    for (int i = 0; i < FAIRY_INST_COUNT; i++) {
        struct fairy_inst *fairy = &g_fairy_table[i];

        // Do not use a fairy that is already present in this scene.
        if ((game->scene_index == fairy->scene1) ||
            (game->scene_index == fairy->scene2))
            continue;

        // Do not use a fairy that has already been used in this scene.
        if (g_fairy_table[i].used)
            continue;

        // Set used, and return instance
        g_fairy_table[i].used = true;
        *inst = g_fairy_table[i].instance;
        return true;
    }

    return false;
}

// Spawn a fairy actor.
static z2_actor_t* spawn_fairy_actor(z2_game_t *game, z2_xyzf_t pos, u16 inst) {
    z2_rot_t rot = { 0, 0, 0 };
    return actor_spawn(game, Z2_ACTOR_EN_ELF, pos, rot, inst);
}

// Spawn the next avaiable fairy actor.
z2_actor_t* spawn_next_fairy_actor(z2_game_t *game, z2_xyzf_t pos) {
    u16 inst;

    if (get_next_fairy_instance(&inst, game)) {
        return spawn_fairy_actor(game, pos, inst);
    } else {
        return NULL;
    }
}

// Resets the "used" field for each fairy instance.
// Should be called when transitioning to a new scene.
void reset_fairy_instance_usage() {
    for (int i = 0; i < FAIRY_INST_COUNT; i++) {
        g_fairy_table[i].used = false;
    }
}
