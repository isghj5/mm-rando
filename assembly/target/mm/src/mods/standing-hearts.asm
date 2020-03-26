; =========================================================
; File: 0x00B3C000, Address: 0x00B3C974, Offset: 0x00000974
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
.org 0x800A6434
    sltiu   at, v1, 0x001D

; =========================================================
; File: 0x00B3C000, Address: 0x00B3C980, Offset: 0x00000980
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v1, at, 0x800A6454
.org 0x800A6440
    sltiu   at, v1, 0x001D
    beq     r0, at, 0x800A6454

; =========================================================
; File: 0x00B3C000, Address: 0x00B3CA90, Offset: 0x00000A90
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v1, at, 0x800A6564
.org 0x800A6550
    sltiu   at, v1, 0x001D
    beq     r0, at, 0x800A6564

; =========================================================
; File: 0x00B3C000, Address: 0x00B3CAFC, Offset: 0x00000AFC
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v1, at, 0x800A65D4
.org 0x800A65BC
    sltiu   at, v1, 0x001D
    beq     r0, at, 0x800A65D4

; =========================================================
; File: 0x00B3C000, Address: 0x00B3CB2C, Offset: 0x00000B2C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v1, at, 0x800A6608
.org 0x800A65EC
    sltiu   at, v1, 0x001D
    beq     r0, at, 0x800A6608

; =========================================================
; File: 0x00B3C000, Address: 0x00B3D694, Offset: 0x00001694
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   beqz    at, 0x800A729C
.org 0x800A7154
    beq     r0, at, 0x800A7184

; =========================================================
; File: 0x00B3C000, Address: 0x00B3D354, Offset: 0x00001354
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   beqz    at, 0x800A6FD8
.org 0x800A6E14
    beq     r0, at, 0x800A6F78

; =========================================================
; File: 0x00B3C000, Address: 0x00B3D4B8, Offset: 0x000014B8
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   t3, r0, 0x000C
.org 0x800A6F78
    addiu   t3, v1, 0x0080

; =========================================================
; File: 0x00B3C000, Address: 0x00B3D55C, Offset: 0x0000155C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   t9, v0, -0x0006
;   sltiu   at, t9, 0x0017
.org 0x800A701C
    jal     0x801DC120
    addiu   t9, v0, -0x0006

; =========================================================
; File: 0x00B3C000, Address: 0x00C72660, Offset: 0x00136660
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B30 ; daddiu  a3, t3, 0x5B30
;   .dw 0x5D000000 ; bgtzl   t0, 0x801DC134
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B31 ; daddiu  a3, t3, 0x5B31
;   .dw 0x5D000000 ; bgtzl   t0, 0x801DC148
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B32 ; daddiu  a3, t3, 0x5B32
;   .dw 0x5D000000 ; bgtzl   t0, 0x801DC15C
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B33 ; daddiu  a3, t3, 0x5B33
;   .dw 0x5D000000 ; bgtzl   t0, 0x801DC170
.org 0x801DC120
    sltiu   at, v0, 0x001D
    bne     r0, at, 0x801DC130
    nop
    or      t9, r0, r0
    jr      ra
    sltiu   at, t9, 0x0017
    lui     a3, 0x41F0
    lhu     t0, 0x00A4(a1)
    addiu   at, r0, 0x001F
    beql    at, t0, 0x801DC168
    addiu   a2, r0, 0x011A
    addiu   at, r0, 0x0044
    beql    at, t0, 0x801DC168
    addiu   a2, r0, 0x011B
    addiu   at, r0, 0x005F
    beql    at, t0, 0x801DC168
    addiu   a2, r0, 0x011C
    addiu   a2, r0, 0x011D
    jr      ra
    nop

; =========================================================
; File: 0x00D36FE0, Address: 0x00D37120, Offset: 0x00000140
; Name: Item_B_Heart :: Heart Container
; =========================================================
.headersize (0x808BCDF0 - 0x00D36FE0)
; Replaces:
;   addiu   a2, r0, 0x000D
;   lui     a3, 0x41F0
.org 0x808BCF30
    jal     0x801DC138
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C44704, Offset: 0x00108704
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   srlv    r0, r0, r0
.org 0x801AE1C4
    .dw 0x00000091

