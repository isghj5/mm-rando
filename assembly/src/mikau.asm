mikau_should_activate_beach_cutscene_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      t3, 0x0010 (sp)
    swc1    f0, 0x0014 (sp)

    jal     mikau_should_activate_beach_cutscene
    or      a0, s0, r0 ;; A0 = Player.

    ; Move result into T2.
    or      t2, v0, r0

    ; Displaced code.
    addiu   v0, r0, 0x0001

    lw      t3, 0x0010 (sp)
    lwc1    f0, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
