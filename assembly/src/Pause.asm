PauseMenu_SelectItemDrawIcon_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)

    ; Parameter 5 (inventory slot index)
    sw      s1, 0x0010 (sp)

    ; Parameter 6 (vertices index for Quadrangle)
    lw      at, 0x0030 (sp)
    sw      at, 0x0014 (sp)

    ; Parameter 7 (Vertex buffer index)
    sw      s2, 0x0018 (sp)

    jal     PauseMenu_SelectItemDrawIcon
    ; Overwrite A1 with item Id in V0 (we can calculate A1 with it later)
    or      a1, v0, r0

    lw      ra, 0x001C (sp)

    jr      ra
    addiu   sp, sp, 0x20

PauseMenu_SelectItemSubscreenAfterProcess_Hook:
    j       PauseMenu_SelectItemSubscreenAfterProcess
    lw      a0, 0x0000 (sp)

PauseMenu_SelectItemProcessAButton_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      t1, 0x0014 (sp)
    sw      t2, 0x0018 (sp)

    ; Move AT (none_val) and S1 (cur_val) into arguments
    or      a1, s1, r0
    addiu   a2, r0, 0x03E7

    jal     PauseMenu_SelectItemProcessAButton
    ; Load A0 from caller stack
    lw      a0, 0x0060 (sp)

    lw      ra, 0x0010 (sp)
    lw      t1, 0x0014 (sp)
    lw      t2, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

PauseMenu_SelectItemShowAButtonEnabled_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     PauseMenu_SelectItemShowAButtonEnabled
    sw      t1, 0x0014 (sp)

    lw      ra, 0x0010 (sp)
    lw      t1, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x18

PauseMenu_BeforeUpdate_Hook:
    j       PauseMenu_BeforeUpdate
    ; Displaced code
    or      s0, a0, r0
