Cows_IsCloseEnoughToGiveMilk_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     Cows_IsCloseEnoughToGiveMilk
    sw      a3, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
