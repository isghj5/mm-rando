; =========================================================
; File: 0x00CA7F00, Address: 0x00CD6864, Offset: 0x0002E964
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   .dw 0x8400B0C4 ; lh      r0, -0x4F3C(r0)
;   .dw 0x013F8501
;   .dw 0xAF02013F ; sw      v0, 0x013F(t8)
;   .dw 0x8602AE03 ; lh      v0, -0x51FD(s0)
;   .dw 0x013F8702
;   .dw 0xAE04013F ; sw      a0, 0x013F(s0)
;   .dw 0x8814AC05 ; lwl     s4, -0x53FB(r0)
;   .dw 0x013F8914
;   .dw 0xAB06013F ; swl     a2, 0x013F(t8)
;   .dw 0x8A13BD07 ; lwl     s3, -0x42F9(s0)
;   .dw 0x013F5AA0
;   .dw 0x210800A8 ; addi    t0, t0, 0x00A8
;   .dw 0x5BA02209 ; blezl   sp, 0x80864C4C
;   .dw 0x00A88380
;   .dw 0x090A0090 ; j       0x84280240
;   .dw 0x83A0090B ; lb      r0, 0x090B(sp)
;   .dw 0x00907BA0
;   .dw 0x140C0096 ; bne     r0, t4, 0x8085C694
;   .dw 0x6FA0130D ; ldr     r0, 0x130D(sp)
;   .dw 0x0096796F
;   .dw 0xE30E00A4 ; sc      t6, 0x00A4(t8)
;   .dw 0x7A6EE20F ; cle_s.d w8, w28, w14
;   .dw 0x00A48380
.org 0x8085C3F4
    .dw 0x06576F6F
    .dw 0x6466616C ; daddiu  a2, v1, 0x616C
    .dw 0x6C205465 ; ldr     r0, 0x5465(at)
    .dw 0x6D706C65 ; ldr     s0, 0x6C65(t3)
    .dw 0xBF000000 ; cache   0x0000, (t8)
    .dw 0x02536E6F
    .dw 0x77686561 ; jalx    0x8DA19584
    .dw 0x64205465 ; daddiu  r0, at, 0x5465
    .dw 0x6D706C65 ; ldr     s0, 0x6C65(t3)
    .dw 0xBF000000 ; cache   0x0000, (t8)
    .dw 0x05477265
    .dw 0x61742042 ; daddi   s4, t3, 0x2042
    .dw 0x61792054 ; daddi   t9, t3, 0x2054
    .dw 0x656D706C ; daddiu  t5, t3, 0x706C
    .dw 0x65BF0000 ; daddiu  ra, t5, 0x0000
    .dw 0x0453746F ; bgezall v0, 0x808795F0
    .dw 0x6E652054 ; ldr     a1, 0x2054(s3)
    .dw 0x6F776572 ; ldr     s7, 0x6572(k1)
    .dw 0x2054656D ; addi    s4, v0, 0x656D
    .dw 0x706C65BF ; sdbbp   0x1B196
    .dw 0x48310922
    .dw 0x09240037 ; j       0x849000DC
    .dw 0x00390052

