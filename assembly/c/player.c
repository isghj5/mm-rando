#include <stdbool.h>
#include "reloc.h"
#include "z2.h"

bool player_can_receive_item(z2_game_t *game) {
    z2_link_t *link = Z2_LINK(game);
    if ((link->action_state1 & Z2_ACTION_STATE1_AIM ) != 0) {
        return false;
    }
    u16 current_animation_id = link->current_animation_id;
    bool result = false;
    switch (current_animation_id) {
        case 0xE208: // rolling - Goron
            result = game->common.input[0].raw.pad.a != 0;
            break;
        case 0xDD28: // rolling - Human, Goron, Zora
        case 0xDF20: // idle - Human with sword
        case 0xDF28: // idle - Human, Deku
        case 0xE260: // idle - Goron
        case 0xE410: // idle - Zora
        case 0xD988: // idle - Fierce Deity
        case 0xD918: // walking with sword
        case 0xDE40: // walking - Human, Deku, Goron, Zora
        case 0xD920: // walking - Fierce Deity
        case 0xD800: // backwalking after backflip - Human, Goron, Zora, Fierce Deity
        case 0xDD18: // backwalking after backflip - Deku
        case 0xD928: // sidewalking - Fierce Deity
        case 0xDE78: // sidewalking - Human, Deku, Goron, Zora
            result = true;
            break;
    }
    return result;
}

/**
 * Helper function used to perform effects if entering water, and update the player swim flag.
 **/
static void HandleEnterWater(z2_link_t *link, z2_game_t *game) {
    // Check water distance to determine if in water.
    if (link->table_A68[11] < link->common.water_surface_dist) {
        // If swim flag not set, perform effects (sound + visual) for entering water.
        if ((link->action_state1 & Z2_ACTION_STATE1_SWIM) == 0) {
            z2_PerformEnterWaterEffects(game, link);
        }
        // Set swim flag.
        link->action_state1 |= Z2_ACTION_STATE1_SWIM;
    }
}

/**
 * Helper function called to check if the player is in water.
 **/
static bool InWater(z2_link_t *link) {
    return ((link->action_state1 & Z2_ACTION_STATE1_SWIM) != 0 ||
            (link->table_A68[11] < link->common.water_surface_dist));
}

/**
 * Hook function called before handling "frozen" player state.
 **/
void player_before_handle_frozen_state(z2_link_t *link, z2_game_t *game) {
    HandleEnterWater(link, game);
    // If frozen while swimming, should float on water.
    if (InWater(link)) {
        z2_PlayerHandleBuoyancy(link);
    }
}

/**
 * Hook function called before handling "voiding" player state.
 **/
void player_before_handle_void_state(z2_link_t *link, z2_game_t *game) {
    HandleEnterWater(link, game);
    // Note: Later in the function of this hook, is where the "frozen" player state flag is set.
    // Since we can't check the player state flags, do the same check this function does instead.
    bool frozen = link->animation_value == 0;
    bool zora = link->form == 2;
    // If Zora is voiding (frozen) and swimming, should float in water.
    if (zora && frozen && InWater(link)) {
        z2_PlayerHandleBuoyancy(link);
    }
}

/**
 * Hook function to check whether or not freezing should void Zora.
 **/
bool player_should_ice_void_zora(z2_link_t *link, z2_game_t *game) {
    u16 scene = game->scene_index;
    switch (scene) {
        case 0x1F: // Odolwa Boss Room
        case 0x44: // Goht Boss Room
        case 0x5F: // Gyorg Boss Room
        case 0x36: // Twinmold Boss Room
            return false;
        default:
            return true;
    }
}

/**
 * Hook function called to determine if swim state should be prevented from being restored.
 **/
bool player_should_prevent_restoring_swim_state(z2_link_t *link, z2_game_t *game, void *function) {
    void *handleFrozen = reloc_resolve_player_ovl(&z2_0x801D0B70.player_ovl, 0x808546D0); // Offset: 0x26C40
    return function == handleFrozen;
}
