#ifndef _Z64ADDRESSES_H_
#define _Z64ADDRESSES_H_

#include <z64.h>

// Virtual File Addresses.
#define ItemTextureFileVROM              0xA36C10

// Data Addresses.
#define ArenaAddr                        0x8009CD20
#define DmaEntryTableAddr                0x8009F8B0
#define ActorOverlayTableAddr            0x801AEFD0
#define GetItemGraphicTableAddr          0x801BB170
#define GameStateTableAddr               0x801BD910
#define ItemUpgradeCapacityAddr          0x801C1E04
#define ItemTextureSegAddrTableAddr      0x801C1E6C // Segment address table used for item textures.
#define ItemSlotsAddr                    0x801C2078
#define ObjectTableAddr                  0x801C2740
#define SongNotesAddr                    0x801CFC98
#define SaveContextAddr                  0x801EF670
#define GameArenaAddr                    0x801F5100
#define SegmentTableAddr                 0x801F8180
#define StaticContextAddr                0x803824D0
#define GlobalContextAddr                0x803E6B20 // Todo: Remove.
#define SequenceContextAddr              0x802050D0

// Data.
#define gActorOverlayTable               ((ActorOverlay*)            ActorOverlayTableAddr)
#define gSaveContext                     (*(SaveContext*)            SaveContextAddr)
#define gItemUpgradeCapacity             (*(ItemUpgradeCapacity*)    ItemUpgradeCapacityAddr)
#define dmadata                          ((DmaEntry*)                DmaEntryTableAddr)
#define gGlobalContext                   (*(GlobalContext*)          GlobalContextAddr)
#define gGameStateInfo                   (*(GameStateTable*)         GameStateTableAddr)
#define gGetItemGraphicTable             ((GetItemGraphicEntry*)     GetItemGraphicTableAddr)
#define gItemSlots                       ((u8*)                      ItemSlotsAddr)
#define gObjectFileTable                 ((ObjectFileTableEntry*)    ObjectTableAddr)
#define gRspSegmentPhysAddrs             (*(SegmentTable*)           SegmentTableAddr)
#define gSongNotes                       (*(SongNotes*)              SongNotesAddr)
#define s803824D0                        (*(StaticContext*)          StaticContextAddr)
#define gSequenceContext                 ((SequenceContext*)         SequenceContextAddr)

// Data (non-struct).
#define gItemTextureSegAddrTable         ((u32*)                     ItemTextureSegAddrTableAddr)

// Data (unknown).
#define s801BD8B0                        (*(struct_801BD8B0*)        0x801BD8B0)
#define s801D0B70                        (*(struct_801D0B70*)        0x801D0B70)