; =========================================================
; File: 0x00CDDC60, Address: 0x00CDEE40, Offset: 0x000011E0
; Name: En_GirlA :: Shop Inventory Data
; =========================================================
.headersize (0x80863870 - 0x00CDDC60)
; Replaces:
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x0001083F ; dsra32  at, at, 0x0000
;   .dw 0x08400000 ; j       0x81000000
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10030 ; tge     a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00010841
;   .dw 0x08420000 ; j       0x81080000
;   .dw 0x0000005C
;   .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10032 ; tlt     a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00010843 ; sra     at, at, 0x0001
;   .dw 0x08440000 ; j       0x81100000
;   .dw 0x0000005D
;   .dw 0x80863A10 ; lb      a2, 0x3A10(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x0272003B
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106B6 ; tne     r0, at, 0x001A
;   .dw 0x06B70000
;   .dw 0x0000005E
;   .dw 0x80864108 ; lb      a2, 0x4108(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0025 ; or      r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x003206BA ; drotr   r0, s2, 0x001A
;   .dw 0x06BB0000
;   .dw 0x00000020 ; add     r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10030 ; tge     a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106B2 ; tlt     r0, at, 0x001A
;   .dw 0x06B30000 ; bgezall s5, 0x80864B00
;   .dw 0x0000005C
;   .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00B30027 ; nor     r0, a1, s3
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106AE
;   .dw 0x06AF0000
;   .dw 0x00000032 ; tlt     r0, r0
;   .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
;   .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x009F001A ; div     r0, a0, ra
;   .dw 0x00000000 ; nop
;   .dw 0x000106B4 ; teq     r0, at, 0x001A
;   .dw 0x06B50000
;   .dw 0x00000019 ; multu   r0, r0
;   .dw 0x80863C6C ; lb      a2, 0x3C6C(a0)
;   .dw 0x80864320 ; lb      a2, 0x4320(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0024 ; and     r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x001E06B8 ; dsll    r0, fp, 0x001A
;   .dw 0x06B90000
;   .dw 0x0000001F ; ddivu   r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00940011
;   .dw 0x800B8118 ; lb      t3, -0x7EE8(r0)
;   .dw 0x000A06B0 ; tge     r0, t2, 0x001A
;   .dw 0x06B10000 ; bgezal  s5, 0x80864B80
;   .dw 0x0000002A ; slt     r0, r0, r0
;   .dw 0x80863B4C ; lb      a2, 0x3B4C(a0)
;   .dw 0x8086425C ; lb      a2, 0x425C(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106AC
;   .dw 0x06AD0000
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x0272003B
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106D3
;   .dw 0x06D40000
;   .dw 0x0000005E
;   .dw 0x80864108 ; lb      a2, 0x4108(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0024 ; and     r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x001E06D5
;   .dw 0x06D60000
;   .dw 0x0000001F ; ddivu   r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0025 ; or      r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x003206D7
;   .dw 0x06D80000
;   .dw 0x00000020 ; add     r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10030 ; tge     a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106CF ; sync    0x001B
;   .dw 0x06D00000 ; bltzal  s6, 0x80864C20
;   .dw 0x0000005C
;   .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00940011
;   .dw 0x800B8118 ; lb      t3, -0x7EE8(r0)
;   .dw 0x000A06CD ; break   0x000A, 0x001B
;   .dw 0x06CE0000 ; tnei    s6, 0x0000
;   .dw 0x0000002A ; slt     r0, r0, r0
;   .dw 0x80863B4C ; lb      a2, 0x3B4C(a0)
;   .dw 0x8086425C ; lb      a2, 0x425C(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x009F001A ; div     r0, a0, ra
;   .dw 0x00000000 ; nop
;   .dw 0x000106D1
;   .dw 0x06D20000 ; bltzall s6, 0x80864C60
;   .dw 0x00000019 ; multu   r0, r0
;   .dw 0x80863C6C ; lb      a2, 0x3C6C(a0)
;   .dw 0x80864320 ; lb      a2, 0x4320(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00B30027 ; nor     r0, a1, s3
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106CB
;   .dw 0x06CC0000 ; teqi    s6, 0x0000
;   .dw 0x00000032 ; tlt     r0, r0
;   .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
;   .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000106C9
;   .dw 0x06CA0000 ; tlti    s6, 0x0000
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x02650010
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000129D9
;   .dw 0x29DA0000 ; slti    k0, t6, 0x0000
;   .dw 0x0000007E ; dsrl32  r0, r0, 0x0001
;   .dw 0x80863D28 ; lb      a2, 0x3D28(a0)
;   .dw 0x8086436C ; lb      a2, 0x436C(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00980017 ; dsrav   r0, t8, a0
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000129DB
;   .dw 0x29DC0000 ; slti    gp, t6, 0x0000
;   .dw 0x0000001B ; divu    r0, r0, r0
;   .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
;   .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00980018 ; mult    a0, t8
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000229DB
;   .dw 0x29DC0000 ; slti    gp, t6, 0x0000
;   .dw 0x0000001C ; dmult   r0, r0
;   .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
;   .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00980019 ; multu   a0, t8
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000329DB
;   .dw 0x29DC0000 ; slti    gp, t6, 0x0000
;   .dw 0x0000001D ; dmultu  r0, r0
;   .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
;   .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00980017 ; dsrav   r0, t8, a0
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00010654
;   .dw 0x06550000
;   .dw 0x0000001B ; divu    r0, r0, r0
;   .dw 0x80863DC8 ; lb      a2, 0x3DC8(a0)
;   .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00980018 ; mult    a0, t8
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00020656
;   .dw 0x06570000
;   .dw 0x0000001C ; dmult   r0, r0
;   .dw 0x80863E48 ; lb      a2, 0x3E48(a0)
;   .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00B00026 ; xor     r0, a1, s0
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0652
;   .dw 0x06530000 ; bgezall s2, 0x80864D80
;   .dw 0x0000001A ; div     r0, r0, r0
;   .dw 0x80863EC8 ; lb      a2, 0x3EC8(a0)
;   .dw 0x8086444C ; lb      a2, 0x444C(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00A5001E ; ddiv    r0, a1, a1
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0650
;   .dw 0x06510000 ; bgezal  s2, 0x80864DA0
;   .dw 0x00000016 ; dsrlv   r0, r0, r0
;   .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
;   .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00B30027 ; nor     r0, a1, s3
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000112DB
;   .dw 0x12DC0000 ; beq     s6, gp, 0x80864DC0
;   .dw 0x00000032 ; tlt     r0, r0
;   .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
;   .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0023 ; subu    r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A12DD
;   .dw 0x12DE0000 ; beq     s6, fp, 0x80864DE0
;   .dw 0x0000001E ; ddiv    r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000112DF
;   .dw 0x12E00000 ; beqz    s7, 0x80864E00
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00A5001E ; ddiv    r0, a1, a1
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0BC5
;   .dw 0x0BC60000 ; j       0x8F180000
;   .dw 0x00000016 ; dsrlv   r0, r0, r0
;   .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
;   .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0023 ; subu    r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0BC7
;   .dw 0x0BC80000 ; j       0x8F200000
;   .dw 0x0000001E ; ddiv    r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00010BC9
;   .dw 0x0BCA0000 ; j       0x8F280000
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00A5001E ; ddiv    r0, a1, a1
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0BCB
;   .dw 0x0BCC0000 ; j       0x8F300000
;   .dw 0x00000016 ; dsrlv   r0, r0, r0
;   .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
;   .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00AF0023 ; subu    r0, a1, t7
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x000A0BCD ; break   0x000A, 0x002F
;   .dw 0x0BCE0000 ; j       0x8F380000
;   .dw 0x0000001E ; ddiv    r0, r0, r0
;   .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
;   .dw 0x80864210 ; lb      a2, 0x4210(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
;   .dw 0x00C10031 ; tgeu    a2, at
;   .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
;   .dw 0x00010BCF ; sync    0x000F
;   .dw 0x0BD00000 ; j       0x8F400000
;   .dw 0x0000005B
;   .dw 0x80863950 ; lb      a2, 0x3950(a0)
;   .dw 0x80864168 ; lb      a2, 0x4168(a0)
;   .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
.org 0x80864A50
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x0001083F ; dsra32  at, at, 0x0000
    .dw 0x08400000 ; j       0x81000000
    .dw 0x000000C3 ; sra     r0, r0, 0x0003
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10030 ; tge     a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00010841
    .dw 0x08420000 ; j       0x81080000
    .dw 0x000000C4
    .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10032 ; tlt     a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00010843 ; sra     at, at, 0x0001
    .dw 0x08440000 ; j       0x81100000
    .dw 0x000000C2 ; srl     r0, r0, 0x0003
    .dw 0x80863A10 ; lb      a2, 0x3A10(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x0272003B
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106B6 ; tne     r0, at, 0x001A
    .dw 0x06B70000
    .dw 0x000000BD
    .dw 0x80864108 ; lb      a2, 0x4108(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0025 ; or      r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x003206BA ; drotr   r0, s2, 0x001A
    .dw 0x06BB0000
    .dw 0x000000C1
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10030 ; tge     a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106B2 ; tlt     r0, at, 0x001A
    .dw 0x06B30000 ; bgezall s5, 0x80864B00
    .dw 0x000000BB ; dsra    r0, r0, 0x0002
    .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00B30027 ; nor     r0, a1, s3
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106AE
    .dw 0x06AF0000
    .dw 0x000000BC ; dsll32  r0, r0, 0x0002
    .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
    .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x009F001A ; div     r0, a0, ra
    .dw 0x00000000 ; nop
    .dw 0x000106B4 ; teq     r0, at, 0x001A
    .dw 0x06B50000
    .dw 0x000000BE ; dsrl32  r0, r0, 0x0002
    .dw 0x80863C6C ; lb      a2, 0x3C6C(a0)
    .dw 0x80864320 ; lb      a2, 0x4320(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0024 ; and     r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x001E06B8 ; dsll    r0, fp, 0x001A
    .dw 0x06B90000
    .dw 0x000000BF ; dsra32  r0, r0, 0x0002
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00940011
    .dw 0x800B8118 ; lb      t3, -0x7EE8(r0)
    .dw 0x000A06B0 ; tge     r0, t2, 0x001A
    .dw 0x06B10000 ; bgezal  s5, 0x80864B80
    .dw 0x000000C0 ; ehb
    .dw 0x80863B4C ; lb      a2, 0x3B4C(a0)
    .dw 0x8086425C ; lb      a2, 0x425C(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106AC
    .dw 0x06AD0000
    .dw 0x000000CD ; break   0x0000, 0x0003
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x0272003B
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106D3
    .dw 0x06D40000
    .dw 0x000000BD
    .dw 0x80864108 ; lb      a2, 0x4108(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0024 ; and     r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x001E06D5
    .dw 0x06D60000
    .dw 0x000000BF ; dsra32  r0, r0, 0x0002
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0025 ; or      r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x003206D7
    .dw 0x06D80000
    .dw 0x000000C1
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10030 ; tge     a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106CF ; sync    0x001B
    .dw 0x06D00000 ; bltzal  s6, 0x80864C20
    .dw 0x000000BB ; dsra    r0, r0, 0x0002
    .dw 0x808639B0 ; lb      a2, 0x39B0(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00940011
    .dw 0x800B8118 ; lb      t3, -0x7EE8(r0)
    .dw 0x000A06CD ; break   0x000A, 0x001B
    .dw 0x06CE0000 ; tnei    s6, 0x0000
    .dw 0x000000C0 ; ehb
    .dw 0x80863B4C ; lb      a2, 0x3B4C(a0)
    .dw 0x8086425C ; lb      a2, 0x425C(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x009F001A ; div     r0, a0, ra
    .dw 0x00000000 ; nop
    .dw 0x000106D1
    .dw 0x06D20000 ; bltzall s6, 0x80864C60
    .dw 0x000000BE ; dsrl32  r0, r0, 0x0002
    .dw 0x80863C6C ; lb      a2, 0x3C6C(a0)
    .dw 0x80864320 ; lb      a2, 0x4320(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00B30027 ; nor     r0, a1, s3
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106CB
    .dw 0x06CC0000 ; teqi    s6, 0x0000
    .dw 0x000000BC ; dsll32  r0, r0, 0x0002
    .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
    .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000106C9
    .dw 0x06CA0000 ; tlti    s6, 0x0000
    .dw 0x000000CD ; break   0x0000, 0x0003
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x02650010
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000129D9
    .dw 0x29DA0000 ; slti    k0, t6, 0x0000
    .dw 0x0000007E ; dsrl32  r0, r0, 0x0001
    .dw 0x80863D28 ; lb      a2, 0x3D28(a0)
    .dw 0x8086436C ; lb      a2, 0x436C(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00980017 ; dsrav   r0, t8, a0
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000129DB
    .dw 0x29DC0000 ; slti    gp, t6, 0x0000
    .dw 0x0000001B ; divu    r0, r0, r0
    .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
    .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00980018 ; mult    a0, t8
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000229DB
    .dw 0x29DC0000 ; slti    gp, t6, 0x0000
    .dw 0x0000001C ; dmult   r0, r0
    .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
    .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00980019 ; multu   a0, t8
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000329DB
    .dw 0x29DC0000 ; slti    gp, t6, 0x0000
    .dw 0x0000001D ; dmultu  r0, r0
    .dw 0x80863D60 ; lb      a2, 0x3D60(a0)
    .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00980017 ; dsrav   r0, t8, a0
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00010654
    .dw 0x06550000
    .dw 0x0000001B ; divu    r0, r0, r0
    .dw 0x80863DC8 ; lb      a2, 0x3DC8(a0)
    .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00980018 ; mult    a0, t8
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00020656
    .dw 0x06570000
    .dw 0x0000001C ; dmult   r0, r0
    .dw 0x80863E48 ; lb      a2, 0x3E48(a0)
    .dw 0x808643B8 ; lb      a2, 0x43B8(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00B00026 ; xor     r0, a1, s0
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0652
    .dw 0x06530000 ; bgezall s2, 0x80864D80
    .dw 0x000000C6
    .dw 0x80863EC8 ; lb      a2, 0x3EC8(a0)
    .dw 0x8086444C ; lb      a2, 0x444C(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00A5001E ; ddiv    r0, a1, a1
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0650
    .dw 0x06510000 ; bgezal  s2, 0x80864DA0
    .dw 0x000000C5 ; lsa     r0, r0, r0, 0x0004
    .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
    .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00B30027 ; nor     r0, a1, s3
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000112DB
    .dw 0x12DC0000 ; beq     s6, gp, 0x80864DC0
    .dw 0x000000CA
    .dw 0x80863C08 ; lb      a2, 0x3C08(a0)
    .dw 0x808642D4 ; lb      a2, 0x42D4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0023 ; subu    r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A12DD
    .dw 0x12DE0000 ; beq     s6, fp, 0x80864DE0
    .dw 0x000000CB
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000112DF
    .dw 0x12E00000 ; beqz    s7, 0x80864E00
    .dw 0x000000CC ; syscall 0x0003
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00A5001E ; ddiv    r0, a1, a1
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0BC5
    .dw 0x0BC60000 ; j       0x8F180000
    .dw 0x000000C7
    .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
    .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0023 ; subu    r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0BC7
    .dw 0x0BC80000 ; j       0x8F200000
    .dw 0x000000C8
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00010BC9
    .dw 0x0BCA0000 ; j       0x8F280000
    .dw 0x000000C9
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00A5001E ; ddiv    r0, a1, a1
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0BCB
    .dw 0x0BCC0000 ; j       0x8F300000
    .dw 0x000000C7
    .dw 0x80863F94 ; lb      a2, 0x3F94(a0)
    .dw 0x808644A4 ; lb      a2, 0x44A4(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00AF0023 ; subu    r0, a1, t7
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x000A0BCD ; break   0x000A, 0x002F
    .dw 0x0BCE0000 ; j       0x8F380000
    .dw 0x000000C8
    .dw 0x80863AAC ; lb      a2, 0x3AAC(a0)
    .dw 0x80864210 ; lb      a2, 0x4210(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)
    .dw 0x00C10031 ; tgeu    a2, at
    .dw 0x800B8050 ; lb      t3, -0x7FB0(r0)
    .dw 0x00010BCF ; sync    0x000F
    .dw 0x0BD00000 ; j       0x8F400000
    .dw 0x000000C9
    .dw 0x80863950 ; lb      a2, 0x3950(a0)
    .dw 0x80864168 ; lb      a2, 0x4168(a0)
    .dw 0x808646A4 ; lb      a2, 0x46A4(a0)

; =========================================================
; File: 0x00B3C000, Address: 0x00B4F020, Offset: 0x00013020
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   slti    at, a2, 0x00BA
;   bnel    at, r0, 0x800B8B08
.org 0x800B8AE0
    addiu   at, r0, 0x00BA
    bnel    a2, at, 0x800B8B08

; =========================================================
; File: 0x00B3C000, Address: 0x00B4F090, Offset: 0x00013090
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   slti    at, a2, 0x00BA
;   beqz    at, 0x800B8B68
.org 0x800B8B50
    addiu   at, r0, 0x00BA
    beq     a2, at, 0x800B8B68

; =========================================================
; File: 0x0104EAC0, Address: 0x0104FAAC, Offset: 0x00000FEC
; Name: En_Dt :: Mayor Dotour (Gameplay)
; =========================================================
.headersize (0x80BE9B20 - 0x0104EAC0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BEAB0C
    addiu   a2, r0, 0x0003

; =========================================================
; File: 0x00ED6B10, Address: 0x00ED78FC, Offset: 0x00000DEC
; Name: En_Mm3 :: Postman (Counting Game)
; =========================================================
.headersize (0x80A6F0A0 - 0x00ED6B10)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80A6FE8C
    addiu   a2, r0, 0x00CE

; =========================================================
; File: 0x01060400, Address: 0x01061250, Offset: 0x00000E50
; Name: En_Rz :: Rosa Sisters
; =========================================================
.headersize (0x80BFB480 - 0x01060400)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BFC2D0
    addiu   a2, r0, 0x002B

; =========================================================
; File: 0x01060400, Address: 0x010612BC, Offset: 0x00000EBC
; Name: En_Rz :: Rosa Sisters
; =========================================================
.headersize (0x80BFB480 - 0x01060400)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BFC33C
    addiu   a2, r0, 0x002B

; =========================================================
; File: 0x01062260, Address: 0x01062E64, Offset: 0x00000C04
; Name: En_Bjt :: Hand in Toilet
; =========================================================
.headersize (0x80BFD2E0 - 0x01062260)
; Replaces:
;   sll     r0, t4, 0x0000
.org 0x80BFDEE4
    .dw 0x002C0000

; =========================================================
; File: 0x01024D90, Address: 0x01026533, Offset: 0x000017A3
; Name: En_Nb :: Anju's Grandmother (Gameplay)
; =========================================================
.headersize (0x80BBFDB0 - 0x01024D90)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80BC1553
    .dw 0x002D0000

; =========================================================
; File: 0x01024D90, Address: 0x010264F8, Offset: 0x00001768
; Name: En_Nb :: Anju's Grandmother (Gameplay)
; =========================================================
.headersize (0x80BBFDB0 - 0x01024D90)
; Replaces:
;   sll     r0, t4, 0x0000
.org 0x80BC1518
    .dw 0x002F0000

; =========================================================
; File: 0x0106E050, Address: 0x0106E630, Offset: 0x000005E0
; Name: En_Kitan :: Keaton
; =========================================================
.headersize (0x80C090D0 - 0x0106E050)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80C096B0
    addiu   a2, r0, 0x0030

; =========================================================
; File: 0x00F8B5A0, Address: 0x00F8CA2C, Offset: 0x0000148C
; Name: En_Kendo_Js :: Swordsman
; =========================================================
.headersize (0x80B262A0 - 0x00F8B5A0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80B2772C
    addiu   a2, r0, 0x009F

; =========================================================
; File: 0x00F90B30, Address: 0x00F916C7, Offset: 0x00000B97
; Name: En_Pst :: Postbox
; =========================================================
.headersize (0x80B2B830 - 0x00F90B30)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80B2C3C7
    .dw 0x00A20000

; =========================================================
; File: 0x0102FBB0, Address: 0x01030920, Offset: 0x00000D70
; Name: En_Scopenuts :: Business Scrub (Observatory Telescope) & Grotto
; =========================================================
.headersize (0x80BCABF0 - 0x0102FBB0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BCB960
    addiu   a2, r0, 0x00A5

; =========================================================
; File: 0x00E325D0, Address: 0x00E340E0, Offset: 0x00001B10
; Name: En_Aob_01 :: Mamamu Yan
; =========================================================
.headersize (0x809C10B0 - 0x00E325D0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x809C2BC0
    addiu   a2, r0, 0x00B1

; =========================================================
; File: 0x0101FC80, Address: 0x010203D4, Offset: 0x00000754
; Name: En_Zos :: Evan
; =========================================================
.headersize (0x80BBACA0 - 0x0101FC80)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BBB3F4
    addiu   a2, r0, 0x00B0

; =========================================================
; File: 0x00F748F0, Address: 0x00F75C50, Offset: 0x00001360
; Name: En_Gb2 :: Ghost Hut Proprietor & Ikana Guard
; =========================================================
.headersize (0x80B0F5E0 - 0x00F748F0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80B10940
    addiu   a2, r0, 0x00B2

; =========================================================
; File: 0x00F748F0, Address: 0x00F75C8A, Offset: 0x0000139A
; Name: En_Gb2 :: Ghost Hut Proprietor & Ikana Guard
; =========================================================
.headersize (0x80B0F5E0 - 0x00F748F0)
; Replaces:
;   .dh 0x000C
.org 0x80B1097A
    .dh 0x00B2

; =========================================================
; File: 0x00DD44A0, Address: 0x00DD6370, Offset: 0x00001ED0
; Name: En_Fu :: Honey & Darling (Gameplay)
; =========================================================
.headersize (0x809616E0 - 0x00DD44A0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x809635B0
    addiu   a2, r0, 0x0094

; =========================================================
; File: 0x00F4EDA0, Address: 0x00F509B0, Offset: 0x00001C10
; Name: En_Lift_Nuts :: Deku Scrub Playground Employee
; =========================================================
.headersize (0x80AE9A20 - 0x00F4EDA0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80AEB630
    addiu   a2, r0, 0x0031

; =========================================================
; File: 0x00E379E0, Address: 0x00E39380, Offset: 0x000019A0
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x809C7E60
    addiu   a2, r0, 0x0090

; =========================================================
; File: 0x00E379E0, Address: 0x00E390E0, Offset: 0x00001700
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x809C7BC0
    addiu   a2, r0, 0x00A6

; =========================================================
; File: 0x00E0A810, Address: 0x00E0B6C0, Offset: 0x00000EB0
; Name: En_Gs :: Gossip Stone
; =========================================================
.headersize (0x80997A90 - 0x00E0A810)
; Replaces:
;   addiu   t5, r0, 0x000C
.org 0x80998940
    addiu   t5, r0, 0x00A3

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB7858, Offset: 0x0000F958
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   addiu   t6, t5, -0x0006
;   addu    v1, t6, t7
;   lbu     a0, (v1)
;   addiu   at, r0, 0x00FF
;   beq     a0, at, 0x8083D490
;   nop
;   jal     0x80114978
;   sw      v1, 0x0024(sp)
.org 0x8083D3E8
    jal     0x801449A4
    or      a0, t4, r0
    lbu     a0, (v0)
    addiu   at, r0, 0x00FF
    beq     at, a0, 0x8083D490
    or      v1, v0, r0
    jal     0x80114978
    sw      v0, 0x0024(sp)

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB78F4, Offset: 0x0000F9F4
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   sll     t7, t7, 0x0001
;   addiu   t9, t7, -0x0006
;   addu    v1, t9, t0
.org 0x8083D484
    jal     0x801449A4
    or      a0, t6, r0
    or      v1, v0, r0

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB7938, Offset: 0x0000FA38
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   lh      a1, 0x0004(v1)
.org 0x8083D4C8
    lh      a1, 0x0006(v1)

; =========================================================
; File: 0x00CA7F00, Address: 0x00CA915C, Offset: 0x0000125C
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   sll     t7, t6, 0x0002
;   subu    t7, t7, t6
;   sll     t7, t7, 0x0001
;   addiu   t8, t7, -0x0006
;   addu    v0, t8, t9
;   lb      v1, 0x0002(v0)
;   bgez    v1, 0x8082ED14
;   negu    t0, v1
;   jr      ra
;   sb      t0, 0x0B2A(a0)
;   sb      v1, 0x0B2A(a0)
;   jr      ra
.org 0x8082ECEC
    addiu   sp, sp, -0x0020
    sw      ra, 0x0014(sp)
    nop
    jal     0x801449A4
    or      a0, t6, r0
    lb      v1, 0x0002(v0)
    bgez    v1, 0x8082ED10
    lw      ra, 0x0014(sp)
    negu    v1, v1
    sb      v1, 0x0B2A(s0)
    jr      ra
    addiu   sp, sp, 0x0020

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB76E4, Offset: 0x0000F7E4
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   slti    at, v0, 0x00BA
;   beqz    at, 0x8083D6C4
.org 0x8083D274
    addiu   at, r0, 0x00BA
    beq     at, v0, 0x8083D6C4

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB7700, Offset: 0x0000F800
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   addiu   t0, t9, -0x0006
;   addu    a2, t0, t1
;   sw      s0, 0x0120(t7)
;   lbu     a0, (a2)
;   jal     0x80114978
;   sw      a2, 0x0028(sp)
.org 0x8083D290
    jal     0x801449A4
    or      a0, v0, r0
    sw      s0, 0x0120(t7)
    lbu     a0, (v0)
    jal     0x800B3488
    sw      v0, 0x0028(sp)

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB7724, Offset: 0x0000F824
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   lh      t2, 0x0004(a2)
.org 0x8083D2B4
    lh      t2, 0x0006(a2)

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB7754, Offset: 0x0000F854
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   lh      a1, 0x0004(a2)
.org 0x8083D2E4
    lh      a1, 0x0006(a2)

; =========================================================
; File: 0x00CA7F00, Address: 0x00CC2794, Offset: 0x0001A894
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   addiu   t8, t7, -0x0006
;   addiu   t1, r0, 0x0001
;   addu    v0, t8, t9
;   sb      t1, 0x0AE7(a2)
;   lbu     a1, 0x0003(v0)
;   sw      a3, 0x0020(sp)
;   sw      a2, 0x0024(sp)
;   sw      v0, 0x001C(sp)
;   jal     0x801518B0
;   or      a0, a3, r0
;   lw      v0, 0x001C(sp)
;   lw      a0, 0x0020(sp)
;   jal     0x80112E80
;   lbu     a1, (v0)
;   lw      t2, 0x0024(sp)
;   lh      v1, 0x0384(t2)
;   slti    at, v1, 0x0078
;   bnez    at, 0x80848388
;   slti    at, v1, 0x0090
;   beqz    at, 0x80848388
;   nop
;   jal     0x801A3098
;   addiu   a0, r0, 0x0037
;   b       0x808484BC
;   or      v0, r0, r0
;   blez    v1, 0x80848394
;   slti    at, v1, 0x0004
;   bnez    at, 0x808483A0
;   addiu   at, r0, 0x000A
;   bnel    v1, at, 0x808483B4
;   addiu   at, r0, 0x000D
;   jal     0x8019F0C8
;   addiu   a0, r0, 0x4831
;   b       0x808484BC
;   or      v0, r0, r0
;   addiu   at, r0, 0x000D
;   beq     v1, at, 0x808483E0
;   addiu   v0, r0, 0x000C
;   bne     v0, v1, 0x808483E8
;   lui     t0, 0x801F
;   addiu   t0, t0, -0x0990
;   lw      t3, 0x00BC(t0)
;   lui     at, 0xF000
;   and     t4, t3, at
;   lui     at, 0x4000
;   bne     t4, at, 0x808483E8
;   nop
;   b       0x80848410
;   addiu   v0, r0, 0x0924
;   beq     v0, v1, 0x80848400
;   slti    at, v1, 0x0005
;   bnez    at, 0x80848408
;   slti    at, v1, 0x0008
;   beql    at, r0, 0x8084840C
;   addiu   v1, r0, 0x0922
;   b       0x8084840C
;   addiu   v1, r0, 0x0039
;   addiu   v1, r0, 0x0922
;   or      v0, v1, r0
;   jal     0x801A3098
;   andi    a0, v0, 0xFFFF
;   b       0x808484BC
.org 0x80848324
    sw      t9, 0x0018(sp)
    jal     0x801449A4
    or      a0, v1, r0
    addiu   t0, r0, 0x0001
    sb      t0, 0x0AE7(s0)
    lhu     a1, 0x0004(v0)
    sw      v0, 0x001C(sp)
    jal     0x801518B0
    or      a0, s1, r0
    lw      v0, 0x001C(sp)
    lw      t9, 0x0018(sp)
    lb      t0, 0x0003(v0)
    andi    t0, t0, 0x000F
    sll     t1, t0, 0x0001
    addu    t8, t9, t1
    bne     r0, t0, 0x80848378
    lhu     a0, 0x0050(t8)
    jal     0x8019F0C8
    nop
    b       0x80848380
    nop
    jal     0x801A3098
    nop
    lw      v0, 0x001C(sp)
    lb      t0, 0x0003(v0)
    andi    t0, t0, 0x00F0
    srl     t1, t0, 0x0004
    addiu   t2, t1, -0x0001
    nop
    lbu     a1, (v0)
    jal     0x80112E80
    or      a0, s1, r0
    b       0x808484BC
    or      v0, r0, r0
    slti    t3, t2, 0x0004
    bne     r0, t3, 0x808483C8
    nop
    jal     0x8012EF0C
    or      a0, a1, r0
    b       0x808484BC
    or      v0, r0, r0
    lui     t9, 0x801F
    addu    t8, t9, t2
    bnel    r0, a1, 0x808483E8
    lb      t6, -0x08D0(t8)
    lb      t6, -0x08C6(t8)
    addiu   t7, t6, 0x0001
    b       0x808483F0
    sb      t7, -0x08C6(t8)
    or      t7, t6, a1
    sb      t7, -0x08D0(t8)
    lw      t9, 0x0018(sp)
    sll     t0, t2, 0x0004
    sll     t1, t2, 0x0002
    addu    a0, t0, t1
    addu    a1, a0, t9
    lui     a0, 0x8040
    addiu   a0, a0, -0x31E5
    jal     0x800FEC90
    addiu   a2, r0, 0x0014
    b       0x808484BC
    or      v0, r0, r0

; =========================================================
; File: 0x00ECC620, Address: 0x00ECD790, Offset: 0x00001170
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   addiu   a2, a2, 0x0008
.org 0x80A65610
    addiu   a2, r0, 0x0008

; =========================================================
; File: 0x00ECC620, Address: 0x00ECD810, Offset: 0x000011F0
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80A65690
    addiu   a2, r0, 0x0108

; =========================================================
; File: 0x00DC53E0, Address: 0x00DC5630, Offset: 0x00000250
; Name: En_Ms :: Magic Bean Seller
; =========================================================
.headersize (0x80952620 - 0x00DC53E0)
; Replaces:
;   addiu   a2, r0, 0x0035
.org 0x80952870
    addiu   a2, r0, 0x011E

; =========================================================
; File: 0x00CA7F00, Address: 0x00CB76F0, Offset: 0x0000F7F0
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   subu    t9, t9, v0
;   sll     t9, t9, 0x0001
.org 0x8083D280
    jal     0x801DC790
    or      a0, v0, r0

; =========================================================
; File: 0x00B3C000, Address: 0x00C72CD0, Offset: 0x00136CD0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x325D0000 ; andi    sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x335D0000 ; andi    sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x345D0000 ; ori     sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x355D0000 ; ori     sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x365D0000 ; ori     sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x375D0000 ; ori     sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x385D0000 ; xori    sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x395D0000 ; xori    sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
.org 0x801DC790
    addiu   sp, sp, -0x0020
    lui     t0, 0x801F
    lh      t0, -0x0986(t0)
    bnel    t0, r0, 0x801DC828
    or      v1, a0, r0
    sw      ra, 0x001C(sp)
    sw      a1, 0x0014(sp)
    jal     0x800B343C
    sw      a0, 0x0018(sp)
    bnez    v0, 0x801DC7D0
    lw      a1, 0x0014(sp)
    beqz    a1, 0x801DC7CC
    nop
    jal     0x801449D4
    or      a0, v1, r0
    lw      v1, 0x0018(sp)
    or      at, r0, r0
    addiu   t1, r0, 0x0102
    lui     t2, 0x801E
    addiu   t2, t2, -0x37C0
    addu    t3, t2, at
    lhu     t0, (t3)
    beq     t0, v1, 0x801DC800
    addiu   at, at, 0x0002
    bne     t1, at, 0x801DC7E0
    nop
    bnel    v0, r0, 0x801DC800
    addiu   v1, r0, 0x000A
    lw      a1, 0x0014(sp)
    beq     r0, a1, 0x801DC818
    nop
    jal     0x801449D4
    lw      a0, 0x0018(sp)
    nop
    lw      t0, 0x1CCC(s6)
    beql    t0, s0, 0x801DC824
    sh      v1, 0x0384(s0)
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0020
    jr      ra
    or      v0, v1, r0

; =========================================================
; File: 0x00B3C000, Address: 0x00C72D80, Offset: 0x00136D80
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x315D0000 ; andi    sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x325D0000 ; andi    sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x335D0000 ; andi    sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x345D0000 ; ori     sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x355D0000 ; ori     sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x365D0000 ; ori     sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x375D0000 ; ori     sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x385D0000 ; xori    sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B39 ; daddiu  a3, t3, 0x5B39
;   .dw 0x395D0000 ; xori    sp, t2, 0x0000
.org 0x801DC840
    .dw 0x00010002 ; srl     r0, at, 0x0000
    .dw 0x00040005 ; lsa     r0, r0, a0, 0x0001
    .dw 0x00060007 ; srav    r0, a2, r0
    .dw 0x000A000B ; movn    r0, r0, t2
    .dw 0x000E000F ; sync
    .dw 0x00100011
    .dw 0x00120013
    .dw 0x00150016 ; dsrlv   r0, s5, r0
    .dw 0x0019001A ; div     r0, r0, t9
    .dw 0x001E001F ; ddivu   r0, r0, fp
    .dw 0x00210028
    .dw 0x002A0032 ; tlt     at, t2
    .dw 0x00340035
    .dw 0x0036003A ; drotr   r0, s6, 0x0000
    .dw 0x00450051
    .dw 0x0052005B
    .dw 0x005C005D
    .dw 0x005E005F
    .dw 0x00610062
    .dw 0x00630065
    .dw 0x00660067
    .dw 0x00680069
    .dw 0x006B006D
    .dw 0x006E0091
    .dw 0x00920093
    .dw 0x009B009C
    .dw 0x009D009E
    .dw 0x00A400A9
    .dw 0x00B300CF ; sync    0x0003
    .dw 0x00BB00BC
    .dw 0x00BD00BE
    .dw 0x00BF00C0
    .dw 0x00C100C2
    .dw 0x00C300C4
    .dw 0x00C500C6
    .dw 0x00C700C8
    .dw 0x00C900CA
    .dw 0x00CB00CC ; syscall 0x32C03
    .dw 0x00CD010A
    .dw 0x00D200D3
    .dw 0x00D500D7
    .dw 0x01090000
    .dw 0x00000000 ; nop
    .dw 0x00000000 ; nop

; =========================================================
; File: 0x00D326A0, Address: 0x00D33F40, Offset: 0x000018A0
; Name: Door_Warp1 :: Blue Warp & Boss Lair Warps
; =========================================================
.headersize (0x808B8490 - 0x00D326A0)
; Replaces:
;   addiu   a2, v0, 0x0054
.org 0x808B9D30
    addiu   a2, r0, 0x0077

; =========================================================
; File: 0x00D326A0, Address: 0x00D326DC, Offset: 0x0000003C
; Name: Door_Warp1 :: Blue Warp & Boss Lair Warps
; =========================================================
.headersize (0x808B8490 - 0x00D326A0)
; Replaces:
;   bnez    t7, 0x808B84DC
.org 0x808B84CC
    nop

; =========================================================
; File: 0x00D326A0, Address: 0x00D32710, Offset: 0x00000070
; Name: Door_Warp1 :: Blue Warp & Boss Lair Warps
; =========================================================
.headersize (0x808B8490 - 0x00D326A0)
; Replaces:
;   bnel    t9, r0, 0x808B8514
.org 0x808B8500
    nop

; =========================================================
; File: 0x00D326A0, Address: 0x00D32738, Offset: 0x00000098
; Name: Door_Warp1 :: Blue Warp & Boss Lair Warps
; =========================================================
.headersize (0x808B8490 - 0x00D326A0)
; Replaces:
;   bnel    t1, r0, 0x808B853C
.org 0x808B8528
    nop

; =========================================================
; File: 0x00D326A0, Address: 0x00D32760, Offset: 0x000000C0
; Name: Door_Warp1 :: Blue Warp & Boss Lair Warps
; =========================================================
.headersize (0x808B8490 - 0x00D326A0)
; Replaces:
;   bnel    t3, r0, 0x808B8560
.org 0x808B8550
    nop

; =========================================================
; File: 0x01062260, Address: 0x01062E80, Offset: 0x00000C20
; Name: En_Bjt :: Hand in Toilet
; =========================================================
.headersize (0x80BFD2E0 - 0x01062260)
; Replaces:
;   bltz    s0, 0x80BFE704
.org 0x80BFDF00
    bltz    s0, 0x80C08F04

; =========================================================
; File: 0x00F90B30, Address: 0x00F916D4, Offset: 0x00000BA4
; Name: En_Pst :: Postbox
; =========================================================
.headersize (0x80B2B830 - 0x00F90B30)
; Replaces:
;   jal     0x80180004
.org 0x80B2C3D4
    jal     0x80180288

; =========================================================
; File: 0x00EBA820, Address: 0x00EBAE9B, Offset: 0x0000067B
; Name: En_Dnh :: Boat Cruise Target Spot
; =========================================================
.headersize (0x80A50D40 - 0x00EBA820)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80A513BB
    .dw 0x00A80000

; =========================================================
; File: 0x00EFC060, Address: 0x00EFE654, Offset: 0x000025F4
; Name: En_Az :: Beaver Bros.
; =========================================================
.headersize (0x80A94A30 - 0x00EFC060)
; Replaces:
;   addiu   t5, r0, 0x000C
.org 0x80A97024
    addiu   t5, r0, 0x00AD

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC864, Offset: 0x000009B4
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   beqz    v0, 0x80B676FC
;   addiu   t4, r0, 0x0008
.org 0x80B676E4
    b       0x80B676FC
    addiu   t4, r0, 0x0009

; =========================================================
; File: 0x00E7AD60, Address: 0x00E800C9, Offset: 0x00005369
; Name: En_Go :: Goron (Generic) [?]
; =========================================================
.headersize (0x80A10FD0 - 0x00E7AD60)
; Replaces:
;   .dw 0x00340000
.org 0x80A16339
    .dw 0x01230000

; =========================================================
; File: 0x010788A0, Address: 0x01079520, Offset: 0x00000C80
; Name: En_Jgame_Tsn :: Fisherman (Fisherman's Jumping Game)
; =========================================================
.headersize (0x80C13930 - 0x010788A0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80C145B0
    addiu   a2, r0, 0x00AF

; =========================================================
; File: 0x00D1D880, Address: 0x00D1E8D8, Offset: 0x00001058
; Name: En_Minifrog :: Frog I [?]
; =========================================================
.headersize (0x808A3670 - 0x00D1D880)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x808A46C8
    addiu   a2, r0, 0x00AC

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C325, Offset: 0x00000E75
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80AE6FA5
    .dw 0x00A70000

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C35E, Offset: 0x00000EAE
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80AE6FDE
    .dw 0x00A70000

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C4AF, Offset: 0x00000FFF
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dw 0x000C0000 ; sll     r0, t4, 0x0000
.org 0x80AE712F
    .dw 0x00A70000

; =========================================================
; File: 0x00FC0470, Address: 0x00FC12F8, Offset: 0x00000E88
; Name: En_Ot :: Seahorse
; =========================================================
.headersize (0x80B5B2E0 - 0x00FC0470)
; Replaces:
;   addiu   t8, r0, 0x0004
;   addiu   t9, r0, 0x000C
.org 0x80B5C168
    addiu   t8, r0, 0x00AE
    addiu   t9, r0, 0x00AE

; =========================================================
; File: 0x00E379E0, Address: 0x00E39318, Offset: 0x00001938
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   addiu   a2, v0, 0x0022
.org 0x809C7DF8
    addiu   a2, r0, 0x0023

; =========================================================
; File: 0x00E379E0, Address: 0x00E39068, Offset: 0x00001688
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   addiu   a2, v1, 0x0022
.org 0x809C7B48
    addiu   a2, r0, 0x0024

; =========================================================
; File: 0x00E379E0, Address: 0x00E39098, Offset: 0x000016B8
; Name: En_Syateki_Man :: Shooting Gallery Proprietors [?]
; =========================================================
.headersize (0x809C64C0 - 0x00E379E0)
; Replaces:
;   addiu   a2, r0, 0x0004
.org 0x809C7B78
    addiu   a2, r0, 0x0024

; =========================================================
; File: 0x00F2F8C0, Address: 0x00F2F9AC, Offset: 0x000000EC
; Name: En_Time_Tag
; =========================================================
.headersize (0x80AC9EA0 - 0x00F2F8C0)
; Replaces:
;   beqz    t8, 0x80AC9F9C
.org 0x80AC9F8C
    nop

; =========================================================
; File: 0x00F2F8C0, Address: 0x00F2FD30, Offset: 0x00000470
; Name: En_Time_Tag
; =========================================================
.headersize (0x80AC9EA0 - 0x00F2F8C0)
; Replaces:
;   beql    t0, r0, 0x80ACA338
.org 0x80ACA310
    nop

; =========================================================
; File: 0x00F2F8C0, Address: 0x00F2FC38, Offset: 0x00000378
; Name: En_Time_Tag
; =========================================================
.headersize (0x80AC9EA0 - 0x00F2F8C0)
; Replaces:
;   lw      a0, 0x001C(sp)
;   jal     0x80152498
;   addiu   a0, a0, 0x4908
;   addiu   at, r0, 0x0005
;   bnel    v0, at, 0x80ACA25C
;   lw      ra, 0x0014(sp)
;   jal     0x80147624
;   lw      a0, 0x001C(sp)
;   beql    v0, r0, 0x80ACA25C
;   lw      ra, 0x0014(sp)
;   jal     0x801477B4
;   lw      a0, 0x001C(sp)
.org 0x80ACA218
    jal     0x80147624
    lw      a0, 0x001C(sp)
    beql    r0, v0, 0x80ACA25C
    lw      ra, 0x0014(sp)
    jal     0x801477B4
    lw      a0, 0x001C(sp)
    lui     t0, 0x803F
    lw      t1, -0x7814(t0)
    lw      a1, 0x0018(sp)
    addiu   a2, r0, 0x0070
    sh      a2, 0x0384(t1)
    sw      a1, 0x0388(t1)

; =========================================================
; File: 0x00F1A780, Address: 0x00F1D120, Offset: 0x000029A0
; Name: En_Mnk :: Monkey
; =========================================================
.headersize (0x80AB4D10 - 0x00F1A780)
; Replaces:
;   addiu   sp, sp, -0x0030
;   sw      s0, 0x0018(sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C(sp)
;   sw      a1, 0x0034(sp)
;   jal     0x80136CD0
;   addiu   a0, s0, 0x0148
;   jal     0x80136CD0
;   addiu   a0, s0, 0x02A0
;   lw      t6, 0x0034(sp)
;   lui     at, 0x0001
;   lui     v1, 0x0001
;   addu    t7, t6, at
;   sw      t7, 0x0024(sp)
;   addu    v1, v1, t6
;   lhu     v1, 0x680C(v1)
;   slti    at, v1, 0x1B5D
;   bnez    at, 0x80AB770C
;   addiu   at, r0, 0x1B66
;   beql    v1, at, 0x80AB77A4
;   or      a0, s0, r0
;   b       0x80AB77EC
;   lw      a0, 0x0034(sp)
;   slti    at, v1, 0x08E3
;   bnez    at, 0x80AB772C
.org 0x80AB76B0
    addiu   sp, sp, -0x0030
    sw      ra, 0x001C(sp)
    lui     at, 0x41A0
    lui     a3, 0x42C8
    sw      at, 0x0010(sp)
    jal     0x800B8A1C
    addiu   a2, r0, 0x0073
    beq     r0, v0, 0x80AB7704
    nop
    or      a0, s0, r0
    jal     0x800B89F8
    or      a1, s7, r0
    beq     r0, v0, 0x80AB7704
    nop
    jal     0x800B670C
    or      a0, s0, r0
    lui     t0, 0x8040
    ori     t1, r0, 0x5010
    sw      t1, -0x0C68(t0)
    addiu   t2, r0, 0x0001
    sb      t2, -0x0996(t0)
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0030
    jr      ra
    nop

; =========================================================
; File: 0x00FB55A0, Address: 0x00FB65A0, Offset: 0x00001000
; Name: En_Gk :: Goron Elder's Son
; =========================================================
.headersize (0x80B50410 - 0x00FB55A0)
; Replaces:
;   addiu   sp, sp, -0x0020
;   sw      s0, 0x0018(sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C(sp)
;   lui     at, 0x42C8
;   mtc1    at, f6
;   lwc1    f4, 0x0098(s0)
;   lw      v0, 0x1CCC(a1)
;   c.lt.s  f4, f6
;   nop
;   bc1fl   0x80B514F4
;   lhu     t5, 0x01E4(s0)
;   lbu     t6, 0x014B(v0)
;   addiu   at, r0, 0x0001
;   bne     t6, at, 0x80B51480
;   lui     at, 0x0001
;   addu    v0, a1, at
;   lhu     t7, 0x6932(v0)
;   addiu   at, r0, 0x0003
;   bne     t7, at, 0x80B51480
;   nop
;   lhu     t8, 0x6936(v0)
;   addiu   at, r0, 0x000E
;   bne     t8, at, 0x80B51480
;   nop
;   lhu     t9, 0x01E4(s0)
;   ori     t0, t9, 0x0020
;   sh      t0, 0x01E4(s0)
;   jal     0x800F1BE4
;   lh      a0, 0x0318(s0)
;   beqz    v0, 0x80B514B4
;   or      a0, s0, r0
;   jal     0x800B8EC8
;   addiu   a1, r0, 0x3A17
;   lh      a0, 0x0318(s0)
;   jal     0x800F1D84
.org 0x80B51410
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    lui     t9, 0x801F
    lb      t8, -0x0970(t9)
    addiu   t7, r0, 0x0001
    bne     t7, t8, 0x80B51490
    nop
    lb      t0, 0x031A(s0)
    bne     r0, t0, 0x80B51478
    nop
    addiu   a0, s0, 0x0008
    jal     0x800FF92C
    addiu   a1, s7, 0x0008
    cvt.w.s f0, f0
    nop
    mfc1    t0, f0
    addiu   t1, r0, 0x00C8
    sltu    v0, t0, t1
    beq     r0, v0, 0x80B51490
    nop
    jal     0x801DC650
    addiu   a2, r0, 0x0074
    lb      t0, 0x031A(s0)
    addiu   t2, r0, 0x0001
    b       0x80B51490
    sb      t2, 0x031A(s0)
    lw      t4, 0x0388(s7)
    bne     r0, t4, 0x80B51490
    nop
    addiu   a0, r0, 0x000A
    jal     0x800F1D84
    or      a1, s0, r0
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0020
    jr      ra
    nop

; =========================================================
; File: 0x00F12010, Address: 0x00F12A58, Offset: 0x00000A48
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   jal     0x80112E80
.org 0x80AACFE8
    nop

; =========================================================
; File: 0x00F12010, Address: 0x00F12408, Offset: 0x000003F8
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   sw      a0, 0x0018(sp)
;   or      a2, a1, r0
;   lui     at, 0x0001
;   ori     at, at, 0x7D88
;   addu    a0, a2, at
;   jal     0x8012F608
;   addiu   a1, r0, 0x0119
;   bltz    v0, 0x80AAC9CC
;   lw      v1, 0x0018(sp)
.org 0x80AAC998
    jal     0x800B8BD0
    addiu   a2, r0, 0x0079
    beqz    v0, 0x80AAC9CC
    or      a0, s1, r0
    jal     0x800B89F8
    or      a1, s7, r0
    beqz    v0, 0x80AAC9CC
    nop
    sw      r0, 0x0138(s1)

; =========================================================
; File: 0x00F12010, Address: 0x00F12434, Offset: 0x00000424
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   sb      v0, 0x018F(v1)
;   sw      t6, 0x0188(v1)
.org 0x80AAC9C4
    sb      v0, 0x018F(s1)
    sw      t6, 0x0188(s1)

; =========================================================
; File: 0x02A6B000, Address: 0x02A6B52C, Offset: 0x0000052C
; Name: Z2_GORON_HAKA :: Goron Graveyard - Scene File
; =========================================================
; Replaces:
;   .dw 0x00000BB8
.orga 0x02A6B52C
    .dw 0x000000EF

; =========================================================
; File: 0x00F12010, Address: 0x00F12B6C, Offset: 0x00000B5C
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   jal     0x80112E80
.org 0x80AAD0FC
    nop

; =========================================================
; File: 0x026BF000, Address: 0x026C05D0, Offset: 0x000015D0
; Name: Z2_30GYOSON :: Great Bay Coast - Scene File
; =========================================================
; Replaces:
;   .dw 0x000003E8
.orga 0x026C05D0
    .dw 0x00000127

; =========================================================
; File: 0x00F12010, Address: 0x00F12794, Offset: 0x00000784
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   sw      a0, 0x0018(sp)
;   or      a2, a1, r0
;   lui     at, 0x0001
;   ori     at, at, 0x7D88
;   addu    a0, a2, at
;   jal     0x8012F608
;   addiu   a1, r0, 0x011A
;   bltz    v0, 0x80AACD58
;   lw      v1, 0x0018(sp)
;   lui     t6, 0x80AB
;   addiu   t6, t6, -0x3298
;   sb      v0, 0x018F(v1)
;   sw      t6, 0x0188(v1)
;   lw      ra, 0x0014(sp)
;   addiu   sp, sp, 0x0018
;   jr      ra
;   nop
.org 0x80AACD24
    jal     0x800B8BD0
    addiu   a2, r0, 0x007A
    beq     r0, v0, 0x80AACD58
    or      a0, s1, r0
    jal     0x800B89F8
    or      a1, s7, r0
    beq     r0, v0, 0x80AACD58
    ori     t1, r0, 0x68A0
    lui     t0, 0x8040
    sw      t1, -0x0C68(t0)
    addiu   t2, r0, 0x0001
    sb      t2, -0x0996(t0)
    sw      r0, 0x0138(s1)
    lw      ra, 0x0014(sp)
    addiu   sp, sp, 0x0018
    jr      ra
    nop

; =========================================================
; File: 0x00DCC0A0, Address: 0x00DCCCA0, Offset: 0x00000C00
; Name: En_Mk :: Marine Scientist
; =========================================================
.headersize (0x809592E0 - 0x00DCC0A0)
; Replaces:
;   jal     0x80112E80
.org 0x80959EE0
    nop

; =========================================================
; File: 0x00DCC0A0, Address: 0x00DCCAE8, Offset: 0x00000A48
; Name: En_Mk :: Marine Scientist
; =========================================================
.headersize (0x809592E0 - 0x00DCC0A0)
; Replaces:
;   addiu   sp, sp, -0x0020
;   sw      s0, 0x0018(sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C(sp)
;   sw      a1, 0x0024(sp)
;   jal     0x80136CD0
;   addiu   a0, s0, 0x0190
;   lw      t6, 0x0024(sp)
;   lbu     t7, 0x1F2C(t6)
;   bnel    t7, r0, 0x80959DC0
;   lb      a0, 0x0038(s0)
;   lb      t8, 0x0038(s0)
;   addiu   at, r0, -0x0001
;   lui     t9, 0x801F
;   bnel    t8, at, 0x80959DC0
;   lb      a0, 0x0038(s0)
;   lbu     t9, 0x057C(t9)
;   lui     t8, 0x8096
.org 0x80959D28
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    lui     at, 0x41A0
    lui     a3, 0x42C8
    sw      at, 0x0010(sp)
    lb      t9, 0x0038(s0)
    lb      t8, 0x0277(s0)
    bne     t9, t8, 0x80959D54
    nop
    jal     0x800B8BD0
    addiu   a2, r0, 0x0075
    lui     t9, 0x8096
    addiu   t9, t9, -0x61E8
    sw      t9, 0x0280(s0)
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0020
    jr      ra
    nop

; =========================================================
; File: 0x00DCC0A0, Address: 0x00DCD1CC, Offset: 0x0000112C
; Name: En_Mk :: Marine Scientist
; =========================================================
.headersize (0x809592E0 - 0x00DCC0A0)
; Replaces:
;   .dw 0x45000A8C ; bc1f    0x8095CE40
;   .dw 0x46000A9C
.org 0x8095A40C
    .dw 0x45000A74 ; bc1f    0x8095CDE0
    .dw 0x46000A78

; =========================================================
; File: 0x00F12010, Address: 0x00F12C78, Offset: 0x00000C68
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   jal     0x80112E80
.org 0x80AAD208
    nop

; =========================================================
; File: 0x00F12010, Address: 0x00F1288C, Offset: 0x0000087C
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   or      a2, a1, r0
;   lui     at, 0x0001
;   ori     at, at, 0x7D88
;   addu    a0, a2, at
;   jal     0x8012F608
;   addiu   a1, r0, 0x020B
;   bltz    v0, 0x80AACE4C
.org 0x80AACE1C
    or      a0, s1, r0
    or      a1, s0, r0
    jal     0x800B8BD0
    addiu   a2, r0, 0x0087
    beq     r0, v0, 0x80AACE4C
    addiu   t2, r0, 0x0800
    sw      t2, 0x0A6C(s7)

; =========================================================
; File: 0x02B48000, Address: 0x02B494E4, Offset: 0x000014E4
; Name: Z2_MUSICHOUSE :: Music Box House - Scene File
; =========================================================
; Replaces:
;   .dw 0x00000444
.orga 0x02B494E4
    .dw 0x000001F3

; =========================================================
; File: 0x00E24DA0, Address: 0x00E28518, Offset: 0x00003778
; Name: En_Knight :: Igos du Ikana & Henchmen [?]
; =========================================================
.headersize (0x809B20F0 - 0x00E24DA0)
; Replaces:
;   jal     0x800F1BE4
;   lb      a0, 0x0038(s0)
;   beqz    v0, 0x809B5890
;   or      a1, s0, r0
;   jal     0x800F1C68
;   lb      a0, 0x0038(s0)
.org 0x809B5868
    jal     0x801DC650
    addiu   a2, r0, 0x0076
    nop
    nop
    nop
    nop

; =========================================================
; File: 0x0103B520, Address: 0x0103B710, Offset: 0x000001F0
; Name: Bg_Haka_Tomb :: Flat's Tomb
; =========================================================
.headersize (0x80BD6580 - 0x0103B520)
; Replaces:
;   sw      a1, 0x001C(sp)
;   lh      a1, 0x0160(a0)
;   sw      a0, 0x0018(sp)
;   addiu   a2, r0, -0x0001
;   jal     0x8013E2D4
;   or      a3, r0, r0
;   beqz    v0, 0x80BD6798
.org 0x80BD6770
    sw      a0, 0x0018(sp)
    nop
    nop
    lw      a1, 0x0018(sp)
    addiu   a2, r0, 0x0072
    jal     0x801DC650
    nop

; =========================================================
; File: 0x00F23710, Address: 0x00F24F24, Offset: 0x00001814
; Name: En_Ma4 :: Romani I [?]
; =========================================================
.headersize (0x80ABDCA0 - 0x00F23710)
; Replaces:
;   sw      a1, 0x0024(sp)
;   lw      t6, 0x0020(sp)
;   lb      a0, 0x0038(t6)
;   jal     0x800F1BE4
;   sh      a0, 0x001E(sp)
;   beqz    v0, 0x80ABF4E8
;   lh      a0, 0x001E(sp)
.org 0x80ABF4B4
    jal     0x801DC650
    addiu   a2, r0, 0x0071
    nop
    nop
    nop
    nop
    nop

; =========================================================
; File: 0x00EA3200, Address: 0x00EA3F0C, Offset: 0x00000D0C
; Name: En_Elfgrp :: Group of Stray Fairies
; =========================================================
.headersize (0x80A396B0 - 0x00EA3200)
; Replaces:
;   lb      a0, 0x0038(s0)
;   jal     0x800F1C68
;   or      a1, s0, r0
;   lh      a1, 0x001C(s0)
;   lui     t6, 0x80A4
;   addiu   t6, t6, -0x5D8C
;   sw      t6, 0x014C(s0)
;   andi    a1, a1, 0xFE00
;   sra     a1, a1, 0x0009
;   jal     0x800B5C34
;   lw      a0, 0x0024(sp)
;   lhu     v1, 0x014A(s0)
;   lw      a0, 0x0024(sp)
;   andi    t7, v1, 0x0002
;   beql    t7, r0, 0x80A3A40C
;   andi    t8, v1, 0x0004
;   jal     0x80112E80
;   addiu   a1, r0, 0x0040
;   lhu     v1, 0x014A(s0)
;   andi    t8, v1, 0x0004
;   beqz    t8, 0x80A3A428
;   lui     v0, 0x801F
;   addiu   v0, v0, -0x0990
;   lbu     t9, 0x0F0F(v0)
;   ori     t0, t9, 0x0002
;   sb      t0, 0x0F0F(v0)
;   lhu     v1, 0x014A(s0)
;   andi    t1, v1, 0x0010
;   beqz    t1, 0x80A3A440
;   lw      a0, 0x0024(sp)
;   jal     0x80112E80
;   addiu   a1, r0, 0x0010
.org 0x80A3A3BC
    lh      t0, 0x024A(a3)
    addiu   at, r0, -0x26E8
    beq     t0, at, 0x80A3A3D8
    addiu   at, r0, -0x21C0
    beq     t0, at, 0x80A3A3D8
    addiu   at, r0, -0x26E0
    bne     t0, at, 0x80A3A470
    lb      a0, 0x0038(s0)
    lh      a1, 0x001C(s0)
    andi    a1, a1, 0xFE00
    sra     a1, a1, 0x0009
    or      t7, a1, r0
    bne     r0, t7, 0x80A3A408
    lui     t8, 0x801F
    addiu   t8, t8, -0x0970
    lb      t8, (t8)
    addiu   t9, r0, 0x0004
    beql    t9, t8, 0x80A3A408
    addiu   t7, t7, 0x0005
    addiu   t7, t7, 0x012C
    or      a0, s0, r0
    or      a1, s6, r0
    jal     0x800B8BD0
    or      a2, t7, r0
    beq     r0, v0, 0x80A3A470
    lui     t6, 0x80A4
    addiu   t6, t6, -0x5D8C
    sw      t6, 0x014C(s0)
    jal     0x800B5C34
    lw      a0, 0x0024(sp)
    b       0x80A3A470
    nop

; =========================================================
; File: 0x00EA3200, Address: 0x00EA471A, Offset: 0x0000151A
; Name: En_Elfgrp :: Group of Stray Fairies
; =========================================================
.headersize (0x80A396B0 - 0x00EA3200)
; Replaces:
;   .dh 0x0D1C
.org 0x80A3ABCA
    .dh 0x0D70

; =========================================================
; File: 0x00EA3200, Address: 0x00EA471E, Offset: 0x0000151E
; Name: En_Elfgrp :: Group of Stray Fairies
; =========================================================
.headersize (0x80A396B0 - 0x00EA3200)
; Replaces:
;   .dh 0x0D20
.org 0x80A3ABCE
    .dh 0x0D74

; =========================================================
; File: 0x00B3C000, Address: 0x00BA99FC, Offset: 0x0006D9FC
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   a0, r0, 0x0006
;   lbu     t8, 0x0008(t1)
;   lui     t0, 0x801F
;   addiu   t0, t0, -0x0990
;   addu    v0, t0, t8
;   lbu     t9, 0x0070(v0)
;   addiu   v1, r0, 0x0008
;   addiu   t6, r0, 0x0008
;   beq     v1, t9, 0x801134E8
;   nop
;   sb      t6, 0x0070(v0)
;   jal     0x8012EDE8
;   addiu   a1, r0, 0x0002
;   lui     t1, 0x801C
;   addiu   t1, t1, 0x2078
;   lbu     t8, 0x0008(t1)
;   lui     t0, 0x801F
;   lui     t7, 0x801C
.org 0x801134BC
    lui     t0, 0x801F
    addiu   t0, t0, -0x0960
    lui     t1, 0x800B
    lb      t1, 0x35F3(t1)
    sra     t1, t1, 0x0004
    lb      t9, 0x0009(t0)
    addiu   t8, r0, 0x0001
    beqz    t1, 0x801134EC
    addiu   t2, r0, 0x0030
    addiu   t2, r0, 0x0060
    sb      t8, 0x0011(t0)
    sb      r0, 0x0008(t0)
    sb      t8, 0x0010(t0)
    sub     t9, t9, t2
    bltzl   t9, 0x801134FC
    sb      t2, 0x0009(t0)
    b       0x801143BC
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9A68, Offset: 0x0006DA68
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lbu     t6, 0x0008(t1)
;   lui     t0, 0x801F
;   addiu   t0, t0, -0x0990
;   addu    v0, t0, t6
;   lbu     t8, 0x0070(v0)
;   addiu   v1, r0, 0x0008
.org 0x80113528
    lui     t8, 0x801F
    lb      t6, 0x057F(t8)
    ori     t6, t6, 0x0002
    sb      t6, 0x057F(t8)
    b       0x801143BC
    addiu   v0, r0, 0x00FF

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9AD0, Offset: 0x0006DAD0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lbu     t7, 0x0009(t1)
;   lui     t0, 0x801F
;   addiu   t0, t0, -0x0990
;   addu    v0, t0, t7
;   lbu     t6, 0x0070(v0)
;   addiu   v1, r0, 0x0009
;   addiu   t9, r0, 0x0009
;   beq     v1, t6, 0x801135B8
;   addiu   a0, r0, 0x0007
;   sb      t9, 0x0070(v0)
;   jal     0x8012EDE8
;   addiu   a1, r0, 0x0002
;   lui     t1, 0x801C
;   addiu   t1, t1, 0x2078
;   lbu     t7, 0x0009(t1)
.org 0x80113590
    lui     t0, 0x800B
    lb      t0, 0x35F3(t0)
    sra     t0, t0, 0x0004
    lui     t1, 0x801F
    beqz    t0, 0x801135BC
    addiu   t1, t1, -0x0990
    addu    t1, t1, t0
    lb      t0, 0x00D3(t1)
    addiu   t0, t0, 0x0001
    b       0x801143BC
    sb      t0, 0x00D3(t1)
    lb      t0, 0x0F00(t1)
    ori     t0, t0, 0x0080
    b       0x801143BC
    sb      t0, 0x0F00(t1)

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9B38, Offset: 0x0006DB38
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lbu     t9, 0x0009(t1)
;   lui     t0, 0x801F
;   addiu   t0, t0, -0x0990
;   addu    v0, t0, t9
;   lbu     t7, 0x0070(v0)
;   addiu   v1, r0, 0x0009
;   addiu   t8, r0, 0x0009
.org 0x801135F8
    lui     t8, 0x801F
    addiu   t8, t8, -0x0960
    addiu   t6, r0, 0x0001
    sb      t6, 0x0012(t8)
    addiu   t6, r0, 0x0014
    b       0x801143BC
    sb      t6, 0x00A3(t8)

; =========================================================
; File: 0x00EC9990, Address: 0x00EC9B24, Offset: 0x00000194
; Name: En_Bal :: Tingle (Gameplay)
; =========================================================
.headersize (0x80A61810 - 0x00EC9990)
; Replaces:
;   lbu     t7, -0x0950(t7)
.org 0x80A619A4
    addiu   t7, r0, 0x0001

; =========================================================
; File: 0x0104EAC0, Address: 0x0104FB78, Offset: 0x000010B8
; Name: En_Dt :: Mayor Dotour (Gameplay)
; =========================================================
.headersize (0x80BE9B20 - 0x0104EAC0)
; Replaces:
;   addiu   a2, r0, 0x000C
.org 0x80BEABD8
    addiu   a2, r0, 0x0003

; =========================================================
; File: 0x00F12010, Address: 0x00F13F14, Offset: 0x00001F04
; Name: Dm_Char05 :: Masks (Cutscenes)
; =========================================================
.headersize (0x80AAC5A0 - 0x00F12010)
; Replaces:
;   .dw 0x450007A8 ; bc1f    0x80AB0348
;   .dw 0x460007AC
.org 0x80AAE4A4
    nop
    nop

; =========================================================
; File: 0x00CA7F00, Address: 0x00CC2894, Offset: 0x0001A994
; Name: player_actor
; =========================================================
.headersize (0x8082DA90 - 0x00CA7F00)
; Replaces:
;   jal     0x80152498
.org 0x80848424
    jal     0x801DC480

; =========================================================
; File: 0x00B3C000, Address: 0x00C729C0, Offset: 0x001369C0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B34 ; daddiu  a3, t3, 0x5B34
;   .dw 0x335D0000 ; andi    sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B34 ; daddiu  a3, t3, 0x5B34
;   .dw 0x345D0000 ; ori     sp, v0, 0x0000
.org 0x801DC480
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    jal     0x80152498
    nop
    ori     v0, v0, 0x0002
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0020
    jr      ra
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00B498F4, Offset: 0x0000D8F4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   nor     t7, a2, t0
;   bnez    t7, 0x800B33E0
;   nop
;   lw      t8, (t1)
;   addiu   t5, r0, 0x000A
;   sw      t5, (a0)
;   addiu   t9, t8, -0x0001
;   bgez    t9, 0x800B33DC
;   sw      t9, (t1)
;   sw      r0, (t1)
;   lhu     a2, 0x000C(v0)
;   lui     t1, 0x801B
;   nor     t6, a2, t2
.org 0x800B33B4
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    addiu   t0, r0, 0x0009
    beq     t0, a0, 0x800B33D8
    addiu   t1, r0, 0x0016
    beq     t1, a0, 0x800B33D8
    nop
    jal     0x800F1C68
    nop
    lw      ra, 0x001C(sp)
    addiu   sp, sp, 0x0020
    jr      ra
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BDAAAC, Offset: 0x0009EAAC
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   jr      ra
.org 0x8014456C
    j       0x800B33E8

; =========================================================
; File: 0x00B3C000, Address: 0x00B49928, Offset: 0x0000D928
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   bnez    t6, 0x800B342C
;   addiu   t1, t1, -0x1390
;   lw      t7, (t1)
;   lui     t3, 0x801B
;   addiu   t3, t3, -0x1710
;   addiu   t8, t7, 0x0001
.org 0x800B33E8
    lb      t0, 0x006D(v0)
    andi    t1, t0, 0x0003
    addiu   t2, t1, 0x004C
    sb      t2, 0x004C(v0)
    jr      ra
    nop

; =========================================================
; File: 0x00F1A780, Address: 0x00F1DD4C, Offset: 0x000035CC
; Name: En_Mnk :: Monkey
; =========================================================
.headersize (0x80AB4D10 - 0x00F1A780)
; Replaces:
;   sb      t3, 0x100E(v0)
.org 0x80AB82DC
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00B4997C, Offset: 0x0000D97C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   t3, t3, -0x1710
;   lw      t9, (t1)
;   addiu   t6, r0, 0x000A
;   sw      t6, (a0)
;   addiu   t5, t9, -0x000A
;   bgez    t5, 0x800B345C
;   sw      t5, (t1)
;   sw      r0, (t1)
;   lhu     a2, 0x000C(v0)
;   nor     t7, a2, t4
;   bnel    t7, r0, 0x800B3498
;   lw      t7, (a0)
;   lw      t8, (t1)
;   addiu   t5, r0, 0x000A
;   sw      t5, (a0)
;   addiu   t9, t8, 0x000A
;   slti    a2, t9, 0x0065
;   xori    a2, a2, 0x0001
;   beqz    a2, 0x800B3494
.org 0x800B343C
    addiu   t0, r0, 0x0059
    beql    t0, a0, 0x800B3470
    addiu   v1, r0, 0x005B
    addiu   t1, r0, 0x006F
    beql    t1, a0, 0x800B3470
    addiu   v1, r0, 0x0091
    addiu   t2, r0, 0x0060
    beql    t2, a0, 0x800B3470
    addiu   v1, r0, 0x0092
    addiu   t3, r0, 0x006A
    beql    t3, a0, 0x800B3470
    addiu   v1, r0, 0x0093
    or      v1, a0, r0
    j       0x80144A28
    or      a0, v1, r0
    nop
    nop
    nop
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00C72B90, Offset: 0x00136B90
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x365D0000 ; ori     sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x375D0000 ; ori     sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B36 ; daddiu  a3, t3, 0x5B36
;   .dw 0x385D0000 ; xori    sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
.org 0x801DC650
    lui     t0, 0x803F
    lw      t1, -0x7814(t0)
    addiu   t2, r0, 0x0800
    sw      t2, 0x0A6C(t1)
    sw      s0, 0x0388(t1)
    sh      a2, 0x0384(t1)
    jr      ra
    nop
    lui     t0, 0x803F
    lw      t1, -0x7814(t0)
    addiu   t2, r0, 0x0800
    sw      t2, 0x0A6C(t1)
    sw      s1, 0x0388(t1)
    sh      a2, 0x0384(t1)
    jr      ra
    nop

; =========================================================
; File: 0x01051B70, Address: 0x01051FB2, Offset: 0x00000442
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   .dh 0x0007
.org 0x80BED022
    .dh 0x0125

; =========================================================
; File: 0x00DDB570, Address: 0x00DDD414, Offset: 0x00001EA4
; Name: En_Js :: Moon Child
; =========================================================
.headersize (0x809687B0 - 0x00DDB570)
; Replaces:
;   lbu     t7, -0x0920(t7)
.org 0x8096A654
    addiu   t7, r0, 0x00FF

; =========================================================
; File: 0x00E0F010, Address: 0x00E0F268, Offset: 0x00000258
; Name: En_Cow :: Cow
; =========================================================
.headersize (0x8099C290 - 0x00E0F010)
; Replaces:
;   bne     v0, at, 0x8099C7F0
.org 0x8099C4E8
    nop

; =========================================================
; File: 0x01FD9000, Address: 0x01FD90B2, Offset: 0x000000B2
; Name: Z2_OMOYA_room_00 :: Ranch House & Barn - Room 00
; =========================================================
; Replaces:
;   .dh 0x0002
.orga 0x01FD90B2
    .dh 0x0132

; =========================================================
; File: 0x01FD9000, Address: 0x01FD90C2, Offset: 0x000000C2
; Name: Z2_OMOYA_room_00 :: Ranch House & Barn - Room 00
; =========================================================
; Replaces:
;   .dh 0x0002
.orga 0x01FD90C2
    .dh 0x01A2

; =========================================================
; File: 0x01FD9000, Address: 0x01FD90D2, Offset: 0x000000D2
; Name: Z2_OMOYA_room_00 :: Ranch House & Barn - Room 00
; =========================================================
; Replaces:
;   .dh 0x0002
.orga 0x01FD90D2
    .dh 0x0182

; =========================================================
; File: 0x029ED000, Address: 0x029ED07E, Offset: 0x0000007E
; Name: Z2_REDEAD_room_09 :: Beneath the Well - Room 09
; =========================================================
; Replaces:
;   .dh 0x0000
.orga 0x029ED07E
    .dh 0x0135

; =========================================================
; File: 0x00B3C000, Address: 0x00BA96D4, Offset: 0x0006D6D4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lhu     v1, 0x3F36(t0)
;   addiu   t5, r0, 0x0001
;   addiu   v0, r0, 0x00FF
;   addu    t9, t0, v1
;   lb      t6, 0x00CA(t9)
;   addu    t7, t0, v1
;   addu    a0, t0, v1
;   bgezl   t6, 0x801131C4
;   lb      t8, 0x00CA(a0)
;   b       0x801143BC
;   sb      t5, 0x00CA(t7)
;   lb      t8, 0x00CA(a0)
;   addiu   v0, r0, 0x00FF
;   addiu   t9, t8, 0x0001
;   b       0x801143BC
;   sb      t9, 0x00CA(a0)
.org 0x80113194
    lui     v1, 0x800B
    lb      v1, 0x35F3(v1)
    sra     v1, v1, 0x0004
    addu    t0, t0, v1
    lb      t6, 0x00CA(t0)
    addiu   t5, r0, 0x0001
    addiu   v0, r0, 0x00FF
    bgezl   t6, 0x801131B8
    addiu   t5, t6, 0x0001
    b       0x801143BC
    sb      t5, 0x00CA(t0)
    nop
    nop
    nop
    nop
    nop

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9678, Offset: 0x0006D678
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addiu   at, r0, 0x0074
;   beq     a3, at, 0x80113154
;   lui     t0, 0x801F
;   addiu   at, r0, 0x0075
;   beq     a3, at, 0x80113154
;   addiu   at, r0, 0x0076
;   bne     a3, at, 0x80113184
;   addiu   t0, t0, -0x0990
;   lhu     v1, 0x3F36(t0)
.org 0x80113138
    slti    at, a3, 0x0074
    bnez    at, 0x80113184
    slti    at, a3, 0x0077
    beqz    at, 0x80113184
    lui     t0, 0x801F
    addiu   t0, t0, -0x0990
    lui     v1, 0x800B
    lb      v1, 0x35F3(v1)
    sra     v1, v1, 0x0004

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9464, Offset: 0x0006D464
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   b       0x801143BC
;   addiu   v0, r0, 0x00FF
;   addiu   at, r0, 0x006D
.org 0x80112F24
    lui     a0, 0x800B
    b       0x801131C0
    lb      a0, 0x35F3(a0)

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9700, Offset: 0x0006D700
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lb      t8, 0x00CA(a0)
;   addiu   v0, r0, 0x00FF
;   addiu   t9, t8, 0x0001
;   b       0x801143BC
.org 0x801131C0
    jal     0x8012EF0C
    sra     a0, a0, 0x0004
    b       0x801143BC
    addiu   v0, r0, 0x00FF

; =========================================================
; File: 0x00B3C000, Address: 0x00C72C50, Offset: 0x00136C50
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x365D0000 ; ori     sp, s2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x375D0000 ; ori     sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x385D0000 ; xori    sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x395D0000 ; xori    sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x305D0000 ; andi    sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B38 ; daddiu  a3, t3, 0x5B38
;   .dw 0x315D0000 ; andi    sp, t2, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
.org 0x801DC710
    addiu   sp, sp, -0x0020
    sw      ra, 0x001C(sp)
    sw      a1, 0x0014(sp)
    jal     0x801DC790
    addiu   a1, r0, 0x0001
    jal     0x801449A4
    or      a0, v0, r0
    sw      v0, 0x0018(sp)
    lw      a0, 0x0014(sp)
    lh      a1, 0x0004(v0)
    jal     0x801518B0
    or      a2, r0, r0
    lw      t5, 0x0018(sp)
    lui     t0, 0x8075
    lb      t1, 0x0003(t5)
    andi    t1, t1, 0x000F
    sll     a0, t1, 0x0001
    addu    a0, t0, a0
    lui     t9, 0x801A
    beql    r0, t1, 0x801DC768
    addiu   t9, t9, -0x0F38
    addiu   t9, t9, 0x3098
    jalr    t9
    lhu     a0, -0x672C(a0)
    lw      a0, 0x0014(sp)
    lw      t5, 0x0018(sp)
    jal     0x80112E80
    lb      a1, (t5)
    lw      ra, 0x001C(sp)
    jr      ra
    addiu   sp, sp, 0x0020

; =========================================================
; File: 0x01045480, Address: 0x01046981, Offset: 0x00001501
; Name: En_Tab :: Mr. Barten
; =========================================================
.headersize (0x80BE04E0 - 0x01045480)
; Replaces:
;   .dh 0x0091
.org 0x80BE19E1
    .dh 0x0180

; =========================================================
; File: 0x01045480, Address: 0x010469ED, Offset: 0x0000156D
; Name: En_Tab :: Mr. Barten
; =========================================================
.headersize (0x80BE04E0 - 0x01045480)
; Replaces:
;   .dh 0x0091
.org 0x80BE1A4D
    .dh 0x0180

; =========================================================
; File: 0x01045480, Address: 0x01046990, Offset: 0x00001510
; Name: En_Tab :: Mr. Barten
; =========================================================
.headersize (0x80BE04E0 - 0x01045480)
; Replaces:
;   .dh 0x0092
.org 0x80BE19F0
    .dh 0x0181

; =========================================================
; File: 0x01045480, Address: 0x010469FC, Offset: 0x0000157C
; Name: En_Tab :: Mr. Barten
; =========================================================
.headersize (0x80BE04E0 - 0x01045480)
; Replaces:
;   .dh 0x0092
.org 0x80BE1A5C
    .dh 0x0181

; =========================================================
; File: 0x01015A20, Address: 0x010160B6, Offset: 0x00000696
; Name: En_Kujiya :: Lottery Shop Kiosk
; =========================================================
.headersize (0x80BB08E0 - 0x01015A20)
; Replaces:
;   .dh 0x0005
.org 0x80BB0F76
    .dh 0x0086

; =========================================================
; File: 0x00ECC620, Address: 0x00ECD7CA, Offset: 0x000011AA
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   .dh 0x0002
.org 0x80A6564A
    .dh 0x013D

; =========================================================
; File: 0x00F4EDA0, Address: 0x00F509E6, Offset: 0x00001C46
; Name: En_Lift_Nuts :: Deku Scrub Playground Employee
; =========================================================
.headersize (0x80AE9A20 - 0x00F4EDA0)
; Replaces:
;   .dh 0x0005
.org 0x80AEB666
    .dh 0x0133

; =========================================================
; File: 0x00DD44A0, Address: 0x00DD63A6, Offset: 0x00001F06
; Name: En_Fu :: Honey & Darling (Gameplay)
; =========================================================
.headersize (0x809616E0 - 0x00DD44A0)
; Replaces:
;   .dh 0x0005
.org 0x809635E6
    .dh 0x0183

; =========================================================
; File: 0x00EF2EC0, Address: 0x00EF391E, Offset: 0x00000A5E
; Name: En_Trt :: Kotake (No Broom) [?]
; =========================================================
.headersize (0x80A8B770 - 0x00EF2EC0)
; Replaces:
;   .dh 0x0004
.org 0x80A8C1CE
    .dh 0x0187

; =========================================================
; File: 0x01051B70, Address: 0x01051FFE, Offset: 0x0000048E
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   .dh 0x0035
.org 0x80BED06E
    .dh 0x019B

; =========================================================
; File: 0x01051B70, Address: 0x0105200E, Offset: 0x0000049E
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   .dh 0x005C
.org 0x80BED07E
    .dh 0x019C

; =========================================================
; File: 0x01051B70, Address: 0x01052016, Offset: 0x000004A6
; Name: En_Akindonuts :: Business Scrub (Burrowed) [?]
; =========================================================
.headersize (0x80BECBE0 - 0x01051B70)
; Replaces:
;   .dh 0x005D
.org 0x80BED086
    .dh 0x019D

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C395, Offset: 0x00000EE5
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dh 0x0002
.org 0x80AE7015
    .dh 0x0188

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C51E, Offset: 0x0000106E
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dh 0x0002
.org 0x80AE719E
    .dh 0x0188

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C382, Offset: 0x00000ED2
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dh 0x0004
.org 0x80AE7002
    .dh 0x018F

; =========================================================
; File: 0x00F4B4B0, Address: 0x00F4C50B, Offset: 0x0000105B
; Name: En_Shn :: Swamp Tourist Center Guide
; =========================================================
.headersize (0x80AE6130 - 0x00F4B4B0)
; Replaces:
;   .dh 0x0004
.org 0x80AE718B
    .dh 0x018F

; =========================================================
; File: 0x00D6D270, Address: 0x00D6E9BE, Offset: 0x0000174E
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   .dh 0x0092
.org 0x808F47FE
    .dh 0x01A0

; =========================================================
; File: 0x00D6D270, Address: 0x00D6F222, Offset: 0x00001FB2
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   .dh 0x0092
.org 0x808F5062
    .dh 0x01A0

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC816, Offset: 0x00000966
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   .dh 0x0005
.org 0x80B67696
    .dh 0x0134

; =========================================================
; File: 0x00FCBEB0, Address: 0x00FCC81A, Offset: 0x0000096A
; Name: En_Sth
; =========================================================
.headersize (0x80B66D30 - 0x00FCBEB0)
; Replaces:
;   .dh 0x0004
.org 0x80B6769A
    .dh 0x01A3

; =========================================================
; File: 0x00FFB730, Address: 0x00FFE0DA, Offset: 0x000029AA
; Name: En_Zot :: Zora (Land)
; =========================================================
.headersize (0x80B965D0 - 0x00FFB730)
; Replaces:
;   .dh 0x0002
.org 0x80B98F7A
    .dh 0x019E

; =========================================================
; File: 0x00FFB730, Address: 0x00FFDA66, Offset: 0x00002336
; Name: En_Zot :: Zora (Land)
; =========================================================
.headersize (0x80B965D0 - 0x00FFB730)
; Replaces:
;   .dh 0x0002
.org 0x80B98906
    .dh 0x01A8

; =========================================================
; File: 0x00FFB730, Address: 0x00FFDA9E, Offset: 0x0000236E
; Name: En_Zot :: Zora (Land)
; =========================================================
.headersize (0x80B965D0 - 0x00FFB730)
; Replaces:
;   .dh 0x0004
.org 0x80B9893E
    .dh 0x01C3

; =========================================================
; File: 0x00F8B5A0, Address: 0x00F8CA16, Offset: 0x00001476
; Name: En_Kendo_Js :: Swordsman
; =========================================================
.headersize (0x80B262A0 - 0x00F8B5A0)
; Replaces:
;   .dh 0x0004
.org 0x80B27716
    .dh 0x009F

; =========================================================
; File: 0x0106E050, Address: 0x0106E64E, Offset: 0x000005FE
; Name: En_Kitan :: Keaton
; =========================================================
.headersize (0x80C090D0 - 0x0106E050)
; Replaces:
;   .dh 0x0004
.org 0x80C096CE
    .dh 0x0030

; =========================================================
; File: 0x00E325D0, Address: 0x00E340C2, Offset: 0x00001AF2
; Name: En_Aob_01 :: Mamamu Yan
; =========================================================
.headersize (0x809C10B0 - 0x00E325D0)
; Replaces:
;   .dh 0x0004
.org 0x809C2BA2
    .dh 0x00B1

; =========================================================
; File: 0x00ED6B10, Address: 0x00ED7916, Offset: 0x00000E06
; Name: En_Mm3 :: Postman (Counting Game)
; =========================================================
.headersize (0x80A6F0A0 - 0x00ED6B10)
; Replaces:
;   .dh 0x0005
.org 0x80A6FEA6
    .dh 0x00CE

; =========================================================
; File: 0x00F62F50, Address: 0x00F631F2, Offset: 0x000002A2
; Name: En_Col_Man :: Piece of Heart
; =========================================================
.headersize (0x80AFDC40 - 0x00F62F50)
; Replaces:
;   .dh 0x000A
.org 0x80AFDEE2
    .dh 0x0112

; =========================================================
; File: 0x00B3C000, Address: 0x00BAA7E4, Offset: 0x0006E7E4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   b       0x801143A0
.org 0x801142A4
    b       0x801143BC

; =========================================================
; File: 0x00EF2EC0, Address: 0x00EF3C7A, Offset: 0x00000DBA
; Name: En_Trt :: Kotake (No Broom) [?]
; =========================================================
.headersize (0x80A8B770 - 0x00EF2EC0)
; Replaces:
;   .dh 0x005B
.org 0x80A8C52A
    .dh 0x0059

; =========================================================
; File: 0x00EF2EC0, Address: 0x00EF5168, Offset: 0x000022A8
; Name: En_Trt :: Kotake (No Broom) [?]
; =========================================================
.headersize (0x80A8B770 - 0x00EF2EC0)
; Replaces:
;   .dh 0x1440
.org 0x80A8DA18
    .dh 0x1000

; =========================================================
; File: 0x00EF2EC0, Address: 0x00EF5254, Offset: 0x00002394
; Name: En_Trt :: Kotake (No Broom) [?]
; =========================================================
.headersize (0x80A8B770 - 0x00EF2EC0)
; Replaces:
;   beqz    v0, 0x80A8DB58
.org 0x80A8DB04
    nop

; =========================================================
; File: 0x00F38DA0, Address: 0x00F39954, Offset: 0x00000BB4
; Name: En_Trt2 :: Kotake (Broom) [?]
; =========================================================
.headersize (0x80AD3380 - 0x00F38DA0)
; Replaces:
;   beqz    v0, 0x80AD3F50
.org 0x80AD3F34
    nop

; =========================================================
; File: 0x00F46EF0, Address: 0x00F48EA0, Offset: 0x00001FB0
; Name: En_Fsn :: Curiosity Shop Proprietor
; =========================================================
.headersize (0x80AE1B70 - 0x00F46EF0)
; Replaces:
;   addiu   v1, r0, 0x0005
;   lui     t4, 0x80AE
;   beq     v0, v1, 0x80AE3B64
;   addiu   t4, t4, 0x3BC4
;   addiu   at, r0, 0x000A
;   beq     v0, at, 0x80AE3B70
;   addiu   t1, r0, 0x0003
;   addiu   at, r0, 0x0014
;   beq     v0, at, 0x80AE3B78
;   addiu   t2, r0, 0x0004
;   addiu   at, r0, 0x0032
;   beq     v0, at, 0x80AE3B80
;   addiu   at, r0, 0x00C8
;   beq     v0, at, 0x80AE3B88
;   addiu   t3, r0, 0x0007
;   b       0x80AE3B8C
;   nop
;   addiu   t0, r0, 0x0002
;   b       0x80AE3B8C
;   sw      t0, 0x037C(s0)
;   b       0x80AE3B8C
;   sw      t1, 0x037C(s0)
;   b       0x80AE3B8C
;   sw      t2, 0x037C(s0)
;   b       0x80AE3B8C
;   sw      v1, 0x037C(s0)
;   sw      t3, 0x037C(s0)
.org 0x80AE3B20
    addiu   at, r0, 0x0005
    lui     t4, 0x80AE
    addiu   t4, t4, 0x3BC4
    beq     v0, at, 0x80AE3B6C
    addiu   t0, r0, 0x01C7
    addiu   at, r0, 0x000A
    beq     v0, at, 0x80AE3B6C
    addiu   t0, r0, 0x0003
    addiu   at, r0, 0x0014
    beq     v0, at, 0x80AE3B6C
    addiu   t0, r0, 0x01C8
    addiu   at, r0, 0x0032
    beq     v0, at, 0x80AE3B6C
    addiu   t0, r0, 0x01C9
    addiu   at, r0, 0x00C8
    beq     v0, at, 0x80AE3B6C
    addiu   t0, r0, 0x01CA
    b       0x80AE3B8C
    nop
    b       0x80AE3B8C
    sw      t0, 0x037C(s0)
    nop
    nop
    nop
    nop
    nop
    nop

; =========================================================
; File: 0x00F46EF0, Address: 0x00F4B282, Offset: 0x00004392
; Name: En_Fsn :: Curiosity Shop Proprietor
; =========================================================
.headersize (0x80AE1B70 - 0x00F46EF0)
; Replaces:
;   .dh 0x1FBC
.org 0x80AE5F02
    .dh 0x1FB8

; =========================================================
; File: 0x00D6D270, Address: 0x00D6EE78, Offset: 0x00001C08
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   beqz    v0, 0x808F4CF8
.org 0x808F4CB8
    nop

; =========================================================
; File: 0x00D6D270, Address: 0x00D6F5B8, Offset: 0x00002348
; Name: En_In :: Gorman Bros.
; =========================================================
.headersize (0x808F30B0 - 0x00D6D270)
; Replaces:
;   beqz    v0, 0x808F5438
.org 0x808F53F8
    nop

; =========================================================
; File: 0x00ECC620, Address: 0x00ECCD2A, Offset: 0x0000070A
; Name: En_Ginko_Man :: Bank Teller
; =========================================================
.headersize (0x80A644A0 - 0x00ECC620)
; Replaces:
;   .dh 0x1388
.org 0x80A64BAA
    .dh 0x03E8

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9FC4, Offset: 0x0006DFC4
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   lbu     t9, 0x0076(t0)
.org 0x80113A84
    lb      t9, 0x0076(t0)

; =========================================================
; File: 0x00B3C000, Address: 0x00BA9FD0, Offset: 0x0006DFD0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   beql    v0, t9, 0x80113AC8
;   lbu     t8, 0x0006(t1)
;   lbu     t6, 0x0006(t1)
;   lui     t9, 0x801C
;   addu    t9, t9, a2
;   addu    v1, t0, t6
;   sb      t8, 0x0070(v1)
;   lh      t9, -0x0696(t9)
;   lb      t7, 0x00A0(v1)
;   addiu   v0, r0, 0x00FF
.org 0x80113A90
    beq     v0, t9, 0x80113AC8
    lbu     t6, 0x0006(t1)
    bltz    t9, 0x801143BC
    addiu   v0, r0, 0x00FF
    lui     t9, 0x801C
    addu    t9, t9, a2
    addu    v1, t0, t6
    sb      t8, 0x0070(v1)
    lh      t9, -0x0696(t9)
    lb      t7, 0x00A0(v1)

; =========================================================
; File: 0x00B3C000, Address: 0x00BAA010, Offset: 0x0006E010
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   addu    v1, t0, t8
.org 0x80113AD0
    addu    v1, t0, t6

; =========================================================
; File: 0x00E0F010, Address: 0x00E0F9CC, Offset: 0x000009BC
; Name: En_Cow :: Cow
; =========================================================
.headersize (0x8099C290 - 0x00E0F010)
; Replaces:
;   addiu   a2, r0, 0x0092
.org 0x8099CC4C
    lh      a2, 0x001C(s0)

; =========================================================
; File: 0x00B3C000, Address: 0x00C72C1C, Offset: 0x00136C1C
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces:
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x335D0000 ; andi    sp, k0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x345D0000 ; ori     sp, v0, 0x0000
;   .dw 0x7765656B ; jalx    0x8D9595AC
;   .dw 0x5F657665
;   .dw 0x6E745F72 ; ldr     s4, 0x5F72(s3)
;   .dw 0x65675B37 ; daddiu  a3, t3, 0x5B37
;   .dw 0x355D0000 ; ori     sp, t2, 0x0000
.org 0x801DC6DC
    addiu   at, r0, 0x0064
    sh      at, 0x0118(s7)
    lw      t6, 0x0A6C(s7)
    ori     t6, t6, 0x0200
    jr      ra
    sw      t6, 0x0A6C(s7)
    sh      r0, 0x0118(s7)
    lui     at, 0xFFFF
    ori     at, at, 0xFDFF
    lw      t6, 0x0A6C(s7)
    and     t6, t6, at
    jr      ra
    sw      t6, 0x0A6C(s7)
