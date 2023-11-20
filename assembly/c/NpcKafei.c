#include <z64.h>
#include "Misc.h"

//========================================================================
// Hook functions for swapping the keaton mask with other field masks,
// or saving a transformation matrix for a getItem hook.
//========================================================================

void NpcKafei_KeatonMaskInit(ActorPlayer* actor) {
    if (!MISC_CONFIG.drawFlags.freestanding) {
        return;
    }

    if (MISC_CONFIG.MMRbytes.npcKafeiReplaceMask == 0) {
        return;
    } else {
        if (actor->mask != 0) {
            actor->mask = MISC_CONFIG.MMRbytes.npcKafeiReplaceMask;
            actor->previousMask = MISC_CONFIG.MMRbytes.npcKafeiReplaceMask;
        }
    }
}

bool NpcKafei_ProcessMask(GlobalContext* ctxt, ActorPlayer* actor, u8 mask, bool isHand) {
    bool setBackUp = false;

    if (mask == 0x05) {
        if (MISC_CONFIG.drawFlags.freestanding) {
            if (MISC_CONFIG.MMRbytes.npcKafeiReplaceMask == 0) {
                Vec3f pos;
                Vec3s rot;

                z2_PushMatrixStackCopy();

                if (isHand) {

                    pos.x = 32.0;
                    pos.y = 768.0;
                    pos.z = -128.0;
                    rot.x = 0x0000;
                    rot.y = 0x0800;
                    rot.z = 0xE000;
                } else {

                    pos.x = 768.0;
                    pos.y = -192.0;
                    pos.z = 0.0;
                    rot.x = 0x8000;
                    rot.y = 0xc000;
                    rot.z = 0x0000;
                }
                z2_TransformMatrixStackTop(&pos, &rot);
                z2_CopyFromMatrixStackTop(&actor->attachmentMtx1);
                z2_PopMatrixStack();
                setBackUp = true;
                return setBackUp;
            }
        }
    }

    if (mask == 0x04) {
        z2_PlayerBunnyHoodLimbs(ctxt);
    }

    if (mask == 0x08) {
        z2_PlayerGormanTears(ctxt, actor);
    }

    if (mask == 0x12) {
        z2_PlayerBlastMaskAnim(ctxt, actor);
    }

    if (mask == 0x0B) {
        if (!isHand) {
            z2_PlayerGreatFairyLimbs(ctxt, actor);
        } else {
            setBackUp = true;
        }
    }
    return setBackUp;
}

void NpcKafei_CheckHand(GlobalContext* ctxt, ActorPlayer* actor) {
    u16 currentAnimationId = (u32)actor->skelAnime.linkAnimetionSeg;
    if ((currentAnimationId == 0xD0A8) || ((actor->stateFlags.state2 & PLAYER_STATE2_MASKHAND) != 0)) {
        if (actor->mask != 0) {
            bool skipMaskDraw;

            skipMaskDraw = NpcKafei_ProcessMask(ctxt, actor, actor->mask, true);

            if (skipMaskDraw) {
                u8 maskTemp = actor->mask;
                actor->mask = 0x00;
                actor->previousMask = 0x00;
                z2_80128640(ctxt, actor);
                actor->mask = maskTemp;
                actor->previousMask = maskTemp;
                return;
            }
        }
    }
    z2_80128640(ctxt, actor);
}

void NpcKafei_CheckHead(GlobalContext* ctxt, ActorPlayer* actor, PlayerMaskDList* maskDList) {
    bool skipMask;
    skipMask = NpcKafei_ProcessMask(ctxt, actor, actor->mask, false);
    if (!skipMask) {
        u32 dl = maskDList->maskDListEntry[actor->mask - 1];
        gSPDisplayList(ctxt->state.gfxCtx->polyOpa.p++, dl);
    }
}
