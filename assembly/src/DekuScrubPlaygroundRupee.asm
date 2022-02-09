DekuScrubPlaygroundRupee_BeforeDisappearing_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a2, 0x0020 (sp)

    jal     DekuScrubPlaygroundRupee_BeforeDisappearing
    nop

    lw      a2, 0x0020 (sp)

    ; Displaced code
    lw      v1, 0x1CCC (a2)
    or      at, v0, r0

    lh      v0, 0x019C (s0)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
