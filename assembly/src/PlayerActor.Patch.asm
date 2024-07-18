;==================================================================================================
; Player actor
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

;==================================================================================================
; Player process get-item
;==================================================================================================

; Replaces:
;   this->unk_AE7 = 1;
;   Message_StartTextbox(play, giEntry->textId, &this->actor);
;   Item_Give(play, giEntry->itemId);
;
;   if ((this->getItemId >= GI_MASK_DEKU) && (this->getItemId <= GI_MASK_KAFEIS_MASK)) {
;       Audio_PlayFanfare(NA_BGM_GET_NEW_MASK);
;   } else if (((this->getItemId >= GI_RUPEE_GREEN) && (this->getItemId <= GI_RUPEE_10)) ||
;               (this->getItemId == GI_RECOVERY_HEART)) {
;       play_sound(NA_SE_SY_GET_BOXITEM);
;   } else {
;       s32 seqId;
;
;       if ((this->getItemId == GI_HEART_CONTAINER) ||
;           ((this->getItemId == GI_HEART_PIECE) && EQ_MAX_QUEST_HEART_PIECE_COUNT)) {
;           seqId = NA_BGM_GET_HEART | 0x900;
;       } else {
;           s32 var_v1;
;
;           if ((this->getItemId == GI_HEART_PIECE) ||
;               ((this->getItemId >= GI_RUPEE_PURPLE) && (this->getItemId <= GI_RUPEE_HUGE))) {
;               var_v1 = NA_BGM_GET_SMALL_ITEM;
;           } else {
;               var_v1 = NA_BGM_GET_ITEM | 0x900;
;           }
;           seqId = var_v1;
;       }
;
;       Audio_PlayFanfare(seqId);
;   }
.org 0x80848324
.area 0xF0, 0
    addiu   t0, r0, 0x0001
    sb      t0, 0x0AE7 (s0)
    or      a0, a3, r0
    or      a1, v1, r0
    jal     MMR_ProcessItem
    or      a2, r0, r0
.endarea

;==================================================================================================
; Player actor update hooks
;==================================================================================================

; Runs when in the "main game" (and not using the menu)
; Replaces:
;   lw      t6, 0x0A74 (s0)
;   addiu   at, r0, 0xFFEF
.org 0x808460D0 ; In RDRAM: 0x80763560
    jal     Player_BeforeUpdate_Hook
    nop

;==================================================================================================
; Damage processing hook
;==================================================================================================

; Replaces:
;   sw      s0, 0x0028 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x002C (sp)
.org 0x80834604 ; In RDRAM: 0x80751A94
    sw      ra, 0x002C (sp)
    jal     Player_BeforeDamageProcess_Hook
    sw      s0, 0x0028 (sp)

;==================================================================================================
; Before Handle Player Frozen State
;==================================================================================================

; Replaces:
;   or      s0, a0, r0
;   or      s1, a1, r0
;   sw      ra, 0x001C (sp)
.org 0x808546DC ; RDRAM: 0x80771B6C, Offset: 0x26C4C
    sw      ra, 0x001C (sp)
    jal     Player_BeforeHandleFrozenState_Hook
    or      s0, a0, r0

;==================================================================================================
; Before Handle Player Voiding State
;==================================================================================================

; Replaces:
;   or      s0, a0, r0
;   or      s1, a1, r0
;   sw      ra, 0x001C (sp)
.org 0x80854228 ; RDRAM: 0x807716B8, Offset: 0x26798
    sw      ra, 0x001C (sp)
    jal     Player_BeforeHandleVoidingState_Hook
    or      s0, a0, r0

;==================================================================================================
; Should Ice Void Zora
;==================================================================================================

; Call function to determine if Zora should void during freeze.
; Replaces:
;   lbu     t9, 0x014B (s0)
;   addiu   at, r0, 0x0002
;   lui     t2, 0x0002
;   bne     t9, at, 0x8085479C
;   addu    t2, t2, s1
.org 0x8085475C ; RDRAM: 0x80771BEC, Offset: 0x26CCC
    jal     Player_ShouldIceVoidZora_Hook
    lbu     t9, 0x014B (s0)  ;; T9 = Link form value.
    lui     t2, 0x0002       ;;
    beqz    v0, 0x8085479C   ;; If returned false (0), jump past code which sets void flag.
    addu    t2, t2, s1       ;;

;==================================================================================================
; Should Prevent Restoring Swim State
;==================================================================================================

; Fix branch into patched code, jump into the branch (instead of the delay slot).
; Replaces:
;   bnel    v1, at, 0x8083BE0C
.org 0x8083BCC4
    bnel    v1, at, 0x8083BE08

; Branch to function end early if not restoring swim state.
; Replaces:
;   lw      t4, 0xA6C (a1)
;   sll     t5, t4, 4
;   bgez    t5, 0x8083BEB4
;   nop
;   lb      t6, 0x0145 (a1)
.org 0x8083BE08 ; RDRAM: 0x80759298
    jal     Player_ShouldPreventRestoringSwimState_Hook
    or      a2, v0, r0      ;; A2 = Function pointer.
    bnez    at, 0x8083BF44  ;; Branch to function end if not restoring swim state.
    lb      t6, 0x0145 (a1) ;; Displaced code.
    bgez    t5, 0x8083BEB4  ;; Original branch if swim flag not set.

;==================================================================================================
; Change Deku Mid-air speed modifier
;==================================================================================================

; Replaces:
;   lui     at, 0x3F00
;   mtc1    at, f4
.org 0x8084C2AC
    jal     DekuHop_GetSpeedModifier_Hook
    nop

;==================================================================================================
; Handle climbing anywhere
;==================================================================================================

