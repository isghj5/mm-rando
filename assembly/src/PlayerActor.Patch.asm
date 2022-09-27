;==================================================================================================
; Player actor update hooks
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   lui     at, 0x3F00
;   mtc1    at, f4
.org 0x8084C2AC
    jal     DekuHop_GetSpeedModifier_Hook
    nop

;==================================================================================================
; Handle climbing anywhere
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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
    or      a0, at, r0

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
