#include "misc.h"
#include "quest_items.h"
#include "z2.h"

/**
 * Hook function called while giving the Moon's Tear to the Clock Town Business Scrub.
 **/
void business_scrub_before_give_item_clock_town(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.quest_consume == QUEST_CONSUME_ALWAYS) {
        quest_items_remove(Z2_ITEM_MOON_TEAR);
    }
}

/**
 * Hook function called when consuming an item by giving it to a traveling Business Scrub.
 **/
u32 business_scrub_consume_item(z2_actor_t *actor, z2_game_t *game) {
    u8 consume = MISC_CONFIG.quest_consume;
    u16 flag = actor->variable & 3;

    if (flag == 0) {
        // Southern Swamp Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            quest_items_remove(Z2_ITEM_TOWN_DEED);
        }
        return 0x98;
    } else if (flag == 1) {
        // Goron Village Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            quest_items_remove(Z2_ITEM_SWAMP_DEED);
        }
        return 0x99;
    } else if (flag == 2) {
        // Zora Chamber Business Scrub
        if (consume == QUEST_CONSUME_ALWAYS) {
            quest_items_remove(Z2_ITEM_MOUNTAIN_DEED);
        }
        return 0x9A;
    } else if (flag == 3) {
        // Ikana Canyon Business Scrub
        if (consume == QUEST_CONSUME_DEFAULT || consume == QUEST_CONSUME_ALWAYS) {
            quest_items_remove(Z2_ITEM_OCEAN_DEED);
        }
        return 0x125;
    } else {
        return 0;
    }
}

u16 business_scrub_set_initial_message(z2_en_akindonuts_t *actor, z2_game_t *game) {
    u16 type = actor->common.variable & 3;
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
    actor->last_message_id = result;
    return result;
}
