#include <stdbool.h>
#include <z64.h>
#include <z64addresses.h>
#include "Music.h"
#include "enums.h"
#include "macro.h"
#include "Text.h"
#include "Sprite.h"

struct MusicConfig MUSIC_CONFIG = {
    .magic = MUSIC_CONFIG_MAGIC,
    .version = 1,
};

static MusicState musicState = {
    .currentState = 0,
};

static u32 sLoadingSequenceId = 0;
static bool sIsMusicIndoors = false;
static bool sIsMusicCave = false;

static void LoadMuteMask() {
    u8 sequenceId = gSequenceContext->sequenceId;
    if (musicState.loadedSequenceId != sequenceId) {
        musicState.loadedSequenceId = sequenceId;

        u32 index = MUSIC_CONFIG.sequenceMaskFileIndex;
        if (index) {
            DmaEntry entry = dmadata[index];

            u32 start = entry.romStart + (sequenceId * SEQUENCE_DATA_SIZE);

            z2_RomToRam(start, &musicState.playMask, SEQUENCE_DATA_SIZE);
            musicState.hasSequenceMaskFile = 1;
        } else {
            musicState.hasSequenceMaskFile = 0;
        }
    }
}

static const u8* sAudioBaseFilter = (u8*)0x801D66E0;

static u16 CalculateCurrentState() {
    u16 state;
    ActorPlayer* player = GET_PLAYER(&gGlobalContext);
    if (player) {
        state = 1 << player->form;

        if (!sIsMusicIndoors && !sIsMusicCave) {
            state = musicState.cumulativeStates.outdoors ? state | SEQUENCE_PLAY_STATE_OUTDOORS : SEQUENCE_PLAY_STATE_OUTDOORS;
        }
        if (sIsMusicIndoors) {
            state = musicState.cumulativeStates.indoors ? state | SEQUENCE_PLAY_STATE_INDOORS : SEQUENCE_PLAY_STATE_INDOORS;
        }
        if (sIsMusicCave) {
            state = musicState.cumulativeStates.cave ? state | SEQUENCE_PLAY_STATE_CAVE : SEQUENCE_PLAY_STATE_CAVE;
        }
        if (player->stateFlags.state1 & PLAYER_STATE1_EPONA) {
            state = musicState.cumulativeStates.epona ? state | SEQUENCE_PLAY_STATE_EPONA : SEQUENCE_PLAY_STATE_EPONA;
        }
        if (player->stateFlags.state1 & PLAYER_STATE1_SWIM || player->stateFlags.state3 & PLAYER_STATE3_ZORA_SWIM || *sAudioBaseFilter == 0x20) {
            state = musicState.cumulativeStates.swimming ? state | SEQUENCE_PLAY_STATE_SWIM : SEQUENCE_PLAY_STATE_SWIM;
        }
        if (player->stateFlags.state3 & PLAYER_STATE3_GORON_SPIKE) {
            state = musicState.cumulativeStates.spikeRolling ? state | SEQUENCE_PLAY_STATE_SPIKE_ROLLING : SEQUENCE_PLAY_STATE_SPIKE_ROLLING;
        }
        if (gGlobalContext.actorCtx.targetContext.nearbyEnemy) {
            state = musicState.cumulativeStates.combat ? state | SEQUENCE_PLAY_STATE_COMBAT : SEQUENCE_PLAY_STATE_COMBAT;
        }
        if (z2_LifeMeter_IsCritical()) {
            state = musicState.cumulativeStates.criticalHealth ? state | SEQUENCE_PLAY_STATE_CRITICAL_HEALTH : SEQUENCE_PLAY_STATE_CRITICAL_HEALTH;
        }
    } else if (gGameStateInfo.fileSelect.loadedRamAddr) {
        u16 formMask = 0;
        u16 cumulativeStates = musicState.cumulativeStates.value;
        u16 nonCumulativeStates = ~cumulativeStates;
        if (gGlobalContext.state.input[0].pressEdge.buttons.du || gGlobalContext.state.input[0].pressEdge.buttons.l) {
            u8 startIndex = musicState.fileSelectMusicFormIndex;
            do {
                musicState.fileSelectMusicFormIndex = (musicState.fileSelectMusicFormIndex + 1) & 0xF;
                formMask = (1 << musicState.fileSelectMusicFormIndex) & nonCumulativeStates;
            } while (!formMask && musicState.fileSelectMusicFormIndex != startIndex);
        } else if (gGlobalContext.state.input[0].pressEdge.buttons.dd) {
            u8 startIndex = musicState.fileSelectMusicFormIndex;
            do {
                musicState.fileSelectMusicFormIndex = (musicState.fileSelectMusicFormIndex - 1) & 0xF;
                formMask = (1 << musicState.fileSelectMusicFormIndex) & nonCumulativeStates;
            } while (!formMask && musicState.fileSelectMusicFormIndex != startIndex);
        } else {
            formMask = (1 << musicState.fileSelectMusicFormIndex) & nonCumulativeStates;
        }

        u16 miscMask = 0;
        if (gGlobalContext.state.input[0].pressEdge.buttons.dr) {
            u8 startIndex = musicState.fileSelectMusicMiscIndex;
            do {
                musicState.fileSelectMusicMiscIndex = (musicState.fileSelectMusicMiscIndex + 1) & 0xF;
                miscMask = (1 << musicState.fileSelectMusicMiscIndex) & cumulativeStates;
            } while (!miscMask && musicState.fileSelectMusicMiscIndex != startIndex && musicState.fileSelectMusicMiscIndex);
        } else if (gGlobalContext.state.input[0].pressEdge.buttons.dl) {
            u8 startIndex = musicState.fileSelectMusicMiscIndex;
            do {
                musicState.fileSelectMusicMiscIndex = (musicState.fileSelectMusicMiscIndex - 1) & 0xF;
                miscMask = (1 << musicState.fileSelectMusicMiscIndex) & cumulativeStates;
            } while (!miscMask && musicState.fileSelectMusicMiscIndex != startIndex && musicState.fileSelectMusicMiscIndex);
        } else {
            miscMask = (1 << musicState.fileSelectMusicMiscIndex) & cumulativeStates;
        }

        state = formMask | miscMask;
    } else if (musicState.currentState) {
        state = musicState.currentState;
    } else {
        state = 1;
    }
    return state;
}

