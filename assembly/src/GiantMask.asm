GiantMask_GetLedgeWalkOffHeight_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    swc1    f0, 0x0018 (sp)
    swc1    f2, 0x0014 (sp)
    sw      a0, 0x0020 (sp)
    or      a0, s0, r0

    jal     GiantMask_GetLedgeWalkOffHeight
    nop
    mov.s   f16, f0

    lw      a0, 0x0020 (sp)
    lwc1    f2, 0x0014 (sp)
    lwc1    f0, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
