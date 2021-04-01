WorldColors_GetBlueBubbleColor_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v0, 0x0010 (sp)

    or      a0, s1, r0
    jal     WorldColors_GetBlueBubbleColor
    or      a1, s2, r0

    ; Place return value into T9
    or      t9, v0, r0

    lw      v0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x18
