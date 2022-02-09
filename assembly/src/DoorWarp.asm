DoorWarp_GiveItem_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)

    jal     DoorWarp_GiveItem
    sw      a1, 0x001C (sp)

    lw      a1, 0x001C (sp)
    lw      a0, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

DoorWarp_GiveItem2_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      t5, 0x0018 (sp)
    sw      t6, 0x0014 (sp)

    or      a0, s0, r0
    jal     DoorWarp_GiveItem2
    or      a1, s1, r0

    lw      t5, 0x0018 (sp)
    lw      t6, 0x0014 (sp)

    ; Displaced code
    andi    v0, t5, 0xFFFF
    slt     at, t6, v0

    lw      ra, 0x001C (sp)
    lw      v1, 0x1CCC (s1)
    jr      ra
    addiu   sp, sp, 0x20
