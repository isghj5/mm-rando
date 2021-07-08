#ifndef _Z64SCENE_H_
#define _Z64SCENE_H_

#include <n64.h>
#include <PR/ultratypes.h>
#include <unk.h>

typedef struct {
/* 0x0 */ u32 vromStart;
/* 0x4 */ u32 vromEnd;
} RoomFileLocation; // size = 0x8

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ u32 data2;
} SCmdBase;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdSpawnList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdActorList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdCsCameraList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdColHeader;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdRoomList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x2 */ UNK_TYPE1 pad2[2];
    /* 0x4 */ s8 west;
    /* 0x5 */ s8 vertical;
    /* 0x6 */ s8 south;
    /* 0x7 */ u8 clothIntensity;
} SCmdWindSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdEntranceList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 cUpElfMsgNum;
    /* 0x4 */ u32 keepObjectId;
} SCmdSpecialFiles;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 gpFlag1;
    /* 0x4 */ u32 gpFlag2;
} SCmdRoomBehavior;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdMesh;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdObjectList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdLightList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdPathList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdTransiActorList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdLightSettingList;
// Cloudmodding has this as Environment Settings

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x2 */ UNK_TYPE1 pad2[2];
    /* 0x4 */ u8 hour;
    /* 0x5 */ u8 min;
    /* 0x6 */ u8 unk6;
} SCmdTimeSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x2 */ UNK_TYPE1 pad2[2];
    /* 0x4 */ u8 skyboxId;
    /* 0x5 */ u8 unk5;
    /* 0x6 */ u8 unk6;
} SCmdSkyboxSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x2 */ UNK_TYPE1 pad2[2];
    /* 0x4 */ u8 unk4;
    /* 0x5 */ u8 unk5;
} SCmdSkyboxDisables;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdExitList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ u32 data2;
} SCmdEndMarker;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 bgmId;
    /* 0x2 */ UNK_TYPE1 pad2[4];
    /* 0x6 */ u8 nighttimeSFX;
    /* 0x7 */ u8 musicSeq;
} SCmdSoundSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x2 */ UNK_TYPE1 pad2[5];
    /* 0x7 */ u8 echo;
} SCmdEchoSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdCutsceneData;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdAltHeaders;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ u32 data2;
} SCmdWorldMapVisited;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdTextureAnimations;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdCutsceneActorList;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 data1;
    /* 0x4 */ void* segment;
} SCmdMinimapSettings;

typedef struct {
    /* 0x0 */ u8 code;
    /* 0x1 */ u8 num;
    /* 0x4 */ void* segment;
} SCmdMinimapChests;

typedef struct {
    /* 0x0 */ u32 opaqueDl;
    /* 0x4 */ u32 translucentDl;
} RoomMeshType0Params; // size = 0x8

// Fields TODO
typedef struct {
    /* 0x0 */ u8 type;
    /* 0x1 */ u8 format; // 1 = single, 2 = multi
} RoomMeshType1; // size = 0x2

// Size TODO
typedef struct {
    /* 0x0 */ UNK_TYPE1 pad0[0x10];
} RoomMeshType1Params; // size = 0x10

typedef struct {
    /* 0x0 */ UNK_TYPE1 pad0[0x10];
} RoomMeshType2Params; // size = 0x10

typedef struct {
    /* 0x0 */ u8 type;
    /* 0x1 */ u8 count;
    /* 0x2 */ UNK_TYPE1 pad2[0x2];
    /* 0x4 */ RoomMeshType0Params* paramsStart;
    /* 0x8 */ RoomMeshType0Params* paramsEnd;
} RoomMeshType0; // size = 0xC

typedef struct {
    /* 0x0 */ u8 type;
    /* 0x1 */ u8 count;
    /* 0x2 */ UNK_TYPE1 pad2[0x2];
    /* 0x4 */ RoomMeshType2Params* paramsStart;
    /* 0x8 */ RoomMeshType2Params* paramsEnd;
} RoomMeshType2; // size = 0xC

typedef union {
    RoomMeshType0 type0;
    RoomMeshType1 type1;
    RoomMeshType2 type2;
} RoomMesh; // size = 0xC

typedef struct {
    /* 0x00 */ s8 num;
    /* 0x01 */ u8 unk1;
    /* 0x02 */ u8 unk2;
    /* 0x03 */ u8 unk3;
    /* 0x04 */ s8 echo;
    /* 0x05 */ u8 unk5;
    /* 0x06 */ u8 enablePosLights;
    /* 0x07 */ UNK_TYPE1 pad7[0x1];
    /* 0x08 */ RoomMesh* mesh;
    /* 0x0C */ void* segment;
    /* 0x10 */ UNK_TYPE1 pad10[0x4];
} Room; // size = 0x14

typedef struct {
    /* 0x00 */ Room currRoom;
    /* 0x14 */ Room prevRoom;
    /* 0x28 */ void* roomMemPages[2]; // In a scene with transitions, roomMemory is split between two pages that toggle each transition. This is one continuous range, as the second page allocates from the end
    /* 0x30 */ u8 activeMemPage; // 0 - First page in memory, 1 - Second page
    /* 0x31 */ s8 unk31;
    /* 0x32 */ UNK_TYPE1 pad32[0x2];
    /* 0x34 */ void* activeRoomVram;
    /* 0x38 */ DmaRequest dmaRequest;
    /* 0x58 */ OSMesgQueue loadQueue;
    /* 0x70 */ OSMesg loadMsg[1];
    /* 0x74 */ void* unk74;
    /* 0x78 */ s8 transitionCount;
    /* 0x79 */ s8 unk79;
    /* 0x7A */ UNK_TYPE2 unk7A[1];
    /* 0x7C */ void* transitionList;
} RoomContext; // size = 0x80

