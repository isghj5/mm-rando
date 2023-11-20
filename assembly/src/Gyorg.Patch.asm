;==================================================================================================
; Don't grab Giant Link
;==================================================================================================

.headersize G_BOSS_03_DELTA

; Replaces:
;   LHU     T2, 0x0090 (S1)
;   LUI     AT, 0X43DB
;   ANDI    T3, T2, 0x0001
;   BEQZL   T3, .+0x28
;   LH      T4, 0x024C (S0)
;   LWC1    F4, 0x00D8 (S1)
;   MTC1    AT, F6
;   NOP
;   C.LE.S  F6, F4
;   NOP
;   BC1TL   .+0x20
;   LW      T5, 0x0120 (S1)
;   LH      T4, 0x024C (S0)
;   ADDIU   A0, S1, 0x0024
;   LUI     A2, 0x3F80
;   BNEZL   T4, .+0x40
.org 0x809E3F70
.area 0x40
    jal     Gyorg_ShouldStopCatchingPlayer
    or      a0, s0, r0
    bnez    v0, .+0x3C
    lhu     t2, 0x0090 (s1)
    lui     at, 0x43DB
    andi    t3, t2, 0x0001
    addiu   a0, s1, 0x0024
    beqz    t3, .+0x5C
    lui     a2, 0x3F80
    lwc1    f4, 0x00D8 (s1)
    mtc1    at, f6
    nop
    c.le.s  f6, f4
    nop
    bc1f    .+0x40
    nop
.endarea