static void ProcessChannel(u8 channelIndex, u16 stateMask) {
    SequenceChannelContext* channelContext = gSequenceContext->channels[channelIndex];
    if (channelContext->playState.playing) {
        bool shouldBeMuted = false;
        if (!channelContext->playState.muted) {
            musicState.forceMute &= ~(1 << channelIndex);
        } else {
            shouldBeMuted = musicState.forceMute & (1 << channelIndex);
        }
        if (!shouldBeMuted) {
            u16 playMask = musicState.playMask[channelIndex];
            u16 formMask = playMask & ~musicState.cumulativeStates.value;
            u16 miscMask = playMask & musicState.cumulativeStates.value;
            u16 formState = stateMask & ~musicState.cumulativeStates.value;
            u16 miscState = stateMask & musicState.cumulativeStates.value;
            bool shouldPlay = (!miscMask || (miscMask & miscState)) && (formMask & formState);
            shouldBeMuted = !shouldPlay;
        }
        bool isMuted = channelContext->playState.muted;
        if (!isMuted && shouldBeMuted) {
            channelContext->playState.muted = true;
        } else if (isMuted && !shouldBeMuted) {
            channelContext->playState.muted = false;
        }
    }
}

static void HandleFormChannels(GlobalContext* ctxt) {
    LoadMuteMask();

    if (musicState.hasSequenceMaskFile) {
        u16 state = CalculateCurrentState();
        if (musicState.currentState != state) {
            musicState.currentState = state;

            for (u8 i = 0; i < sizeof(gSequenceContext->channels) / sizeof(SequenceChannelContext*); i++) {
                ProcessChannel(i, state);
            }
        }
    }
}

static char sCurrentTrackName[SEQUENCE_NAME_MAX_SIZE] = "\0";
static ColorRGBA8 sColor = { 0xFF, 0xFF, 0xFF, 0xFF };
static s16 sAlpha = 0;
void Music_DrawCurrentTrackName(GlobalContext* ctxt) {
    if (!MUSIC_CONFIG.flags.showTrackName || !s801D0B70.selected || !sAlpha) {
        return;
    }

    sColor.a = sAlpha < 0x100 ? (u8)sAlpha : 0xFF;
    Text_PrintWithColor(sCurrentTrackName, 5, 0xE0, sColor);

    DispBuf* db = &ctxt->state.gfxCtx->overlay;
    gSPDisplayList(db->p++, &gSetupDb);
    gDPPipeSync(db->p++);
    Text_Flush(db);
    gDPPipeSync(db->p++);

    z2_Math_StepToS(&sAlpha, 0, 0x10);
}

