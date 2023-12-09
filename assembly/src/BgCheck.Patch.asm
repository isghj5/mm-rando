;==================================================================================================
; Adjust player ability to burrow into ground.
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   ADDIU   SP, SP, -0x18
;   SW      RA, 0x0014 (SP)
;   SW      A1, 0x001C (SP)
;   JAL     BgCheck_GetCollisionHeader
;   OR      A1, A2, R0
;   BNEZ    V0, .+0x10
;   LW      RA, 0x0014 (SP)
;   B       .+0x18
;   ADDIU   V0, R0, 0x0001
;   LW      T6, 0x001C (SP)
;   LHU     V1, 0x0004 (T6)
;   ANDI    V1, V1, 0x4000
;   SLTU    V0, R0, V1
;   JR      RA
;   ADDIU   SP, SP, 0x18
.org 0x800C9DDC
.area 0x38, 0
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     SurfaceType_IsBurrowable
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
.endarea