; =========================================================
; File: 0x00F62F50, Address: 0x00F631D4, Offset: 0x00000284
; Name: En_Col_Man :: Piece of Heart
; =========================================================
.headersize (0x80AFDC40 - 0x00F62F50)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80AFDEC4
    addiu   a2, r0, 0x0112

; =========================================================
; File: 0x02087000, Address: 0x020870D2, Offset: 0x000000D2
; Name: Z2_IKANA_room_04 :: Ikana Canyon - Room 04
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x020870D2
    .dh 0x0098

; =========================================================
; File: 0x02087000, Address: 0x02087302, Offset: 0x00000302
; Name: Z2_IKANA_room_04 :: Ikana Canyon - Room 04
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x02087302
    .dh 0x0098

; =========================================================
; File: 0x02087000, Address: 0x020874BA, Offset: 0x000004BA
; Name: Z2_IKANA_room_04 :: Ikana Canyon - Room 04
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x020874BA
    .dh 0x0098

; =========================================================
; File: 0x02266000, Address: 0x02266132, Offset: 0x00000132
; Name: Z2_CASTLE_room_00 :: Ancient Castle of Ikana - Room 00
; =========================================================
; Replaces:
;   .dh 0x0A06
.orga 0x02266132
    .dh 0x0099

; =========================================================
; File: 0x023E6000, Address: 0x023E62AE, Offset: 0x000002AE
; Name: Z2_PIRATE_room_11 :: Pirates' Fortress Interior - Room 11
; =========================================================
; Replaces:
;   .dh 0x0C06
.orga 0x023E62AE
    .dh 0x0094

; =========================================================
; File: 0x02521000, Address: 0x0252114A, Offset: 0x0000014A
; Name: Z2_LAST_DEKU_room_00 :: The Moon Deku Trial - Room 00
; =========================================================
; Replaces:
;   .dh 0x0106
.orga 0x0252114A
    .dh 0x009F

; =========================================================
; File: 0x02563000, Address: 0x02563172, Offset: 0x00000172
; Name: Z2_22DEKUCITY_room_02 :: Deku Palace - Room 02
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x02563172
    .dh 0x008F

; =========================================================
; File: 0x026DE000, Address: 0x026DE0EE, Offset: 0x000000EE
; Name: Z2_30GYOSON_room_00 :: Great Bay Coast - Room 00
; =========================================================
; Replaces:
;   .dh 0x0506
.orga 0x026DE0EE
    .dh 0x0097

; =========================================================
; File: 0x026DE000, Address: 0x026DEE52, Offset: 0x00000E52
; Name: Z2_30GYOSON_room_00 :: Great Bay Coast - Room 00
; =========================================================
; Replaces:
;   .dh 0x0506
.orga 0x026DEE52
    .dh 0x0097

; =========================================================
; File: 0x02715000, Address: 0x0271535A, Offset: 0x0000035A
; Name: Z2_31MISAKI_room_00 :: Zora Cape - Room 00
; =========================================================
; Replaces:
;   .dh 0x0706
.orga 0x0271535A
    .dh 0x0093

; =========================================================
; File: 0x02715000, Address: 0x02715A96, Offset: 0x00000A96
; Name: Z2_31MISAKI_room_00 :: Zora Cape - Room 00
; =========================================================
; Replaces:
;   .dh 0x0706
.orga 0x02715A96
    .dh 0x0093

; =========================================================
; File: 0x0279E000, Address: 0x0279E18E, Offset: 0x0000018E
; Name: Z2_LAST_GORON_room_00 :: The Moon Goron Trial - Room 00
; =========================================================
; Replaces:
;   .dh 0x0106
.orga 0x0279E18E
    .dh 0x00A0

; =========================================================
; File: 0x027C1000, Address: 0x027C12CA, Offset: 0x000002CA
; Name: Z2_24KEMONOMITI_room_00 :: Road to Southern Swamp - Room 00
; =========================================================
; Replaces:
;   .dh 0x0106
.orga 0x027C12CA
    .dh 0x008D

