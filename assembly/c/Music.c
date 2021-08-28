#include <stdbool.h>
#include <z64.h>
#include <z64addresses.h>
#include "Music.h"

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
        DmaEntry entry = dmadata[index];

        u32 start = entry.romStart + (sequenceId * sizeof(musicState.muteMask));

        z2_RomToRam(start, &musicState.muteMask, sizeof(musicState.muteMask));
    }
}

static u8 CalculateCurrentState() {
    u8 state = 0;
    ActorPlayer* player = GET_PLAYER(&gGlobalContext);
    if (player) {
        state = player->form & 3;
    }
    return 1 << state;
}

static void ProcessChannel(u8 channelIndex, u8 stateMask) {
    SequenceChannelContext* channelContext = gSequenceContext->channels[channelIndex];
    if (channelContext->playState.playing) {
        bool shouldBeMuted = musicState.forceMute & (1 << channelIndex);
        if (!shouldBeMuted) {
            u8 muteMask = musicState.muteMask[channelIndex];
            shouldBeMuted = muteMask & stateMask;
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

    u8 state = CalculateCurrentState();
    if (musicState.currentState != state) {
        musicState.currentState = state;

        for (u8 i = 0; i < sizeof(gSequenceContext->channels) / sizeof(SequenceChannelContext*); i++) {
            ProcessChannel(i, state);
        }
    }
}

void Music_Update(GlobalContext* ctxt) {
    HandleFormChannels(ctxt);
}

void Music_AfterChannelInit(SequenceContext* sequence, u8 channelIndex) {
    if (sequence == gSequenceContext) {
        LoadMuteMask();

        u8 state = CalculateCurrentState();
        musicState.currentState = state;
        ProcessChannel(channelIndex, state);
    }
}

void Music_HandleChannelMute(SequenceChannelContext* channelContext, ChannelState* channelState, SequenceContext* sequence, u8 channelIndex) {
    u8 shouldBeMuted = channelState->param;
    if (shouldBeMuted) {
        if (sequence == gSequenceContext && !channelContext->playState.stopped) {
            musicState.forceMute |= (1 << channelIndex);
        }
        channelContext->playState.muted = true;
    } else {
        if (sequence == gSequenceContext) {
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