; Replaces:
;   or      t0, r0, r0
;   andi    t1, v0, 0x0008
.org 0x8083D8D4
    jal     Player_GetCollisionType_Hook
    nop

; Replaces:
;   lw      v0, 0x2B0C (v0) ; relocated
; With:
;   lw      a2, 0x2B0C (v0) ; relocated
.org 0x8083D8D0
    .dh 0x8C46

;==================================================================================================
; Handle chest cutscene
;==================================================================================================

; Replaces:
;   LW      V1, 0x0024 (SP)
;   ADDIU   AT, R0, 0x00FF
;   LBU     A0, 0x0000 (V1)
;   BEQ     A0, AT, 0x8083D5B4
;   LW      T2, 0x002C (SP)
;   LB      T5, 0x0002 (V1)
;   BLTZ    T5, 0x8083D5B4
;   NOP
;   JAL     0x80114978
;   NOP
;   ADDIU   AT, R0, 0x00FF
;   BNE     V0, AT, 0x8083D5B4
.org 0x8083D534
    lw      a0, 0x002C (sp) ; chest
    lw      a1, 0x0034 (sp) ; GlobalContext
    jal     Chest_IsLongOpening
    lw      a2, 0x0024 (sp) ; GetItemEntry
    beqz    v0, 0x8083D5B4
    nop
    nop
    nop
    nop
    nop
    nop
    nop

;==================================================================================================
; Override transformation behavior
;==================================================================================================

; Replaces:
;   BNEZL   T7, 0x80831BD8
.org 0x80831BBC
    bnez    t7, 0x80831BD4
    nop


; Replaces:
;   ADDIU   A1, R0, 0x0005
;   SB      S1, 0x014A (S0)
.org 0x80831BD4
    jal     Player_StartTransformation_Hook
    lw      a0, 0x0068 (sp) ; GlobalContext

; Replaces:
;   SB      A1, 0x0AA5 (S0)
.org 0x80831BE0
    nop

;==================================================================================================
; Skip post-transformation init code if instant transformation is enabled
;==================================================================================================

; Replaces:
;   LBU     V0, 0x0394 (S0)
;   ADDIU   AT, R0, 0x0009
;   OR      A0, S1, R0
;   BEQ     V0, AT, 0x80841E98
;   OR      A1, S0, R0
.org 0x80841E78
    jal     Player_AfterTransformInit_Hook
    or      a0, s0, r0
    bnez    v1, 0x808424FC
    addiu   at, r0, 0x0009
    beq     v0, at, 0x80841E98

;==================================================================================================
; Handle pulling out an item
;==================================================================================================

; Replaces:
;   LW      T7, 0x0A74 (S0)
;   LUI     AT, 0x4000
;   SB      V0, 0x0148 (S0)
;   OR      T8, T7, AT
;   B       0x80831F20
;   SW      T8, 0x0A74 (S0)
.org 0x80831EA0
    lw      a0, 0x0068 (sp)
    or      a1, s0, r0
    or      a2, v0, r0
    jal     Player_UseHeldItem
    or      a3, s1, r0
    b       0x80831F20
;   OR      A0, S0, R0

;==================================================================================================
; Fix C-Button priority when holding bow and bomb buttons
;==================================================================================================

; Replaces:
;   LUI     T8, 0x8086
;   LW      T8, 0x2B44 (T8)
;   LUI     V0, 0x8086
;   ADDIU   V0, V0, 0xCFA8
;   OR      A3, R0, R0
;   LHU     V1, 0x0000 (T8)
;   LHU     T9, 0x0000 (V0)
;   NOR     T2, T9, R0
;   NOR     T3, T2, V1
;   BEQZL   T3, 0x8083016C
.org 0x806ED5C0 + 0x142B70
    lui     a2, 0x8086
    lw      a2, 0x2B44 (a2)
    lui     a3, 0x8086
    addiu   a3, a3, 0xCFA8
    lhu     a2, 0x0000 (a2)
    lw      a1, 0x0054 (sp)
    jal     Player_CheckHeldItem
    or      a0, s0, r0
    b       0x808302BC
    lw      ra, 0x0034 (sp)

;==================================================================================================
; Prevent Keg ammo being depleted when going through a loading zone while holding a keg
;==================================================================================================

; Replaces:
;   JAL     Inventory_ChangeAmmo
.org 0x806ECC44 + 0x142B70
    jal     Player_UseExplosiveAmmo

;==================================================================================================
; Stop player action chain if doing an instant transformation
;==================================================================================================

; Replaces:
;   JAL     0x80838A90
.org 0x806F69BC + 0x142B70
    jal     Player_HandleCutsceneItem

; Fix relocations.
; Replaces:
;   .dw 0x4400BA9C
.org 0x8082DA90 + 0x31B24
    .dw 0x00000000

; Replaces:
;   SW      A1, 0x003C (SP)
;   LBU     T6, 0x0AA5 (S0)
;   OR      V0, R0, R0
;   BEQZ    T6, 0x808391C4
;   NOP
.org 0x80838AA0
    jal     Player_BeforeActionChange_13
    sw      a1, 0x003C (sp)
    bltzl   v0, 0x808391C4
    or      v0, r0, r0
    bgtz    v0, 0x808391C4

;==================================================================================================
; Handle Giant Mask transformation states
;==================================================================================================

; Replaces:
;   LW      A0, 0x001C (SP)
;   LUI     AT, 0x2000
;   ORI     AT, AT, 0x0100
;   LW      T6, 0x0A6C (A0)
;   OR      T7, T6, AT
;   JAL     0x8082DAD4
;   SW      T7, 0x0A6C (A0)
.org 0x808389F4
    lw      a0, 0x0018 (sp)
    jal     Player_SetGiantMaskTransformationState
    lw      a1, 0x001C (sp)
    nop
    nop
    jal     0x8082DAD4
    lw      a0, 0x001C (sp)

