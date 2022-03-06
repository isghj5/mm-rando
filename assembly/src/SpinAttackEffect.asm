SpinAttack_ShouldSpinMainRun_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a1, 0x001C (sp)

    jal     SpinAttack_ShouldSpinMainRun
    nop

    lw     a1, 0x001C (sp)

    ; Displaced code:
    lw      t7, 0x1CCC (a1)
    addiu   a0, s0, 0x1A4
    addiu   a1, r0, 0x0000

@@caller_return:
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
