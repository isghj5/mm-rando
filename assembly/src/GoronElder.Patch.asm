.headersize G_EN_JG_DELTA

; Replaces:
;   LUI     A1, 0x801C
;   ADDIU   A1, A1, 0x1D30
;   ANDI    T9, T9, 0x0010
;   BNEZ    T9, 0x80B7510C
;   NOP
;   LW      V1, 0x00BC (A0)
;   LW      T1, 0x001C (A1)
;   AND     T2, T1, V1
;   BNEZ    T2, 0x80B7510C
;   NOP
;   LW      T3, 0x0060 (A1)
;   AND     T4, T3, V1
;   BEQZ    T4, 0x80B75114
.org 0x80B750D4
    addiu   sp, sp, -0x0018
    sw      ra, 0x0014 (sp)
    jal     GoronElder_HasGivenReward
    andi    a0, t9, 0x0010
    lw      ra, 0x0014 (sp)
    beqz    v0, 0x80B75114
    addiu   sp, sp, 0x0018
    nop
    nop
    nop
    nop
    nop
    nop

; Replaces:
;   ANDI    T5, V0, 0x0040
.org 0x80B7468C
    andi    a0, v0, 0x0040

; Replaces:
;   BNEZ    T5, 0x80B746FC
;   LUI     A1, 0x801C
;   ADDIU   A1, A1, 0x1D30
;   LW      T6, 0x001C (A1)
;   LW      V1, 0x00BC (A0)
;   AND     T7, T6, V1
;   BNEZ    T7, 0x80B74700
;   OR      A0, S0, R0
;   LW      T8, 0x0060 (A1)
;   LUI     AT, 0x0001
;   ADDU    AT, AT, A3
;   AND     T9, T8, V1
;   BEQZ    T9, 0x80B7472C
;   ADDIU   T1, R0, 0x0043
.org 0x80B746C4
    jal     GoronElder_HasGivenReward
    nop
    lw      a3, 0x002C (sp)
    lui     at, 0x0001
    addu    at, at, a3
    beqz    v0, 0x80B7472C
    addiu   t1, r0, 0x0043
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Replaces:
;   LUI     V0, 0x801F
;   ADDIU   T7, R0, 0x0063
;   ADDIU   T8, R0, 0x03E8
;   ADDIU   V0, V0, 0xF670
;   SB      T7, 0x03CB (S0)
;   SH      T8, 0x03A2 (S0)
.org 0x80B74E18
    or      a0, s0, r0
    jal     GoronElder_GiveReward
    lw      a1, 0x005C (sp)
    lui     v0, 0x801F
    addiu   t8, r0, 0x03E8
    addiu   v0, v0, 0xF670
