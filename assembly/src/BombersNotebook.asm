BombersNotebook_Grant_Hook:
    addiu   sp, sp, -0x30
    sw      ra, 0x002C (sp)
    sw      t2, 0x0028 (sp)
    sw      a3, 0x0024 (sp)
    sw      t3, 0x0020 (sp)
    sw      t1, 0x001C (sp)
    sw      t5, 0x0018 (sp)
    sw      t4, 0x0014 (sp)

    ; Displaced code
    sb      t6, 0x20B1 (at)

    jal     BombersNotebook_Grant
    nop

    lw      t2, 0x0028 (sp)

    ; Displaced code
    lbu     v1, 0x20B1 (t2)

    lui     t0, 0x0001
    lw      a3, 0x0024 (sp)
    lw      t3, 0x0020 (sp)
    lw      t1, 0x001C (sp)
    lw      t5, 0x0018 (sp)
    lw      t4, 0x0014 (sp)

    ; Displaced code
    addu    t7, a3, v1
    addu    t8, t7, t0
    lbu     t9, 0x20B2 (t8)
    sll     t6, t9, 1

    lw      ra, 0x002C (sp)
    jr      ra
    addiu   sp, sp, 0x30