static void LoadTrackName(u32 id) {
    u32 index = MUSIC_CONFIG.sequenceNamesFileIndex;
    if (index) {
        DmaEntry entry = dmadata[index];

        u32 start = entry.romStart + (id * SEQUENCE_NAME_MAX_SIZE);

        z2_RomToRam(start, &sCurrentTrackName, SEQUENCE_NAME_MAX_SIZE);

        sAlpha = 0x300;
    }
}

void Music_Draw(GlobalContext* ctxt) {
    Music_DrawCurrentTrackName(ctxt);
}

void Music_Update(GlobalContext* ctxt) {
    HandleFormChannels(ctxt);
}

void Music_AfterChannelInit(SequenceContext* sequence, u8 channelIndex) {
    if (sequence == gSequenceContext) {
        LoadMuteMask();

        if (musicState.hasSequenceMaskFile) {
            u16 state = CalculateCurrentState();
            musicState.currentState = state;
            ProcessChannel(channelIndex, state);
        }
    }
}

void Music_HandleChannelMute(SequenceChannelContext* channelContext, ChannelState* channelState, SequenceContext* sequence, u8 channelIndex) {
    u8 shouldBeMuted = channelState->param;
    if (shouldBeMuted) {
        if (musicState.hasSequenceMaskFile && sequence == gSequenceContext && !channelContext->playState.stopped) {
            musicState.forceMute |= (1 << channelIndex);
        }
        channelContext->playState.muted = true;
    } else {
        if (musicState.hasSequenceMaskFile && sequence == gSequenceContext) {
            musicState.forceMute &= ~(1 << channelIndex);
            ProcessChannel(channelIndex, musicState.currentState);
        } else {
            channelContext->playState.muted = false;
        }
    }
}

void Music_SetLoadingSequenceId(u32 id) {
    sLoadingSequenceId = id;
    if (id) {
        LoadTrackName(id);
    }
}

s8 Music_GetAudioLoadType(AudioInfo* audioInfo, u8 audioType) {
    s8 defaultLoadType = audioInfo[1].metadata[1];
    if (audioType == 1 && defaultLoadType != 0 && sLoadingSequenceId != 0) {
        AudioInfo* sequenceInfoTable = z2_GetAudioTable(0);
        return sequenceInfoTable[sLoadingSequenceId + 1].metadata[1];
    } else {
        return defaultLoadType;
    }
}

static u8 sLastSeqId = 0xFF;

