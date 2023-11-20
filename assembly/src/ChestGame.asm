ChestGame_DrawMap_Hook:
    ldc1    f22, 0x0020 (sp)
    lw      a1, 0x0028 (sp) ; actor saved on stack
    lw      a0, 0x002C (sp) ; ctxt saved on stack
    addiu   sp, sp, -0x0020
    sw      ra, 0x0010 (sp)
    lw      t5, 0x0140 (a1) ; get actor table entry
    lw      a2, 0x0010 (t5) ; get overlay address
    jal     ChestGame_DrawMap
    addiu   a2, a2, 0x1748 ; make pointer to maze tiles
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x0020

ChestGame_ResetMap_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0008 (sp)
    jal     ChestGame_ResetMap
    sw      t2, 0x0144 (s2)
    lw      ra, 0x0008 (sp)
    jr      ra
    addiu   sp, sp, 0x18
