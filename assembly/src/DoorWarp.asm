DoorWarp_GiveItem_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     DoorWarp_GiveItem
    sw      a1, 0x0018 (sp)

    lw      a1, 0x0018 (sp)
    lw      a0, 0x0014 (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

DoorWarp_GiveItem2_Hook:
    addiu   sp, sp, -0x1C
    sw      ra, 0x0018 (sp)
    sw      t5, 0x0014 (sp)
    sw      t6, 0x0010 (sp)

    or      a0, s0, r0
    jal     DoorWarp_GiveItem2
    or      a1, s1, r0

    lw      t5, 0x0014 (sp)
    lw      t6, 0x0010 (sp)

    ; Displaced code
    andi    v0, t5, 0xFFFF
    slt     at, t6, v0

    lw      ra, 0x0018 (sp)
    lw      v1, 0x1CCC (s1)
    jr      ra
    addiu   sp, sp, 0x1C
