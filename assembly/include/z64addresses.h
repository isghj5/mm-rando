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
#define GameInfoAddr                     0x801F3F60
#define GameArenaAddr                    0x801F5100
#define SegmentTableAddr                 0x801F8180
#define StaticContextAddr                0x803824D0
#define GlobalContextAddr                0x803E6B20 // Todo: Remove.
#define SequenceContextAddr              0x802050D0

// Data.
#define gActorOverlayTable               ((ActorOverlay*)            ActorOverlayTableAddr)
#define gSaveContext                     (*(SaveContext*)            SaveContextAddr)
#define gGameInfo                        (*(GameInfo**)              GameInfoAddr)
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
extern u8 z2_Player_MaskIdToItemId(s32 maskIdMinusOne);
extern void z2_Player_SetBootData(GlobalContext* ctxt, ActorPlayer* player);
extern void z2_Player_SetEquipmentData(GlobalContext* ctxt, ActorPlayer* player);
extern int z2_Player_InBlockingCsMode(GlobalContext* ctxt, ActorPlayer* player);
extern int z2_Inventory_GetBtnItem(GlobalContext* ctxt, ActorPlayer* player, s32 buttonIndex);
extern void z2_DrawButtonAmounts(GlobalContext* ctxt, u32 arg1, u16 alpha);
extern void z2_DrawBButtonIcon(GlobalContext* ctxt);
extern void z2_DrawCButtonIcons(GlobalContext* ctxt);
extern BgMeshHeader* z2_BgCheck_GetCollisionHeader(CollisionContext* colCtx, s32 bgId);
extern bool z2_BgCheck_EntityCheckCeiling(CollisionContext* colCtx, f32* outY, Vec3f* pos, f32 checkHeight, BgPolygon** outPoly, s32* outBgId, Actor* actor);
extern Actor* z2_DynaPoly_GetActor(CollisionContext* colCtx, s32 bgId);
extern u32 z2_GetFloorPhysicsType(void* arg0, void* arg1, u8 arg2);
extern bool z2_SurfaceType_GetFloorType(CollisionContext* colCtx, BgPolygon* poly, s32 bgId);
extern bool z2_SurfaceType_IsHorseBlocked(CollisionContext* colCtx, BgPolygon* poly, s32 bgId);
extern bool z2_SurfaceType_IsWallDamage(CollisionContext* colCtx, BgPolygon* poly, s32 bgId);
extern bool z2_Camera_IsHookArrival(Camera* camera);
extern bool z2_Camera_ChangeSetting(Camera* camera, s16 setting);
extern void z2_PushMatrixStackCopy();
extern void z2_PopMatrixStack();
extern f32* z2_GetMatrixStackTop();
extern void z2_Matrix_RotateY(s16 rotation, s32 appendToState);
extern void z2_Matrix_InsertZRotation_f(f32 rotation, s32 appendToState);
extern void z2_TransformMatrixStackTop(Vec3f* pos, Vec3s* rot);
extern void z2_TranslateMatrix(f32 x, f32 y, f32 z, u8 matrixMode);
extern void z2_Matrix_Scale(f32 x, f32 y, f32 z, u8 matrixMode);
extern void z2_Matrix_RotateXS(s16 x, u8 matrixMode);
extern Gfx* z2_ShiftMatrix(GraphicsContext* gfxCtx);
extern void z2_Matrix_MultVec3f(Vec3f* src, Vec3f* dest);
extern void z2_Matrix_GetStateTranslationAndScaledX(f32 scale, Vec3f* dst);
extern void z2_Matrix_GetStateTranslationAndScaledY(f32 scale, Vec3f* dst);
extern void z2_Matrix_GetStateTranslationAndScaledZ(f32 scale, Vec3f* dst);
extern AudioInfo* z2_GetAudioTable(u8 audioType);
extern void z2_PlaySfx(u32 id);
extern void z2_PlaySfx_2(u16 id);
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
extern s32 z2_Math_AsymStepToS(s16* pValue, s16 target, s16 incrStep, s16 decrStep);
extern s32 z2_Math_AsymStepToF(f32* pValue, f32 target, f32 incrStep, f32 decrStep);
extern bool z2_SetGetItemLongrange(Actor* actor, GlobalContext* ctxt, u16 giIndex);
extern void z2_UpdatePictoFlags(GlobalContext* ctxt);
extern void z2_8012C654(GraphicsContext* gfxCtx);
extern Gfx* z2_Gfx_DrawTexRectIA8(Gfx* gfx, void* texture, s16 textureWidth, s16 textureHeight, s16 rectLeft, s16 rectTop,
                        s16 rectWidth, s16 rectHeight, u16 dsdx, u16 dtdy);
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
extern s32 z2_get_generic_flag(GlobalContext* ctxt, s32 flag);
extern void z2_set_generic_flag();
extern void z2_remove_generic_flag(GlobalContext* ctxt, s8 flag);
extern s32 z2_get_chest_flag(GlobalContext* ctxt, s8 flag);
extern void z2_set_chest_flag();
extern void z2_set_all_chest_flags();
extern void z2_get_all_chest_flags();
extern void z2_get_clear_flag();
extern void z2_set_clear_flag();
extern void z2_remove_clear_flag();
extern void z2_get_temp_clear_flag();
extern void z2_set_temp_clear_flag();
extern void z2_remove_temp_clear_flag();
extern s32 z2_get_collectible_flag(GlobalContext* ctxt, s32 flag);
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
extern f32 z2_Player_GetHeight_WithoutEpona(ActorPlayer* player);

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
extern void z2_Sram_ResetSaveFromMoonCrash(SramContext* sramCtxt);
extern void z2_Sram_SaveSpecialNewDay(GlobalContext* ctxt);
extern void z2_Sram_SetFlashPagesDefault(SramContext* sramCtxt, u32 curPage, u32 numPages);
extern void z2_Sram_StartWriteToFlashDefault(SramContext* sramCtxt);
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
extern s16 z2_Inventory_GetSkullTokenCount(s16 sceneIndex);
extern s32 z2_Health_ChangeBy(GlobalContext* ctxt, s16 healthChange);
extern void z2_AddRupees(s32 amount);
extern void z2_Inventory_ChangeAmmo(s16 item, s16 ammoChange);

