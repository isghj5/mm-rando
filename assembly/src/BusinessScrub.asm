BusinessScrub_BeforeGiveItemClockTown_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     BusinessScrub_BeforeGiveItemClockTown
    sw      a1, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

BusinessScrub_ConsumeItem_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     BusinessScrub_ConsumeItem
    sw      a0, 0x0010 (sp)

    lw      a0, 0x0010 (sp)

    ; Displaced code
    lh      v0, 0x001C (a0)
    addiu   at, r0, 0x0001

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

BusinessScrub_InitialDialogue_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    jal     BusinessScrub_SetInitialMessage
    nop
    or      t9, v0, r0
    andi    a3, v0, 0xffff
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
