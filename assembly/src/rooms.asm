room_before_load_hook:
    ; Store A1 on stack for room_after_load_hook to retrieve.
    sw      a1, 0x0044 (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)

    jal     room_before_load
    sw      a2, 0x001C (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)

    ; Displaced code
    or      s0, a1, r0

    jr      ra
    addiu   sp, sp, 0x20

room_after_load_hook:
    lw      a0, 0x0000 (sp)
    lw      a1, 0x0004 (sp)

    j       room_after_load
    lw      a2, 0x0008 (sp)

room_before_unload_hook:
    ; Store A0 on stack for room_after_unload_hook to retrieve.
    sw      a0, 0x0028 (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     room_before_unload
    sw      a1, 0x0018 (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    ; Displaced code
    or      s0, a0, r0

    jr      ra
    addiu   sp, sp, 0x20

room_after_unload_hook:
    lw      a0, 0x0000 (sp)

    j       room_after_unload
    lw      a1, 0x0004 (sp)
