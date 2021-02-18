message_table_get_data_file_vrom_hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0024 (sp)
    sw      a2, 0x0010 (sp)
    sw      a3, 0x0014 (sp)
    sw      t6, 0x0018 (sp)
    sw      t8, 0x001C (sp)

    jal     message_table_get_data_file_vrom
    sw      at, 0x0020 (sp)

    lw      a2, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      t6, 0x0018 (sp)
    lw      t8, 0x001C (sp)
    lw      at, 0x0020 (sp)
    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x28

message_table_is_primary_message_hook:
    addiu   sp, sp, -0x30
    sw      ra, 0x0028 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      a3, 0x001C (sp)
    sw      v1, 0x0020 (sp)
    sw      t3, 0x0024 (sp)

    lw      a0, 0x0070 (sp) ;; Load GlobalContext from caller stack (reserved).
    jal     message_table_is_primary_message
    or      a1, t1, r0      ;; Message Id.

    ; Move result to AT
    or      at, v0, r0

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      a3, 0x001C (sp)
    lw      v1, 0x0020 (sp)
    lw      t3, 0x0024 (sp)
    lw      ra, 0x0028 (sp)
    jr      ra
    addiu   sp, sp, 0x30

message_table_lookup_extended_hook:
    lw      a1, 0x0004 (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0010 (sp)
    sw      a2, 0x0014 (sp)

    jal     message_table_lookup_extended
    sw      v1, 0x0018 (sp)

    ; Move result to T9
    or      t9, v0, r0

    lw      a0, 0x0010 (sp)
    lw      a2, 0x0014 (sp)
    lw      v1, 0x0018 (sp)

    ; Displaced code
    addiu   v0, a0, 0x4A70
    lui     at, 0x0001
    lw      a1, 0x0004 (v1)
    lw      a3, 0x000C (v1)
    addu    at, at, v0

    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