// Function Prototypes (HUD).
extern void z2_HudSetAButtonText(GlobalContext* ctxt, u16 textId);
extern void z2_InitButtonNoteColors(GlobalContext* ctxt);
extern void z2_ReloadButtonTexture(GlobalContext* ctxt, u8 idx);
extern void z2_UpdateButtonsState(u32 state);

// Function Prototypes (Math).
extern f32 z2_Math_SinS(s16 angle);
extern f32 z2_Math_CosS(s16 angle);
extern s32 z2_Math_StepToS(s16* pValue, s16 target, s16 step);
extern s32 z2_Math_StepToC(s8* pValue, s8 target, s8 step);
extern s32 z2_Math_StepToF(f32* pValue, f32 target, f32 step);
extern void z2_Math_Vec3f_Copy(Vec3f* dest, Vec3f* src);
extern void z2_Math_Vec3s_ToVec3f(Vec3f* dest, Vec3s* src);
extern void z2_Math_Vec3f_ToVec3s(Vec3s* dest, Vec3f* src);
extern void z2_Math_Vec3f_Lerp(Vec3f* a, Vec3f* b, f32 t, Vec3f* dest);
extern f32 z2_Math_Vec3f_DistXZ(Vec3f* p1, Vec3f* p2);
extern f32 z2_Math_SmoothStepToF(f32* pValue, f32 target, f32 fraction, f32 step, f32 minStep);
extern f32 Math_ApproachF(f32* pValue, f32 target, f32 scale, f32 maxStep);
extern f32 Math_ApproachZeroF(f32* pValue, f32 scale, f32 maxStep);
extern s16 z2_Math_SmoothStepToS(s16* pValue, s16 target, s16 scale, s16 step, s16 minStep);
extern f32 z2_Math3D_Vec3fDistSq(Vec3f* a, Vec3f* b);

