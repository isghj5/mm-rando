; =========================================================
; File: 0x00F31E90, Address: 0x00F32E84, Offset: 0x00000FF4
; Name: En_Elforg :: Stray Fairy
; =========================================================
.headersize (0x80ACC470 - 0x00F31E90)
; Replaces:
;   bne     t1, at, 0x80ACD4AC
;   addiu   t2, r0, 0x0064
;   lw      t3, 0x0A6C(v0)
;   lui     at, 0x2000
;   sh      t2, 0x0118(v0)
;   or      t4, t3, at
;   sw      t4, 0x0A6C(v0)
;   or      a0, s1, r0
;   addiu   a1, r0, 0x0579
;   jal     0x801518B0
;   or      a2, r0, r0
;   lui     t5, 0x80AD
;   addiu   t5, t5, -0x2E10
;   sw      t5, 0x022C(s0)
;   jal     0x800F1BA4
;   addiu   a0, r0, 0x007C
;   b       0x80ACD58C
;   lw      ra, 0x0024(sp)
;   jal     0x8010A074
;   or      a0, s1, r0
;   beqz    v0, 0x80ACD50C
;   lui     v1, 0x801F
;   addiu   v1, v1, -0x0990
;   lhu     t6, 0x48C8(v1)
;   or      a0, s1, r0
.org 0x80ACD464
    addiu   t2, r0, 0x0064
    lw      t3, 0x0A6C(v0)
    lui     t0, 0x2000
    sh      t2, 0x0118(v0)
    or      t4, t3, t0
    sw      t4, 0x0A6C(v0)
    beql    t1, at, 0x80ACD4A4
    addiu   a0, r0, 0x003B
    addiu   t1, r0, 0x0014
    lui     t2, 0x801F
    lh      t2, 0x3F38(t2)
    mult    t2, t1
    mflo    t2
    andi    a0, a1, 0x001F
    addiu   a0, a0, 0x016D
    addu    a0, t2, a0
    jal     0x801DC710
    or      a1, s1, r0
    lui     t5, 0x80AD
    addiu   t5, t5, -0x2E10
    sw      t5, 0x022C(s0)
    jal     0x800F1BA4
    addiu   a0, r0, 0x007C
    b       0x80ACD50C
    nop

; =========================================================
; File: 0x00F31E90, Address: 0x00F3364A, Offset: 0x000017BA
; Name: En_Elforg :: Stray Fairy
; =========================================================
.headersize (0x80ACC470 - 0x00F31E90)
; Replaces:
;   .dh 0x1020
.org 0x80ACDC2A
    .dh 0x103C

; =========================================================
; File: 0x00F31E90, Address: 0x00F3364E, Offset: 0x000017BE
; Name: En_Elforg :: Stray Fairy
; =========================================================
.headersize (0x80ACC470 - 0x00F31E90)
; Replaces:
;   .dh 0x1024
.org 0x80ACDC2E
    .dh 0x1040

; =========================================================
; File: 0x00F31E90, Address: 0x00F32C9C, Offset: 0x00000E0C
; Name: En_Elforg :: Stray Fairy
; =========================================================
.headersize (0x80ACC470 - 0x00F31E90)
; Replaces:
;   sb      t2, 0x0F00(v0)
.org 0x80ACD27C
    nop

; =========================================================
; File: 0x00F31E90, Address: 0x00F31FE4, Offset: 0x00000154
; Name: En_Elforg :: Stray Fairy
; =========================================================
.headersize (0x80ACC470 - 0x00F31E90)
; Replaces:
;   lbu     t0, 0x0570(t0)
.org 0x80ACC5C4
    addiu   t0, r0, 0x0000

; =========================================================
; File: 0x00B3C000, Address: 0x00BDAA4C, Offset: 0x0009EA4C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sb      r0, 0x00D1(s0)
.org 0x8014450C
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDAA54, Offset: 0x0009EA54
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sb      r0, 0x00D2(s0)
.org 0x80144514
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDAA5C, Offset: 0x0009EA5C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sb      r0, 0x00D3(s0)
.org 0x8014451C
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDAA6C, Offset: 0x0009EA6C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sb      r0, 0x00D0(s0)
.org 0x8014452C
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC20, Offset: 0x00120C20
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .db 0x0C
.org 0x801C66E0
    .db 0xCC

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2738, Offset: 0x000B6738
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   v0, v0, -0x0990
;   lhu     a1, 0x48C8(v0)
.org 0x8015C1F8
    addiu   v0, v0, -0x0A04
    lhu     a1, 0x1F04(t1)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2844, Offset: 0x000B6844
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lui     v0, 0x801F
;   lhu     v0, 0x3F38(v0)
;   lui     t1, 0x801F
;   addiu   t1, t1, -0x0990
;   andi    a1, v0, 0xFFFF
;   addu    t8, a1, t1
;   lb      t5, 0x00D4(t8)
.org 0x8015C304
    addu    v0, s6, s7
    lhu     v0, 0x1F04(v0)
    lui     t1, 0x801F
    addu    t1, t1, v0
    lb      t5, -0x0930(t1)
    sb      t5, 0x00CC(sp)
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2868, Offset: 0x000B6868
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   andi    v1, v0, 0xFFFF
;   addu    t0, v1, t1
;   lb      t6, 0x00D4(t0)
.org 0x8015C328
    nop
    nop
    lb      t6, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF28D0, Offset: 0x000B68D0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lui     t8, 0x801F
;   addiu   t8, t8, -0x0990
;   addu    t5, a1, t8
;   lb      t1, 0x00D4(t5)
.org 0x8015C390
    nop
    nop
    nop
    lb      t1, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF28E8, Offset: 0x000B68E8
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addu    t0, v1, t8
;   lb      t6, 0x00D4(t0)
.org 0x8015C3A8
    nop
    lb      t6, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF294C, Offset: 0x000B694C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lui     t5, 0x801F
;   addiu   t5, t5, -0x0990
;   addu    t1, a1, t5
;   lb      t8, 0x00D4(t1)
.org 0x8015C40C
    nop
    nop
    nop
    lb      t8, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2964, Offset: 0x000B6964
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addu    t0, v1, t5
;   lb      t6, 0x00D4(t0)
.org 0x8015C424
    nop
    lb      t6, 0x00CC(sp)
