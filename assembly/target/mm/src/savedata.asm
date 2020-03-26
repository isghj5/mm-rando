savedata_after_load_hook:
    lw      t0, 0x0040 (sp)
    lw      t1, 0x0044 (sp)

    addiu   sp, sp, -0x28
    sw      ra, 0x0024 (sp)

    sw      t0, 0x0014 (sp)
    sw      t1, 0x0018 (sp)
    sw      a0, 0x001C (sp)
    sw      a1, 0x0020 (sp)

    ; Original memcpy call
    jal     0x800FEC90
    sw      a2, 0x0010 (sp)

    ; Call hook function
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)
    jal     savedata_after_load
    lw      a3, 0x0020 (sp)

    lw      ra, 0x0024 (sp)

    jr      ra
    addiu   sp, sp, 0x28

savedata_after_prepare_hook:
    j       savedata_after_prepare
    ; Displaced code (stack adjustment)
    addiu   sp, sp, 0x30

savedata_after_write_owl_hook:
    j       savedata_after_write_hook
    ori     a3, r0, 1

savedata_after_write_sot_hook:
    j       savedata_after_write_hook
    ori     a3, r0, 0

savedata_after_write_hook:
    addiu   sp, sp, -0x28

    sw      ra, 0x0020 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)

    ; Original memcpy call
    jal     0x800FEC90
    sw      a3, 0x001C (sp)

    ; Call hook function
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    jal     savedata_after_write
    lw      a3, 0x001C (sp)

    lw      ra, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28
