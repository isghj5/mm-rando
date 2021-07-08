Room_BeforeLoad_Hook:
    ; Store A1 on stack for Room_AfterLoad_Hook to retrieve.
    sw      a1, 0x0044 (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)

    jal     Room_BeforeLoad
    sw      a2, 0x001C (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)

    ; Displaced code
    or      s0, a1, r0

    jr      ra
    addiu   sp, sp, 0x20

Room_AfterLoad_Hook:
    lw      a0, 0x0000 (sp)
    lw      a1, 0x0004 (sp)

    j       Room_AfterLoad
    lw      a2, 0x0008 (sp)

Room_BeforeUnload_Hook:
    ; Store A0 on stack for Room_AfterUnload_Hook to retrieve.
    sw      a0, 0x0028 (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     Room_BeforeUnload
    sw      a1, 0x0018 (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    ; Displaced code
    or      s0, a0, r0

    jr      ra
    addiu   sp, sp, 0x20

Room_AfterUnload_Hook:
    lw      a0, 0x0000 (sp)

    j       Room_AfterUnload
    lw      a1, 0x0004 (sp)