; Replaces:
;   LW      A0, 0x001C (SP)
;   LUI     AT, 0x2000
;   ORI     AT, AT, 0x0100
;   LW      T6, 0x0A6C (A0)
;   SB      R0, 0x0153 (A0)
;   OR      T7, T6, AT
;   JAL     0x8082DAD4
;   SW      T7, 0x0A6C (A0)
.org 0x80838A58
    lw      a0, 0x0018 (sp)
    jal     Player_SetGiantMaskTransformationState
    lw      a1, 0x001C (sp)
    nop
    nop
    lw      a0, 0x001C (sp)
    jal     0x8082DAD4
    sb      r0, 0x0153 (a0)


;==================================================================================================
; Handle Giant Mask speed
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   LBU     T2, 0x014B (A1)
;   LUI     AT, 0x3FC0
;   BNEZ    T2, 0x80832E08
;   NOP
;   LWC1    F6, 0x0000 (A2)
;   MTC1    AT, F8
;   NOP
;   MUL.S   F10, F6, F8
;   SWC1    F10, 0x0000 (A2)
.org 0x80832DE4
    jal     Player_HandleFormSpeed_Hook
    nop
    mtc1    r0, f16
    nop
    nop
    nop
    nop
    nop
    nop

;==================================================================================================
; Handle Giant Mask wall collision
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   LUI     A2, 0x41D6
;   MFC1    A3, F0
;   ORI     A2, A2, 0x6667
.org 0x80843360
    jal     Player_GetWallCollisionHeight_Hook
    nop
    lw      a3, 0x00BC (sp)

;==================================================================================================
; Handle Giant Mask swim depth
;==================================================================================================

; Replaces:
;   ADDIU   SP, SP, -0x08
.org 0x808475B8
    addiu   sp, sp, -0x18

; Replaces:
;   SWC1    F8, 0x0004 (SP)
.org 0x80847674
    swc1    f8, 0x0010 (sp)

; Replaces:
;   SWC1    F4, 0x0004 (SP)
.org 0x808476B8
    swc1    f4, 0x0010 (sp)

; Replaces:
;   SWC1    F6, 0x0004 (SP)
.org 0x808476E8
    swc1    f6, 0x0010 (sp)

; Replaces
;   SWC1    F4, 0x0004 (SP)
;   LUI     AT, 0x42C8
;   MTC1    AT, F6
;   NOP
;   C.LT.S  F6, F14
.org 0x80847764
    swc1    f4, 0x0010 (sp)
    sw      ra, 0x0014 (sp)
    jal     Player_GetDiveDepth_Hook
    nop
    lw      ra, 0x0014 (sp)

; Replaces:
;   LWC1    F8, 0x0004 (SP)
.org 0x80847780
    lwc1    f8, 0x0010 (sp)

; Replaces:
;   LWC1    F8, 0x0004 (SP)
.org 0x80847794
    lwc1    f8, 0x0010 (sp)

; Replaces:
;   LWC1    F8, 0x0004 (SP)
.org 0x808477A4
    lwc1    f8, 0x0010 (sp)

; Replaces:
;   ADDIU   SP, SP, 0x08
.org 0x808477CC
    addiu   sp, sp, 0x18

;==================================================================================================
; Handle Giant Mask ledge climb
;==================================================================================================

; Replaces:
;   LBU     T1, 0x014B (S0)
;   LUI     AT, 0x4248
;   BNEZL   T1, 0x80834EC4
;   MTC1    AT, F0
;   LUI     AT, 0x42A0
;   MTC1    AT, F0
;   B       0x80834ECC
;   LWC1    F8, 0x008C (S0)
;   MTC1    AT, F0
;   NOP
.org 0x80834EA0
    jal     Player_GetWaterSurfaceDistanceClimbHeight
    or      a0, s0, r0
    lw      v1, 0x005C (sp)
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Replaces:
;   LW      T0, 0x0A68 (S0)
;   LUI     AT, 0x4270
;   MTC1    AT, F18
.org 0x808350D8
    jal     Player_GetLedgeClimbFactorFromSwim_Hook
    nop
    lw      t0, 0x0A68 (s0)

; Replaces:
;   LUI     AT, 0x4224
;   LWC1    F8, 0x0018 (V0)
.org 0x80835118
    jal     Player_GetLedgeClimbFactor_Hook
    nop

; Replaces:
;   LUI     AT, 0x426C
;   MTC1    AT, F10
.org 0x80835130
    jal     Player_GetLedgeClimbFactor2_Hook
    nop

; Replaces:
;   LUI     AT, 0x42C8
;   MTC1    AT, F16
.org 0x80835170
    jal     Player_GetInvertedLedgeClimbFactor_Hook
    nop

; Replaces:
;   LUI     AT, 0x40B0
;   MTC1    AT, F8
.org 0x808352CC
    jal     Player_ModifyLedgeJumpWallHeight_Hook
    nop

; Replaces:
;   ADDIU   AT, R0, 0x0004
;   BNE     T3, AT, .+0x14
;   LUI     AT, 0x3F80
;   MTC1    AT, F10
;   NOP
;   ADD.S   F0, F0, F10
.org 0x8084D5B0
    or      a0, s0, r0
    jal     Player_ModifyWallJumpSpeed_Hook
    mfc1    a1, f0
    lw      a0, 0x0044 (sp)
    or      a1, s0, r0
    or      a2, r0, r0