bool Music_ShouldFadeOut(GlobalContext* ctxt, s16 sceneLayer) {
    // TODO handle alternate exit scenarios
    // TODO handle taking a water void exit: z_player line 5760
    s16 currentScene = ctxt->sceneNum;
    u16 entrance = ctxt->warpDestination + sceneLayer;
    if (MUSIC_CONFIG.flags.removeMinorMusic && currentScene != SCENE_SPOT00) { // not cutscene
        if (z2_AudioSeq_GetActiveSeqId(3) != 0xFFFF) { // SEQ_PLAYER_BGM_SUB is playing
            return true;
        }
        u16 activeBgm = z2_AudioSeq_GetActiveSeqId(0);
        switch (activeBgm) {
            case 0x0D: // NA_BGM_ALIEN_INVASION
            case 0x38: // NA_BGM_MINI_BOSS
            case 0x55: // NA_BGM_SONG_OF_SOARING
            case 0xFFFF: // Nothing playing
                return true;
        }

        s32 nextScene = z2_Entrance_GetSceneIdAbsolute(entrance);
        if (nextScene == currentScene) {
            switch (activeBgm) {
                case 0x1D: // NA_BGM_MARKET // Clock Town
                    return gSaveContext.extra.voidFlag == -4 // Day transition
                        || (CHECK_EVENTINF(0x43) && !CHECK_EVENTINF(0x42)); // grandma story and not grandma short story
                default:
                    return false;
            }
        }
        if (gSaveContext.extra.voidFlag != -2) { // after-minigame respawn
            switch (currentScene) {
                case SCENE_KAKUSIANA: // Grottos
                case SCENE_WITCH_SHOP: // Potion Shop
                case SCENE_AYASHIISHOP: // Curiosity Shop
                case SCENE_OMOYA: // Ranch House and Barn
                case SCENE_BOWLING: // Honey and Darling
                case SCENE_SONCHONOIE: // Mayor's Residence
                //case SCENE_MILK_BAR: // Milk Bar
                case SCENE_TAKARAYA: // Treasure Chest Shop
                case SCENE_DEKUTES: // Deku Scrub Playground
                case SCENE_SYATEKI_MIZU: // Town Shooting Gallery
                case SCENE_SYATEKI_MORI: // Swamp Shooting Gallery
                case SCENE_SINKAI: // Pinnacle Rock
                case SCENE_YOUSEI_IZUMI: // Fairy's Fountain
                case SCENE_KAJIYA: // Mountain Smithy
                case SCENE_POSTHOUSE: // Post Office
                case SCENE_LABO: // Marine Research Lab
                case SCENE_8ITEMSHOP: // Trading Post
                case SCENE_TAKARAKUJI: // Lottery Shop
                case SCENE_FISHERMAN: // Fisherman's Hut
                case SCENE_GORONSHOP: // Goron Shop
                //case SCENE_35TAKI: // Waterfall Rapids
                case SCENE_BANDROOM: // Zora Hall Rooms
                case SCENE_GORON_HAKA: // Goron Graveyard
                case SCENE_TOUGITES: // Poe Hut
                case SCENE_DOUJOU: // Swordsman's School
                case SCENE_MAP_SHOP: // Tourist Information
                case SCENE_YADOYA: // Stock Pot Inn
                case SCENE_BOMYA: // Bomb Shop
                    return false;
            }
            switch (nextScene) {
                case SCENE_KAKUSIANA: // Grottos
                case SCENE_WITCH_SHOP: // Potion Shop
                case SCENE_AYASHIISHOP: // Curiosity Shop
                case SCENE_OMOYA: // Ranch House and Barn
                case SCENE_BOWLING: // Honey and Darling
                case SCENE_SONCHONOIE: // Mayor's Residence
                //case SCENE_MILK_BAR: // Milk Bar
                case SCENE_TAKARAYA: // Treasure Chest Shop
                case SCENE_DEKUTES: // Deku Scrub Playground
                case SCENE_MITURIN_BS: // Odolwa's Lair
                case SCENE_SYATEKI_MIZU: // Town Shooting Gallery
                case SCENE_SYATEKI_MORI: // Swamp Shooting Gallery
                case SCENE_SINKAI: // Pinnacle Rock
                case SCENE_YOUSEI_IZUMI: // Fairy's Fountain
                case SCENE_KAJIYA: // Mountain Smithy
                case SCENE_POSTHOUSE: // Post Office
                case SCENE_LABO: // Marine Research Lab
                case SCENE_8ITEMSHOP: // Trading Post
                case SCENE_INISIE_BS: // Twinmold's Lair
                case SCENE_TAKARAKUJI: // Lottery Shop
                case SCENE_FISHERMAN: // Fisherman's Hut
                case SCENE_GORONSHOP: // Goron Shop
                case SCENE_HAKUGIN_BS: // Goht's Lair
                //case SCENE_35TAKI: // Waterfall Rapids
                case SCENE_BANDROOM: // Zora Hall Rooms
                case SCENE_GORON_HAKA: // Goron Graveyard
                case SCENE_TOUGITES: // Poe Hut
                case SCENE_DOUJOU: // Swordsman's School
                case SCENE_MAP_SHOP: // Tourist Information
                case SCENE_SEA_BS: // Gyorg's Lair
                case SCENE_YADOYA: // Stock Pot Inn
                case SCENE_BOMYA: // Bomb Shop
                    return false;
                case SCENE_INISIE_R: // Inverted Stone Tower Temple
                    return currentScene != SCENE_F41; // Inverted Stone Tower
                case SCENE_F41: // Inverted Stone Tower
                    return currentScene != SCENE_INISIE_R; // Inverted Stone Tower Temple
            }
        }
    }
    return !(z2_Entrance_GetTransitionFlags(entrance) & 0x8000);
}

