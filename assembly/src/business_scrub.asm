business_scrub_before_give_item_clock_town_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     business_scrub_before_give_item_clock_town
    sw      a1, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

business_scrub_consume_item_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     business_scrub_consume_item
    sw      a0, 0x0010 (sp)

    lw      a0, 0x0010 (sp)

    ; Displaced code
    lh      v0, 0x001C (a0)
    addiu   at, r0, 0x0001

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

business_scrub_initial_dialogue_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    jal     business_scrub_set_initial_message
    nop
    or      t9, v0, r0
    andi    a3, v0, 0xffff
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
