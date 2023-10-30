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
    /* 0x0 */ u8  code;
    /* 0x1 */ u8  specId;
    /* 0x2 */ UNK_TYPE1 unk_02[4];
    /* 0x6 */ u8  ambienceId;
    /* 0x7 */ u8  seqId;
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

typedef enum {
    /* 0x00 */ SCENE_20SICHITAI2, // Southern Swamp (Clear)
    /* 0x01 */ SCENE_UNSET_1,
    /* 0x02 */ SCENE_UNSET_2,
    /* 0x03 */ SCENE_UNSET_3,
    /* 0x04 */ SCENE_UNSET_4,
    /* 0x05 */ SCENE_UNSET_5,
    /* 0x06 */ SCENE_UNSET_6,
    /* 0x07 */ SCENE_KAKUSIANA, // Lone Peak Shrine & Grottos
    /* 0x08 */ SCENE_SPOT00, // Cutscene Scene
    /* 0x09 */ SCENE_UNSET_9,
    /* 0x0A */ SCENE_WITCH_SHOP, // Magic Hags' Potion Shop
    /* 0x0B */ SCENE_LAST_BS, // Majora's Lair
    /* 0x0C */ SCENE_HAKASHITA, // Beneath the Graveyard
    /* 0x0D */ SCENE_AYASHIISHOP, // Curiosity Shop
    /* 0x0E */ SCENE_UNSET_E,
    /* 0x0F */ SCENE_UNSET_F,
    /* 0x10 */ SCENE_OMOYA, // Mama's House (Ranch House in PAL) & Barn
    /* 0x11 */ SCENE_BOWLING, // Honey & Darling's Shop
    /* 0x12 */ SCENE_SONCHONOIE, // The Mayor's Residence
    /* 0x13 */ SCENE_IKANA, // Ikana Canyon
    /* 0x14 */ SCENE_KAIZOKU, // Pirates' Fortress
    /* 0x15 */ SCENE_MILK_BAR, // Milk Bar
    /* 0x16 */ SCENE_INISIE_N, // Stone Tower Temple
    /* 0x17 */ SCENE_TAKARAYA, // Treasure Chest Shop
    /* 0x18 */ SCENE_INISIE_R, // Inverted Stone Tower Temple
    /* 0x19 */ SCENE_OKUJOU, // Clock Tower Rooftop
    /* 0x1A */ SCENE_OPENINGDAN, // Before Clock Town
    /* 0x1B */ SCENE_MITURIN, // Woodfall Temple
    /* 0x1C */ SCENE_13HUBUKINOMITI, // Path to Mountain Village
    /* 0x1D */ SCENE_CASTLE, // Ancient Castle of Ikana
    /* 0x1E */ SCENE_DEKUTES, // Deku Scrub Playground
    /* 0x1F */ SCENE_MITURIN_BS, // Odolwa's Lair
    /* 0x20 */ SCENE_SYATEKI_MIZU, // Town Shooting Gallery
    /* 0x21 */ SCENE_HAKUGIN, // Snowhead Temple
    /* 0x22 */ SCENE_ROMANYMAE, // Milk Road
    /* 0x23 */ SCENE_PIRATE, // Pirates' Fortress Interior
    /* 0x24 */ SCENE_SYATEKI_MORI, // Swamp Shooting Gallery
    /* 0x25 */ SCENE_SINKAI, // Pinnacle Rock
    /* 0x26 */ SCENE_YOUSEI_IZUMI, // Fairy's Fountain
    /* 0x27 */ SCENE_KINSTA1, // Swamp Spider House
    /* 0x28 */ SCENE_KINDAN2, // Oceanside Spider House
    /* 0x29 */ SCENE_TENMON_DAI, // Astral Observatory
    /* 0x2A */ SCENE_LAST_DEKU, // Moon Deku Trial
    /* 0x2B */ SCENE_22DEKUCITY, // Deku Palace
    /* 0x2C */ SCENE_KAJIYA, // Mountain Smithy
    /* 0x2D */ SCENE_00KEIKOKU, // Termina Field
    /* 0x2E */ SCENE_POSTHOUSE, // Post Office
    /* 0x2F */ SCENE_LABO, // Marine Research Lab
    /* 0x30 */ SCENE_DANPEI2TEST, // Beneath the Graveyard (Day 3) and Dampe's House
    /* 0x31 */ SCENE_UNSET_31,
    /* 0x32 */ SCENE_16GORON_HOUSE, // Goron Shrine
    /* 0x33 */ SCENE_33ZORACITY, // Zora Hall
    /* 0x34 */ SCENE_8ITEMSHOP, // Trading Post
    /* 0x35 */ SCENE_F01, // Romani Ranch
    /* 0x36 */ SCENE_INISIE_BS, // Twinmold's Lair
    /* 0x37 */ SCENE_30GYOSON, // Great Bay Coast
    /* 0x38 */ SCENE_31MISAKI, // Zora Cape
    /* 0x39 */ SCENE_TAKARAKUJI, // Lottery Shop
    /* 0x3A */ SCENE_UNSET_3A,
    /* 0x3B */ SCENE_TORIDE, // Pirates' Fortress Moat
    /* 0x3C */ SCENE_FISHERMAN, // Fisherman's Hut
    /* 0x3D */ SCENE_GORONSHOP, // Goron Shop
    /* 0x3E */ SCENE_DEKU_KING, // Deku King's Chamber
    /* 0x3F */ SCENE_LAST_GORON, // Moon Goron Trial
    /* 0x40 */ SCENE_24KEMONOMITI, // Road to Southern Swamp
    /* 0x41 */ SCENE_F01_B, // Doggy Racetrack
    /* 0x42 */ SCENE_F01C, // Cucco Shack
    /* 0x43 */ SCENE_BOTI, // Ikana Graveyard
    /* 0x44 */ SCENE_HAKUGIN_BS, // Goht's Lair
    /* 0x45 */ SCENE_20SICHITAI, // Southern Swamp (poison)
    /* 0x46 */ SCENE_21MITURINMAE, // Woodfall
    /* 0x47 */ SCENE_LAST_ZORA, // Moon Zora Trial
    /* 0x48 */ SCENE_11GORONNOSATO2, // Goron Village (spring)
    /* 0x49 */ SCENE_SEA, // Great Bay Temple
    /* 0x4A */ SCENE_35TAKI, // Waterfall Rapids
    /* 0x4B */ SCENE_REDEAD, // Beneath the Well
    /* 0x4C */ SCENE_BANDROOM, // Zora Hall Rooms
    /* 0x4D */ SCENE_11GORONNOSATO, // Goron Village (winter)
    /* 0x4E */ SCENE_GORON_HAKA, // Goron Graveyard
    /* 0x4F */ SCENE_SECOM, // Sakon's Hideout
    /* 0x50 */ SCENE_10YUKIYAMANOMURA, // Mountain Village (winter)
    /* 0x51 */ SCENE_TOUGITES, // Ghost Hut
    /* 0x52 */ SCENE_DANPEI, // Deku Shrine
    /* 0x53 */ SCENE_IKANAMAE, // Road to Ikana
    /* 0x54 */ SCENE_DOUJOU, // Swordsman's School
    /* 0x55 */ SCENE_MUSICHOUSE, // Music Box House
    /* 0x56 */ SCENE_IKNINSIDE, // Igos du Ikana's Lair
    /* 0x57 */ SCENE_MAP_SHOP, // Tourist Information
    /* 0x58 */ SCENE_F40, // Stone Tower
    /* 0x59 */ SCENE_F41, // Inverted Stone Tower
    /* 0x5A */ SCENE_10YUKIYAMANOMURA2, // Mountain Village (spring)
    /* 0x5B */ SCENE_14YUKIDAMANOMITI, // Path to Snowhead
    /* 0x5C */ SCENE_12HAKUGINMAE, // Snowhead
    /* 0x5D */ SCENE_17SETUGEN, // Path to Goron Village (winter)
    /* 0x5E */ SCENE_17SETUGEN2, // Path to Goron Village (spring)
    /* 0x5F */ SCENE_SEA_BS, // Gyorg's Lair
    /* 0x60 */ SCENE_RANDOM, // Secret Shrine
    /* 0x61 */ SCENE_YADOYA, // Stock Pot Inn
    /* 0x62 */ SCENE_KONPEKI_ENT, // Great Bay Cutscene
    /* 0x63 */ SCENE_INSIDETOWER, // Clock Tower Interior
    /* 0x64 */ SCENE_26SARUNOMORI, // Woods of Mystery
    /* 0x65 */ SCENE_LOST_WOODS, // Lost Woods (Intro)
    /* 0x66 */ SCENE_LAST_LINK, // Moon Link Trial
    /* 0x67 */ SCENE_SOUGEN, // The Moon
    /* 0x68 */ SCENE_BOMYA, // Bomb Shop
    /* 0x69 */ SCENE_KYOJINNOMA, // Giants' Chamber
    /* 0x6A */ SCENE_KOEPONARACE, // Gorman Track
    /* 0x6B */ SCENE_GORONRACE, // Goron Racetrack
    /* 0x6C */ SCENE_TOWN, // East Clock Town
    /* 0x6D */ SCENE_ICHIBA, // West Clock Town
    /* 0x6E */ SCENE_BACKTOWN, // North Clock Town
    /* 0x6F */ SCENE_CLOCKTOWER, // South Clock Town
    /* 0x70 */ SCENE_ALLEY, // Laundry Pool
    /* 0x71 */ SCENE_MAX
} SceneId;

#endif
