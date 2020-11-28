#include <stdbool.h>
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
