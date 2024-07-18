#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "Items.h"
#include "BaseRupee.h"
#include "Scopecoin.h"
#include "SoftSoilPrize.h"
#include "KeatonGrassCluster.h"
#include "GossipStone.h"
#include "Minifrog.h"
#include "Butterfly.h"

#define SkulltulaSoundTimerPtr(actor) ((s8*)(&actor->shape.pad16))

static void ProcessActorGiIndex(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    s8 skulltulaSoundTimer = *SkulltulaSoundTimerPtr(actor);
    if ((ctxt->actorCtx.unk5 & 0x8) && (skulltulaSoundTimer > 1)) {
        return;
    }

    if (!giIndex) {
        return;
    }

    u8 item = MMR_GetGiEntry(MMR_GetNewGiIndex(ctxt, NULL, giIndex, false))->item;

    if (item == CUSTOM_ITEM_STRAY_FAIRY && MISC_CONFIG.flags.fairyMaskShimmer) {
        ctxt->actorCtx.unk5 |= 0x8;
    }

    if (item == ITEM_SKULLTULA_SPIRIT && skulltulaSoundTimer >= 0 && MISC_CONFIG.flags.skullTokenSounds) {
        if (skulltulaSoundTimer == 1) {
            z2_PlaySfxAtActor(actor, 0x39DA);
            if (z2_Rand_ZeroOne() < 0.1f) {
                skulltulaSoundTimer = 0;
            } else {
                *SkulltulaSoundTimerPtr(actor) = 9;
            }
        }
        if (skulltulaSoundTimer == 0) {
            *SkulltulaSoundTimerPtr(actor) = 41 + (s8)(z2_Rand_ZeroOne() * 80);
        }
    }
}

static void ProcessGoldenSkulltulaFlag(Actor* actor, GlobalContext* ctxt, u16 skulltulaFlag) {
    u16 enSwBaseGiIndex = ctxt->sceneNum == SCENE_KINSTA1 ? 0x13A : 0x158;
    *SkulltulaSoundTimerPtr(actor) = -2;
    ProcessActorGiIndex(actor, ctxt, enSwBaseGiIndex + skulltulaFlag);
}

/**
 * Check if a Stray Fairy actor gives an item
 **/
static bool StrayFairyGivesItem(Actor* actor, GlobalContext* ctxt) {
    u16 flag = actor->params & 0xF;

    // Check if a Stray Fairy is in a Great Fairy fountain:
    // 1 is used for Stray Fairies in the Great Fairy fountain.
    // 8 is used for animating Stray Fairies when being given to the fountain.
    // Optionally check Great Fairy fountain scene: 0x26
    return (flag != 1) && (flag != 8);
}

static void ProcessStrayFairyFlag(Actor* actor, GlobalContext* ctxt, u16 strayFairyFlag) {
    u16 curDungeonOffset = *(u16*)0x801F3F38;
    ProcessActorGiIndex(actor, ctxt, 0x16D + (curDungeonOffset * 0x14) + strayFairyFlag);
}

