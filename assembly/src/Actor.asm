Actor_AfterDtor_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    ; Call actor file's dtor function
    jalr    v0
    nop

    ; Load arguments from caller's caller's frame
    lw      a0, 0x0030 (sp)
    sw      r0, 0x0134 (a0)
    jal     Actor_AfterDtor
    lw      a1, 0x0034 (sp)

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18
