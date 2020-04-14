; =========================================================
; File: 0x00DC53E0, Address: 0x00DC550C, Offset: 0x0000012C
; Name: En_Ms :: Magic Bean Seller
; =========================================================
.headersize (0x80952620 - 0x00DC53E0)
; Replaces:
;   lbu     t8, -0x0916(t8)
;   subu    v1, t6, t7
;   sll     v1, v1, 0x0010
;   addiu   at, r0, 0x00FF
;   bne     t8, at, 0x80952770
.org 0x8095274C
    lbu     t8, 0x3603(t8)
    subu    v1, t6, t7
    sll     v1, v1, 0x0010
    andi    at, t8, 0x0040
    bne     r0, at, 0x80952770

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB787C, Offset: 0x0000F97C
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   bne     v0, at, 0x8083D424
.org 0x8083D40C
    nop

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB78A4, Offset: 0x0000F9A4
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   beq     v0, at, 0x8083D490
.org 0x8083D434
    b       0x8083D490

; =========================================================
; File: 0x00EC9990, Address: 0x00ECB1D8, Offset: 0x00001848
; Name: En_Bal :: Tingle (Gameplay)
; =========================================================
.headersize (0x80A61810 - 0x00EC9990)
; Replaces:
;   beqz    v0, 0x80A63098
.org 0x80A63058
    b       0x80A63098

; =========================================================
; File: 0x00EC9990, Address: 0x00ECB060, Offset: 0x000016D0
; Name: En_Bal :: Tingle (Gameplay)
; =========================================================
.headersize (0x80A61810 - 0x00EC9990)
; Replaces:
;   jal     0x8012EF0C
.org 0x80A62EE0
    nop

; =========================================================
; File: 0x0104EAC0, Address: 0x0104EEB8, Offset: 0x000003F8
; Name: En_Dt :: Mayor Dotour (Gameplay)
; =========================================================
.headersize (0x80BE9B20 - 0x0104EAC0)
; Replaces:
;   lbu     t6, 0x0F34(v0)
;   andi    t7, t6, 0x0010
.org 0x80BE9F18
    lbu     t6, 0x3F68(v0)
    andi    t7, t6, 0x0008

; =========================================================
; File: 0x00ED6B10, Address: 0x00ED78F4, Offset: 0x00000DE4
; Name: En_Mm3 :: Postman (Counting Game)
; =========================================================
.headersize (0x80A6F0A0 - 0x00ED6B10)
; Replaces:
;   lbu     t8, 0x0F45(v1)
.org 0x80A6FE84
    lbu     t8, 0x3F85(v1)

; =========================================================
; File: 0x00ED6B10, Address: 0x00ED7900, Offset: 0x00000DF0
; Name: En_Mm3 :: Postman (Counting Game)
; =========================================================
.headersize (0x80A6F0A0 - 0x00ED6B10)
; Replaces:
;   andi    t9, t8, 0x0001
.org 0x80A6FE90
    andi    t9, t8, 0x0040

; =========================================================
; File: 0x01060400, Address: 0x01061334, Offset: 0x00000F34
; Name: En_Rz :: Rosa Sisters
; =========================================================
.headersize (0x80BFB480 - 0x01060400)
; Replaces:
;   beqz    t9, 0x80BFC3CC
.org 0x80BFC3B4
    b       0x80BFC3CC

; =========================================================
; File: 0x00F8B5A0, Address: 0x00F8CA0C, Offset: 0x0000146C
; Name: En_Kendo_Js :: Swordsman
; =========================================================
.headersize (0x80B262A0 - 0x00F8B5A0)
; Replaces:
;   lbu     t6, 0x05A7(t6)
.org 0x80B2770C
    lbu     t6, 0x35EF(t6)

; =========================================================
; File: 0x00F8B5A0, Address: 0x00F8CA18, Offset: 0x00001478
; Name: En_Kendo_Js :: Swordsman
; =========================================================
.headersize (0x80B262A0 - 0x00F8B5A0)
; Replaces:
;   andi    t7, t6, 0x0020
.org 0x80B27718
    andi    t7, t6, 0x0080

; =========================================================
; File: 0x0106E050, Address: 0x0106E628, Offset: 0x000005D8
; Name: En_Kitan :: Keaton
; =========================================================
.headersize (0x80C090D0 - 0x0106E050)
; Replaces:
;   lbu     t9, 0x0F47(v0)
.org 0x80C096A8
    lbu     t9, 0x3F6E(v0)