; Replaces:
;   LUI     AT, 0x4090
;   MTC1    AT, F4
;   LUI     A3, 0x4040
.org 0x808397D0
    jal     Player_GetMidAirJumpSlashHeight_Hook
    nop
    MFC1    A3, F0

; Replaces:
;   LB      T6, 0x0145 (S0)
;   ADDIU   V0, R0, 0x0002
;   OR      A1, S0, R0
;   BNE     V0, T6, 0x806F6D58
;   SLL     T7, S1, 2
;   LUI     AT, 0x3F00
;   MTC1    AT, F4
;   NOP
;   MUL.S   F0, F0, F4
;   NOP
; Replaces:
;   MTC1    A1, F12
;   ANDI    A1, A2, 0xFFFF
;   SW      RA, 0x0014 (SP)
;   SW      A2, 0x0020 (SP)
;   LUI     AT, hi(0x8085C3E4)
;   LWC1    F4, lo(0x8085C3E4) (AT)
;   LHU     T6, 0x0090 (A0)
;   MUL.S   F6, F12, F4
;   ANDI    T7, T7, 0xFFFE
;   SH      T7, 0x0090 (A0)
;   BEQZ    A1, .+0x24
;   SWC1    F6, 0x0068 (A0)
.org 0x80834CD4
    sw      ra, 0x0014 (sp)
    lhu     t6, 0x0090 (a0)
    andi    t7, t6, 0xFFFE
    sh      t7, 0x0090 (a0)
    lui     at, hi(0x8085C3E4)
    lwc1    f4, lo(0x8085C3E4) (at)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)
    jal     Player_SetVelocityY
    sw      a2, 0x0020 (sp)
    lhu     a1, 0x0022 (sp)
    beqz    a1, .+0x20


; Replaces:
;   LW      T2, 0x0A70 (S0)
;   LUI     AT, 0x0008
;   OR      A0, S0, R0
.org 0x80839934
    jal     Player_ModifyJumpVelocity_Hook
    or      a0, s0, r0
    lui     at, 0x0008

; Replaces:
;   LW      A0, 0x0034 (SP)
;   BEQ     T9, AT, 0x80839C88
;   LUI     AT, 0x40A0
;   MTC1    AT, F0
.org 0x80839C5C
    beq     t9, at, 0x80839C88
    lw      a0, 0x0034 (sp)
    jal     Player_GetJumpSlashHeight_Hook
    nop

; Replaces:
;   LUI     AT, 0xC1A0
;   MTC1    AT, F4
.org 0x8083BF6C
    jal     Player_GetNewMinVelocityY
    nop

; Replaces:
;   SWC1    F4, 0x0078 (S0)
.org 0x8083BF7C
    swc1    f0, 0x0078 (s0)

; Replaces:
;   LUI     AT, 0x3FC0
;   MTC1    AT, F16
;   MTC1    T6, F4
;   MTC1    A1, F12
;   SWC1    F16, 0x0010 (SP)
;   CVT.S.W F6, F4
;   LH      T7, 0x004A (V0)
;   SW      T7, 0x0014 (SP)
;   DIV.S   F10, F6, F8
;   MFC1    A3, F10
.org 0x8083CB84
    mtc1    t6, f4
    mtc1    a1, f12
    cvt.s.w f6, f4
    lh      t7, 0x004A (v0)
    sw      t7, 0x0014 (sp)
    div.s   f10, f6, f8
    mfc1    a3, f10
    jal     Player_GetRunDeceleration_Hook
    nop
    swc1    f0, 0x0010 (sp)

; Backwalk

; Replaces:
;   JAL     0x800FF2F8
.org 0x8084A708
    jal     Player_HandleInputVelocity

; Sidewalk

; Replaces:
;   JAL     0x800FF2F8
.org 0x8084AAF8
    jal     Player_HandleInputVelocity

; Bonk Speed check

; Replaces:
;   LWC1    F4, 0x0000 (A2)
;   OR      A3, R0, R0
;   C.LE.S  F12, F4
;   NOP
;   BC1FL   0x80840CC0
.org 0x80840A48
    jal     Player_IsAboveBonkThreshold
    nop
    nop
    or      a3, r0, r0
    beqz    v0, 0x80840CC0

; After bonk backwards linear velocity

; Replaces:
;   JAL     0x800FF03C ; Math_StepToF
.org 0x8084C7D0
    jal     GiantMask_Math_StepToF

; Grotto spawn linear velocity

; Replaces:
;   OR      A3, A0, R0
;   LUI     AT, 0xBF80
;   MTC1    AT, F4
;   ADDIU   A1, A3, 0x0240
;   SWC1    F4, 0x0074 (A3)
;   SW      A3, 0x0018 (SP)
.org 0x808543A8
    jal     GiantMask_GetSimpleScaleModifier
    sw      a0, 0x0018 (sp)
    lw      a3, 0x0018 (sp)
    neg.s   f0, f0
    swc1    f0, 0x0074 (a3)
    addiu   a1, a3, 0x0240

; Replaces:
;   LW      A3, 0x0018 (SP)
;   MTC1    R0, F6
;   LUI     AT, 0x40C0
;   LWC1    F0, 0x0068 (A3)
;   OR      A0, A3, R0
;   C.LT.S  F0, F6
;   NOP
;   BC1FL   .+0x1C
;   MTC1    AT, F8
.org 0x808543C8
    lui     at, 0x40C0
    jal     GiantMask_GetScaledFloat
    mtc1    at, f12
    mtc1    r0, f6
    lwc1    f8, 0x0068 (s0)
    c.lt.s  f8, f6
    nop
    bc1f    .+0x1C
    or      a0, s0, r0

