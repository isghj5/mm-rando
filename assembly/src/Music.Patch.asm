.headersize G_CODE_DELTA

; Replaces:
;   jr      ra
.org 0x80197D1C
    j       Music_AfterChannelInit_Hook

; Replaces:
;   LB      T1, 0x0004 (A1)
;   LBU     T4, 0x0000 (A0)
;   SLL     T2, T1, 4
;   ANDI    T3, T2, 0x0010
;   ANDI    T5, T4, 0xFFEF
;   OR      T6, T3, T5
;   B       0x801942AC
;   SB      T6, 0x0000 (A0)
.org 0x801941D8
    or      a2, s4, r0
    jal     Music_HandleChannelMute
    or      a3, s0, r0
    nop
    nop
    nop
    b       0x801942AC
    nop