; =========================================================
; File: 0x0106E050, Address: 0x0106E634, Offset: 0x000005E4
; Name: En_Kitan :: Keaton
; =========================================================
.headersize (0x80C090D0 - 0x0106E050)
; Replaces:
;   andi    t0, t9, 0x0080
.org 0x80C096B4
    andi    t0, t9, 0x0001

; =========================================================
; File: 0x0105F080, Address: 0x0105FA54, Offset: 0x000009D4
; Name: En_Yb :: Kamaro
; =========================================================
.headersize (0x80BFA100 - 0x0105F080)
; Replaces:
;   bne     t4, at, 0x80BFAAF4
.org 0x80BFAAD4
    b       0x80BFAAF4

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC74, Offset: 0x00120C74
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   add     r0, at, gp
.org 0x801C6734
    add     r0, r0, r0

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CCC4, Offset: 0x00120CC4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x03000000
.org 0x801C6784
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC78, Offset: 0x00120C78
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   syscall 0x00C0
.org 0x801C6738
    sll     a2, r0, 0x0000

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC5C, Offset: 0x00120C5C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jal     0x80003000
.org 0x801C671C
    jal     0x80000000

; =========================================================
; File: 0x00E325D0, Address: 0x00E3409C, Offset: 0x00001ACC
; Name: En_Aob_01 :: Mamamu Yan
; =========================================================
.headersize (0x809C10B0 - 0x00E325D0)
; Replaces:
;   lbu     t0, 0x0F00(v1)
.org 0x809C2B7C
    lbu     t0, 0x3F82(v1)

; =========================================================
; File: 0x00E325D0, Address: 0x00E340A8, Offset: 0x00001AD8
; Name: En_Aob_01 :: Mamamu Yan
; =========================================================
.headersize (0x809C10B0 - 0x00E325D0)
; Replaces:
;   andi    t1, t0, 0x0020
.org 0x809C2B88
    andi    t1, t0, 0x0002

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC7C, Offset: 0x00120C7C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   andi    r0, r0, 0x0000
.org 0x801C673C
    nop

; =========================================================
; File: 0x0102E230, Address: 0x0102EC7C, Offset: 0x00000A4C
; Name: En_Stone_heishi :: Shiro
; =========================================================
.headersize (0x80BC9270 - 0x0102E230)
; Replaces:
;   bne     t7, at, 0x80BC9CE4
.org 0x80BC9CBC
    b       0x80BC9CE4

; =========================================================
; File: 0x00DC5A10, Address: 0x00DC5EF4, Offset: 0x000004E4
; Name: En_Hs :: Grog
; =========================================================
.headersize (0x80952C50 - 0x00DC5A10)
; Replaces:
;   bne     t5, at, 0x8095315C
.org 0x80953134
    b       0x8095315C

; =========================================================
; File: 0x01043140, Address: 0x01043AC8, Offset: 0x00000988
; Name: En_Al :: Madame Aroma (Gameplay)
; =========================================================
.headersize (0x80BDE1A0 - 0x01043140)
; Replaces:
;   lbu     t8, 0x20AF(t8)
;   lui     v0, 0x801F
;   addiu   v0, v0, -0x0990
;   addu    t9, v0, t8
;   lbu     t1, 0x0070(t9)
;   addiu   at, r0, 0x0037
.org 0x80BDEB28
    jal     0x80144A28
    addiu   a0, r0, 0x008F
    bnez    v0, 0x80BDEB54
    nop
    nop
    nop

; =========================================================
; File: 0x01043140, Address: 0x01043AE4, Offset: 0x000009A4
; Name: En_Al :: Madame Aroma (Gameplay)
; =========================================================
.headersize (0x80BDE1A0 - 0x01043140)
; Replaces:
;   beq     t1, at, 0x80BDEB54
.org 0x80BDEB44
    nop

; =========================================================
; File: 0x0102BBC0, Address: 0x0102C264, Offset: 0x000006A4
; Name: En_Guruguru :: Guru-Guru
; =========================================================
.headersize (0x80BC6BF0 - 0x0102BBC0)
; Replaces:
;   bnel    t5, at, 0x80BC72B0
.org 0x80BC7294
    b       0x80BC72B0

