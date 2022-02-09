Dampe_GiveReward_Hook:
    or      a2, a0, r0
    or      a0, s0, r0
    lw      a1, 0x002C (sp)
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Dampe_GiveReward
    nop

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
