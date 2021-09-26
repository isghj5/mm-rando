#include <stdbool.h>
#include <z64.h>
#include <z64addresses.h>
#include "Music.h"
#include "enums.h"

struct MusicConfig MUSIC_CONFIG = {
    .magic = MUSIC_CONFIG_MAGIC,
    .version = 1,
};

static MusicState musicState = {
    .currentState = -1,
};

static u32 loadingSequenceId = 0;

static void LoadMuteMask() {
    u8 sequenceId = gSequenceContext->sequenceId;
    if (musicState.loadedSequenceId != sequenceId) {
        musicState.loadedSequenceId = sequenceId;

        u32 index = MUSIC_CONFIG.sequenceMaskFileIndex;
        if (index) {
            DmaEntry entry = dmadata[index];

            u32 start = entry.romStart + (sequenceId * 20);

            z2_RomToRam(start, &musicState.playMask, 20);
            musicState.hasSequenceMaskFile = 1;
        } else {
            musicState.hasSequenceMaskFile = 0;
        }
    }
}

static u8 CalculateCurrentState() {
    u8 state = 1;
    ActorPlayer* player = GET_PLAYER(&gGlobalContext);
    if (player) {
        state = 1 << (player->form & 3);

        if (player->stateFlags.state1 & PLAYER_STATE1_EPONA) {
            state = musicState.cumulativeStates.epona ? state | 0x10 : 0x10;
        }
        if (player->stateFlags.state1 & PLAYER_STATE1_SWIM) {
            state = musicState.cumulativeStates.swimming ? state | 0x20 : 0x20;
        }
        if (player->stateFlags.state3 & PLAYER_STATE3_GORON_SPIKE) {
            state = musicState.cumulativeStates.spikeRolling ? state | 0x40 : 0x40;
        }
        if (gGlobalContext.actorCtx.targetContext.nearbyEnemy) {
            state = musicState.cumulativeStates.combat ? state | 0x80 : 0x80;
        }
    }
    return state;
}

static void ProcessChannel(u8 channelIndex, u8 stateMask) {
    SequenceChannelContext* channelContext = gSequenceContext->channels[channelIndex];
    if (channelContext->playState.playing) {
        bool shouldBeMuted = false;
        if (!channelContext->playState.muted) {
            musicState.forceMute &= ~(1 << channelIndex);
        } else {
            shouldBeMuted = musicState.forceMute & (1 << channelIndex);
        }
        if (!shouldBeMuted) {
            u8 playMask = musicState.playMask[channelIndex];
            u8 formMask = playMask & ~musicState.cumulativeStates.value;
            u8 miscMask = playMask & musicState.cumulativeStates.value;
            u8 formState = stateMask & ~musicState.cumulativeStates.value;
            u8 miscState = stateMask & musicState.cumulativeStates.value;
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
        u8 state = CalculateCurrentState();
        if (musicState.currentState != state) {
            musicState.currentState = state;

            for (u8 i = 0; i < sizeof(gSequenceContext->channels) / sizeof(SequenceChannelContext*); i++) {
                ProcessChannel(i, state);
            }
        }
    }
}

void Music_Update(GlobalContext* ctxt) {
    HandleFormChannels(ctxt);
}

void Music_AfterChannelInit(SequenceContext* sequence, u8 channelIndex) {
    if (sequence == gSequenceContext) {
        LoadMuteMask();

        if (musicState.hasSequenceMaskFile) {
            u8 state = CalculateCurrentState();
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
    loadingSequenceId = id;
}

s8 Music_GetAudioLoadType(AudioInfo* audioInfo, u8 audioType) {
    s8 defaultLoadType = audioInfo[1].metadata[1];
    if (audioType == 1 && defaultLoadType != 0 && loadingSequenceId != 0) {
        AudioInfo* sequenceInfoTable = z2_GetAudioTable(0);
        return sequenceInfoTable[loadingSequenceId + 1].metadata[1];
    } else {
        return defaultLoadType;
    }
}