; =========================================================
; File: 0x00E35CB0, Address: 0x00E364C8, Offset: 0x00000818
; Name: En_Bom_Bowl_Man :: Bomber I [?]
; =========================================================
.headersize (0x809C4790 - 0x00E35CB0)
; Replaces:
;   beql    t1, r0, 0x809C514C
.org 0x809C4FA8
    b       0x809C514C

; =========================================================
; File: 0x00E35CB0, Address: 0x00E35DFC, Offset: 0x0000014C
; Name: En_Bom_Bowl_Man :: Bomber I [?]
; =========================================================
.headersize (0x809C4790 - 0x00E35CB0)
; Replaces:
;   bnez    t3, 0x809C4910
.org 0x809C48DC
    nop

; =========================================================
; File: 0x01016810, Address: 0x0101832C, Offset: 0x00001B1C
; Name: En_Geg :: Don Gero
; =========================================================
.headersize (0x80BB16D0 - 0x01016810)
; Replaces:
;   bnel    t7, at, 0x80BB3228
.org 0x80BB31EC
    b       0x80BB3228

; =========================================================
; File: 0x00F9A270, Address: 0x00F9B838, Offset: 0x000015C8
; Name: En_Gg :: Darmani's Ghost I [?]
; =========================================================
.headersize (0x80B34F70 - 0x00F9A270)
; Replaces:
;   bne     t8, at, 0x80B36550
.org 0x80B36538
    b       0x80B36550

; =========================================================
; File: 0x00F46EF0, Address: 0x00F47074, Offset: 0x00000184
; Name: En_Fsn :: Curiosity Shop Proprietor
; =========================================================
.headersize (0x80AE1B70 - 0x00F46EF0)
; Replaces:
;   bnel    t5, at, 0x80AE1D38
.org 0x80AE1CF4
    b       0x80AE1D38

; =========================================================
; File: 0x00F38DA0, Address: 0x00F39A74, Offset: 0x00000CD4
; Name: En_Trt2 :: Kotake (Broom) [?]
; =========================================================
.headersize (0x80AD3380 - 0x00F38DA0)
; Replaces:
;   lbu     t1, 0x0F04(v1)
.org 0x80AD4054
    lbu     t1, 0x3F73(v1)

; =========================================================
; File: 0x00F38DA0, Address: 0x00F39A7C, Offset: 0x00000CDC
; Name: En_Trt2 :: Kotake (Broom) [?]
; =========================================================
.headersize (0x80AD3380 - 0x00F38DA0)
; Replaces:
;   andi    t2, t1, 0x0010
.org 0x80AD405C
    andi    t2, t1, 0x0002

; =========================================================
; File: 0x00EBA820, Address: 0x00EBAD54, Offset: 0x00000534
; Name: En_Dnh :: Boat Cruise Target Spot
; =========================================================
.headersize (0x80A50D40 - 0x00EBA820)
; Replaces:
;   mult    r0, t5
.org 0x80A51274
    sll     r0, t5, 0x0000

; =========================================================
; File: 0x00F5CE50, Address: 0x00F60A21, Offset: 0x00003BD1
; Name: En_Pm :: Postman (Delivering Letters)
; =========================================================
.headersize (0x80AF7B40 - 0x00F5CE50)
; Replaces:
;   .dw 0x003E0023 ; subu    r0, at, fp
.org 0x80AFB711
    .dw 0x003E0000

; =========================================================
; File: 0x01043140, Address: 0x01044DDD, Offset: 0x00001C9D
; Name: En_Al :: Madame Aroma (Gameplay)
; =========================================================
.headersize (0x80BDE1A0 - 0x01043140)
; Replaces:
;   .dw 0x39080013 ; xori    t0, t0, 0x0013
.org 0x80BDFE3D
    .dw 0x39080000 ; xori    t0, t0, 0x0000

; =========================================================
; File: 0x00EBA820, Address: 0x00EBAE72, Offset: 0x00000652
; Name: En_Dnh :: Boat Cruise Target Spot
; =========================================================
.headersize (0x80A50D40 - 0x00EBA820)
; Replaces:
;   .dw 0x1A40003A ; blez    s2, 0x80A5147E
.org 0x80A51392
    .dw 0x1A400000 ; blez    s2, 0x80A51396

; =========================================================
; File: 0x00ECC620, Address: 0x00ECD768, Offset: 0x00001148
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   bnez    t7, 0x80A65628
.org 0x80A655E8
    nop

