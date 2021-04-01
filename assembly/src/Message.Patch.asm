;==================================================================================================
; Before a character in a raw message is processed
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lw      t2, 0x0070 (sp)
;   lh      t6, 0x00DA (sp)
;   addiu   at, r0 0x0010
;   lhu     t3, 0x1FEC (t2)
;   addu    t7, s6, t6
;   addu    t8, t7, s7
;   addu    t4, s3, t3
;   addu    t5, t4, s7
;   lbu     s2, 0x1880 (t5)
;   sb      s2, 0x1F24 (t8)
.org 0x8015B27C
    or      a0, s4, r0
    jal     Message_BeforeCharacterProcess
    or      a1, sp, r0
    addiu   at, r0, 0x00FF
    beq     v0, at, 0x8015E6E4
    addiu   at, r0, 0x0010
    or      s2, v0, r0
    nop
    nop
    nop
