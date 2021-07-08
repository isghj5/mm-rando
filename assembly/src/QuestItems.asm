QuestItems_DoorCheck_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     QuestItems_DoorCheck
    sw      a0, 0x0014 (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)

    ; Move return value into T0
    or      t0, v0, r0

    ; Load back into A1, A2, V0, V1
    lw      a1, 0x003C (sp)
    lw      v0, 0x0000 (a1)
    lbu     v1, 0x0001 (v0)
    or      a2, v0, r0

    jr      ra
    addiu   sp, sp, 0x18

QuestItems_TimeTagCheck_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     QuestItems_TimeTagCheck
    sw      a1, 0x0018 (sp)

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

QuestItems_FixTradePrompt_Hook:
    ; Displaced code
    sll     a1, a1, 16

    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a1, 0x0010 (sp)
    sw      a2, 0x0014 (sp)
    sw      v1, 0x0018 (sp)

    or      a2, a0, r0
    lw      a0, 0x0020 (sp)
    lw      a1, 0x0024 (sp)
    jal     QuestItems_FixTradePrompt
    or      a3, t2, r0

    lw      a1, 0x0010 (sp)
    lw      a2, 0x0014 (sp)
    lw      v1, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
