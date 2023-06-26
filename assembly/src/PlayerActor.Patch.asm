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
; Override transformation behavior
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

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

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   JAL     Inventory_ChangeAmmo
.org 0x806ECC44 + 0x142B70
    jal     Player_UseExplosiveAmmo

;==================================================================================================
; Stop player action chain if doing an instant transformation
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   JAL     0x80838A90
.org 0x806F69BC + 0x142B70
    jal     Player_HandleCutsceneItem

; Fix relocations.
; Replaces:
;   .dw 0x4400BA9C
.org 0x8082DA90 + 0x31B24
    .dw 0x00000000