; Replaces:
;   MTC1    AT, F8
;   ADDIU   A0, A3, 0x0AD0
;   LUI     A1, 0x4040
;   C.LT.S  F0, F8
.org 0x808543FC
    nop
    addiu   a0, s0, 0x0AD0
    lui     a1, 0x4040
    c.lt.s  f8, f0

; Replaces:
;   JAL     0x800FF03C ; Math_StepToF
.org 0x80854418
    jal     GiantMask_Math_StepToF

; Goron Rolling

; Replaces:
;   LUI     AT, 0x8086
;   MUL.S   F6, F4, F4
;   LWC1    F4, 0x0094 (SP)
;   ADD.S   F0, F10, F6
;   BEQZ    T9, 0x808586CC
;   SQRT.S  F14, F0
.org 0x80858640
    mul.s   f6, f4, f4
    add.s   f0, f10, f6
    sqrt.s  f14, f0
    swc1    f14, 0x00B8 (sp)
    beqz    t9, 0x808586CC
    nop

; Replaces:
;   BNEZL   AT, 0x80858740
.org 0x8085868C
    bnezl   at, 0x808586E0

; Replaces:
;   B       0x8085873C
;   LWC1    F14, 0x00B8 (SP)
;   LWC1    F8, 0xE6DC (AT)
;   LWC1    F6, 0x0038 (SP)
;   LUI     AT, 0x8086
;   MUL.S   F10, F8, F4
;   LWC1    F4, 0x009C (SP)
;   LWC1    F8, 0xE6E0 (AT)
;   LUI     AT, 0x40C0
;   ADD.S   F16, F10, F6
;   MUL.S   F10, F8, F4
;   LWC1    F6, 0x0034 (SP)
;   MUL.S   F8, F16, F16
;   ADD.S   F18, F10, F6
;   MUL.S   F4, F18, F18
;   ADD.S   F0, F8, F4
;   SQRT.S  F0, F0
;   C.LT.S  F0, F14
;   NOP
;   BC1TL   .+0x24
;   SWC1    F16, 0x00A4 (SP)
;   MTC1    AT, F10
;   NOP
;   C.LS.T  F0, F10
;   NOP
;   BC1FL   .+0x18
;   MTC1    R0, F12
;   SWC1    F16, 0x00A4 (SP)
;   SWC1    F18, 0x00A0 (SP)
;   MOV.S   F14, F0
;   MTC1    R0, F12
;   LUI     AT, 0x8086
;   C.EQ.S  F14, F12
;   NOP
;   BC1T    0x80858828
;   NOP
;   LWC1    F2, 0xE6E4 (AT)
.org 0x808586C4
.area 0x94, 0
    b       .+0x1C
    nop
    addiu   a0, sp, 0x94 ; &slopeNormal
    addiu   a1, sp, 0xA4 ; &deltaMovementX
    addiu   a2, sp, 0xA0 ; &deltaMovementZ
    jal     Player_HandleGoronRollSlopeAdjustment
    addiu   a3, sp, 0xB8 ; &deltaMovementXZ
    jal     Player_GetGoronRollAutoRollThreshold
    nop
    mov.s   f2, f0
    lwc1    f14, 0x00B8 (sp)
    mtc1    r0, f12
    c.eq.s  f14, f12
    nop
    bc1t    0x80858828
.endarea

; Fix relocations.
; Replaces:
;   .dw 0x4502ABB0
;   .dw 0x4602AC3C
;   .dw 0x4502AC44
;   .dw 0x4602AC50
;   .dw 0x4502ACB0
;   .dw 0x4602ACC4
.org 0x8082DA90 + 0x34770
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000

; Replaces:
;   LUI     AT, 0x4448
;   BNEZ    T4, 0x80858828
;   NOP
;   MTC1    AT, F6
.org 0x808587B0
    bnez    t4, 0x80858828
    nop
    jal     Player_GetGoronRollSoundFactor_Hook
    nop

; Replaces:
;   LUI     AT, 0x4140
;   ANDI    T6, T5, 0x0008
;   BEQZL   T6, 0x80857DCC
;   LW      T8, 0x0A70 (S0)
;   LWC1    F0, 0x0B08 (S0)
;   MTC1    AT, F10
;   ORI     A3, R0, 0x8000
;   LUI     AT, hi(0x8085E6BC)
;   C.LE.S  F10, F0
;   NOP
;   BC1FL   0x80857DCC
.org 0x80857D04
    andi    t6, t5, 0x0008
    beqzl   t6, 0x80857DCC
    lw      t8, 0x0A70 (s0)
    jal     Player_GetGoronRollReboundSpeedThreshold
    nop
    lwc1    f10, 0x0B08 (s0)
    ori     a3, r0, 0x8000
    lui     at, hi(0x8085E6BC)
    c.le.s  f0, f10
    nop
    bc1fl   0x80857DCC

; Replaces:
;   MUL.S   F8, F0, F6
.org 0x80857D6C
    mul.s   f8, f10, f6

; Replaces:
;   LW      A2, 0x001C (SP)
;   LUI     AT, 0x41F0
;   MTC1    AT, F4
;   LUI     AT, 0x43FA
;   MTC1    AT, F6
;   LWC1    F0, 0x0AD0 (A2)
.org 0x80857A70
    jal     Player_GetGoronInitialRollRotatationMultiplier
    lw      a0, 0x001C (sp)
    lw      a2, 0x001C (sp)
    lui     at, 0x41F0
    mtc1    at, f4
    lwc1    f6, 0x0AD0 (a2)

; Replaces:
;   SWC1    F0, 0x0B08 (A2)
.org 0x80857AC4
    swc1    f6, 0x0B08 (a2)

