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

;==================================================================================================
; Get stray fairy color index
;==================================================================================================

; Replaces:
;   LUI     A2, 0x801F
;   ADDIU   A2, A2, 0xF670
;   ADDIU   A1, R0, 0x0003
;   OR      A0, V0, R0
;   SW      T0, 0x0000 (A0)
;   LHU     T6, 0x48C8 (A2)
.org 0x8014A2A4
    or      a0, v0, r0
    sw      t0, 0x0000 (a0)
    jal     Message_GetStrayFairyIconColorIndex_Hook
    nop
    addiu   a1, r0, 0x0003
    nop

; Replaces:
;   SW      T9, 0x0000 (A0)
;   LHU     T8, 0x48C8 (A2)
.org 0x8014A318
    jal     Message_GetStrayFairyIconColorIndex_Hook
    sw      t9, 0x0000 (a0)

; Replaces:
;   SW      T6, 0x0000 (V1)
;   LHU     A0, 0x48C8 (A2)
;   LUI     T9, 0x801D
;   LUI     T6, 0x072F
;   SLL     T7, A0, 2
.org 0x8014A4B8
    jal     Message_GetStrayFairyIconColorIndex_Hook
    sw      t6, 0x0000 (v1)
    lui     t9, 0x801D
    lui     t6, 0x072F
    sll     t7, t8, 2
