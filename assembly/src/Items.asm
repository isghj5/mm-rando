Items_AfterReceive_Hook:
    lw      a0, 0x0000 (sp)
    lw      a1, 0x0004 (sp)
    andi    a1, a1, 0x00FF

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Items_AfterReceive
    sw      v0, 0x0014 (sp)

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x18

Items_AfterRemoval_Hook:
    lw      a0, 0x0000 (sp)

    j       Items_AfterRemoval
    lw      a1, 0x0004 (sp)

Items_ShouldTryWriteToInventory_Hook:
    ; Displaced code
    lbu     t8, 0x0047 (sp)
    addiu   t0, t0, 0xF670
    addu    t9, t0, t3

    jr      ra
    slti    v0, a3, 0x00A4

Items_ShouldCheckItemUsabilityWhileSwimming_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a1, 0x0024 (sp)
    sw      a3, 0x002C (sp)
    sw      t3, 0x0018 (sp)
    sw      t4, 0x0014 (sp)

    jal     Items_ShouldCheckItemUsabilityWhileSwimming
    or      a1, a2, r0

    lw      t4, 0x0014 (sp)
    lw      t3, 0x0018 (sp)
    lw      a3, 0x002C (sp)
    lw      a1, 0x0024 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