; Replaces:
;   MTC1    AT, F10
.org 0x80857E48
    jal     Player_GetGoronMaxSpikeRoll_Hook; Into F10

; Replaces:
;   LH      A1, 0x00DE (SP)
.org 0x80858B20
    lw      a1, 0x00DC (sp)

; Replaces:
;   JAL     Math_AsymStepToS
.org 0x80858B3C
    jal     Player_StepGoronRollRotation

; Replaces:
;   JAL     0x800FF2F8
.org 0x80857F8C
    jal     Player_HandleInputVelocity

; Replaces:
;   JAL     0x800FF2F8
.org 0x8085847C
    jal     Player_HandleInputVelocity

; Replaces:
;   JAL     0x800FF2F8
.org 0x80850C3C
    jal     Player_HandleZoraSwimInputVelocity

; Zora initial swim speed
; Replaces:
;   MTC1    AT, F10
;   NOP
;   SWC1    F10, 0x0B48 (S0)
.org 0x80850F38
    jal     GiantMask_GetScaledFloat
    mtc1    at, f12
    swc1    f0, 0x0B48 (s0)

; Zora dive max speed
; Replaces:
;   LUI     AT, 0x4158
;   MTC1    AT, F0
.org 0x8083B600
    jal     Player_GetZoraDiveMaxSpeed
    nop

; Replaces:
;   LHU     T9, 0x0090 (S0)
;   LUI     AT, 0xBF80
;   ANDI    T0, T9, 0x0001
;   BEQZL   T0, 0x8084CB20
;   MTC1    AT, R6
.org 0x8084CAA4
    jal     Player_GetZoraDiveGravity
    nop
    lhu     t9, 0x0090 (s0)
    andi    t0, t9, 0x0001
    beqz    t0, 0x8084CB1C

; Replaces:
;   MTC1    AT, F6
.org 0x8084CB1C
    mov.s   f6, f0



; Replaces:
;   MTC1    AT, F10
;   LWC1    F8, 0x00E4 (SP)
.org 0x80857F38
    jal     Player_ModifyGoronRollMultiplier_Hook
    mtc1    at, f12

; Replaces:
;   LWC1    F4, 0x00BC (SP)
;   LWC1    F6, 0x00AC (SP)
;   LWC1    F8, 0x00A0 (SP)
;   MUL.S   F10, F0, F4
;   LWC1    F4, 0x00A4 (SP)
;   LUI     AT, 0x4190
;   MTC1    AT, F16
.org 0x80858844
    jal     Player_GetGoronMaxRoll_Hook ; Into F16
    nop
    lwc1    f4, 0x00BC (sp)
    lwc1    f6, 0x00AC (sp)
    lwc1    f8, 0x00A0 (sp)
    mul.s   f10, f0, f4
    lwc1    f4, 0x00A4 (sp)

; Replaces:
;   LUI     AT, 0x4190
;   MTC1    AT, F4
;   LUI     AT, 0x4190
;   C.LT.S  F4, F12
;   SWC1    F12, 0x0B08 (S0)
;   BC1FL   0x80858A30
;   LWC1    F8, 0x0B08 (S0)
;   MTC1    AT, F10
;   B       0x80858AF8
;   SWC1    F10, 0x0B08 (S0)
;   LWC1    F8, 0x0B08 (S0)
;   B       0x80858AF8
;   SWC1    F8, 0x0B08 (S0)
.org 0x80858A1C
    jal     Player_GetGoronMaxRoll
    swc1    f12, 0x0B08 (s0)
    lwc1    f12, 0x0B08 (s0)
    c.lt.s  f0, f12
    nop
    bc1tl   0x80858A38
    swc1    f0, 0x0B08 (s0)
    b       0x80858AF8
    nop
    nop
    nop
    nop
    nop

; Goron Roll Turning Circle

; Replaces:
;   LWC1    F0, 0x0070 (S0)
;   LUI     AT, 0x41A0
;   MTC1    AT, F4
;   ABS.S   F0, F0
.org 0x80858484
    jal     Player_GetGoronRollYawStepMultiplier
    or      a0, s0, r0
    lwc1    f4, 0x0070 (s0)
    abs.s   F4, F4

; Replaces:
;   ADDIU   T9, R0, 0x00C8
;   SW      T9, 0x0018 (SP)
;   LH      A2, 0x0042 (SP)
;   LUI     A3, 0x3F80
 .org 0x8084C374
    jal     Player_GetMidAirAcceleration_Hook
    nop
    sw      t9, 0x0018 (sp)
    lh      a2, 0x0042 (sp)

; Replaces:
;   MTC1    AT, F12
.org 0x8084C564
    jal     Player_GetLedgeGrabDistance_Hook

; Replaces:
;   ADDIU   A1, S0, 0x0240
;   JAL     0x801360E0
.org 0x8084CBBC
    or      a1, s0, r0
    jal     Player_AfterJumpSlashGravity_Hook

; Replaces:
;   LUI     AT, 0x3FC0
;   MTC1    AT, F14
.org 0x8084CEC0
    jal     Player_GetSpinChargeWalkSpeedFactor_Hook
    nop

; Replaces:
;   MTC1    AT, F14
;   NOP
.org 0x8084D248
    jal     Player_GetSpinChargeWalkSpeedFactor_Hook
    nop

; Replaces:
;   LUI     AT, 0xC000
;   MTC1    AT, F10
;   NOP
;   SWC1    F10, 0x0068 (S0)
.org 0x808517CC
    nop
    jal     Player_GetDiveSpeed
    nop
    swc1    f0, 0x0068 (s0)