; =========================================================
; File: 0x00ECC620, Address: 0x00ECD800, Offset: 0x000011E0
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   bnez    t3, 0x80A656A4
.org 0x80A65680
    nop

; =========================================================
; File: 0x0100D960, Address: 0x0100DCD4, Offset: 0x00000374
; Name: En_Baba :: Bomb Shop Proprietor's Mother
; =========================================================
.headersize (0x80BA8820 - 0x0100D960)
; Replaces:
;   bne     t5, at, 0x80BA8BB4
.org 0x80BA8B94
    b       0x80BA8BB4

; =========================================================
; File: 0x00FA8CB0, Address: 0x00FB0130, Offset: 0x00007480
; Name: En_Invadepoh
; =========================================================
.headersize (0x80B439B0 - 0x00FA8CB0)
; Replaces:
;   beqz    t7, 0x80B4AE64
.org 0x80B4AE30
    b       0x80B4AE64

; =========================================================
; File: 0x00D6D270, Address: 0x00D6EE28, Offset: 0x00001BB8
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   bne     t9, at, 0x808F4C84
.org 0x808F4C68
    b       0x808F4C84

; =========================================================
; File: 0x00D6D270, Address: 0x00D6F4EC, Offset: 0x0000227C
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   .dh 0x15A1
.org 0x808F532C
    .dh 0x1000

; =========================================================
; File: 0x00FF3690, Address: 0x00FF3B2C, Offset: 0x0000049C
; Name: En_Ma_Yto :: Cremia
; =========================================================
.headersize (0x80B8E520 - 0x00FF3690)
; Replaces:
;   bne     t0, at, 0x80B8E9F8
.org 0x80B8E9BC
    b       0x80B8E9F8

; =========================================================
; File: 0x00FF3690, Address: 0x00FF5284, Offset: 0x00001BF4
; Name: En_Ma_Yto :: Cremia
; =========================================================
.headersize (0x80B8E520 - 0x00FF3690)
; Replaces:
;   bne     t7, at, 0x80B90140
.org 0x80B90114
    b       0x80B90140

; =========================================================
; File: 0x01051B70, Address: 0x01052234, Offset: 0x000006C4
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   bne     t7, at, 0x80BED2B4
.org 0x80BED2A4
    b       0x80BED2B4

; =========================================================
; File: 0x01051B70, Address: 0x0105224C, Offset: 0x000006DC
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   beql    at, r0, 0x80BED2D0
.org 0x80BED2BC
    b       0x80BED2D0

; =========================================================
; File: 0x00E7AD60, Address: 0x00E80037, Offset: 0x000052D7
; Name: En_Go :: Goron (Generic) [?]
; =========================================================
.headersize (0x80A10FD0 - 0x00E7AD60)
; Replaces:
;   .dw 0x000C006F
.org 0x80A162A7
    .dw 0x000C0000 ; sll     r0, t4, 0x0000

; =========================================================
; File: 0x00E7AD60, Address: 0x00E8004D, Offset: 0x000052ED
; Name: En_Go :: Goron (Generic) [?]
; =========================================================
.headersize (0x80A10FD0 - 0x00E7AD60)
; Replaces:
;   .dw 0x000C0059
.org 0x80A162BD
    .dw 0x000C0000 ; sll     r0, t4, 0x0000

; =========================================================
; File: 0x00FB55A0, Address: 0x00FB76E0, Offset: 0x00002140
; Name: En_Gk :: Goron Elder's Son
; =========================================================
.headersize (0x80B50410 - 0x00FB55A0)
; Replaces:
;   lbu     t6, 0x0F21(v1)
.org 0x80B52550
    lbu     t6, 0x3F79(v1)

; =========================================================
; File: 0x00FB55A0, Address: 0x00FB76EC, Offset: 0x0000214C
; Name: En_Gk :: Goron Elder's Son
; =========================================================
.headersize (0x80B50410 - 0x00FB55A0)
; Replaces:
;   andi    t7, t6, 0x0008
.org 0x80B5255C
    andi    t7, t6, 0x0004

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC3E, Offset: 0x00120C3E
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dh 0xC00C
.org 0x801C66FE
    .dh 0x000C

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC42, Offset: 0x00120C42
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dh 0x0003
.org 0x801C6702
    .dh 0x0000

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC834, Offset: 0x00000984
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   beqz    t5, 0x80B676C8
.org 0x80B676B4
    b       0x80B676C8

