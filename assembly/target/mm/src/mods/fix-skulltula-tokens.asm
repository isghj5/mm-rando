; =========================================================
; File: 0x00DFF7A0, Address: 0x00DFF854, Offset: 0x000000B4
; Name: En_Si :: Gold Skulltula Token
; =========================================================
.headersize (0x8098CA20 - 0x00DFF7A0)
; Replaces:
;   sw      s0, 0x0018(sp)
;   or      s0, a1, r0
;   sw      ra, 0x001C(sp)
;   lh      a2, 0x001C(a0)
;   andi    a2, a2, 0x00FC
;   sra     a2, a2, 0x0002
;   slti    at, a2, 0x0020
;   beql    at, r0, 0x8098CB0C
;   or      a0, s0, r0
;   bltz    a2, 0x8098CB08
;   or      a0, s0, r0
;   jal     0x800B5C90
;   or      a1, a2, r0
;   or      a0, s0, r0
;   jal     0x80112E80
;   addiu   a1, r0, 0x006E
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s0)
;   slti    at, v0, 0x001E
;   bnez    at, 0x8098CB48
;   or      a0, s0, r0
;   or      a0, s0, r0
;   addiu   a1, r0, 0x00FC
;   jal     0x801518B0
;   or      a2, r0, r0
;   jal     0x801A3098
;   addiu   a0, r0, 0x0922
;   b       0x8098CB60
;   lw      ra, 0x001C(sp)
;   addiu   a1, r0, 0x0052
;   jal     0x801518B0
;   or      a2, r0, r0
;   jal     0x801A3098
;   addiu   a0, r0, 0x0039
;   lw      ra, 0x001C(sp)
;   lw      s0, 0x0018(sp)
;   addiu   sp, sp, 0x0020
;   jr      ra
;   nop
.org 0x8098CAD4
    sw      ra, 0x001C(sp)
    lh      a2, 0x001C(a0)
    andi    a2, a2, 0x00FC
    sra     a2, a2, 0x0002
    or      a0, s6, r0
    jal     0x800B5C90
    or      a1, a2, r0
    lh      t0, 0x00A4(s6)
    addiu   a0, r0, 0x0027
    bnel    a0, t0, 0x8098CB04
    addiu   a0, r0, 0x0158
    addiu   a0, r0, 0x013A
    addu    a0, a0, a2
    jal     0x801DC710
    or      a1, s6, r0
    lui     t6, 0x8099
    addiu   t6, t6, -0x34D0
    jal     0x801DC6DC
    sw      t6, 0x0144(s0)
    sw      r0, 0x013C(s0)
    lw      ra, 0x001C(sp)
    jr      ra
    addiu   sp, sp, 0x0020
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    jal     0x801DC6DC
    nop
    nop
    jal     0x800B867C
    nop
    beqz    v0, 0x8098CB64
    nop
    jal     0x801DC6F4
    nop
    jal     0x800B670C
    or      a0, s0, r0
    lw      ra, 0x001C(sp)
    jr      ra
    addiu   sp, sp, 0x0020

; =========================================================
; File: 0x00DFF7A0, Address: 0x00DFF930, Offset: 0x00000190
; Name: En_Si :: Gold Skulltula Token
; =========================================================
.headersize (0x8098CA20 - 0x00DFF7A0)
; Replaces:
;   jal     0x800B670C
;   lw      a0, 0x0018(sp)
.org 0x8098CBB0
    nop
    nop

; =========================================================
; File: 0x00DFF7A0, Address: 0x00DFF980, Offset: 0x000001E0
; Name: En_Si :: Gold Skulltula Token
; =========================================================
.headersize (0x8098CA20 - 0x00DFF7A0)
; Replaces:
;   jal     0x800B670C
;   lw      a0, 0x0018(sp)
.org 0x8098CC00
    nop
    nop

; =========================================================
; File: 0x00DFF7A0, Address: 0x00DFFB58, Offset: 0x000003B8
; Name: En_Si :: Gold Skulltula Token
; =========================================================
.headersize (0x8098CA20 - 0x00DFF7A0)
; Replaces:
;   nop
;   nop
;   sll     r0, r0, 0x000D
;   sll     r0, r0, 0x0002
;   nop
;   nop
;   mthi    r0
.org 0x8098CDD8
    .dw 0x00000340 ; sll     r0, r0, 0x000D
    .dw 0x00000078 ; dsll    r0, r0, 0x0001
    .dw 0x00000000 ; nop
    .dw 0x00000000 ; nop
    .dw 0x00000013 ; mtlo    r0
    .dw 0x450000F0 ; bc1f    0x8098D1B0
    .dw 0x460000F4

; =========================================================
; File: 0x00DFF7A0, Address: 0x00DFFBBF, Offset: 0x0000041F
; Name: En_Si :: Gold Skulltula Token
; =========================================================
.headersize (0x8098CA20 - 0x00DFF7A0)
; Replaces:
;   .db 0x60
.org 0x8098CE3F
    .db 0x68

; =========================================================
; File: 0x00B3C000, Address: 0x00BC570A, Offset: 0x0008970A
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dh 0x0027
.org 0x8012F1CA
    .dh 0x0052

; =========================================================
; File: 0x00B3C000, Address: 0x00BA943C, Offset: 0x0006D43C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lw      t7, 0x0040(sp)
.org 0x80112EFC
    lui     t7, 0x800B

; =========================================================
; File: 0x00B3C000, Address: 0x00BA944C, Offset: 0x0006D44C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lh      a0, 0x00A4(t7)
.org 0x80112F0C
    lh      a0, 0x35F4(t7)

; =========================================================
; File: 0x00B3C000, Address: 0x00BE7448, Offset: 0x000AB448
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8010EF68
.org 0x80150F08
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDA9DC, Offset: 0x0009E9DC
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sw      t4, 0x0EC0(s2)
.org 0x8014449C
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDA9E4, Offset: 0x0009E9E4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sw      t6, 0x0EC0(s2)
.org 0x801444A4
    nop

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC0D3, Offset: 0x00000223
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   .db 0x20
.org 0x80B66F53
    .db 0x80

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC127, Offset: 0x00000277
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   .db 0x20
.org 0x80B66FA7
    .db 0x80

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2A28, Offset: 0x000B6A28
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sh      r0, 0x00CA(sp)
;   sh      r0, 0x00C8(sp)
;   lh      a0, 0x00A4(s4)
.org 0x8015C4E8
    lh      a0, 0x1F04(t1)
    sw      r0, 0x00C8(sp)
    addiu   a0, a0, -0x002B

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2B28, Offset: 0x000B6B28
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
;   addiu   at, r0, 0x0001
;   beql    v0, at, 0x8015C614
;   lh      t4, 0x00DA(sp)
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
.org 0x8015C5E8
    addu    t1, s6, s7
    lh      a0, 0x1F04(t1)
    jal     0x8012F22C
    addiu   a0, a0, -0x002B
    addiu   at, r0, 0x0001
    beq     v0, at, 0x8015C610
    sh      v0, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2BA0, Offset: 0x000B6BA0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
.org 0x8015C660
    nop
    lh      v0, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2BB4, Offset: 0x000B6BB4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
.org 0x8015C674
    nop
    lh      v0, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2C18, Offset: 0x000B6C18
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
.org 0x8015C6D8
    nop
    lh      v0, 0x00CC(sp)

; =========================================================
; File: 0x00B3C000, Address: 0x00BF2C2C, Offset: 0x000B6C2C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x8012F22C
;   lh      a0, 0x00A4(s4)
.org 0x8015C6EC
    nop
    lh      v0, 0x00CC(sp)
