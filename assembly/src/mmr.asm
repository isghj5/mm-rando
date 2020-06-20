mmr_GetNewGiIndex_stub:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      s6, 0x0010 (sp)

    ; Set global context pointer to S6, and shift arguments
    or      s6, a0, r0
    or      a0, a1, r0
    jal     0x801DC790
    or      a1, a2, r0

    lw      s6, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
