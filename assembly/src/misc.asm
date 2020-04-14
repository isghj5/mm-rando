misc_crit_wiggle_check_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)

    or      a0, s0, r0
    jal     misc_crit_wiggle_check
    or      a1, v1, r0

    ; Move result to AT
    or      at, v0, r0

    lw      ra, 0x0018 (sp)
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x20

underwater_ocarina_check:
    ; Original check to disallow using items before the empty bottle underwater.
    slti    at, t7, 0x0012
    beqz    at, @@return
    nop
@@check_ocarina:
    addiu   sp, sp, -0x30
    sw      ra, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      a3, 0x001C (sp)
    sw      t0, 0x0020 (sp)
    sw      t2, 0x0024 (sp)
    sw      t5, 0x0028 (sp)

    jal     misc_can_use_ocarina_underwater
    sw      t7, 0x002C (sp)

    lw      ra, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      a3, 0x001C (sp)
    lw      t0, 0x0020 (sp)
    lw      t2, 0x0024 (sp)
    lw      t5, 0x0028 (sp)
    lw      t7, 0x002C (sp)

    ; If underwater ocarina disabled, return early.
    beqz    v0, @@return
    addiu   sp, sp, 0x30

    ; If not ocarina (0x00) won't branch and thus will skip instruction which sets AT.
    beqzl   t7, @@return
    addiu   at, r0, 0
@@return:
    jr      ra
    nop