; =========================================================
; File: 0x02837000, Address: 0x0283731E, Offset: 0x0000031E
; Name: Z2_20SICHITAI_room_00 :: Southern Swamp (Poisoned) - Room 00
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x0283731E
    .dh 0x008E

; =========================================================
; File: 0x01F1E000, Address: 0x01F1E2C6, Offset: 0x000002C6
; Name: Z2_20SICHITAI2_room_00 :: Southern Swamp (Clear) - Room 00
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x01F1E2C6
    .dh 0x008E

; =========================================================
; File: 0x028A9000, Address: 0x028A90BA, Offset: 0x000000BA
; Name: Z2_LAST_ZORA_room_00 :: The Moon Zora Trial - Room 00
; =========================================================
; Replaces:
;   .dh 0x0106
.orga 0x028A90BA
    .dh 0x00A1

; =========================================================
; File: 0x02A2A000, Address: 0x02A2A086, Offset: 0x00000086
; Name: Z2_BANDROOM_room_02 :: Zora Hall - Rooms - Room 02
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x02A2A086
    .dh 0x0095

; =========================================================
; File: 0x02A54000, Address: 0x02A540E6, Offset: 0x000000E6
; Name: Z2_11GORONNOSATO_room_00 :: Goron Village (Winter) - Room 00
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x02A540E6
    .dh 0x0090

; =========================================================
; File: 0x028BF000, Address: 0x028BF0BA, Offset: 0x000000BA
; Name: Z2_11GORONNOSATO2_room_00 :: Goron Village (Spring) - Room 00
; =========================================================
; Replaces:
;   .dh 0x1E06
.orga 0x028BF0BA
    .dh 0x0090

; =========================================================
; File: 0x02C04000, Address: 0x02C0408E, Offset: 0x0000008E
; Name: Z2_14YUKIDAMANOMITI_room_00 :: Path to Snowhead - Room 00
; =========================================================
; Replaces:
;   .dh 0x0806
.orga 0x02C0408E
    .dh 0x0096

; =========================================================
; File: 0x02C04000, Address: 0x02C044C6, Offset: 0x000004C6
; Name: Z2_14YUKIDAMANOMITI_room_00 :: Path to Snowhead - Room 00
; =========================================================
; Replaces:
;   .dh 0x0806
.orga 0x02C044C6
    .dh 0x0096

; =========================================================
; File: 0x02D52000, Address: 0x02D52076, Offset: 0x00000076
; Name: Z2_LAST_LINK_room_04 :: The Moon Link Trial - Room 04
; =========================================================
; Replaces:
;   .dh 0x0106
.orga 0x02D52076
    .dh 0x00A2

; =========================================================
; File: 0x02E2E000, Address: 0x02E2E0C2, Offset: 0x000000C2
; Name: Z2_BACKTOWN_room_00 :: North Clock Town - Room 00
; =========================================================
; Replaces:
;   .dh 0x0A06
.orga 0x02E2E0C2
    .dh 0x008C

; =========================================================
; File: 0x02E2E000, Address: 0x02E2E446, Offset: 0x00000446
; Name: Z2_BACKTOWN_room_00 :: North Clock Town - Room 00
; =========================================================
; Replaces:
;   .dh 0x0A06
.orga 0x02E2E446
    .dh 0x008C

; =========================================================
; File: 0x02E5C000, Address: 0x02E5C0E2, Offset: 0x000000E2
; Name: Z2_CLOCKTOWER_room_00 :: South Clock Town - Room 00
; =========================================================
; Replaces:
;   .dh 0x0A06
.orga 0x02E5C0E2
    .dh 0x008B

; =========================================================
; File: 0x02E5C000, Address: 0x02E5C6D6, Offset: 0x000006D6
; Name: Z2_CLOCKTOWER_room_00 :: South Clock Town - Room 00
; =========================================================
; Replaces:
;   .dh 0x0A06
.orga 0x02E5C6D6
    .dh 0x008B

; =========================================================
; File: 0x00B3C000, Address: 0x00B3C3A4, Offset: 0x000003A4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   beqz    at, 0x800A6110
.org 0x800A5E64
    beq     r0, at, 0x800A5EDC