; Replaces:
;   LUI     AT, 0x4248
;   MTC1    AT, F8
.org 0x8084F6D8
    jal     Player_GetClimbDelta_Hook
    nop

; Replaces:
;   LUI     A2, 0x41D6
.org 0x8084F6EC
    mfc1    a2, f0

; Replaces:
;   ORI     A2, A2, 0x6667
.org 0x8084F6F4
    nop

;==================================================================================================
; Handle Giant Mask transformation height check
;==================================================================================================

; Replaces:
;   JAL     0x800C4F84
.org 0x80831B80
    jal     Player_UseItem_CheckCeiling_Hook

;==================================================================================================
; Handle Giant Mask collision cylinder
;==================================================================================================

; Replaces:
;   JR      RA
.org 0x8082F0DC
    j       Player_AfterUpdateCollisionCylinder

; Replaces:
;   JR      RA
.org 0x8082F128
    j       Player_AfterUpdateCollisionCylinder

; Replaces:
;   JR      RA
.org 0x8082F14C
    j       Player_AfterUpdateCollisionCylinder

; Replaces:
;   JR      RA
.org 0x8082F15C
    j       Player_AfterUpdateCollisionCylinder

;==================================================================================================
; Handle on-load mask check
;==================================================================================================

; Replaces
;   BNE     V1, AT, .+0x28
.org 0x80841C78
    nop

; Replaces:
;   LBU     V0, 0xF674 (V0)
;   ADDIU   AT, R0, 0x0014
;   BNE     V0, AT, 0x80841C98
;   LUI     AT, 0x801F
;   SB      R0, 0xF674 (AT)
;   OR      V0, R0, R0
.org 0x80841C80
    or      a0, s0, r0
    jal     Player_GetMaskOnLoad
    or      a1, s1, r0
    nop
    nop
    nop

;==================================================================================================
; Handle item use while swimming check
;==================================================================================================

; Replaces:
;   ADDIU   AT, R0, 0x0050
;   SLL     T0, T9, 4
;   BGEZL   T0, 0x80831A5C
;   XORI    A2, A2, 0x0001
;   BEQL    S1, AT, 0x80831A5C
;   XORI    A2, A2, 0x0001
;   LB      T1, 0x0145 (S0)
;   SLTI    AT, T1, 0x0005
.org 0x80831A24
    sll     t0, t9, 4
    bgezl   t0, 0x80831A5C
    xori    a2, a2, 0x0001
    or      a0, s0, r0
    jal     Player_ShouldCheckItemUsabilityWhileSwimming_Hook
    or      a1, s1, r0
    beqzl   v0, 0x80831A5C
    xori    a2, a2, 0x0001

;==================================================================================================
; After getting crushed
;==================================================================================================

; Replaces:
;   LUI     V0, 0x801F
;   ADDIU   V0, V0, 0xF670
;   LUI     T6, 0x801C
;   LBU     T6, 0x20AA (T6)
;   ADDIU   AT, R0, 0x0032
;   ADDIU   T9, R0, 0x0004
;   ADDU    T7, V0, T6
;   LBU     T8, 0x0070 (T7)
;   BEQ     T8, AT, .+0x10
;   NOP
;   SB      T9, 0x0020 (V0)
;   SB      R0, 0x0004 (V0)
;   JR      RA
.org 0x808345C8
.area 0x34, 0
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     Player_AfterCrushed
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
.endarea

;==================================================================================================
; Player's weapon damage info
;==================================================================================================

; Replaces:
;   LW      A2, 0x0000 (V0)
.org 0x80833820
    jal     Player_GetWeaponDamageFlags_Hook

; Replaces:
;   LW      V0, 0x0024 (SP)
.org 0x80833838
    nop

; Replaces:
;   LW      A2, 0x0000 (V0)
.org 0x8083384C
    lw      a2, 0x0024 (sp)

;==================================================================================================
; Player check if should be knocked over
;==================================================================================================

; Replaces:
;   BEQ     V1, AT, 0x80833DB8
;   LW      A0, 0x0030 (SP)
;   ADDIU   AT, R0, 0x0002
;   BEQ     V1, AT, 0x80833DB8
;   NOP
;   LHU     T8, 0x0090 (S0)
;   LUI     AT, 0x0020
;   ORI     AT, AT, 0x6004
;   ANDI    T9, T8, 0x0001
;   BEQZ    T9, 0x80833DB8
;   AND     T0, V0, AT
;   BEQZ    T0, 0x80833ED0
;   LUI     AT, 0x4080
.org 0x80833D84
.area 0x34, 0
    lw      a0, 0x0030 (sp)
    or      a1, s0, r0
    jal     Player_ShouldBeKnockedOver
    or      a2, v1, r0
    beqz    v0, 0x80833ED0
    lui     at, 0x4080
    lw      a0, 0x0030 (sp)
.endarea

;==================================================================================================
; Player additional checks if can be grabbed
;==================================================================================================

; Replaces:
;   JAL     Player_InBlockingCsMode
.org 0x8085B200
    jal     Player_CantBeGrabbed

;==================================================================================================
; Player goron pound gravity
;==================================================================================================

; Replaces:
;   C.LT.S  F4, F12
;   LUI     AT, 0xC120
;   BC1FL   .+0x1C
;   MTC1    AT, F10
;   LUI     AT, hi(0x8085E6EC)
;   LWC1    F6, lo(0x8085E6EC) (AT)
;   B       .+0x18
;   SWC1    F6, 0x0074 (S0)
;   MTC1    AT, F10
;   ADDIU   T6, R0, 0x0001
;   SB      T6, 0x03D0 (S0)
;   SWC1    F10, 0x0074 (S0)
.org 0x80858AC0
.area 0x30, 0
    nop
    nop
    nop
    nop
    lui     a1, hi(0x8085E6EC)
    addiu   a1, a1, lo(0x8085E6EC)
    jal     Player_SetGoronPoundGravity
    or      a0, s0, r0
