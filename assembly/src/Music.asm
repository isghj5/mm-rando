Music_AfterChannelInit_Hook:
    addiu   sp, sp, -0x28

    or      a0, s2, r0
    jal     Music_AfterChannelInit
    lw      a1, 0x002C (sp)

    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x28
