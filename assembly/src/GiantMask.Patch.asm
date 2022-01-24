;==================================================================================================
; Adjust player height
;==================================================================================================

.headersize G_CODE_DELTA

.org 0x800B6FC8
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Camera_PlayerGetExtraHeight
    nop

    mov.s   f2, f0
    nop
    nop
    nop

    lw      ra, 0x0014 (sp)
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
