models_before_stray_fairy_main_hook:
    ; Store A1 for later usage if needed
    sw      a1, 0x001C (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     models_before_stray_fairy_main
    sw      a1, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    lw      t9, 0x022C (a0)

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

models_draw_stray_fairy_hook:
    ; Displaced code
    or      s0, a1, r0

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     models_draw_stray_fairy
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0018 (sp)
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    ; Will be returning from caller function, so restore S0
    addiu   sp, sp, 0x20
    lw      s0, 0x0028 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x002C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x40

models_draw_heart_container_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     models_draw_heart_container
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    or      a3, r0, r0
    lw      a2, 0x0000 (a1)

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    addiu   sp, sp, 0x20

    ; Restore RA from caller's caller function
    lw      ra, 0x0014 (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x48

models_draw_boss_remains_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    ; Shift arguments
    or      a2, a1, r0
    or      a1, a0, r0

    jal     models_draw_boss_remains
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

models_before_moons_tear_main_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     models_before_moons_tear_main
    sw      a1, 0x0014 (sp)

    ; Displaced code
    lui     at, 0x1000
    ori     at, at, 0x0282

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

models_draw_moons_tear_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)
    jal     models_draw_moons_tear
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    or      s1, a1, r0

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    addiu   sp, sp, 0x20

    ; Restore RA from caller's caller function
    lw      ra, 0x001C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x38

models_draw_lab_fish_heart_piece_hook:
    ; Displaced code
    sw      a1, 0x0034 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     models_draw_lab_fish_heart_piece
    nop

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

@@caller_return:
    ; Restore S0
    addiu   sp, sp, 0x18
    lw      s0, 0x0018 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x001C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x30

models_before_seahorse_main_hook:
    ; Displaced code
    or      s0, a0, r0

    addiu   sp, sp, 0x18
    sw      ra, 0x0014 (sp)

    jal     models_before_seahorse_main
    sw      a1, 0x0010 (sp)

    lw      a1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, -0x18

models_draw_seahorse_hook:
    ; Displaced code
    sw      a1, 0x0054 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     models_draw_seahorse
    nop

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

@@caller_return:
    ; Restore S0
    addiu   sp, sp, 0x18
    lw      s0, 0x0028 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x002C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x50

models_draw_shop_inventory_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    ; Shift arguments
    or      a2, a1, r0
    or      a1, a0, r0

    jal     models_draw_shop_inventory
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18
