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
; RAM + 0x142B70
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
    jal     Player_GetLedgeJumpSpeed_Hook
    nop

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
.org 0x808398A0
    sw      v1, 0x0028 (sp)
    jal     Player_ModifyJumpHeight_Hook
    or      a1, s0, r0
    sll     t7, s1, 2
    lw      v1, 0x0028 (sp)
    nop
    nop
    nop
    nop
    nop

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

; Goron Rolling

; Replaces:
;   MTC1    AT, F10
.org 0x80857E48
    jal     Player_GetGoronMaxSpikeRoll_Hook; Into F10

; Replaces:
;   JAL     0x800FF2F8
.org 0x80857F8C
    jal     Player_HandleInputVelocity

; Replaces:
;   JAL     0x800FF2F8
.org 0x8085847C
    jal     Player_HandleInputVelocity

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

; Replaces:
;   LBU     V0, 0xF674 (V0)
;   ADDIU   AT, R0, 0x0014
;   BNE     V0, AT, 0x80841C98
;   LUI     AT, 0x801F
;   SB      R0, 0xF674 (AT)
;   OR      V0, R0, R0
.org 0x80841C80
    jal     Player_GetMaskOnLoad
    or      a0, s0, r0
    nop
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
