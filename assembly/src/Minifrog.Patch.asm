;==================================================================================================
; Minifrog - Randomize
;==================================================================================================

.headersize G_EN_MINIFROG_DELTA

; Replaces:
;   LUI     V0, 0x808A
;   LUI     T2, 0x801F
;   SLL     T0, T9, 1
;   ADDU    V0, V0, T0
;   LHU     T3, 0x4D7C (V0)
;   ADDIU   T2, T2, 0xF670
;   SRA     T1, T3, 8
;   ADDU    V1, T1, T2
;   LBU     A0, 0x0EF8 (V1)
;   OR      T4, T3, A0
;   B       0x808A3F08
;   SB      T4, 0x0EF8 (V1)
.org 0x808A3E74
    or      a0, s0, r0
    lw      a1, 0x0024 (sp)
    jal     Minifrog_GiveReward
    or      a2, t9, r0
    b       0x808A3F08
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Fix relocations.
; Replaces:
;   .dw 0x45000804
;   .dw 0x46000814
.org 0x808A4EB8
    .dw 0x00000000
    .dw 0x00000000