.endarea

;==================================================================================================
; Player goron pound and punch crater spawn
;==================================================================================================

; Replaces:
;   JAL     Actor_Spawn
.org 0x80840470
    jal     Player_SpawnCrater

; Replaces:
;   JAL     Actor_Spawn
.org 0x80857BB8
    jal     Player_SpawnCrater

;==================================================================================================
; Player limb rotation
;==================================================================================================

; Replaces:
;   LWC1    F0, 0x0AD0 (S0)
;   MTC1    AT, F4
.org 0x8083C91C
    jal     Player_GetLinearVelocityForLimbRotation_Hook
    or      a0, s0, r0

;==================================================================================================
; Fix Sakon's Hideout global void respawn
;==================================================================================================

; Replaces
;   JAL     0x800C9E88 ; SurfaceType_IsWallDamage
.org 0x80835AD4
    jal     Player_ShouldNotSetGlobalVoidFlag

; Replaces
;   JAL     0x800C9E88 ; SurfaceType_IsWallDamage
.org 0x80854300
    jal     Player_ShouldNotSetGlobalVoidFlag

;==================================================================================================
; Iron Goron
;==================================================================================================

; Replaces:
;   ADDIU   A0, R0, 0x0020
.org 0x8083BB68
    nop

; Replaces:
;   OR      A0, R0, R0
.org 0x8083BB90
    nop

; Replaces:
;   JAL     0x801A3E38
.org 0x8083BBAC
    jal     Player_HandleIronGoronLand

; Replaces:
;   JAL     0x801A3E38
;   SWC1    F0, 0x001C (SP)
;   LW      A1, 0x0024 (SP)
;   ADDIU   AT, R0, 0x0002
;   LWC1    F0, 0x001C (SP)
;   LBU     T1, 0x014B (A1)
;   BEQ     T1, AT, .+0x20
.org 0x8083BBC0
    jal     Player_ShouldResetUnderwaterTimer
    swc1    f0, 0x001C (sp)
    bnez    v0, .+0x30
    lw      a1, 0x0024 (sp)
    lwc1    f0, 0x001C (sp)
    nop
    nop

; Replaces:
;   LBU     V1, 0x014B (A1)
;   ADDIU   AT, R0, 0x0001
;   LW      A0, 0x0020 (SP)
;   BNE     V1, AT, .+0x30
;   LUI     A2, 0x0401
;   ADDIU   A2, A2, 0xDFE8
.org 0x8083BC84
    jal     Player_HandleGoronInWater_Hook
    lw      a0, 0x0020 (sp)
    bgtzl   at, .+0x2BC
    lw      ra, 0x0014 (sp)
    bltz    at, .+0x2C
    lbu     v1, 0x014B (A1)

; Replaces:
;   ADDIU   AT, R0, 0x0005
;   BNEL    T5, AT, .+0x60
.org 0x8083D2FC
    slti    at, t5, 0x0005
    bnezl   at, .+0x60

; Replaces:
;   JAL     0x800C6248 ; DynaPoly_GetActor
.org 0x8084048C
    jal     Player_GetGoronPunchCollisionActor

;==================================================================================================
; Take Damage on Epona
;==================================================================================================

.org 0x80845648
    bnezl   t8, 0x8084573C
    nop

.org 0x8084565C
    bltzl   t1, 0x8084573C
    nop

.org 0x8084566C
    bnez    t2, 0x8084573C

.org 0x8084567C
    beqzl   v0, 0x8084573C
    nop

.org 0x80845710
    b       0x8084572C

.org 0x8084571C
    lui     a2, 0x0401
    beqzl   t1, .+0xC
    addiu   a2, a2, 0xD698
    addiu   a2, a2, 0xDC28
    jal     0x80831F34 ; TODO relocation
    or      a1, s0, r0
    b       0x80845800
    lw      a3, 0x0074 (sp)
    jal     Player_ShouldSkipParentDamageCheck
    or      a0, s0, r0
    bnez    v0, .+0x14
    nop

; Relocation
.org 0x808606CC
    .dw 0x44017C9C

;==================================================================================================
; Take Damage while shielding
;==================================================================================================

.org 0x808349EC
    jal     Player_GetHittingActor_Hook
    nop
    beqz    v0, 0x80834B24
    or      t5, v0, r0

;==================================================================================================
; Take Damage after minor void
;==================================================================================================

; Replaces:
;   JAL     Player_SetEquipmentData
.org 0x808498B8
    jal     Player_OnMinorVoid

; Replaces:
;   JAL     z2_PerformEnterWaterEffects
.org 0x8083BDF8
    jal     Player_OnDekuWaterVoid

; Fix relocations.
; Replaces:
;   .dw 0x4400E368
.org 0x8085FA04
    .dw 0x00000000

; Replaces:
;   JAL     Audio_PlaySfx_2
.org 0x8083584C
    jal     Player_VoidExit

; Replaces:
;   JAL     Audio_PlaySfx_2
.org 0x8083589C
    jal     Player_VoidExit

;==================================================================================================
; Player Lib
;==================================================================================================
;==================================================================================================
; Iron Goron Room Transition Boot Data
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   LW      T4, 0x0A6C (A2)
;   SLL     T5, T4, 4
;   BGEZL   T5, .+0x10
.org 0x801231B8
    jal     Player_Lib_IsBootDataSwimming_Hook
    nop
    beqzl   t5, .+0x10