// Function Prototypes.
extern int z2_CanInteract(GlobalContext* ctxt);
extern int z2_CanInteract2(GlobalContext* ctxt, ActorPlayer* player);
extern int z2_Inventory_GetBtnItem(GlobalContext* ctxt, ActorPlayer* player, s32 buttonIndex);
extern void z2_DrawButtonAmounts(GlobalContext* ctxt, u32 arg1, u16 alpha);
extern void z2_DrawBButtonIcon(GlobalContext* ctxt);
extern void z2_DrawCButtonIcons(GlobalContext* ctxt);
extern u32 z2_GetFloorPhysicsType(void* arg0, void* arg1, u8 arg2);
extern bool z2_Camera_IsHookArrival(Camera* camera);
extern void z2_PushMatrixStackCopy();
extern void z2_PopMatrixStack();
extern f32* z2_GetMatrixStackTop();
extern void z2_TransformMatrixStackTop(Vec3f* pos, Vec3s* rot);
extern void z2_TranslateMatrix(f32 x, f32 y, f32 z, u8 matrixMode);
extern void z2_Matrix_Scale(f32 x, f32 y, f32 z, u8 matrixMode);
extern void z2_Matrix_RotateXS(s16 x, u8 matrixMode);
extern Gfx* z2_ShiftMatrix(GraphicsContext* gfxCtx);
extern void z2_Matrix_MultVec3f(Vec3f* src, Vec3f* dest);
extern AudioInfo* z2_GetAudioTable(u8 audioType);
extern void z2_PlaySfx(u32 id);
extern void z2_PlaySfxDecide();
extern void z2_PlaySfxCancel();
extern void z2_PlayPlayerSfx(ActorPlayer* player, s16 sfxId);
extern void z2_PlaySfxAtActor(Actor* actor, u32 id);
extern void z2_PlayLoopingSfxAtActor(Actor* actor, u32 id);
extern Actor* z2_SpawnActor(ActorContext* actorCtxt, GlobalContext* ctxt, u16 id, f32 x, f32 y, f32 z, u16 rx, u16 ry, u16 rz, u16 params);
extern void z2_UpdateButtonUsability(GlobalContext* ctxt);
extern void z2_WriteHeartColors(GlobalContext* ctxt);
extern bool z2_LifeMeter_IsCritical(void);
extern void* z2_Lib_SegmentedToVirtual(void* ptr);
extern u8 z2_CheckItemObtainability(u32 item);
extern void z2_RemoveItem(u32 item, u8 slot);
extern void z2_ToggleSfxDampen(int enable);
extern void z2_HandleInputVelocity(f32* linearVelocity, f32 inputVelocity, f32 increaseBy, f32 decreaseBy);
extern bool z2_SetGetItemLongrange(Actor* actor, GlobalContext* ctxt, u16 giIndex);
extern void z2_UpdatePictoFlags(GlobalContext* ctxt);
extern void z2_8012C654(GraphicsContext* gfxCtx);
extern Gfx* z2_8010CFBC(Gfx* gfx, u32 arg1, u16 tileX, u16 tileY, u16 x, u16 y, u16 w, u16 h, u16 widthFactor, u16 heightFactor, s16 r, s16 g, s16 b, s16 a);
extern Gfx* z2_8010D480(Gfx* gfx, u32 arg1, u16 tileX, u16 tileY, u16 x, u16 y, u16 w, u16 h, u16 widthFactor, u16 heightFactor, s16 r, s16 g, s16 b, s16 a, u16 arg14, u16 arg15);

extern void z2_80128640(GlobalContext* ctxt, ActorPlayer* actor);
extern void z2_Player_DrawGetItem(GlobalContext* ctxt, ActorPlayer* actor);
extern void z2_PlayerBunnyHoodLimbs(GlobalContext* ctxt);
extern void z2_PlayerGormanTears(GlobalContext* ctxt, ActorPlayer* player);
extern void z2_PlayerBlastMaskAnim(GlobalContext* ctxt, ActorPlayer* player);
extern void z2_PlayerGreatFairyLimbs(GlobalContext* ctxt, ActorPlayer* player);
extern void z2_CopyFromMatrixStackTop(z_Matrix* mtx);
extern void z2_CopyToMatrixStackTop(z_Matrix* mtx);


// Function Prototypes (Scene Flags).
// TODO parameters
extern void z2_get_generic_flag();
extern void z2_set_generic_flag();
extern void z2_remove_generic_flag(GlobalContext* ctxt, s8 flag);
extern void z2_get_chest_flag();
extern void z2_set_chest_flag();
extern void z2_set_all_chest_flags();
extern void z2_get_all_chest_flags();
extern void z2_get_clear_flag();
extern void z2_set_clear_flag();
extern void z2_remove_clear_flag();
extern void z2_get_temp_clear_flag();
extern void z2_set_temp_clear_flag();
extern void z2_remove_temp_clear_flag();
extern bool z2_get_collectible_flag(GlobalContext* ctxt, s32 flag);
extern void z2_set_collectibe_flag();
extern void z2_load_scene_flags();
extern u16 z2_check_scene_pairs(u16 sceneId);
extern void z2_store_scene_flags();

/* Function Prototypes (Spawners) */
// TODO parameters
extern s8 z2_item_can_be_spawned(u8 type);
extern ActorEnItem00* z2_fixed_drop_spawn(GlobalContext* ctxt, Vec3f* position, u16 type);
extern void z2_rupee_drop_spawn();
extern void z2_random_drop_spawn();
extern void z2_spawn_map_actors();
extern void z2_actor_spawn_2();
extern Actor* z2_Actor_SpawnAsChild(ActorContext* actorCtx, Actor* parent, GlobalContext* ctxt, s16 actorId, f32 posX, f32 posY,
                          f32 posZ, s16 rotX, s16 rotY, s16 rotZ, s32 params);
