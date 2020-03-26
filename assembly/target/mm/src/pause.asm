pause_menu_select_item_draw_icon_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)

    ; Parameter 5 (inventory slot index)
    sw      s1, 0x0010 (sp)

    ; Parameter 6 (vertices index for Quadrangle)
    lw      at, 0x0030 (sp)
    sw      at, 0x0014 (sp)

    ; Parameter 7 (Vertex buffer index)
    sw      s2, 0x0018 (sp)

    jal     pause_menu_select_item_draw_icon
    ; Overwrite A1 with item Id in V0 (we can calculate A1 with it later)
    or      a1, v0, r0

    lw      ra, 0x001C (sp)

    jr      ra
    addiu   sp, sp, 0x20

pause_menu_select_item_subscreen_after_process_hook:
    j       pause_menu_select_item_subscreen_after_process
    lw      a0, 0x0000 (sp)

pause_menu_select_item_process_a_button_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      t1, 0x0014 (sp)
    sw      t2, 0x0018 (sp)

    ; Move AT (none_val) and S1 (cur_val) into arguments
    or      a1, s1, r0
    addiu   a2, r0, 0x03E7

    jal     pause_menu_select_item_process_a_button
    ; Load A0 from caller stack
    lw      a0, 0x0060 (sp)

    lw      ra, 0x0010 (sp)
    lw      t1, 0x0014 (sp)
    lw      t2, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

pause_menu_select_item_show_a_button_enabled_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     pause_menu_select_item_show_a_button_enabled
    sw      t1, 0x0014 (sp)

    lw      ra, 0x0010 (sp)
    lw      t1, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x18
