elegy_speedup_get_block_speed_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     elegy_speedup_get_block_speed
    or      a0, s0, r0

    ; Store result in actor field offset 0x70.
    swc1    f0, 0x0070 (s0)

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

elegy_speedup_get_lock_params_hook:
    ; Displaced code
    or      a3, a1, r0

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a2, 0x0010 (sp)

    jal     elegy_speedup_get_lock_params
    sw      a3, 0x0014 (sp)

    ; Move hiword of result to T0.
    srl     t0, v0, 16
    ; Move loword of result to T1.
    andi    t1, v0, 0xFFFF

    lw      a2, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

elegy_speedup_get_statue_despawn_counter_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    or      a0, s1, r0
    jal     elegy_speedup_get_statue_despawn_counter
    or      a1, s0, r0

    ; Move result to T9.
    or      t9, v0, r0

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

elegy_speedup_get_statue_fade_speed_hook:
    lw      at, 0x0044 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a1, 0x0010 (sp)

    or      a0, s0, r0
    jal     elegy_speedup_get_statue_fade_speed
    or      a1, at, r0

    lw      a1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

elegy_speedup_handle_camera_hook:
    lw      at, 0x002C (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)

    or      a0, a1, r0
    jal     elegy_speedup_should_update_camera
    or      a1, at, r0

    beqz    v0, @@no_update
    nop

    ; Perform original call to update camera.
    lw      a0, 0x0010 (sp)
    jal     0x800F1D84
    lw      a1, 0x0014 (sp)

@@no_update:
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

elegy_speedup_handle_darken_hook:
    lw      at, 0x002C (sp)

    addiu   sp, sp, -0x28
    sw      ra, 0x0024 (sp)
    swc1    f2, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)
    sw      a2, 0x001C (sp)
    sw      a3, 0x0020 (sp)

    or      a0, s0, r0
    jal     elegy_speedup_should_darken
    or      a1, at, r0

    beqz    v0, @@no_darken
    nop

    ; Perform original call to darken scene.
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)
    jal     0x800FD2B4
    lw      a3, 0x0020 (sp)

@@no_darken:
    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x28

elegy_speedup_update_effect_position_hook:
    ; Displaced code
    or      s0, a0, r0

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     elegy_speedup_update_effect_position
    nop

    lw      ra, 0x0010 (sp)
    addiu   sp, sp, 0x18
    jr      ra
    lw      a1, 0x002C (sp)
