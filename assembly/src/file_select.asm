file_select_before_draw_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     file_select_before_draw
    sw      a0, 0x0014 (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)

    ; Displaced code
    or      s0, a0, r0

    jr      ra
    addiu   sp, sp, 0x18

file_select_draw_hash_hook:
    ; Displaced code
    sw      t6, 0x02B0 (a1)

    addiu   sp, sp, -0x30
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)
    sw      a2, 0x001C (sp)
    sw      a3, 0x0020 (sp)
    sw      v0, 0x0024 (sp)
    sw      t4, 0x0028 (sp)

    jal     file_select_draw_hash
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)
    lw      a3, 0x0020 (sp)
    lw      v0, 0x0024 (sp)
    lw      t4, 0x0028 (sp)

    jr      ra
    addiu   sp, sp, 0x30
