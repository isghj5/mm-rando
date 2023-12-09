.headersize G_CODE_DELTA

; Replaces:
; TODO
.org 0x80151BB4
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0020 (sp)
    jal     BombersNotebook_ShouldGrant
    sw      a1, 0x0024 (sp)
    beqz    v0, @@caller_return
    lw      ra, 0x001C (sp)
    lw      a0, 0x0020 (sp)
    lw      a1, 0x0024 (sp)
    addiu   v0, a0, 0x4908
    lui     a2, 0x0001
    addu    v1, v0, a2
    lb      t4, 0x20B1 (v1)
    addu    t5, v1, t4
    sb      a1, 0x20B2 (t5)
    addiu   t8, t4, 0x0001
    sb      t8, 0x20B1 (v1)
@@caller_return:
    jr      ra
    addiu   sp, sp, 0x20

; Replaces:
;   SB      T6, 0x20B1 (AT)
;   LBU     V1, 0x20B1 (T2)
;   ADDU    T7, A3, V1
;   ADDU    T8, T7, T0
;   LBU     T9, 0x20B2 (T8)
;   SLL     T6, T9, 1
.org 0x80151CFC
    jal     BombersNotebook_Grant_Hook
    lw      a0, 0x0020 (sp)
    bgtzl   v0, 0x80151D90
    addiu   v0, r0, 0x0001
    bltz    v0, 0x80151CE4
    nop
