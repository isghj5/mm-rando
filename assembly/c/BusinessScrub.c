#include <z64.h>
#include "Misc.h"
#include "QuestItems.h"

/**
 * Hook function called while giving the Moon's Tear to the Clock Town Business Scrub.
 **/
void BusinessScrub_BeforeGiveItemClockTown(Actor* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.questConsume == QUEST_CONSUME_ALWAYS) {
        QuestItems_Remove(ITEM_MOON_TEAR);
    }
}

/**
 * Hook function called when consuming an item by giving it to a traveling Business Scrub.
 **/
void BusinessScrub_ConsumeItem(Actor* actor) {
    u8 consume = MISC_CONFIG.flags.questConsume;
    u16 flag = actor->params & 3;

    if (flag == 0) {
        // Southern Swamp Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            QuestItems_Remove(ITEM_TOWN_DEED);
        }
    } else if (flag == 1) {
        // Goron Village Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            QuestItems_Remove(ITEM_SWAMP_DEED);
        }
    } else if (flag == 2) {
        // Zora Chamber Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            QuestItems_Remove(ITEM_MOUNTAIN_DEED);
        }
    } else if (flag == 3) {
        // Ikana Canyon Business Scrub
        if (consume == QUEST_CONSUME_DEFAULT || consume == QUEST_CONSUME_ALWAYS) {
            QuestItems_Remove(ITEM_OCEAN_DEED);
        }
    }
}

u16 BusinessScrub_SetInitialMessage(ActorEnAkindonuts* actor, GlobalContext* ctxt) {
    u16 type = actor->base.params & 3;
    bool flyAway = false;
    u16 result;
    if (type == 0) {
        result = 0x15E0;
        u8 landDeed = *(u8*)(0x801F05A5);
        if ((landDeed & 0x10) != 0) {
            flyAway = true;
        }
    } else if (type == 1) {
        result = 0x15F4;
        u8 swampDeed = *(u8*)(0x801F05A5);
        if ((swampDeed & 0x80) != 0) {
            flyAway = true;
        }
    } else if (type == 2) {
        result = 0x1607;
        u8 mountainDeed = *(u8*)(0x801F05A6);
        if ((mountainDeed & 0x04) != 0) {
            flyAway = true;
        }
    } else {
        result = 0x161B;
        u8 oceanDeed = *(u8*)(0x801F05A6);
        if ((oceanDeed & 0x20) != 0) {
            flyAway = true;
        }
    }
    if (flyAway) {
        result = 0x1638;
        actor->state |= 0x20;
    }
    actor->lastMessageId = result;
    return result;
}
