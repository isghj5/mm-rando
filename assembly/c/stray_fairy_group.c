#include "z2.h"
#include "misc.h"
#include "player.h"

// returns false if using vanilla layout and cutscene should start
bool stray_fairy_group_give_reward(z2_actor_t *actor, z2_game_t *game, void* nextState) {
    if (MISC_CONFIG.vanilla_layout) {
        return false;
    }

    if (player_can_receive_item(game)) {
        u16 gi_index = actor->variable >> 9;
        if (gi_index == 0 && z2_file.current_form == 4) { // Town Fairy + Human
            gi_index = 5;
        }
        gi_index += 0x12C;
        if (z2_SetGetItemLongrange(actor, game, gi_index)) {
            void **nextStatePointer = (void**)(((u8*)actor) + 0x14C);
            *nextStatePointer = nextState;
            z2_remove_generic_flag(game, actor->variable >> 9);
        }
    }

    return true;
}