#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "Items.h"
#include "BaseRupee.h"
#include "Item00.h"
#include "Scopecoin.h"
#include "SoftSoilPrize.h"
#include "KeatonGrassCluster.h"

#define SkulltulaSoundTimerPtr(actor) ((s8*)(&actor->shape.pad16))

static void ProcessActorGiIndex(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    s8 skulltulaSoundTimer = *SkulltulaSoundTimerPtr(actor);
    if (skulltulaSoundTimer < 0 || ((ctxt->actorCtx.unk5 & 0x8) && (skulltulaSoundTimer > 1))) {
        return;
    }

    if (!giIndex) {
        return;
    }

    u8 item = MMR_GetGiEntry(MMR_GetNewGiIndex(ctxt, NULL, giIndex, false))->item;

    if (item == CUSTOM_ITEM_STRAY_FAIRY) {
        ctxt->actorCtx.unk5 |= 0x8;
    }

    if (item == ITEM_SKULLTULA_SPIRIT) {
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
    ProcessActorGiIndex(actor, ctxt, enSwBaseGiIndex + skulltulaFlag);
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

    if (skulltulaSoundTimer < 0) {
        return;
    }

    // great fairy mask already flagged to shimmer and skulltula sound timer already set, or those features are disabled
    if ((!MISC_CONFIG.flags.fairyMaskShimmer || (ctxt->actorCtx.unk5 & 0x8)) && (!MISC_CONFIG.flags.skullTokenSounds || skulltulaSoundTimer > 1)) {
        return;
    }

    switch (actor->id) {
        case ACTOR_EN_BOX:; // Treasure Chest
            ActorEnBox* box = (ActorEnBox*)actor;
            if (!z2_get_chest_flag(ctxt, box->base.params & 0x1F)) {
                ProcessActorGiIndex(actor, ctxt, box->giIndex);
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
            break;
        case ACTOR_EN_GK: // Goron Elder's Son
            ProcessActorGiIndex(actor, ctxt, 0x6A);
            ProcessActorGiIndex(actor, ctxt, 0x74);
            break;
        case ACTOR_EN_AZ: // Beaver Bros
            ProcessActorGiIndex(actor, ctxt, 0x5A);
            ProcessActorGiIndex(actor, ctxt, 0xAD);
            break;
        case ACTOR_EN_AL: // Madame Aroma
            ProcessActorGiIndex(actor, ctxt, 0x6F);
            ProcessActorGiIndex(actor, ctxt, 0x8F);
            break;
        case ACTOR_EN_BOMJIMA: // Bomber Jim
            ProcessActorGiIndex(actor, ctxt, 0x50);
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
        case ACTOR_EN_AN: // Anju
            if (gSaveContext.perm.day%5 == 1 && gSaveContext.perm.time < 0xAC68) {
                ProcessActorGiIndex(actor, ctxt, 0xA0); // Room Key
            }
            // TODO specify circumstances
            ProcessActorGiIndex(actor, ctxt, 0xAA); // Letter to Kafei
            ProcessActorGiIndex(actor, ctxt, 0x85); // Couple's Mask
            break;
        case ACTOR_EN_IG: // Link the Goron
            if (gSaveContext.perm.day%5 == 1 && gSaveContext.perm.time >= 0xAC68) {
                ProcessActorGiIndex(actor, ctxt, 0xA0); // Room Key
            }
            break;
        case ACTOR_EN_TEST3: // Kafei
            ProcessActorGiIndex(actor, ctxt, 0x85); // Couple's Mask
            ProcessActorGiIndex(actor, ctxt, 0xAB); // Pendant of Memories
            // TODO only while wearing Keaton Mask
            ProcessActorGiIndex(actor, ctxt, 0x80); // Keaton Mask
            break;
        case ACTOR_EN_FSN: // Curiosity Shop Man
            if (actor->params == 1) { // Back Room
                ProcessActorGiIndex(actor, ctxt, 0xA1); // Letter to Mama
                ProcessActorGiIndex(actor, ctxt, 0x80); // Keaton Mask
            } else {
                ProcessActorGiIndex(actor, ctxt, 0x1C7);
                ProcessActorGiIndex(actor, ctxt, 0x1C8);
                ProcessActorGiIndex(actor, ctxt, 0x1C9);
                ProcessActorGiIndex(actor, ctxt, 0x1CA);
            }
            break;
        case ACTOR_EN_DT: // Mayor
            ProcessActorGiIndex(actor, ctxt, 0x03);
            break;
        case ACTOR_EN_PM:; // Postman
            u8 postmanIsDrawn = *(((u8*)actor)+0x258);
            if (postmanIsDrawn) {
                ProcessActorGiIndex(actor, ctxt, 0xCE);
                ProcessActorGiIndex(actor, ctxt, 0x84);
            }
            break;
        case ACTOR_EN_RZ: // Rosa Sisters
            ProcessActorGiIndex(actor, ctxt, 0x2B);
            break;
        case ACTOR_EN_BJT: // Toilet Hand
            ProcessActorGiIndex(actor, ctxt, 0x2C);
            break;
        case ACTOR_EN_NB: // Anju's Grandmother
            ProcessActorGiIndex(actor, ctxt, 0x2D);
            ProcessActorGiIndex(actor, ctxt, 0x2F);
            break;
        case ACTOR_EN_KUSA2: // Keaton Grass
            if (!actor->params & 1) { // Not grass, but the grouping
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
            break;
        case ACTOR_EN_GS: // Gossip Stone
            if (actor->params == 0x1800) {
                ProcessActorGiIndex(actor, ctxt, 0xA3);
            }
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
        case ACTOR_EN_MINIFROG: // Frog
        case ACTOR_EN_PAMETFROG: // Gekko
            // TODO check if Gekko in GBT works
            ProcessActorGiIndex(actor, ctxt, 0xAC);
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
        case ACTOR_EN_BABA: // Bomb Shop Proprietor's Mother
            ProcessActorGiIndex(actor, ctxt, 0x8D);
            if (ctxt->sceneNum == SCENE_BACKTOWN && (gSaveContext.perm.entrance.value != 0xD670 || gSaveContext.perm.weekEventReg.bytes[33] & 8)) {
                ProcessActorGiIndex(actor, ctxt, 0x1C); // Big Bomb Bag
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
            break;
        case ACTOR_EN_GURUGURU: // Guru Guru
            ProcessActorGiIndex(actor, ctxt, 0x8C);
            break;
        case ACTOR_EN_HS: // Grog
            ProcessActorGiIndex(actor, ctxt, 0x7F);
            break;
        case ACTOR_EN_GEG: // Hungry Goron
            ProcessActorGiIndex(actor, ctxt, 0x88);
            break;
        case ACTOR_EN_DNO: // Deku Butler
            ProcessActorGiIndex(actor, ctxt, 0x8E);
            break;
        case ACTOR_EN_MA_YTO: // Cremia
            ProcessActorGiIndex(actor, ctxt, 0x82);
            break;
        case ACTOR_EN_GM: // Gorman
            ProcessActorGiIndex(actor, ctxt, 0x83);
            break;
        case ACTOR_EN_YB: // Kamaro
            ProcessActorGiIndex(actor, ctxt, 0x89);
            break;
        case ACTOR_EN_HG: // Pamela's Father
        case ACTOR_EN_HGO: // Pamela's Father
            ProcessActorGiIndex(actor, ctxt, 0x87);
            break;
        case ACTOR_EN_IN: // Gorman Bros
            ProcessActorGiIndex(actor, ctxt, 0x81);
            ProcessActorGiIndex(actor, ctxt, 0x1A0);
            break;
        case ACTOR_EN_GG: // Darmani's Ghost
        case ACTOR_EN_GG2: // Darmani's Ghost
            // TODO fix Darmani's Ghost checking for Goron Mask in inventory
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
            ProcessActorGiIndex(actor, ctxt, 0x75);
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
            if (actor->params & 0x3F == 0xC) { // Piece of Heart
                ProcessActorGiIndex(actor, ctxt, 0x111);
            } else if (actor->params & 0x8000) { // Golden Skulltula
                ProcessGoldenSkulltulaFlag(actor, ctxt, actor->params & 0x1F);
            } else {
                u16 objCombFlag = (actor->params >> 8) & 0x7F;
                ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(objCombFlag));
            }
            break;
        case ACTOR_EN_FISH2: // Marine Research Lab Fish
            ProcessActorGiIndex(actor, ctxt, 0x112);
            break;
        case ACTOR_ITEM_B_HEART: // Heart Container
            switch (ctxt->sceneNum) {
                case 0x1F:
                    ProcessActorGiIndex(actor, ctxt, 0x11A);
                case 0x44:
                    ProcessActorGiIndex(actor, ctxt, 0x11B);
                case 0x5F:
                    ProcessActorGiIndex(actor, ctxt, 0x11C);
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
                ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(tsuboFlag));
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
                ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(kibako2Flag));
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
                ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(kusaFlag));
            }
            break;
        case ACTOR_OBJ_SNOWBALL: // Large Snowball
        case ACTOR_OBJ_SNOWBALL2: // Small Snowball
        case ACTOR_OBJ_TARU: // Barrel
        case ACTOR_OBJ_KIBAKO:; // Small Crate
        case ACTOR_OBJ_FLOWERPOT:; // Flower Pot
            u16 collectibleFlag = (actor->params >> 8) & 0x7F;
            ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(collectibleFlag));
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
            ProcessActorGiIndex(actor, ctxt, Item00_CollectableFlagToGiIndex(ishiFlag));
            break;
        case ACTOR_EN_TK: // Dampe
            ProcessActorGiIndex(actor, ctxt, 0x446);
            break;
        case ACTOR_DM_HINA: // Boss Remains
            ProcessActorGiIndex(actor, ctxt, 0x448 + actor->params);
            break;
        default:
            *SkulltulaSoundTimerPtr(actor) = -1;
            break;
    }
}
