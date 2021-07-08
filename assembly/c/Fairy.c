#include <stdbool.h>
#include <z64.h>
#include "Actor.h"

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
