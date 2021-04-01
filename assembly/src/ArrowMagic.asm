ArrowMagic_GetInitialConsumeState_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a1, 0x0010 (sp)
    sw      a3, 0x0014 (sp)

    jal     ArrowMagic_GetInitialConsumeState
    sw      v1, 0x0018 (sp)

    ; Place return value in T2.
    or      t2, v0, r0

    ; Displaced code
    addiu   at, r0, 0x0007

    lw      a1, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      v1, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

ArrowMagic_ShouldSetMagicCost_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a1, 0x0010 (sp)
    sw      t2, 0x0014 (sp)
    sw      v1, 0x0018 (sp)

    jal     ArrowMagic_ShouldSetMagicCost
    andi    a1, t0, 0x0008

    ; Place return value in AT.
    or      at, v0, r0

    ; Displaced code
    addiu   v0, r0, 0x0001

    lw      a1, 0x0010 (sp)
    lw      t2, 0x0014 (sp)
    lw      v1, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
