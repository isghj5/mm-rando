Message_GetStrayFairyIconColorIndex_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0020 (sp)
    sw      v0, 0x0018 (sp)
    sw      v1, 0x0014 (sp)

    jal     Message_GetStrayFairyIconColorIndex
    or      a0, s1, r0

    or      t6, v0, r0
    or      t8, v0, r0
    sll     t7, t8, 2

    lui     t3, 0x0700
    lui     t4, 0xE600
    lui     t5, 0xF300

    lw      v1, 0x0014 (sp)
    lw      v0, 0x0018 (sp)
    lw      a0, 0x0020 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