void Music_HandleCommandSoundSettings(GlobalContext* ctxt, SceneCmd* cmd) {
    ctxt->sequenceCtx.ambienceId = cmd->soundSettings.ambienceId;

    sIsMusicCave = false;
    sIsMusicIndoors = false;
    switch (ctxt->sceneNum) {
        case SCENE_KAKUSIANA: // Grottos
        case SCENE_DEKUTES: // Deku Scrub Playground
        case SCENE_YOUSEI_IZUMI: // Fairy's Fountain
        case SCENE_GORON_HAKA: // Goron Graveyard
            sIsMusicCave = true;
            break;
        case SCENE_WITCH_SHOP: // Potion Shop
        case SCENE_AYASHIISHOP: // Curiosity Shop
        case SCENE_OMOYA: // Ranch House and Barn
        case SCENE_BOWLING: // Honey and Darling
        case SCENE_SONCHONOIE: // Mayor's Residence
        //case SCENE_MILK_BAR: // Milk Bar
        case SCENE_TAKARAYA: // Treasure Chest Shop
        case SCENE_SYATEKI_MIZU: // Town Shooting Gallery
        case SCENE_SYATEKI_MORI: // Swamp Shooting Gallery
        case SCENE_KAJIYA: // Mountain Smithy
        case SCENE_POSTHOUSE: // Post Office
        case SCENE_LABO: // Marine Research Lab
        case SCENE_8ITEMSHOP: // Trading Post
        case SCENE_TAKARAKUJI: // Lottery Shop
        case SCENE_FISHERMAN: // Fisherman's Hut
        case SCENE_GORONSHOP: // Goron Shop
        case SCENE_BANDROOM: // Zora Hall Rooms
        case SCENE_TOUGITES: // Poe Hut
        case SCENE_DOUJOU: // Swordsman's School
        case SCENE_MAP_SHOP: // Tourist Information
        case SCENE_YADOYA: // Stock Pot Inn
        case SCENE_BOMYA: // Bomb Shop
            sIsMusicIndoors = true;
            break;
    }

    if (!MUSIC_CONFIG.flags.removeMinorMusic) {
        ctxt->sequenceCtx.seqId = cmd->soundSettings.seqId;
        return;
    }
    bool shouldContinueMusic = sIsMusicIndoors || sIsMusicCave;
    if (!shouldContinueMusic) {
        switch (ctxt->sceneNum) {
            case SCENE_MITURIN_BS: // Odolwa's Lair
            case SCENE_SINKAI: // Pinnacle Rock
            case SCENE_INISIE_BS: // Twinmold's Lair
            case SCENE_HAKUGIN_BS: // Goht's Lair
            case SCENE_SEA_BS: // Gyorg's Lair
                shouldContinueMusic = true;
                break;
        }
    }

    u8 seqId = gSaveContext.extra.seqId;

    if (shouldContinueMusic && seqId != 0xFF) {
        sLastSeqId = seqId;
    } else if (!shouldContinueMusic || sLastSeqId == 0xFF) {
        sLastSeqId = cmd->soundSettings.seqId;
    }
    ctxt->sequenceCtx.seqId = sLastSeqId;
}

static bool ObjSound_ShouldSetBgm(Actor* objSound, GlobalContext* ctxt) {
    if (MUSIC_CONFIG.flags.removeMinorMusic) {
        switch (ctxt->sceneNum) {
            case SCENE_BOWLING: // Honey and Darling
            //case SCENE_MILK_BAR: // Milk Bar
            case SCENE_TAKARAYA: // Treasure Chest Shop
            case SCENE_SYATEKI_MIZU: // Town Shooting Gallery
            case SCENE_SYATEKI_MORI: // Swamp Shooting Gallery
            case SCENE_8ITEMSHOP: // Trading Post
            case SCENE_TAKARAKUJI: // Lottery Shop
            case SCENE_GORONSHOP: // Goron Shop
            case SCENE_BANDROOM: // Zora Hall Rooms
            case SCENE_MAP_SHOP: // Tourist Information
            case SCENE_BOMYA: // Bomb Shop
                return false;
        }
    }
    return true;
}

void Music_ObjSound_PlayBgm(Actor* objSound, GlobalContext* ctxt) {
    if (!ObjSound_ShouldSetBgm(objSound, ctxt)) {
        z2_ActorUnload(objSound);
        return;
    }
    z2_Audio_PlayObjSoundBgm(&objSound->projectedPos, objSound->params);
}

void Music_ObjSound_StopBgm(Actor* objSound, GlobalContext* ctxt) {
    if (ObjSound_ShouldSetBgm(objSound, ctxt)) {
        z2_Audio_PlayObjSoundBgm(NULL, 0);
    }
}