; =========================================================
; File: 0x00B3C000, Address: 0x00B3C320, Offset: 0x00000320
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   beqz    v0, 0x800A5E1C
;   or      a0, s0, r0
;   lh      t2, 0x001C(s0)
;   addiu   at, r0, 0x0006
;   addiu   t3, r0, 0x0003
;   bne     t2, at, 0x800A5E0C
;   nop
;   sw      r0, 0x0030(sp)
;   sh      r0, 0x0148(s0)
;   b       0x800A5E1C
;   sh      t3, 0x001C(s0)
;   jal     0x800B670C
.org 0x800A5DE0
    lh      t2, 0x001C(s0)
    sltiu   at, t2, 0x001D
    bne     r0, at, 0x800A5DF8
    nop
    jal     0x801DC600
    addiu   a0, t2, 0x0080
    beq     r0, v0, 0x800A5E1C
    or      a0, s0, r0
    jal     0x800B670C
    nop
    b       0x800A6368
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00B3DFD0, Offset: 0x00001FD0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v0, at, 0x800A7AA8
.org 0x800A7A90
    sltiu   at, v0, 0x001D
    beq     r0, at, 0x800A7AA8

; =========================================================
; File: 0x00B3C000, Address: 0x00B3E62C, Offset: 0x0000262C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0006
;   beq     v0, at, 0x800A8104
.org 0x800A80EC
    sltiu   at, v0, 0x001D
    beq     r0, at, 0x800A8104

; =========================================================
; File: 0x00B3C000, Address: 0x00B3C8A8, Offset: 0x000008A8
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lw      ra, 0x0024(sp)
;   lw      s0, 0x0020(sp)
;   addiu   sp, sp, 0x0048
;   jr      ra
.org 0x800A6368
    lh      t0, 0x001C(s0)
    lb      t2, 0x00B0(s0)
    j       0x800B3400
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00B49940, Offset: 0x0000D940
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sll     t9, t8, 0x0003
;   addu    t5, t3, t9
;   lw      a3, (t5)
;   addiu   t6, r0, 0x000A
;   sw      t8, (t1)
;   sltiu   a3, a3, 0x0001
;   beqz    a3, 0x800B3428
;   sw      t6, (a0)
;   addiu   t7, t8, -0x0001
;   sw      t7, (t1)
;   lhu     a2, 0x000C(v0)
;   addiu   a3, r0, -0x0009
;   lui     t3, 0x801B
;   nor     t8, a2, a3
;   bnez    t8, 0x800B3460
.org 0x800B3400
    sltiu   t1, t0, 0x001D
    or      t3, t2, t1
    bne     r0, t3, 0x800B3428
    addiu   a0, t0, 0x0080
    jal     0x80144A28
    nop
    beq     r0, v0, 0x800B3428
    or      a0, s0, r0
    jal     0x800B670C
    nop
    lw      ra, 0x0024(sp)
    lw      s0, 0x0020(sp)
    addiu   sp, sp, 0x0048
    jr      ra
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C72B40, Offset: 0x00136B40
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x325D0000 ; andi    sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x335D0000 ; andi    sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x345D0000 ; ori     sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x355D0000 ; ori     sp, t2, 0x0000
.org 0x801DC600
    addiu   t0, r0, 0x0000
    beq     t0, a0, 0x801DC638
    nop
    addiu   t1, r0, 0x0000
    beq     t1, a0, 0x801DC638
    nop
    addiu   t2, r0, 0x0000
    beq     t2, a0, 0x801DC638
    nop
    addiu   t3, r0, 0x0000
    beq     t3, a0, 0x801DC638
    nop
    jr      ra
    nop
    addiu   t5, r0, 0x0001
    sb      t5, 0x00B0(s0)
    jr      ra
    nop

; =========================================================
; File: 0x00D1C6E0, Address: 0x00D1CD94, Offset: 0x000006B4
; Name: En_Boom :: Zora Fins
; =========================================================
.headersize (0x808A24D0 - 0x00D1C6E0)
; Replaces:
;   addiu   at, r0, 0x0006
;   bne     a0, at, 0x808A2B98
.org 0x808A2B84
    slti    at, a0, 0x001D
    bne     r0, at, 0x808A2B98
