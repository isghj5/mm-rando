stray_fairy_group_give_item_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)

    jal     stray_fairy_group_give_reward
    sw      a2, 0x001C (sp) ; Store relocated pointer

    bnez    v0, @@caller_return
    nop


    ;Displaced code:
    lb      a0, 0x0038 (s0)
    jal     0x800F1C68
    or      a1, s0, r0
    lh      a1, 0x001C (s0)
    lw      t6, 0x001C (sp) ; Load relocated pointer
    sw      t6, 0x14C (s0)

    or      v0, r0, r0

@@caller_return:
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20