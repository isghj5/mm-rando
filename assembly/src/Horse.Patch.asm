;==================================================================================================
; Spawn rupee
;==================================================================================================

.headersize G_EN_HORSE_DELTA

; Replaces:
;   JAL     SurfaceType_IsHorseBlocked
;   LW      A2, 0x0000 (T3)
;   BNEZ    V0, .+0x28
;   LW      T4, 0x0044 (SP)
;   LW      T5, 0x004C (SP)
;   OR      A0, S0, R0
;   LW      A1, 0x0000 (T4)
;   JAL     SurfaceType_GetFloorType
;   LW      A2, 0x0000 (T5)
;   ADDIU   AT, R0, 0x0007
;   BNEL    V0, AT, .+0x14
;   OR      V0, R0, R0
.org G_EN_HORSE_VRAM + 0x9AA0
    lw      a2, 0x0000 (t3)
    jal     EnHorse_IsHorseBlocked
    lw      a3, 0x0038 (sp)
    beqz    v0, .+0x30
    or      v0, r0, r0
    nop
    nop
    nop
    nop
    nop
    nop
    nop