extern void z2_object_spawn();
extern void z2_load_objects();
extern void z2_load_scene();

extern void z2_EffectSsKiraKira_SpawnSmall(GlobalContext* globalCtx, Vec3f* pos, Vec3f* velocity, Vec3f* accel,
                                 ColorRGBA8* primColor, ColorRGBA8* envColor);
extern void z2_EffectSsHitmark_SpawnCustomScale(GlobalContext* ctxt, s32 type, s16 scale, Vec3f* pos);
extern void z2_EffectSsIceSmoke_Spawn(GlobalContext* ctxt, Vec3f* pos, Vec3f* velocity, Vec3f* accel, s16 scale);

// Function Prototypes (Actors).
extern void z2_ActorProc(Actor* actor, GlobalContext* ctxt);
extern void z2_ActorRemove(ActorContext* actorCtxt, Actor* actor, GlobalContext* ctxt);
extern void z2_ActorUnload(Actor* actor);
extern void z2_SetActorSize(Actor *actor, f32 size);
extern void z2_SetShape(ActorShape* shape, f32 yDisplacement, void* shadowDrawFunc, f32 scale);
extern void z2_Actor_ChangeAnimation(SkelAnime* skelAnime, ActorAnimationEntry* animation, s32 index);
extern void z2_Actor_OffsetOfPointInActorCoords(Actor* actor, Vec3f* offset, Vec3f* point);

// Function Prototypes (Actor Cutscene).
extern void z2_ActorCutscene_ClearWaiting(void);
extern void z2_ActorCutscene_ClearNextCutscenes(void);
extern void z2_ActorCutscene_MarkNextCutscenes(void);
extern void z2_ActorCutscene_End(void);
extern void z2_ActorCutscene_Update(void);
extern void z2_ActorCutscene_SetIntentToPlay(s16 index);
extern s16 z2_ActorCutscene_GetCanPlayNext(s16 index);
extern s16 z2_ActorCutscene_StartAndSetUnkLinkFields(s16 index, Actor* actor);
extern s16 z2_ActorCutscene_StartAndSetFlag(s16 index, Actor* actor);
extern s16 z2_ActorCutscene_Start(s16 index, Actor* actor);
extern s16 z2_ActorCutscene_Stop(s16 index);
extern s16 z2_ActorCutscene_GetCurrentIndex(void);
extern ActorCutscene* z2_ActorCutscene_GetCutscene(s16 index);
extern s16 z2_ActorCutscene_GetAdditionalCutscene(s16 index);
extern s16 z2_ActorCutscene_GetLength(s16 index);
extern s16 z2_ActorCutscene_GetCurrentCamera(void);
extern void z2_ActorCutscene_SetReturnCamera(s16 index);

// Function Prototypes (Font)
extern void z2_Kanfont_LoadAsciiChar(GlobalContext* ctxt, u8 character, s32 iParm3);

// Function Prototypes (Drawing).
extern void z2_BaseDrawCollectable(Actor* actor, GlobalContext* ctxt);
extern void z2_BaseDrawGiModel(GlobalContext* ctxt, u32 graphicIdMinus1);
extern void z2_CallSetupDList(GraphicsContext* gfx);
extern Gfx* z2_8012BC50(Gfx* gfx, u8 r, u8 g, u8 b, u8 a, u16 unk_a5, f32 unk_a6);
extern Gfx* z2_Gfx_CallSetupDL(Gfx* gfx, u32 i);
extern void z2_Gfx_8012C28C(GraphicsContext* gfx);
extern void z2_DrawHeartPiece(Actor* actor, GlobalContext* ctxt);
extern void z2_DrawRupee(Actor* actor, GlobalContext* ctxt);
extern void z2_PreDraw1(Actor* actor, GlobalContext* ctxt, u32 unknown);
extern void z2_PreDraw2(Actor* actor, GlobalContext* ctxt, u32 unknown);
extern void z2_SkelAnime_DrawFlexLod(GlobalContext* ctxt, void** skeleton, Vec3s* jointTable, s32 dListCount,
                                    void* overrideLimbDraw, void* postLimbDraw, Actor* actor, s32 lod);