typedef struct {
    /* 0x0 */ s16 id;
    /* 0x2 */ Vec3s pos;
    /* 0x8 */ Vec3s rot;
    /* 0xE */ s16 params;
} ActorEntry; // size = 0x10

typedef struct {
    /* 0x0 */ u32 data;
    /* 0x4 */ s16 unk4;
    /* 0x6 */ u8 unk6;
    /* 0x7 */ u8 unk7;
} CutsceneEntry; // size = 0x8

typedef struct {
    /* 0x0 */ u8 spawn;
    /* 0x1 */ u8 room;
} EntranceEntry; // size = 0x2

typedef struct {
    /* 0x0 */ s8 scene; // TODO what does it means for this to be neagtive?
    /* 0x1 */ s8 unk1;
    /* 0x2 */ u16 unk2;
} EntranceRecord; // size = 0x4

typedef struct {
    /* 0x0 */ u32 entranceCount;
    /* 0x4 */ EntranceRecord** entrances;
    /* 0x8 */ char* name;
} SceneEntranceTableEntry; // size = 0xC

typedef struct {
    /* 0x00 */ u16 scenes[27];
} SceneIdList; // size = 0x36

typedef struct {
    /* 0x00 */ s16 id; // Negative ids mean that the object is unloaded
    /* 0x02 */ UNK_TYPE1 pad2[0x2];
    /* 0x04 */ void* vramAddr;
    /* 0x08 */ DmaRequest dmaReq;
    /* 0x28 */ OSMesgQueue loadQueue;
    /* 0x40 */ OSMesg loadMsg;
} SceneObject; // size = 0x44

typedef struct {
    /* 0x0 */ u32 romStart;
    /* 0x4 */ u32 romEnd;
    /* 0x8 */ u16 unk8;
    /* 0xA */ UNK_TYPE1 padA[0x1];
    /* 0xB */ u8 sceneConfig; // TODO: This at least controls the behavior of animated textures. Does it do more?
    /* 0xC */ UNK_TYPE1 padC[0x1];
    /* 0xD */ u8 unkD;
    /* 0xE */ UNK_TYPE1 padE[0x2];
} SceneTableEntry; // size = 0x10

typedef struct {
    /* 0x000 */ void* objectVramStart;
    /* 0x004 */ void* objectVramEnd;
    /* 0x008 */ u8 objectCount;
    /* 0x009 */ u8 spawnedObjectCount;
    /* 0x00A */ u8 mainKeepIndex;
    /* 0x00B */ u8 keepObjectId;
    /* 0x00C */ SceneObject objects[35]; // TODO: OBJECT_EXCHANGE_BANK_MAX array size
} SceneContext; // size = 0x958

typedef union {
    /* Command: N/A  */ SCmdBase              base;
    /* Command: 0x00 */ SCmdSpawnList         spawnList;
    /* Command: 0x01 */ SCmdActorList         actorList;
    /* Command: 0x02 */ SCmdCsCameraList      csCameraList;
    /* Command: 0x03 */ SCmdColHeader         colHeader;
    /* Command: 0x04 */ SCmdRoomList          roomList;
    /* Command: 0x05 */ SCmdWindSettings      windSettings;
    /* Command: 0x06 */ SCmdEntranceList      entranceList;
    /* Command: 0x07 */ SCmdSpecialFiles      specialFiles;
    /* Command: 0x08 */ SCmdRoomBehavior      roomBehavior;
    /* Command: 0x09 */ // Unused
    /* Command: 0x0A */ SCmdMesh              mesh;
    /* Command: 0x0B */ SCmdObjectList        objectList;
    /* Command: 0x0C */ SCmdLightList         lightList;
    /* Command: 0x0D */ SCmdPathList          pathList;
    /* Command: 0x0E */ SCmdTransiActorList   transiActorList;
    /* Command: 0x0F */ SCmdLightSettingList  lightSettingList;
    /* Command: 0x10 */ SCmdTimeSettings      timeSettings;
    /* Command: 0x11 */ SCmdSkyboxSettings    skyboxSettings;
    /* Command: 0x12 */ SCmdSkyboxDisables    skyboxDisables;
    /* Command: 0x13 */ SCmdExitList          exitList;
    /* Command: 0x14 */ SCmdEndMarker         endMarker;
    /* Command: 0x15 */ SCmdSoundSettings     soundSettings;
    /* Command: 0x16 */ SCmdEchoSettings      echoSettings;
    /* Command: 0x17 */ SCmdCutsceneData      cutsceneData;
    /* Command: 0x18 */ SCmdAltHeaders        altHeaders;
    /* Command: 0x19 */ SCmdWorldMapVisited   worldMapVisited;
    /* Command: 0x1A */ SCmdTextureAnimations textureAnimations;
    /* Command: 0x1B */ SCmdCutsceneActorList cutsceneActorList;
    /* Command: 0x1C */ SCmdMinimapSettings   minimapSettings;
    /* Command: 0x1D */ // Unused
    /* Command: 0x1E */ SCmdMinimapChests     minimapChests;
} SceneCmd; // size = 0x8

#endif
