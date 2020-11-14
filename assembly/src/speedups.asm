boat_cruise_before_cruise_end_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     boat_cruise_before_cruise_end
    sw      a1, 0x0014 (sp)

    ; Displaced code
    lui     v0, 0x801F
    addiu   v0, v0, 0xF670

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

boat_cruise_get_boat_speed_archery_hook:
    j       boat_cruise_get_boat_speed_hook
    ori     a1, r0, 0x0001

boat_cruise_get_boat_speed_cruise_hook:
    j       boat_cruise_get_boat_speed_hook
    or      a1, r0, r0

boat_cruise_get_boat_speed_hook:
    ; Displaced code
    sw      t0, 0x0180 (a0)

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     boat_cruise_get_boat_speed
    sw      t5, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      t5, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

boat_cruise_should_end_archery_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     boat_cruise_should_end_archery
    or      a0, s0, r0

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

fisherman_boat_get_accel_speed_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     fisherman_boat_get_accel_speed
    or      a0, s0, r0

    ; Move result into A2.
    mfc1    a2, f0

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

fisherman_boat_get_top_speed_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     fisherman_boat_get_top_speed
    or      a0, s0, r0

    ; Move result into AT.
    mfc1    at, f0

    ; Displaced code
    lh      t9, 0x001C (s0)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

fisherman_should_end_game_hook:
    j       fisherman_should_end_game
    or      a0, s0, r0

fisherman_should_pass_timer_check_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     fisherman_should_pass_timer_check
    or      a0, s0, r0

    ; Displaced code
    addiu   a0, s0, 0x0190

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

sakon_should_end_thief_escape_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     sakon_should_end_thief_escape
    or      a1, s1, r0

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

toto_before_advance_formal_replay_hook:
    sw      a1, 0x0024 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     toto_before_advance_formal_replay
    sw      a0, 0x0010 (sp)

    lw      a0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

toto_handle_advance_formal_replay_hook:
    ; Load params from stack for calling hook function.
    lw      a0, 0x0020 (sp)
    jal     toto_handle_advance_formal_replay
    lw      a1, 0x0024 (sp)

    ; Return from caller function early.
    lw      ra, 0x001C (sp)
    lw      s0, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