extern void z2_801660B8(GlobalContext* ctxt, Gfx* gfx);

// Function Prototypes (File Loading).
extern s32 z2_RomToRam(u32 src, void* dst, u32 length);
extern s16 z2_GetFileNumber(u32 vromAddr);
extern u32 z2_GetFilePhysAddr(u32 vromAddr);
extern DmaEntry* z2_GetFileTable(u32 vromAddr);
extern void z2_LoadFile(DmaParams* loadfile);
extern void z2_LoadFileFromArchive(u32 physFile, u8 index, u8* dest, u32 length);
extern void z2_LoadVFileFromArchive(u32 virtFile, u8 index, u8* dest, u32 length);
extern void z2_ReadFile(void* memAddr, u32 vromAddr, u32 size);

extern s32 z2_DmaMgr_SendRequest0(void* vramStart, u32 vromStart, u32 size);
extern void z2_Yaz0_LoadAndDecompressFile(u32 promAddr, void* dest, u32 length);

// Function Prototypes (Get Item).
extern void z2_SetGetItem(Actor* actor, GlobalContext* ctxt, s32 unk2, u32 unk3);
extern bool z2_SetGetItemLongrange(Actor* actor, GlobalContext* ctxt, u16 giIndex);
extern void z2_GiveItem(GlobalContext* ctxt, u8 itemId);
extern u8 z2_IsItemKnown(u8 itemId);
extern bool z2_HasEmptyBottle();
extern void z2_GiveMap(u32 mapIndex);
extern s32 z2_Health_ChangeBy(GlobalContext* ctxt, s16 healthChange);
extern void z2_AddRupees(s32 amount);
extern void z2_Inventory_ChangeAmmo(s16 item, s16 ammoChange);

// Function Prototypes (HUD).
extern void z2_HudSetAButtonText(GlobalContext* ctxt, u16 textId);
extern void z2_InitButtonNoteColors(GlobalContext* ctxt);
extern void z2_ReloadButtonTexture(GlobalContext* ctxt, u8 idx);
extern void z2_UpdateButtonsState(u32 state);

// Function Prototypes (Math).
extern f32 z2_Math_Sins(s16 angle);
extern f32 z2_Math_CosS(s16 angle);
extern void z2_Math_Vec3f_Copy(Vec3f* dest, Vec3f* src);
extern void z2_Math_Vec3s_ToVec3f(Vec3f* dest, Vec3s* src);
extern void z2_Math_Vec3f_ToVec3s(Vec3s* dest, Vec3f* src);
extern void z2_Math_Vec3f_Lerp(Vec3f* a, Vec3f* b, f32 t, Vec3f* dest);
extern f32 z2_Math_Vec3f_DistXZ(Vec3f* p1, Vec3f* p2);

// Function Prototypes (Objects).
extern s8 z2_GetObjectIndex(const SceneContext* ctxt, u16 objectId);

extern s32 z2_Entrance_GetSceneIdAbsolute(u16 entrance);
extern s32 z2_Entrance_GetTransitionFlags(u16 entrance);

extern void z2_AnimatedMat_Draw(GlobalContext* play, AnimatedTexture* matAnim);
extern void z2_SkelAnime_DrawLimb(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, bool* overrideLimbDraw, void* postLimbDraw, Actor* actor);
extern void z2_SkelAnime_DrawLimb2(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, s32 dListCount, bool* overrideLimbDraw, bool* postLimbDraw, Actor* actor);
extern void z2_SkelAnime_DrawLimb3(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, s32 dListCount, bool* overrideLimbDraw, bool* postLimbDraw, void* unkDraw, Actor* actor);

// Function Prototypes (OS).
extern void z2_memcpy(void* dest, const void* src, u32 size);
extern size_t z2_strlen(const unsigned char* s);

