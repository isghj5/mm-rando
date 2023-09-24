#include <stdbool.h>
#include <z64.h>
#include "Actor.h"
#include "MMR.h"
#include "BaseRupee.h"
#include "Player.h"
#include "Misc.h"

// Number of FairyInst entries in table.
#define FAIRY_INST_COUNT 12

struct FairyInst {
    u16 scene1;
    u16 scene2;
    u16 instance;
    bool used;
};

// Table of known fairy instances, and the scene in which they are normally found.
static struct FairyInst gFairyTable[FAIRY_INST_COUNT] = {
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

// Spawn a fairy actor.
static Actor* SpawnFairyActor(GlobalContext* ctxt, Vec3f pos, u16 inst) {
    Vec3s rot = { 0, 0, 0 };
    return Actor_Spawn(ctxt, ACTOR_EN_ELF, pos, rot, inst);
}

// Whether or not Link can interact with a fairy currently.
bool Fairy_CanInteractWith(GlobalContext* ctxt, ActorPlayer* player) {
    // Cannot collect fairy if in Deku flower
    if ((player->stateFlags.state3 & PLAYER_STATE3_DEKU_DIVE) != 0) {
        return false;
    } else {
        return z2_CanInteract(ctxt) == 0;
    }
}

// Get the next available fairy instance Id, and mark as "used" for this scene.
bool Fairy_GetNextInstance(u16* inst, GlobalContext* ctxt) {
    for (int i = 0; i < FAIRY_INST_COUNT; i++) {
        struct FairyInst* fairy = &gFairyTable[i];
        // Do not use a fairy that is already present in this scene.
        if ((ctxt->sceneNum == fairy->scene1) ||
            (ctxt->sceneNum == fairy->scene2))
            continue;
        // Do not use a fairy that has already been used in this scene.
        if (gFairyTable[i].used)
            continue;
        // Set used, and return instance
        gFairyTable[i].used = true;
        *inst = gFairyTable[i].instance;
        return true;
    }
    return false;
}

// Spawn the next avaiable fairy actor.
Actor* Fairy_SpawnNextActor(GlobalContext* ctxt, Vec3f pos) {
    u16 inst;
    if (Fairy_GetNextInstance(&inst, ctxt)) {
        return SpawnFairyActor(ctxt, pos, inst);
    } else {
        return NULL;
    }
}

// Resets the "used" field for each fairy instance.
// Should be called when transitioning to a new scene.
void Fairy_ResetInstanceUsage(void) {
    for (int i = 0; i < FAIRY_INST_COUNT; i++) {
        gFairyTable[i].used = false;
    }
}

bool Fairy_Constructor(ActorEnElf* actor, GlobalContext* ctxt) {
    if (z2_get_collectible_flag(ctxt, actor->collectableFlag)) {
        return true;
    }
    if (actor->collectableFlag != 0) {
        u16 giIndex = Rupee_CollectableFlagToGiIndex(actor->collectableFlag);
        if (giIndex > 0) {
            Rupee_SetGiIndex(&actor->base, giIndex);
            u16 drawGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
            Rupee_SetDrawGiIndex(&actor->base, drawGiIndex);
        }
    }
    return false;
}

void Fairy_GiveItem(ActorEnElf* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetGiIndex(&actor->base);
    if (giIndex == 0) {
        if (!MISC_CONFIG.flags.fewerHealthDrops) {
            z2_Health_ChangeBy(ctxt, 0x80);
        }
        gSaveContext.owl.jinxCounter = 0;
        // return false;
        return;
    }
    if (!MMR_GiveItem(ctxt, &actor->base, giIndex)) {
        Player_Pause(ctxt);
    }
    Rupee_SetGiIndex(&actor->base, 0);
    // return true;
}

static void SpawnIceSmoke(ActorEnElf* actor, GlobalContext* ctxt) {
    Vec3f pos;
    Vec3f velocity = { 0, 1.0f, 0 };
    Vec3f accel = { 0, 0, 0 };
    f32 randomf;

    if (z2_Rand_ZeroOne() < 0.3f) {
        randomf = 2.0f * z2_Rand_ZeroOne() - 1.0f;
        pos = actor->base.currPosRot.pos;
        pos.x += randomf * 20.0f * z2_Math_SinS(actor->base.currPosRot.rot.y + 0x4000);
        pos.z += randomf * 20.0f * z2_Math_CosS(actor->base.currPosRot.rot.y + 0x4000);

        randomf = 2.0f * z2_Rand_ZeroOne() - 1.0f;
        velocity.x = randomf * 1.6f * z2_Math_SinS(actor->base.currPosRot.rot.y);
        velocity.y = 1.8f;
        velocity.z = randomf * 1.6f * z2_Math_CosS(actor->base.currPosRot.rot.y);
        z2_EffectSsIceSmoke_Spawn(ctxt, &pos, &velocity, &accel, 150);
    }
}

void Fairy_PlayItemSfx(ActorEnElf* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetDrawGiIndex(&actor->base);
    if (giIndex > 0) {
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->item == 0xB0) {
            // Ice Trap
            z2_PlayLoopingSfxAtActor(&actor->base, 0x31A4); // NA_SE_EN_MIMICK_BREATH
            SpawnIceSmoke(actor, ctxt);
            return;
        }
    }

    z2_PlaySfxAtActor(&actor->base, 0x20A8); // NA_SE_EV_FIATY_HEAL
}

void Fairy_SetHealthAccumulator(ActorEnElf* actor, GlobalContext* ctxt) {
    // Displaced code:
    actor->unk_246++;
    // End displaced code

    if (MISC_CONFIG.flags.fewerHealthDrops) {
        return;
    }

    u16 giIndex = Rupee_GetDrawGiIndex(&actor->base);
    if (giIndex == 0) {
        gSaveContext.extra.healthAccumulator = 0xA0;
    }
}

const static u16 sBaseGiIndex = 0x4D9;

Actor* Fairy_SpawnFairyGroupMember(ActorContext* actorCtxt, GlobalContext* ctxt, s16 id, f32 x, f32 y, f32 z, s16 rx,
                   s16 ry, s16 rz, s32 params, s32 count) {
    Actor* fairy = z2_SpawnActor(actorCtxt, ctxt, id, x, y, z, rx, ry, rz, params);

    u16 giIndex = sBaseGiIndex + count;

    Rupee_CheckAndSetGiIndex(fairy, ctxt, giIndex);

    return fairy;
}