void ItemDetector_AfterActorUpdate(Actor* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return;
    }

    if (!MISC_CONFIG.flags.fairyMaskShimmer && !MISC_CONFIG.flags.skullTokenSounds) {
        return;
    }

    s8 skulltulaSoundTimer = *SkulltulaSoundTimerPtr(actor);
    if (skulltulaSoundTimer > 1) {
        *SkulltulaSoundTimerPtr(actor) = skulltulaSoundTimer - 1;
    }

    if (skulltulaSoundTimer == -1) {
        return;
    }

    // great fairy mask already flagged to shimmer and skulltula sound timer already set, or those features are disabled
    if ((!MISC_CONFIG.flags.fairyMaskShimmer || (ctxt->actorCtx.unk5 & 0x8)) && (!MISC_CONFIG.flags.skullTokenSounds || skulltulaSoundTimer > 1)) {
        return;
    }

    switch (actor->id) {
        case ACTOR_EN_BOX:; // Treasure Chest
            ActorEnBox* box = (ActorEnBox*)actor;
            u16 boxFlag = box->base.params & 0x1F;
            if (!z2_get_chest_flag(ctxt, boxFlag)) {
                if (box->giIndex == 0x11) { // Stray Fairy
                    ProcessStrayFairyFlag(actor, ctxt, boxFlag);
                } else {
                    ProcessActorGiIndex(actor, ctxt, box->giIndex);
                }
            }
            break;
        case ACTOR_EN_GIRLA:; // Shop Inventory Data
            ActorEnGirlA* girlA = (ActorEnGirlA*)actor;
            ProcessActorGiIndex(actor, ctxt, girlA->giIndex);
            break;
        case ACTOR_EN_MS: // Bean Man
            ProcessActorGiIndex(actor, ctxt, 0x11E);
            break;
        case ACTOR_EN_GO: // Goron
            if (actor->params == 0x8) { // Medigoron
                ProcessActorGiIndex(actor, ctxt, 0x123);
            }
            break;
        case ACTOR_EN_TRU: // Koume (Gameplay)
        // TODO case ACTOR_EN_DNH: // Boat Cruise Target Spot if koume healed
            ProcessActorGiIndex(actor, ctxt, 0x43); // Pictograph Box
            ProcessActorGiIndex(actor, ctxt, 0xA8); // Boat Archery
            break;
        case ACTOR_EN_ELF: // Fairy
            ProcessActorGiIndex(actor, ctxt, Rupee_GetGiIndex(actor));
            break;
        case ACTOR_EN_ELFORG: // Stray Fairy
            if (StrayFairyGivesItem(actor, ctxt)) {
                if ((actor->params & 0xF) == 3) {
                    // Clock Town stray fairy
                    ProcessActorGiIndex(actor, ctxt, 0x3B);
                } else {
                    // Dungeon stray fairies
                    ProcessStrayFairyFlag(actor, ctxt, ((actor->params & 0xFE00) >> 9) & 0x1F);
                }
            }
            break;
        case ACTOR_EN_ELFBUB: // Stray Fairy Bubble
            ProcessStrayFairyFlag(actor, ctxt, ((actor->params & 0xFE00) >> 9) & 0x1F);
            break;
        case ACTOR_EN_ELFGRP: // Stray Fairy Group
            ProcessActorGiIndex(actor, ctxt, 0x12C + (actor->params & 0xF));
            if (!actor->params) {
                ProcessActorGiIndex(actor, ctxt, 0x131); // Town Great Fairy
            }
            break;
        case ACTOR_EN_TRT: // Kotake (Shop)
        case ACTOR_EN_TRT2: // Kotake (Broom)
            ProcessActorGiIndex(actor, ctxt, 0x59);
            ProcessActorGiIndex(actor, ctxt, 0x187);
            break;
        case ACTOR_EN_MA_YTS: // Romani
        case ACTOR_EN_MA4: // Romani
            ProcessActorGiIndex(actor, ctxt, 0x60);
            ProcessActorGiIndex(actor, ctxt, 0x71);
            ProcessActorGiIndex(actor, ctxt, 0x454); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x46B); // Notebook: Romani's Game
            ProcessActorGiIndex(actor, ctxt, 0x46C); // Notebook: Aliens
            break;
        case ACTOR_EN_GK: // Goron Elder's Son
            ProcessActorGiIndex(actor, ctxt, 0x6A);
            ProcessActorGiIndex(actor, ctxt, 0x74);
            break;
        case ACTOR_EN_JG: // Goron Elder
            ProcessActorGiIndex(actor, ctxt, 0x44E);
            break;
        case ACTOR_EN_AZ: // Beaver Bros
            ProcessActorGiIndex(actor, ctxt, 0x5A);
            ProcessActorGiIndex(actor, ctxt, 0xAD);
            break;
        case ACTOR_EN_AL: // Madame Aroma
            ProcessActorGiIndex(actor, ctxt, 0x6F);
            ProcessActorGiIndex(actor, ctxt, 0x8F);
            ProcessActorGiIndex(actor, ctxt, 0x457); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x472); // Notebook: Letter to Mama
            ProcessActorGiIndex(actor, ctxt, 0x47B); // Notebook: Kafei's Mask
            break;
        case ACTOR_EN_TOTO: // Toto
            ProcessActorGiIndex(actor, ctxt, 0x458); // Notebook Meeting
            break;
        case ACTOR_EN_BOMJIMA: // Bomber Jim
            ProcessActorGiIndex(actor, ctxt, 0x50);
            ProcessActorGiIndex(actor, ctxt, 0x44F); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x474); // Notebook: Hide and Seed
            break;
        case ACTOR_EN_KBT: // Zubora
            ProcessActorGiIndex(actor, ctxt, 0x38);
            ProcessActorGiIndex(actor, ctxt, 0x39);
            break;
        case ACTOR_EN_SYATEKI_MAN: // Shooting Gallery
            if (ctxt->sceneNum == SCENE_SYATEKI_MORI) { // Swamp Shooting Gallery
                ProcessActorGiIndex(actor, ctxt, 0x24);
                ProcessActorGiIndex(actor, ctxt, 0xA6);
            } else {
                ProcessActorGiIndex(actor, ctxt, 0x23);
                ProcessActorGiIndex(actor, ctxt, 0x90);
            }
            break;
        case ACTOR_EN_GINKO_MAN: // Bank Teller
            ProcessActorGiIndex(actor, ctxt, 0x08);
            ProcessActorGiIndex(actor, ctxt, 0x108);
            ProcessActorGiIndex(actor, ctxt, 0x13D);
            break;
        case ACTOR_EN_STH:; // Spider House Guy
            u16 sthType = actor->params & 0xF;
            if (sthType == 4 || sthType == 5) {
                // Oceanside Spider House
                ProcessActorGiIndex(actor, ctxt, 0x09);
                ProcessActorGiIndex(actor, ctxt, 0x134);
                ProcessActorGiIndex(actor, ctxt, 0x1A3);
            } else if (sthType == 2) {
                // Swamp Spider House
                ProcessActorGiIndex(actor, ctxt, 0x8A);
            }
            break;
        case ACTOR_EN_SSH: // Cursed Swamp Spider House Guy
            ProcessActorGiIndex(actor, ctxt, 0x8A);
            break;
        case ACTOR_OBJ_MOON_STONE:
            if (actor->draw) {
                ProcessActorGiIndex(actor, ctxt, 0x96);
            }
            break;
        case ACTOR_EN_SELLNUTS:
            ProcessActorGiIndex(actor, ctxt, 0x97);
            break;
        case ACTOR_EN_AKINDONUTS:;
            u16 akindonutsType = actor->params & 0x3;
            switch (akindonutsType) {
                case 0: // Swamp Scrub
                    ProcessActorGiIndex(actor, ctxt, 0x98);
                    ProcessActorGiIndex(actor, ctxt, 0x19B);
                    break;
                case 1: // Mountain Scrub
                    ProcessActorGiIndex(actor, ctxt, 0x99);
                    ProcessActorGiIndex(actor, ctxt, 0x1D);
                    break;
                case 2: // Ocean Scrub
                    ProcessActorGiIndex(actor, ctxt, 0x9A);
                    ProcessActorGiIndex(actor, ctxt, 0x19C);
                    break;
                case 3: // Canyon Scrub
                    ProcessActorGiIndex(actor, ctxt, 0x125);
                    ProcessActorGiIndex(actor, ctxt, 0x19D);
                    break;
            }
            break;
        case ACTOR_EN_AN:; // Anju
            bool anjuIsDrawn = *(((u8*)actor)+0x200) || *(u32*)(((u8*)actor)+0x3B0);
            if (anjuIsDrawn) {
                if (gSaveContext.perm.day%5 == 1 && gSaveContext.perm.time < 0xAC68) {
                    ProcessActorGiIndex(actor, ctxt, 0xA0); // Room Key
                }
                // TODO specify circumstances
                ProcessActorGiIndex(actor, ctxt, 0xAA); // Letter to Kafei
                ProcessActorGiIndex(actor, ctxt, 0x85); // Couple's Mask
                ProcessActorGiIndex(actor, ctxt, 0x450); // Notebook Meeting
                ProcessActorGiIndex(actor, ctxt, 0x463); // Notebook: Inn Reservation
                ProcessActorGiIndex(actor, ctxt, 0x464); // Notebook: Setting up Midnight Meeting
                ProcessActorGiIndex(actor, ctxt, 0x465); // Notebook: Midnight Meeting
                ProcessActorGiIndex(actor, ctxt, 0x469); // Notebook: Deliver the Pendant of Memories
                ProcessActorGiIndex(actor, ctxt, 0x481); // Notebook: Couple's Mask
            }
            break;
        case ACTOR_EN_IG: // Link the Goron
            if (*(((u8*)actor)+0x298)) { // scheduleResult
                if (gSaveContext.perm.day%5 == 1 && gSaveContext.perm.time >= 0xAC68) {
                    ProcessActorGiIndex(actor, ctxt, 0xA0); // Room Key
                }
            }
            break;
        case ACTOR_EN_TEST3: // Kafei
            if (actor->draw) {
                ProcessActorGiIndex(actor, ctxt, 0x85); // Couple's Mask
                ProcessActorGiIndex(actor, ctxt, 0xAB); // Pendant of Memories
                // TODO only while wearing Keaton Mask
                ProcessActorGiIndex(actor, ctxt, 0x80); // Keaton Mask
                ProcessActorGiIndex(actor, ctxt, 0x451); // Notebook Meeting
                ProcessActorGiIndex(actor, ctxt, 0x468); // Notebook: Receive Pendant
                ProcessActorGiIndex(actor, ctxt, 0x481); // Notebook: Couple's Mask
            }
            break;
        case ACTOR_OBJ_NOZOKI:; // Sakon's Hideout Objects
            bool isSunMask = *(((u8*)actor)+0x15C) == 1;
            if (isSunMask) {
                ProcessActorGiIndex(actor, ctxt, 0x46A); // Notebook: Escaping Sakon's Hideout
            }
            break;
        case ACTOR_EN_FSN: // Curiosity Shop Man
            if (actor->params == 1) { // Back Room
                ProcessActorGiIndex(actor, ctxt, 0xA1); // Letter to Mama
                ProcessActorGiIndex(actor, ctxt, 0x80); // Keaton Mask
                ProcessActorGiIndex(actor, ctxt, 0x470); // Notebook: Keaton Mask
                ProcessActorGiIndex(actor, ctxt, 0x471); // Notebook: Letter to Mama
            } else {
                ProcessActorGiIndex(actor, ctxt, 0x1C7);
                ProcessActorGiIndex(actor, ctxt, 0x1C8);
                ProcessActorGiIndex(actor, ctxt, 0x1C9);
                ProcessActorGiIndex(actor, ctxt, 0x1CA);
            }
            ProcessActorGiIndex(actor, ctxt, 0x452); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x47C); // Notebook: All-Night Mask Purchase
            break;
        case ACTOR_EN_DT: // Mayor
            ProcessActorGiIndex(actor, ctxt, 0x03);
            ProcessActorGiIndex(actor, ctxt, 0x456); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x475); // Notebook Reward
            break;
        case ACTOR_EN_PM:; // Postman
            u8 postmanIsDrawn = *(((u8*)actor)+0x258);
            if (postmanIsDrawn) {
                ProcessActorGiIndex(actor, ctxt, 0xCE);
                ProcessActorGiIndex(actor, ctxt, 0x84);
                ProcessActorGiIndex(actor, ctxt, 0x45A); // Notebook Meeting
                ProcessActorGiIndex(actor, ctxt, 0x47A); // Notebook: Postman's Game
                ProcessActorGiIndex(actor, ctxt, 0x480); // Notebook: Freedom
            }
            break;
        case ACTOR_EN_RZ: // Rosa Sisters
            ProcessActorGiIndex(actor, ctxt, 0x2B);
            ProcessActorGiIndex(actor, ctxt, 0x45B); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x476); // Notebook Reward
            break;
        case ACTOR_EN_BJT: // Toilet Hand
            ProcessActorGiIndex(actor, ctxt, 0x2C);
            ProcessActorGiIndex(actor, ctxt, 0x45C); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x477); // Notebook Reward
            break;
        case ACTOR_EN_NB: // Anju's Grandmother
            ProcessActorGiIndex(actor, ctxt, 0x2D);
            ProcessActorGiIndex(actor, ctxt, 0x2F);
            ProcessActorGiIndex(actor, ctxt, 0x45D); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x478); // Notebook: Short Story
            ProcessActorGiIndex(actor, ctxt, 0x479); // Notebook: Long Story
            break;
        case ACTOR_EN_KUSA2: // Keaton Grass
            if (!(actor->params & 1)) { // Not grass, but the grouping
                if (actor->params != 0x7F00) {
                    ProcessActorGiIndex(actor, ctxt, 0x30); // Keaton Quiz
                    for (u16 kusa2Count = 0; kusa2Count < 9; kusa2Count++) {
                        u16 kusa2GiIndex = KeatonGrassCluster_GetGiIndex(ctxt, kusa2Count);
                        if (!kusa2GiIndex) {
                            break;
                        }
                        ProcessActorGiIndex(actor, ctxt, kusa2GiIndex);
                    }
                }
            }
            break;
        case ACTOR_EN_KITAN: // Keaton
            ProcessActorGiIndex(actor, ctxt, 0x30); // Keaton Quiz
            break;
        case ACTOR_EN_LIFT_NUTS: // Deku Scrub Playground Employee
            ProcessActorGiIndex(actor, ctxt, 0x31);
            ProcessActorGiIndex(actor, ctxt, 0x133);
            break;
        case ACTOR_EN_FU: // Honey & Darling
            ProcessActorGiIndex(actor, ctxt, 0x94);
            ProcessActorGiIndex(actor, ctxt, 0x183);
            break;
        case ACTOR_EN_KENDO_JS: // Swordsman
            ProcessActorGiIndex(actor, ctxt, 0x9F);
            break;
        case ACTOR_EN_PST: // Postbox
            ProcessActorGiIndex(actor, ctxt, 0xA2);
            ProcessActorGiIndex(actor, ctxt, 0x467); // Notebook: Depositing the Letter to Kafei
            break;
        case ACTOR_EN_GS: // Gossip Stone
            if (actor->params == 0x1800) {
                ProcessActorGiIndex(actor, ctxt, 0xA3);
            }
            ProcessActorGiIndex(actor, ctxt, GossipStone_GetGiIndex((ActorEnGs*)actor, ctxt));
            break;
        case ACTOR_EN_SCOPENUTS: // Business Scrub
            if (actor->params == 0x1F) {
                ProcessActorGiIndex(actor, ctxt, 0xA5);
            }
            break;
        case ACTOR_EN_SHN: // Swamp Tourist Center Guide
            ProcessActorGiIndex(actor, ctxt, 0xA7);
            ProcessActorGiIndex(actor, ctxt, 0x188);
            ProcessActorGiIndex(actor, ctxt, 0x18F);
            break;
        case ACTOR_EN_PAMETFROG: // Gekko in WFT
            ProcessActorGiIndex(actor, ctxt, 0x466);
            break;
        case ACTOR_EN_BIGSLIME: // Gekko in GBT
            ProcessActorGiIndex(actor, ctxt, 0x46D);
            break;
        case ACTOR_EN_OT: // Seahorse
            ProcessActorGiIndex(actor, ctxt, 0xAE);
            if (actor->params == 0xFFFF) { // Fish Tank
                ProcessActorGiIndex(actor, ctxt, 0x95);
            }
            break;
        case ACTOR_EN_TSN: // Fisherman
        case ACTOR_EN_JGAME_TSN: // Fisherman
            ProcessActorGiIndex(actor, ctxt, 0xAF);
            break;
        case ACTOR_EN_ZOS: // Evan
            ProcessActorGiIndex(actor, ctxt, 0xB0);
            break;
        case ACTOR_EN_AOB_01: // Mamamu Yan
            ProcessActorGiIndex(actor, ctxt, 0xB1);
            break;
        case ACTOR_EN_GB2: // Ghost Hut Proprietor
            // TODO prevent ikana guard and secret shrine ghost?
            ProcessActorGiIndex(actor, ctxt, 0xB2);
            break;
        case ACTOR_EN_TAKARAYA: // Treasure Chest Shop Employee
            ProcessActorGiIndex(actor, ctxt, 0x17);
            ProcessActorGiIndex(actor, ctxt, 0x1C4);
            ProcessActorGiIndex(actor, ctxt, 0x1C5);
            ProcessActorGiIndex(actor, ctxt, 0x1C6);
            break;
        case ACTOR_EN_BABA:; // Bomb Shop Proprietor's Mother
            u16 babaFlags = *(u16*)(((u8*)actor) + 0x40A);
            if (babaFlags & 2) { // visible
                ProcessActorGiIndex(actor, ctxt, 0x8D);
                ProcessActorGiIndex(actor, ctxt, 0x453); // Notebook Meeting
                ProcessActorGiIndex(actor, ctxt, 0x482); // Notebook Reward
                if (ctxt->sceneNum == SCENE_BACKTOWN && (gSaveContext.perm.entrance.value != 0xD670 || gSaveContext.perm.weekEventReg.bytes[33] & 8) && !(babaFlags & 4)) {
                    ProcessActorGiIndex(actor, ctxt, 0x1C); // Big Bomb Bag
                }
            }
            break;
        case ACTOR_EN_SUTTARI:; // Sakon
            u16 suttariFlags1 = *(u16*)(((u8*)actor) + 0x1E4);
            if (suttariFlags1 & 6) { // carrying big bomb bag
                ProcessActorGiIndex(actor, ctxt, 0x1C); // Big Bomb Bag
            }
            break;
        case ACTOR_EN_STONE_HEISHI: // Shiro
            ProcessActorGiIndex(actor, ctxt, 0x8B);
            ProcessActorGiIndex(actor, ctxt, 0x461); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x484); // Notebook Reward
            break;
        case ACTOR_EN_GURUGURU: // Guru Guru
            ProcessActorGiIndex(actor, ctxt, 0x8C);
            ProcessActorGiIndex(actor, ctxt, 0x462); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x485); // Notebook Reward
            break;
        case ACTOR_EN_HS: // Grog
            ProcessActorGiIndex(actor, ctxt, 0x7F);
            ProcessActorGiIndex(actor, ctxt, 0x45F); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x47D); // Notebook Reward
            break;
        case ACTOR_EN_GEG: // Hungry Goron
            ProcessActorGiIndex(actor, ctxt, 0x88);
            break;
        case ACTOR_EN_DNO: // Deku Butler
            ProcessActorGiIndex(actor, ctxt, 0x8E);
            break;
        case ACTOR_EN_MA_YTO: // Cremia
            ProcessActorGiIndex(actor, ctxt, 0x82);
            ProcessActorGiIndex(actor, ctxt, 0x455); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x46E); // Notebook: Protect Milk Delivery
            break;
        case ACTOR_EN_GM: // Gorman
            if (*(((u8*)actor)+0x258)) { // scheduleResult
                ProcessActorGiIndex(actor, ctxt, 0x83);
                ProcessActorGiIndex(actor, ctxt, 0x459); // Notebook Meeting
                ProcessActorGiIndex(actor, ctxt, 0x47F); // Notebook Reward
            }
            break;
        case ACTOR_EN_YB: // Kamaro
            ProcessActorGiIndex(actor, ctxt, 0x89);
            ProcessActorGiIndex(actor, ctxt, 0x45E); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x483); // Notebook Reward
            break;
        case ACTOR_EN_HG: // Pamela's Father
        case ACTOR_EN_HGO: // Pamela's Father
            ProcessActorGiIndex(actor, ctxt, 0x87);
            break;
        case ACTOR_EN_IN: // Gorman Bros
            ProcessActorGiIndex(actor, ctxt, 0x81);
            ProcessActorGiIndex(actor, ctxt, 0x1A0);
            ProcessActorGiIndex(actor, ctxt, 0x460); // Notebook Meeting
            ProcessActorGiIndex(actor, ctxt, 0x47E); // Notebook Reward
            break;
        case ACTOR_EN_GG: // Darmani's Ghost
        case ACTOR_EN_GG2: // Darmani's Ghost
            ProcessActorGiIndex(actor, ctxt, 0x79);
            break;
        case ACTOR_EN_ZOG: // Mikau
            ProcessActorGiIndex(actor, ctxt, 0x7A);
            break;
        case ACTOR_DM_STK: // Skull Kid
            ProcessActorGiIndex(actor, ctxt, 0x4C);
            ProcessActorGiIndex(actor, ctxt, 0x44C);
            break;
        case ACTOR_EN_SEKIHI: // Song Pedestal
            if (actor->params == 0x3) {
                ProcessActorGiIndex(actor, ctxt, 0x70); // Song of Soaring
            }
            break;
        case ACTOR_BG_HAKA_TOMB: // Flat's Tomb
            ProcessActorGiIndex(actor, ctxt, 0x72);
            break;
        case ACTOR_EN_MNK: // Monkey
            if (ctxt->sceneNum == SCENE_DEKU_KING) {
                ProcessActorGiIndex(actor, ctxt, 0x73);
            }
            break;
        case ACTOR_EN_ZORAEGG: // Zora Egg
            if (actor->draw) {
                ProcessActorGiIndex(actor, ctxt, 0x75);
            }
            break;
        case ACTOR_EN_KNIGHT: // Igos
            ProcessActorGiIndex(actor, ctxt, 0x1CB);
            break;
        case ACTOR_DOOR_WARP1: // Boss Warp
            switch (ctxt->sceneNum) {
                case 0x1F:
                case 0x44:
                case 0x5F:
                case 0x36:
                    ProcessActorGiIndex(actor, ctxt, 0x77);
                    break;
            }
            break;
        case ACTOR_BOSS_01: // Odolwa
            ProcessActorGiIndex(actor, ctxt, 0x77);
            ProcessActorGiIndex(actor, ctxt, 0x11A);
            ProcessActorGiIndex(actor, ctxt, 0x448);
            break;
        case ACTOR_BOSS_HAKUGIN: // Goht
            ProcessActorGiIndex(actor, ctxt, 0x77);
            ProcessActorGiIndex(actor, ctxt, 0x11B);
            ProcessActorGiIndex(actor, ctxt, 0x449);
            break;
        case ACTOR_BOSS_03: // Gyorg
            ProcessActorGiIndex(actor, ctxt, 0x77);
            ProcessActorGiIndex(actor, ctxt, 0x11C);
            ProcessActorGiIndex(actor, ctxt, 0x44A);
            break;
        case ACTOR_BOSS_02: // Twinmold
            ProcessActorGiIndex(actor, ctxt, 0x77);
            ProcessActorGiIndex(actor, ctxt, 0x11D);
            ProcessActorGiIndex(actor, ctxt, 0x44B);
            break;
        case ACTOR_EN_ITEM00: // freestanding item
            if (actor->params >= 0x1D) {
                ProcessActorGiIndex(actor, ctxt, actor->params + 0x80);
            } else {
                ProcessActorGiIndex(actor, ctxt, Rupee_GetGiIndex(actor));
            }
            break;
        case ACTOR_OBJ_COMB: // beehive
            if (actor->params & 0x8000) { // Golden Skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, actor->params & 0x1F);
            } else if (actor->params & 0x80) { // Bees
                // Ignore
            } else if (actor->params & 0x3F == 0xC) { // Piece of Heart
                ProcessActorGiIndex(actor, ctxt, 0x111);
            } else {
                u16 objCombFlag = (actor->params >> 8) & 0x7F;
                if (actor->params & 0x3F == 0x11) { // Stray Fairy
                    ProcessStrayFairyFlag(actor, ctxt, objCombFlag);
                } else {
                    ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(objCombFlag));
                }
            }
            break;
        case ACTOR_EN_FISH2: // Marine Research Lab Fish
            ProcessActorGiIndex(actor, ctxt, 0x112);
            break;
        case ACTOR_ITEM_B_HEART: // Heart Container
            switch (ctxt->sceneNum) {
                case 0x1F:
                    ProcessActorGiIndex(actor, ctxt, 0x11A);
                    break;
                case 0x44:
                    ProcessActorGiIndex(actor, ctxt, 0x11B);
                    break;
                case 0x5F:
                    ProcessActorGiIndex(actor, ctxt, 0x11C);
                    break;
                case 0x36:
                    ProcessActorGiIndex(actor, ctxt, 0x11D);
                    break;
            }
            break;
        case ACTOR_EN_BAL: // Tingle
            switch (actor->params) {
                case 0:
                    ProcessActorGiIndex(actor, ctxt, 0xB4);
                    ProcessActorGiIndex(actor, ctxt, 0xB5);
                    break;
                case 1:
                    ProcessActorGiIndex(actor, ctxt, 0xB5);
                    ProcessActorGiIndex(actor, ctxt, 0xB6);
                    break;
                case 2:
                    ProcessActorGiIndex(actor, ctxt, 0xB6);
                    ProcessActorGiIndex(actor, ctxt, 0xB7);
                    break;
                case 3:
                    ProcessActorGiIndex(actor, ctxt, 0xB7);
                    ProcessActorGiIndex(actor, ctxt, 0xB8);
                    break;
                case 4:
                    ProcessActorGiIndex(actor, ctxt, 0xB8);
                    ProcessActorGiIndex(actor, ctxt, 0xB9);
                    break;
                case 5:
                    ProcessActorGiIndex(actor, ctxt, 0xB9);
                    ProcessActorGiIndex(actor, ctxt, 0xB4);
                    break;
            }
            break;
        case ACTOR_EN_JS: // Moon Child
            if (actor->params == 0) {
                ProcessActorGiIndex(actor, ctxt, 0x7B);
            }
            break;
        case ACTOR_EN_COW: // Cow
            ProcessActorGiIndex(actor, ctxt, actor->params);
            break;
        case ACTOR_EN_SI: // Skulltula Token
            ProcessGoldenSkulltulaFlag(actor, ctxt, (actor->params & 0xFC) >> 2);
            break;
        case ACTOR_EN_SW: // Skullwalltula
            if (actor->params & 3) { // Golden Skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, (actor->params & 0xFC) >> 2);
            }
            break;
        case ACTOR_OBJ_TSUBO: // Pot
            if (actor->initPosRot.rot.z == 2) { // spawns skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, actor->params & 0x1F);
            } else {
                u16 tsuboFlag = actor->params >> 9 & 0x7F;
                if (actor->params & 0x3F == 0x11) { // Stray Fairy
                    ProcessStrayFairyFlag(actor, ctxt, tsuboFlag);
                } else {
                    ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(tsuboFlag));
                }
            }
            break;
        case ACTOR_OBJ_MAKEKINSUTA: // Soft Soil with Golden Skulltula
            ProcessGoldenSkulltulaFlag(actor, ctxt, (actor->params & 0x1F00) >> 8);
            break;
        case ACTOR_OBJ_KIBAKO2: // Large Crate
            if (((actor->params) >> 0xF) & 1) { // Spawns Golden Skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, actor->params & 0x1F);
            } else {
                u16 kibako2Flag = actor->params >> 8 & 0x7F;
                if (actor->params & 0x3F == 0x11) { // Stray Fairy
                    ProcessStrayFairyFlag(actor, ctxt, kibako2Flag);
                } else {
                    ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(kibako2Flag));
                }
            }
            break;
        case ACTOR_BG_KIN2_PICTURE: // Skullkid Picture
            if (!(actor->params & 0x20)) { // Spawns Golden Skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, actor->params & 0x1F);
            }
            break;
        case ACTOR_EN_KUJIYA: // Lottery
            ProcessActorGiIndex(actor, ctxt, 0x86);
            break;
        case ACTOR_EN_TAB: // Mr. Barten
            ProcessActorGiIndex(actor, ctxt, 0x180);
            ProcessActorGiIndex(actor, ctxt, 0x181);
            break;
        case ACTOR_EN_ZOT: // Zora
            switch (actor->params & 0x1F) {
                case 1: // Jar Game Zora
                    ProcessActorGiIndex(actor, ctxt, 0x445);
                    break;
                case 7: // Lulu Photos Zora
                    ProcessActorGiIndex(actor, ctxt, 0x1A8);
                    ProcessActorGiIndex(actor, ctxt, 0x1C3);
                    break;
                case 9: // Stage Lights Zora
                    ProcessActorGiIndex(actor, ctxt, 0x19E);
                    break;
            }
            break;
        case ACTOR_EN_KUSA: // Grass
            if (actor->params & 3 == 3) {
                u16 kusaFlag = (actor->params >> 8) & 0x7F;
                ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(kusaFlag));
            }
            break;
        case ACTOR_OBJ_SNOWBALL: // Large Snowball
        case ACTOR_OBJ_SNOWBALL2: // Small Snowball
        case ACTOR_OBJ_TARU: // Barrel
        case ACTOR_OBJ_KIBAKO:; // Small Crate
        case ACTOR_OBJ_FLOWERPOT:; // Flower Pot
            u16 collectibleFlag = (actor->params >> 8) & 0x7F;
            if (actor->params & 0x3F == 0x11) { // Stray Fairy
                ProcessStrayFairyFlag(actor, ctxt, collectibleFlag);
            } else {
                ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(collectibleFlag));
            }
            break;
        case ACTOR_EN_INVISIBLE_RUPPE:; // Invisible Rupee
            u16 invisibleRupeeFlag = actor->params >> 2;
            u16 invisibleRupeeGiIndex = 0x34C + invisibleRupeeFlag;
            ProcessActorGiIndex(actor, ctxt, invisibleRupeeGiIndex);
            break;
        case ACTOR_EN_SCOPECOIN: // Spawned Rupee
            ProcessActorGiIndex(actor, ctxt, Scopecoin_GetGiIndex(actor));
            break;
        case ACTOR_EN_SCOPECROW: // Telescope Guay
        case ACTOR_EN_SC_RUPPE: // Telescope Guay Spawned Rupee
            ProcessActorGiIndex(actor, ctxt, 0x362);
            break;
        case ACTOR_OBJ_DORA: // Swordschool Gong
            ProcessActorGiIndex(actor, ctxt, 0x35C);
            break;
        case ACTOR_OBJ_SWPRIZE:; // Soft Soil
            u16 objSwprizeGiIndex = SoftSoilPrize_GetGiIndex(ctxt, actor);
            if (objSwprizeGiIndex == 0x370 || objSwprizeGiIndex == 0x376) {
                ProcessActorGiIndex(actor, ctxt, 0x370);
                ProcessActorGiIndex(actor, ctxt, 0x376);
            } else {
                ProcessActorGiIndex(actor, ctxt, objSwprizeGiIndex);
            }
            break;
        case ACTOR_EN_RUPPECROW: // Termina Field Guay
            for (u16 ruppeCrowGiIndex = 0x37F; ruppeCrowGiIndex <= 0x395; ruppeCrowGiIndex++) {
                ProcessActorGiIndex(actor, ctxt, ruppeCrowGiIndex);
                // TODO break out of loop early
            }
            break;
        case ACTOR_EN_GAKUFU: // Song Wall
            for (u16 gakufuGiIndex = 0x3A4; gakufuGiIndex <= 0x3B2; gakufuGiIndex++) {
                ProcessActorGiIndex(actor, ctxt, gakufuGiIndex);
                // TODO break out of loop early
            }
            break;
        case ACTOR_EN_GAMELUPY: // Deku Scrub Playground Rupee
            ProcessActorGiIndex(actor, ctxt, Rupee_GetGiIndex(actor));
            break;
        case ACTOR_EN_HIT_TAG:; // Hit Tag
            u16 tagId = actor->params & 0x1F;
            for (u16 hitTagCount = 0; hitTagCount < 3; hitTagCount++) {
                ProcessActorGiIndex(actor, ctxt, 0x3C5 + tagId*3 + hitTagCount);
                // TODO break out of loop early
            }
            break;
        case ACTOR_EN_THIEFBIRD: // Takkuri
            ProcessActorGiIndex(actor, ctxt, 0x40D);
            break;
        case ACTOR_EN_ISHI:; // Small Rock
            u16 ishiFlag = (actor->params >> 9) & 0x7F;
            ProcessActorGiIndex(actor, ctxt, Rupee_CollectableFlagToGiIndex(ishiFlag));
            break;
        case ACTOR_EN_TK: // Dampe
            if (actor->draw) {
                ProcessActorGiIndex(actor, ctxt, 0x446);
            }
            break;
        case ACTOR_DM_HINA: // Boss Remains
            ProcessActorGiIndex(actor, ctxt, 0x448 + actor->params);
            break;
        case ACTOR_EN_MINIFROG:; // Small Frog
            ActorEnMinifrog* minifrog = (ActorEnMinifrog*)actor;
            if (minifrog->frogIndex == 0) {
                ProcessActorGiIndex(actor, ctxt, 0xAC); // Frog Choir
            } else {
                ProcessActorGiIndex(actor, ctxt, Minifrog_GetGiIndex(minifrog, ctxt));
            }
            break;
        case ACTOR_EN_BUTTE: // Butterfly
            ProcessActorGiIndex(actor, ctxt, Bufferfly_GetGiIndex((ActorEnButte*)actor, ctxt));
            break;
        default:
            *SkulltulaSoundTimerPtr(actor) = -1;
            break;
    }
}