// Function Prototypes (RNG).
extern u32 z2_RngInt(void);
extern void z2_RngSetSeed(u32 seed);
extern f32 z2_Rand_ZeroOne(void);

// Function Prototypes (Rooms).
extern void z2_LoadRoom(GlobalContext* ctxt, RoomContext* roomCtxt, u8 roomId);
extern void z2_UnloadRoom(GlobalContext* ctxt, RoomContext* roomCtxt);

// Function Prototypes (Sound).
extern void z2_Audio_PlayObjSoundBgm(Vec3f* pos, s8 seqId);
extern void z2_SetBGM2(u16 bgmId);
extern u16 z2_AudioSeq_GetActiveSeqId(u8 seqPlayerIndex);

// Function Prototypes (Text).
extern void z2_ShowMessage(GlobalContext* ctxt, u16 messageId, Actor* actor);
extern void z2_Message_ContinueTextbox(GlobalContext* ctxt, u16 textId);
extern bool z2_IsMessageClosing(Actor* actor, GlobalContext *ctxt);
extern u8 z2_GetMessageState(MessageContext* msgCtx);

// Function Prototypes (Unknown).
extern s32 z2_800B84D0(Actor* actor, GlobalContext* ctxt); // Actor_IsTalking
extern s32 z2_800B85E0(Actor* actor, GlobalContext* ctxt, f32 uParm3, s32 uParm4);
extern int z2_801242DC(GlobalContext* ctxt);
extern s32 z2_MessageShouldAdvance(GlobalContext* ctxt);
extern void z2_MessageClose(GlobalContext* ctxt);

// Relocatable Functions (kaleido_scope).
#define z2_PauseDrawItemIcon_VRAM        0x80821AD4

// Relocatable Functions (player_actor).
#define z2_LinkDamage_VRAM               0x80833B18
#define z2_LinkInvincibility_VRAM        0x80833998
#define z2_PerformEnterWaterEffects_VRAM 0x8083B8D0
#define z2_PlayerHandleBuoyancy_VRAM     0x808475B4
#define z2_UseItem_VRAM                  0x80831990
#define z2_Player_ItemToActionParam_VRAM 0x8082F524
#define z2_Player_func_8083692C_VRAM     0x8083692C
#define z2_Player_func_80838A90_VRAM     0x80838A90
#define z2_Player_func_80849FE0_VRAM     0x80849FE0
#define z2_Player_func_8084A884_VRAM     0x8084A884
#define z2_Player_func_8084C16C_VRAM     0x8084C16C

// Relocatable Data (player_actor).
#define z2_D_80862B4C_VRAM               0x80862B4C

// Relocatable PlayerUpperActionFunc
#define z2_Player_UpperAction_CarryAboveHead_VRAM   0x808490B4

// Relocatable Types (file_choose).
#define FileChooseDataVRAM               0x80813DF0

// Function Prototypes (Relocatable kaleido_scope functions).
typedef void (*z2_PauseDrawItemIcon_Func)(GraphicsContext* gfx, u32 segAddr, u16 width, u16 height, u16 quadVtxIdx);

// Function Prototypes (Relocatable player_actor functions).
typedef void (*z2_LinkDamage_Func)(GlobalContext* ctxt, ActorPlayer* player, u32 type, f32 xzKnockback, f32 yKnockback, s16 knockbackDirection);
typedef void (*z2_LinkInvincibility_Func)(ActorPlayer* player, u8 frames);
typedef void (*z2_PerformEnterWaterEffects_Func)(GlobalContext* ctxt, ActorPlayer* player);
typedef void (*z2_PlayerHandleBuoyancy_Func)(ActorPlayer* player);
typedef void (*z2_UseItem_Func)(GlobalContext* ctxt, ActorPlayer* player, u8 item);
typedef s32 (*z2_Player_ItemToActionParam_Func)(ActorPlayer* player, s32 itemId);
typedef bool (*z2_Player_func_8083692C_Func)(ActorPlayer* player, GlobalContext* ctxt);
typedef bool (*z2_Player_func_80838A90_Func)(ActorPlayer* player, GlobalContext* ctxt);

#endif