; =========================================================
; File: 0x010788A0, Address: 0x01079528, Offset: 0x00000C88
; Name: En_Jgame_Tsn :: Fisherman (Fisherman's Jumping Game)
; =========================================================
.headersize (0x80C13930 - 0x010788A0)
; Replaces:
;   beqz    t9, 0x80C145DC
.org 0x80C145B8
    b       0x80C145DC

; =========================================================
; File: 0x00ED8C20, Address: 0x00EDA3E8, Offset: 0x000017C8
; Name: En_Dno :: Deku Butler
; =========================================================
.headersize (0x80A711D0 - 0x00ED8C20)
; Replaces:
;   bne     t3, at, 0x80A729AC
.org 0x80A72998
    b       0x80A729AC

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCCBD0, Offset: 0x00000D20
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   bne     t3, at, 0x80B67A6C
.org 0x80B67A50
    b       0x80B67A6C

; =========================================================
; File: 0x00D1D880, Address: 0x00D1EA88, Offset: 0x00001208
; Name: En_Minifrog :: Frog I [?]
; =========================================================
.headersize (0x808A3670 - 0x00D1D880)
; Replaces:
;   beqz    t4, 0x808A4890
.org 0x808A4878
    b       0x808A4890

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C31F, Offset: 0x00000E6F
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dw 0x57040017 ; bnel    t8, a0, 0x80AE6FFF
.org 0x80AE6F9F
    .dw 0x57040000 ; bnel    t8, a0, 0x80AE6FA3

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C358, Offset: 0x00000EA8
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   bnel    t8, a0, 0x80AE7038
.org 0x80AE6FD8
    bnel    t8, a0, 0x80AE6FDC

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C4A9, Offset: 0x00000FF9
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dw 0x57040017 ; bnel    t8, a0, 0x80AE7189
.org 0x80AE7129
    .dw 0x57040000 ; bnel    t8, a0, 0x80AE712D

; =========================================================
; File: 0x00DD44A0, Address: 0x00DD637C, Offset: 0x00001EDC
; Name: En_Fu :: Honey & Darling (Gameplay)
; =========================================================
.headersize (0x809616E0 - 0x00DD44A0)
; Replaces:
;   bnez    t8, 0x809635DC
.org 0x809635BC
    nop

; =========================================================
; File: 0x00F4EDA0, Address: 0x00F509BC, Offset: 0x00001C1C
; Name: En_Lift_Nuts :: Deku Scrub Playground Employee
; =========================================================
.headersize (0x80AE9A20 - 0x00F4EDA0)
; Replaces:
;   bnel    t4, r0, 0x80AEB660
.org 0x80AEB63C
    nop

; =========================================================
; File: 0x00E379E0, Address: 0x00E3930C, Offset: 0x0000192C
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   beql    at, r0, 0x809C7E28
.org 0x809C7DEC
    nop

; =========================================================
; File: 0x00E379E0, Address: 0x00E39324, Offset: 0x00001944
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   bnel    t8, r0, 0x809C7E28
.org 0x809C7E04
    nop

; =========================================================
; File: 0x00E379E0, Address: 0x00E39370, Offset: 0x00001990
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   bnez    t0, 0x809C7E74
;   lui     at, 0x42C8
.org 0x809C7E50
    nop
    nop

; =========================================================
; File: 0x00E379E0, Address: 0x00E3905C, Offset: 0x0000167C
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   beql    at, r0, 0x809C7B78
.org 0x809C7B3C
    nop

; =========================================================
; File: 0x00E379E0, Address: 0x00E390D0, Offset: 0x000016F0
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   bnez    t0, 0x809C7BD4
;   lui     at, 0x42C8
.org 0x809C7BB0
    nop
    nop

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB771C, Offset: 0x0000F81C
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   beq     v0, at, 0x8083D2C8
.org 0x8083D2AC
    beq     at, v0, 0x8083D2C8

; =========================================================
; File: 0x010066F0, Address: 0x01006CA8, Offset: 0x000005B8
; Name: Elf_Msg6
; =========================================================
.headersize (0x80BA15A0 - 0x010066F0)
; Replaces:
;   addu    t2, v0, t1
;   lbu     t3, 0x0070(t2)
.org 0x80BA1B58
    lbu     t2, 0x3F7B(v0)
    andi    t3, t2, 0x0004

; =========================================================
; File: 0x010066F0, Address: 0x01006CB8, Offset: 0x000005C8
; Name: Elf_Msg6
; =========================================================
.headersize (0x80BA15A0 - 0x010066F0)
; Replaces:
;   bnel    t3, at, 0x80BA1B84
.org 0x80BA1B68
    beq     r0, t3, 0x80BA1B84

; =========================================================
; File: 0x010066F0, Address: 0x0100725C, Offset: 0x00000B6C
; Name: Elf_Msg6
; =========================================================
.headersize (0x80BA15A0 - 0x010066F0)
; Replaces:
;   addu    t4, v0, t3
;   lbu     t5, 0x0070(t4)
;   bne     t5, at, 0x80BA212C
.org 0x80BA210C
    lbu     t2, 0x3F7B(v0)
    andi    t3, t2, 0x0004
    beq     r0, t3, 0x80BA212C

; =========================================================
; File: 0x00FF8480, Address: 0x00FF87F0, Offset: 0x00000370
; Name: En_Zog :: Mikau
; =========================================================
.headersize (0x80B93310 - 0x00FF8480)
; Replaces:
;   addu    t1, t1, t9
;   lbu     t1, -0x0920(t1)
;   bnel    t1, at, 0x80B936D0
.org 0x80B93680
    lbu     t2, 0x35EB(t1)
    andi    t3, t2, 0x0004
    beql    r0, t3, 0x80B936D0

; =========================================================
; File: 0x00EAE760, Address: 0x00EAE9F4, Offset: 0x00000294
; Name: En_Sekihi :: Mikau's Grave & Song Pedestals [Early]
; =========================================================
.headersize (0x80A44C80 - 0x00EAE760)
; Replaces:
;   addu    t3, t3, t2
;   lbu     t3, -0x0920(t3)
;   beql    t3, at, 0x80A44F30
.org 0x80A44F14
    lbu     t2, 0x35EB(t3)
    andi    t3, t2, 0x0004
    bnel    r0, t3, 0x80A44F30

; =========================================================
; File: 0x01034170, Address: 0x01034C94, Offset: 0x00000B24
; Name: En_Hg :: Pamela's Father (Normal)
; =========================================================
.headersize (0x80BCF1D0 - 0x01034170)
; Replaces:
;   bnel    t6, at, 0x80BCFD08
.org 0x80BCFCF4
    b       0x80BCFD08

; =========================================================
; File: 0x00F23710, Address: 0x00F248C8, Offset: 0x000011B8
; Name: En_Ma4 :: Romani I [?]
; =========================================================
.headersize (0x80ABDCA0 - 0x00F23710)
; Replaces:
;   beqz    t4, 0x80ABEE78
.org 0x80ABEE58
    b       0x80ABEE78

; =========================================================
; File: 0x00EA3200, Address: 0x00EA364C, Offset: 0x0000044C
; Name: En_Elfgrp :: Group of Stray Fairies
; =========================================================
.headersize (0x80A396B0 - 0x00EA3200)
; Replaces:
;   addu    t0, v0, t9
;   lbu     t2, 0x0070(t0)
;   or      a0, s0, r0
;   bne     t2, at, 0x80A39B20
.org 0x80A39AFC
    lbu     t7, 0x3F7C(v0)
    andi    t8, t7, 0x0040
    or      a0, s0, r0
    beq     r0, t8, 0x80A39B20

; =========================================================
; File: 0x00EA3200, Address: 0x00EA3450, Offset: 0x00000250
; Name: En_Elfgrp :: Group of Stray Fairies
; =========================================================
.headersize (0x80A396B0 - 0x00EA3200)
; Replaces:
;   addiu   at, r0, 0x0010
;   or      a0, s0, r0
;   bnel    t5, at, 0x80A39924
.org 0x80A39900
    lbu     t7, 0x3F6F(v0)
    andi    t8, t7, 0x0008
    beql    r0, t8, 0x80A39924

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB788C, Offset: 0x0000F98C
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   bnel    t0, r0, 0x8083D450
.org 0x8083D41C
    nop

; =========================================================
; File: 0x00EFC060, Address: 0x00EFEA00, Offset: 0x000029A0
; Name: En_Az :: Beaver Bros.
; =========================================================
.headersize (0x80A94A30 - 0x00EFC060)
; Replaces:
;   addiu   at, r0, 0x000C
.org 0x80A973D0
    addiu   at, r0, 0x00AD

; =========================================================
; File: 0x00B3C000, Address: 0x00BA98A0, Offset: 0x0006D8A0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   bnel    t6, r0, 0x801133A8
;   lbu     t6, 0x0006(t1)
.org 0x80113360
    nop
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C5C670, Offset: 0x00120670
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   srl     r0, r0, 0x0000
.org 0x801C6130
    nop

; =========================================================
; File: 0x00F38DA0, Address: 0x00F39A74, Offset: 0x00000CD4
; Name: En_Trt2 :: Kotake (Broom) [?]
; =========================================================
.headersize (0x80AD3380 - 0x00F38DA0)
; Replaces:
;   lbu     t1, 0x0F04(v1)
;   lui     at, 0x4396
;   andi    t2, t1, 0x0010
.org 0x80AD4054
    lbu     t1, 0x3F73(v1)
    lui     at, 0x4396
    andi    t4, t3, 0x0002

; =========================================================
; File: 0x00EF2EC0, Address: 0x00EF3C5C, Offset: 0x00000D9C
; Name: En_Trt :: Kotake (No Broom) [?]
; =========================================================
.headersize (0x80A8B770 - 0x00EF2EC0)
; Replaces:
;   lbu     t3, 0x0F04(a2)
;   lui     at, 0x4396
;   andi    t4, t3, 0x0010
.org 0x80A8C50C
    lbu     t3, 0x3F73(a2)
    lui     at, 0x4396
    andi    t4, t3, 0x0002

; =========================================================
; File: 0x00FB55A0, Address: 0x00FB76F0, Offset: 0x00002150
; Name: En_Gk :: Goron Elder's Son
; =========================================================
.headersize (0x80B50410 - 0x00FB55A0)
; Replaces:
;   beqz    t7, 0x80B52570
.org 0x80B52560
    b       0x80B52570

; =========================================================
; File: 0x00B3C000, Address: 0x00C5C57C, Offset: 0x0012057C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x00000C04
.org 0x801C603C
    sll     at, r0, 0x0010

; =========================================================
; File: 0x00B3C000, Address: 0x00B499C8, Offset: 0x0000D9C8
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   sw      t9, (t1)
;   addiu   t6, r0, 0x0064
;   sw      t6, (t1)
;   lw      t7, (a0)
;   bnel    t7, r0, 0x800B3568
;   lhu     t8, 0x000C(v0)
;   lhu     a2, (v0)
;   nor     t8, a2, t0
;   bnez    t8, 0x800B34D0
;   nor     t7, a2, t2
;   lw      t9, (t1)
;   addiu   t6, r0, 0x0002
;   sw      t6, (a0)
;   addiu   t5, t9, -0x0001
;   bgez    t5, 0x800B3564
;   sw      t5, (t1)
;   b       0x800B3564
;   sw      r0, (t1)
;   bnez    t7, 0x800B350C
;   nor     t9, a2, a3
;   lw      t8, (t1)
;   addiu   t7, r0, 0x0002
;   addiu   t9, t8, 0x0001
;   sll     t5, t9, 0x0003
;   addu    t6, t3, t5
;   lw      a3, (t6)
;   sw      t9, (t1)
;   sw      t7, (a0)
;   sltiu   a3, a3, 0x0001
;   beqz    a3, 0x800B3564
;   addiu   t8, t9, -0x0001
;   b       0x800B3564
;   sw      t8, (t1)
;   bnez    t9, 0x800B3534
;   nor     t8, a2, t4
;   lw      t5, (t1)
;   addiu   t7, r0, 0x0002
;   sw      t7, (a0)
;   addiu   t6, t5, -0x000A
;   bgez    t6, 0x800B3564
;   sw      t6, (t1)
;   b       0x800B3564
;   sw      r0, (t1)
;   bnel    t8, r0, 0x800B3568
;   lhu     t8, 0x000C(v0)
;   lw      t9, (t1)
;   addiu   t6, r0, 0x0002
;   sw      t6, (a0)
;   addiu   t5, t9, 0x000A
;   slti    a2, t5, 0x0065
.org 0x800B3488
    lw      a2, 0x0388(s7)
    lh      a3, (a2)
    addiu   t0, r0, 0x000E
    beq     t0, a3, 0x800B34C0
    nop
    addiu   t1, r0, 0x0033
    beq     t1, a3, 0x800B34F8
    nop
    addiu   t2, r0, 0x0066
    beq     t2, a3, 0x800B34F8
    nop
    addiu   v0, r0, 0x00FF
    jr      ra
    nop
    lh      t4, 0x001C(a2)
    andi    t5, t4, 0x00FF
    addiu   t0, r0, 0x000C
    beq     t0, t5, 0x800B34F8
    nop
    addiu   t1, r0, 0x000D
    beq     t1, t5, 0x800B34F8
    nop
    addiu   t2, r0, 0x0017
    beq     t2, t5, 0x800B34F8
    nop
    addiu   v0, r0, 0x00FF
    jr      ra
    nop
    lui     a1, 0x801F
    addiu   t0, r0, 0x0008
    beq     t0, a0, 0x800B3538
    nop
    addiu   t1, r0, 0x0009
    beq     t1, a0, 0x800B3544
    nop
    addiu   t2, r0, 0x008D
    beq     t2, a0, 0x800B3544
    nop
    addiu   t3, r0, 0x008E
    beq     t3, a0, 0x800B3544
    nop
    addiu   v0, r0, 0x00FF
    jr      ra
    nop
    lbu     v0, -0x0918(a1)
    jr      ra
    nop
    lbu     v0, -0x0917(a1)
    jr      ra
    nop

; =========================================================
; File: 0x00D36FE0, Address: 0x00D370FC, Offset: 0x0000011C
; Name: Item_B_Heart :: Heart Container
; =========================================================
.headersize (0x808BCDF0 - 0x00D36FE0)
; Replaces:
;   jal     0x800B5DB0
.org 0x808BCF0C
    nop

; =========================================================
; File: 0x00F38DA0, Address: 0x00F39A90, Offset: 0x00000CF0
; Name: En_Trt2 :: Kotake (Broom) [?]
; =========================================================
.headersize (0x80AD3380 - 0x00F38DA0)
; Replaces:
;   addiu   a2, r0, 0x005B
.org 0x80AD4070
    addiu   a2, r0, 0x0059

; =========================================================
; File: 0x00B3C000, Address: 0x00C5CC84, Offset: 0x00120C84
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   tge     r0, r0, 0x003F
.org 0x801C6744
    tge     r0, r0, 0x0033

; =========================================================
; File: 0x00F62F50, Address: 0x00F631B8, Offset: 0x00000268
; Name: En_Col_Man :: Piece of Heart
; =========================================================
.headersize (0x80AFDC40 - 0x00F62F50)
; Replaces:
;   lbu     t5, 0x05A0(t5)
;   lui     at, 0x4220
;   andi    t6, t5, 0x0002
.org 0x80AFDEA8
    lbu     t5, 0x3602(t5)
    lui     at, 0x4220
    andi    t6, t5, 0x0004

; =========================================================
; File: 0x00F62F50, Address: 0x00F63168, Offset: 0x00000218
; Name: En_Col_Man :: Piece of Heart
; =========================================================
.headersize (0x80AFDC40 - 0x00F62F50)
; Replaces:
;   lbu     t0, 0x05A0(t0)
;   andi    t1, t0, 0x0002
.org 0x80AFDE58
    lbu     t0, 0x3602(t0)
    andi    t1, t0, 0x0004

; =========================================================
; File: 0x00F62F50, Address: 0x00F63074, Offset: 0x00000124
; Name: En_Col_Man :: Piece of Heart
; =========================================================
.headersize (0x80AFDC40 - 0x00F62F50)
; Replaces:
;   lbu     t6, 0x05A0(t6)
;   lui     t3, 0x80B0
;   lui     t0, 0x80B0
;   andi    t7, t6, 0x0002
.org 0x80AFDD64
    lbu     t6, 0x3602(t6)
    lui     t3, 0x80B0
    lui     t0, 0x80B0
    andi    t7, t6, 0x0004

; =========================================================
; File: 0x00CA7F00, Address: 0x00CAEA58, Offset: 0x00006B58
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   bne     t8, at, 0x808345F8
.org 0x808345E8
    nop

; =========================================================
; File: 0x00DCD2D0, Address: 0x00DCD584, Offset: 0x000002B4
; Name: En_Owl :: Owl
; =========================================================
.headersize (0x8095A510 - 0x00DCD2D0)
; Replaces:
;   .dh 0x55C1
.org 0x8095A7C4
    .dh 0x1000
