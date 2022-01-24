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
; Handle Giant Mask speed
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   LBU	T2, 0x014B (A1)
;   LUI	AT, 0x3FC0
;   BNEZ	T2, 0x80832E08
;   NOP
;   LWC1	F6, 0x0000 (A2)
;   MTC1	AT, F8
;   NOP
;   MUL.S	F10, F6, F8
;   SWC1	F10, 0x0000 (A2)
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
;   LUI	    A2, 0x41D6
;   MFC1	A3, F0
;   ORI	    A2, A2, 0x6667
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
    addiu   sp, sp, -0x10

; Replaces
;   LUI	    AT, 0x42C8
;   MTC1	AT, F6
;   NOP
;   C.LT.S  F6, F14
.org 0x80847768
    sw      ra, 0x000C (sp)
    jal     Player_GetDiveDepth_Hook
    nop
    lw      ra, 0x000C (sp)

; Replaces:
;   ADDIU   SP, SP, 0x08
.org 0x808477CC
    addiu   sp, sp, 0x10

;==================================================================================================
; Handle Giant Mask ledge climb
; RAM + 0x142B70
;==================================================================================================

; Replaces:
;   LUI	    AT, 0x4224
;   LWC1	F8, 0x0018 (V0)
.org 0x80835118
    jal     Player_GetLedgeClimbFactor_Hook
    nop

; Replaces:
;   LUI	    AT, 0x426C
;   MTC1	AT, F10
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
;   LUI	    AT, 0x40B0
;   MTC1    AT, F8
.org 0x808352CC
    jal     Player_GetLedgeJumpSpeed_Hook
    nop

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
;   ADDIU	T9, R0, 0x00C8
;   SW	    T9, 0x0018 (SP)
;   LH	    A2, 0x0042 (SP)
;   LUI	    A3, 0x3F80
 .org 0x8084C374
    jal     Player_GetMidAirAcceleration_Hook
    nop
    sw      t9, 0x0018 (sp)
    lh      a2, 0x0042 (sp)

; Replaces:
;   MTC1    AT, F12
.org 0x8084C564
    jal     Player_GetLedgeGrabDistance_Hook
