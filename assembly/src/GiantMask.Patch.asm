;==================================================================================================
; Adjust player height
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   LW      T6, 0x0A6C (A0)
;   LUI     AT, 0x4200
;   SLL     T7, T6, 8
;   BGEZL   T7, .+0x18
;   MTC1    R0, F2
;   MTC1    AT, F2
;   B       .+0x14
.org 0x800B6FC8
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     Camera_PlayerGetHeight
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

; Replaces:
;   LW	    T6, 0x004C (SP)
;   LW	    T8, 0x0048 (SP)
;   LUI	    AT, 0x4248
;   ANDI	T7, T6, 0x0800
;   BEQZL	T7, 0x800B76BC
;   MTC1	AT, F0
;   LUI	    AT, 0x4120
;   MTC1	AT, F0
;   B	    0x800B76C4
;   LWC1	F4, 0x0004 (T8)
;   MTC1	AT, F0
;   NOP
;   LWC1	F4, 0x0004 (T8)
.org 0x800B7690
    jal     GiantMask_GetFloorHeightCheckDelta
    nop
    lw      t8, 0x0048 (sp)
    lwc1    f4, 0x0004 (t8)
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Replaces:
;   LUI	    AT, 0xC130
;   MTC1	AT, F16
.org 0x800B7764
    nop
    jal     GiantMask_GetLedgeWalkOffHeight_Hook

; Giant's Mask Magic Meter consumption timer, set default value to 20 (as per Twinmold's logic) instead of 80

; Replaces:
;   SH      V1, 0x0AE6 (T5)
.org 0x801731A0
    sh      t9, 0x0AE6 (T5)

;==================================================================================================
; Adjust spin attack size
;==================================================================================================

.headersize G_EN_M_THUNDER_DELTA

; Replaces:
;   JAL     0x800FFCD8
.org 0x808B5FDC
    jal     GiantMask_Math_SmoothStepToF

; Replaces:
;   JAL     0x800E7DF8
.org 0x808B601C
    jal     GiantMask_AdjustSpinAttackHeight

;==================================================================================================
; Regular Boulder hit detection
;==================================================================================================

.headersize G_OBJ_BOMBIWA_DELTA

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V1, 0x0024
.org 0x809393EC
    jal     GiantMask_GetHitDistance
    or      a1, v1, r0

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V0, 0x0024
.org 0x809394B0
    jal     GiantMask_GetHitDistance
    or      a1, v0, r0

;==================================================================================================
; Huge Boulder hit detection
;==================================================================================================

.headersize G_OBJ_HUGEBOMBIWA_DELTA

; Replaces:
;   ADDIU   A1, V0, 0x0024
.org 0x80A54A60
    or      a1, v0, r0

; Replaces:
;   JAL     Math3D_Vec3fDistSq
.org 0x80A54A78
    jal     GiantMask_GetHitDistance

;==================================================================================================
; Barrel hit detection
;==================================================================================================

.headersize G_OBJ_TARU_DELTA

; Replaces:
;   ADDIU   A1, V0, 0x0024
.org 0x80B9BFC8
    or      a1, v0, r0

; Replaces:
;   JAL     Math3D_Vec3fDistSq
.org 0x80B9BFDC
    jal     GiantMask_GetHitDistance

;==================================================================================================
; Large Crate hit detection
;==================================================================================================

.headersize G_OBJ_KIBAKO2_DELTA

; Replaces:
;   ADDIU   A1, V0, 0x0024
.org 0x8098EBB4
    or      a1, v0, r0

; Replaces:
;   JAL     Math3D_Vec3fDistSq
.org 0x8098EBC8
    jal     GiantMask_GetHitDistance

;==================================================================================================
; Snowhead Temple Bombable Wall hit detection
;==================================================================================================

.headersize G_BG_HAKUGIN_BOMBWALL_DELTA

; Replaces:
;   ADDIU   A1, V0, 0x0024
;   JAL     Math3D_Vec3fDistSq
.org 0x80ABCB8C
    or      a1, v0, r0
    jal     GiantMask_GetHitDistance

; Replaces:
;   ADDIU   A1, V0, 0x0024
;   JAL     Math3D_Vec3fDistSq
.org 0x80ABCC44
    or      a1, v0, r0
    jal     GiantMask_GetHitDistance

;==================================================================================================
; Ocean Spider House Bombable Wall hit detection
;==================================================================================================

.headersize G_BG_KIN2_BOMBWALL_DELTA

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V0, 0x0024
.org 0x80B6E050
    jal     GiantMask_GetHitDistance
    or      a1, v0, r0

;==================================================================================================
; Stone Tower Temple Bombable Floor and Wall hit detection
;==================================================================================================

.headersize G_BG_IKANA_BOMBWALL_DELTA

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V0, 0x0024
.org 0x80BD4E70
    jal     GiantMask_GetHitDistance
    or      a1, v0, r0

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V0, 0x0024
.org 0x80BD4ED8
    jal     GiantMask_GetHitDistance
    or      a1, v0, r0

;==================================================================================================
; Beneath the Grave Bombable Wall hit detection
;==================================================================================================

.headersize G_BG_HAKA_BOMBWALL_DELTA

; Replaces:
;   JAL     Math3D_Vec3fDistSq
;   ADDIU   A1, V0, 0x0024
.org 0x80BD5E2C
    jal     GiantMask_GetHitDistance
    or      a1, v0, r0

;==================================================================================================
; Big Octo damage flags
;==================================================================================================

.headersize G_EN_BIGOKUTA_DELTA

; Replaces:
;   .dw 0xF7CFC74F
.org 0x80AC4544
    .dw 0xB7CFC74F

; Replaces:
;   .dw 0x000038B0
.org 0x80AC4570
    .dw 0x400038B0
