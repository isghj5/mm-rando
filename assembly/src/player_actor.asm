before_player_actor_update_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     before_player_actor_update
    nop

    ; Displaced code
    lw      t6, 0x0A74 (s0)
    addiu   at, r0, 0xFFEF

    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x18
