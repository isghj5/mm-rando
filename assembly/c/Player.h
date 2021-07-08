#ifndef PLAYER_H
#define PLAYER_H

#include <stdbool.h>
#include <z64.h>

bool Player_CanReceiveItem(GlobalContext* ctxt);
void Player_Pause(GlobalContext* ctxt);
void Player_Unpause(GlobalContext* ctxt);

#endif // PLAYER_H