// Function Prototypes (Objects).
extern s8 z2_GetObjectIndex(const SceneContext* ctxt, u16 objectId);

extern s32 z2_Entrance_GetSceneIdAbsolute(u16 entrance);
extern s32 z2_Entrance_GetTransitionFlags(u16 entrance);

extern void z2_AnimatedMat_Draw(GlobalContext* play, AnimatedTexture* matAnim);
extern void z2_SkelAnime_DrawLimb(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, bool* overrideLimbDraw, void* postLimbDraw, Actor* actor);
extern void z2_SkelAnime_DrawLimb2(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, s32 dListCount, bool* overrideLimbDraw, bool* postLimbDraw, Actor* actor);
extern void z2_SkelAnime_DrawLimb3(GlobalContext* ctxt, u32* skeleton, Vec3s* limbDrawTable, s32 dListCount, bool* overrideLimbDraw, bool* postLimbDraw, void* unkDraw, Actor* actor);
extern bool z2_PlayerAnimation_Update(GlobalContext* ctxt, SkelAnime* skelAnime);
extern bool z2_SkelAnime_Update(SkelAnime* skelAnime);
extern void z2_Animation_MorphToLoop(SkelAnime* skelAnime, AnimationHeader* animation, f32 morphFrames);

// Function Prototypes (OS).
extern void z2_bzero(void* dest, u32 size);
extern void z2_memcpy(void* dest, const void* src, u32 size);
extern size_t z2_strlen(const unsigned char* s);
extern f32 z2_sqrtf(f32 f);

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
extern void z2_801A3E38(u8 arg0);
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
extern u32 z2_80147624(GlobalContext* ctxt);
extern void z2_Play_EnableMotionBlur(s16 unkA0);
extern s16 z2_Play_DisableMotionBlur();
extern f32 z2_CollisionCheck_GetDamageAndEffectOnBumper(ColCommon* at, ColBodyInfo* atInfo, ColCommon* ac, ColBodyInfo* acInfo, u32* effect);
extern s32 z2_CollisionCheck_GetToucherDamage(ColCommon* at, ColBodyInfo* atInfo, ColCommon* ac, ColBodyInfo* acInfo);
extern void z2_Collider_UpdateCylinder(Actor* actor, ColCylinder* collider);
extern void z2_800EA0D4(GlobalContext* ctxt, CutsceneContext* csCtx);
extern void z2_800EA0EC(GlobalContext* ctxt, CutsceneContext* csCtx);
extern void ShrinkWindow_SetLetterboxTarget(s16 unkA0);
extern s16 z2_Play_CreateSubCamera(GlobalContext* ctxt);
extern void z2_Play_CameraChangeStatus(GlobalContext* ctxt, s16 camId, s16 status);
extern void z2_Play_ClearCamera(GlobalContext* ctxt, s16 camId);
extern Camera* z2_Play_GetCamera(GlobalContext* ctxt, s16 camId);
extern void z2_Play_CameraSetAtEyeUp(GlobalContext* ctxt, s16 camId, Vec3f* at, Vec3f* eye, Vec3f* up);
extern void z2_80169AFC(GlobalContext* ctxt, s16 unkA1, s16 unkA2);

// Relocatable Functions (kaleido_scope).
#define z2_PauseDrawItemIcon_VRAM        0x80821AD4

// Relocatable Functions (player_actor).
#define z2_LinkDamage_VRAM               0x80833B18
#define z2_LinkInvincibility_VRAM        0x80833998
#define z2_PerformEnterWaterEffects_VRAM 0x8083B8D0
#define z2_PlayerHandleBuoyancy_VRAM     0x808475B4
#define z2_UseItem_VRAM                  0x80831990
#define z2_Player_ItemToActionParam_VRAM 0x8082F524
#define z2_PlayerWaitForGiantMask_VRAM   0x80838A20
#define z2_Player_func_8083692C_VRAM     0x8083692C
#define z2_Player_func_80838A90_VRAM     0x80838A90
#define z2_Player_func_8083B930_VRAM     0x8083B930
#define z2_Player_InflictDamage_VRAM     0x8085B3E0
#define z2_Player_StopCutscene_VRAM      0x80838760

