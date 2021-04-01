DekuScrubPlaygroundRupee_BeforeDisappearing_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)
    sw      a2, 0x001C (sp)

    jal     DekuScrubPlaygroundRupee_BeforeDisappearing
    nop

    lw      a2, 0x001C (sp)

    ; Displaced code
    lw      v1, 0x1CCC (a2)
    or      at, v0, r0

    lh      v0, 0x019C (s0)

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14