#define z2_Player_Action_0_VRAM          0x808496AC
#define z2_Player_Action_1_VRAM          0x808497A0
#define z2_Player_Action_2_VRAM          0x80849A9C
#define z2_Player_Action_3_VRAM          0x80849DD0
#define z2_Player_Action_4_VRAM          0x80849FE0
#define z2_Player_Action_5_VRAM          0x8084A26C
#define z2_Player_Action_6_VRAM          0x8084A5C0
#define z2_Player_Action_7_VRAM          0x8084A794
#define z2_Player_Action_8_VRAM          0x8084A884
#define z2_Player_Action_9_VRAM          0x8084A8E8
#define z2_Player_Action_10_VRAM         0x8084AB4C
#define z2_Player_Action_11_VRAM         0x8084AC84
#define z2_Player_Action_12_VRAM         0x8084AEEC
#define z2_Player_Action_13_VRAM         0x8084AF9C
#define z2_Player_Action_14_VRAM         0x8084B0EC
#define z2_Player_Action_15_VRAM         0x8084B288
#define z2_Player_Action_16_VRAM         0x8084B3B8
#define z2_Player_Action_17_VRAM         0x8084B4A8
#define z2_Player_Action_18_VRAM         0x8084B5C0
#define z2_Player_Action_19_VRAM         0x8084BAA4
#define z2_Player_Action_20_VRAM         0x8084BBF0
#define z2_Player_Action_21_VRAM         0x8084BC64
#define z2_Player_Action_22_VRAM         0x8084BE40
#define z2_Player_Action_23_VRAM         0x8084BF28
#define z2_Player_Action_24_VRAM         0x8084BFDC
#define z2_Player_Action_25_VRAM         0x8084C16C
#define z2_Player_Action_26_VRAM         0x8084C6EC
#define z2_Player_Action_27_VRAM         0x8084C94C
#define z2_Player_Action_28_VRAM         0x8084CA24
#define z2_Player_Action_29_VRAM         0x8084CB58
#define z2_Player_Action_30_VRAM         0x8084CCEC
#define z2_Player_Action_31_VRAM         0x8084CE84
#define z2_Player_Action_32_VRAM         0x8084D18C
#define z2_Player_Action_33_VRAM         0x8084D4EC
#define z2_Player_Action_34_VRAM         0x8084D770
#define z2_Player_Action_35_VRAM         0x8084D820
#define z2_Player_Action_36_VRAM         0x8084E034
#define z2_Player_Action_37_VRAM         0x8084E25C
#define z2_Player_Action_38_VRAM         0x8084E334
#define z2_Player_Action_39_VRAM         0x8084E434
#define z2_Player_Action_40_VRAM         0x8084E4E4
#define z2_Player_Action_41_VRAM         0x8084E58C
#define z2_Player_Action_42_VRAM         0x8084E65C
#define z2_Player_Action_43_VRAM         0x8084E724
#define z2_Player_Action_44_VRAM         0x8084E980
#define z2_Player_Action_45_VRAM         0x8084ED9C
#define z2_Player_Action_46_VRAM         0x8084EE50
#define z2_Player_Action_47_VRAM         0x8084EF9C
#define z2_Player_Action_48_VRAM         0x8084F1B8
#define z2_Player_Action_49_VRAM         0x8084F3DC
#define z2_Player_Action_50_VRAM         0x8084F4E8
#define z2_Player_Action_51_VRAM         0x8084FC0C
#define z2_Player_Action_52_VRAM         0x8084FE7C
#define z2_Player_Action_53_VRAM         0x808505D0
#define z2_Player_Action_54_VRAM         0x808508C8
#define z2_Player_Action_55_VRAM         0x80850B18
#define z2_Player_Action_56_VRAM         0x80850D68
#define z2_Player_Action_57_VRAM         0x808513EC
#define z2_Player_Action_58_VRAM         0x80851588
#define z2_Player_Action_59_VRAM         0x808516B4
#define z2_Player_Action_60_VRAM         0x808519FC
#define z2_Player_Action_61_VRAM         0x80851B58
#define z2_Player_Action_62_VRAM         0x80851BD4
#define z2_Player_Action_63_VRAM         0x8085269C
#define z2_Player_Action_64_VRAM         0x80852B28
#define z2_Player_Action_65_VRAM         0x80852C04
#define z2_Player_Action_66_VRAM         0x80852FD4
#define z2_Player_Action_67_VRAM         0x80853194
#define z2_Player_Action_68_VRAM         0x808534C0
#define z2_Player_Action_69_VRAM         0x80853754
#define z2_Player_Action_70_VRAM         0x80853850
#define z2_Player_Action_71_VRAM         0x80853A5C
#define z2_Player_Action_72_VRAM         0x80853CC0
#define z2_Player_Action_73_VRAM         0x80853D68
#define z2_Player_Action_74_VRAM         0x80854010
#define z2_Player_Action_75_VRAM         0x808540A0
#define z2_Player_Action_76_VRAM         0x80854118
#define z2_Player_Action_77_VRAM         0x8085421C
#define z2_Player_Action_78_VRAM         0x8085437C
#define z2_Player_Action_79_VRAM         0x8085439C
#define z2_Player_Action_80_VRAM         0x80854430
#define z2_Player_Action_81_VRAM         0x80854614
#define z2_Player_Action_82_VRAM         0x808546D0
#define z2_Player_Action_83_VRAM         0x80854800
#define z2_Player_Action_84_VRAM         0x808548B8
#define z2_Player_Action_85_VRAM         0x80854C70
#define z2_Player_Action_86_VRAM         0x808553F4
#define z2_Player_Action_87_VRAM         0x80855818
#define z2_Player_Action_88_VRAM         0x80855A7C
#define z2_Player_Action_89_VRAM         0x80855AF4
#define z2_Player_Action_90_VRAM         0x80855B9C
#define z2_Player_Action_91_VRAM         0x80855C28
#define z2_Player_Action_92_VRAM         0x80855E08
#define z2_Player_Action_93_VRAM         0x808561B0
#define z2_Player_Action_94_VRAM         0x80856918
#define z2_Player_Action_95_VRAM         0x808573A4
#define z2_Player_Action_96_VRAM         0x80857BE8

#define z2_Player_PlayAnimationOnce_VRAM 0x80858CC8
#define z2_Player_PlayAnimationLoop_VRAM 0x80858D48

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
typedef void (*z2_PlayerWaitForGiantMask_Func)(GlobalContext* ctxt, ActorPlayer* player);
typedef s32 (*z2_Player_ItemToActionParam_Func)(ActorPlayer* player, s32 itemId);
typedef bool (*z2_Player_func_8083692C_Func)(ActorPlayer* player, GlobalContext* ctxt);
typedef bool (*z2_Player_func_80838A90_Func)(ActorPlayer* player, GlobalContext* ctxt);
typedef bool (*z2_Player_func_8083B930_Func)(GlobalContext* ctxt, ActorPlayer* player);
typedef void (*z2_Player_PlayAnimationOnce_Func)(GlobalContext* ctxt, ActorPlayer* player, void* anim);
typedef void (*z2_Player_PlayAnimationLoop_Func)(GlobalContext* ctxt, ActorPlayer* player, void* anim);
typedef void (*z2_Player_InflictDamage_Func)(GlobalContext* ctxt, s32 damage);
typedef void (*z2_Player_StopCutscene_Func)(ActorPlayer* player);

#endif
