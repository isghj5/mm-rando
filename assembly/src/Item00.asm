Item00_Constructor_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    jal     Item00_Constructor
    sw      a1, 0x0018 (sp)

    ; Displaced code
    lh      t4, 0x001C (s0)
    addiu   at, r0, 0x0015

    lw      a1, 0x0018 (sp)
    lw      a0, 0x0014 (sp)
    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x14

Item00_GiveItem_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)
    sw      a2, 0x001C (sp)

    or      a1, a0, r0
    jal     Item00_GiveItem
    or      a0, s0, r0

    beq     v0, r0, @@displaced_code
    nop

    lui     t3, 0x800A
    b       @@caller_return
    addiu   t3, t3, 0x70CC

@@displaced_code:
    lhu     t3, 0x001C (s0)
    sll     t3, t3, 2
    lui     at, 0x801E
    addu    at, at, t3
    lw      t3, 0xBF24 (at)

@@caller_return:
    lw      a2, 0x001C (sp)
    lw      a1, 0x0018 (sp)
    lw      a0, 0x0014 (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Item00_BeforeBeingPickedUp_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     Item00_BeforeBeingPickedUp
    sw      a1, 0x0018 (sp)

    lw      a1, 0x0018 (sp)

    ; Displaced code
    lw      t6, 0x1CCC (a1)
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Item00_GetDespawnDelayAmount_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     Item00_GetDespawnDelayAmount
    or      a0, s0, r0

    ; Displaced code
    or      t1, v0, r0
    addiu   t2, r0, 0x0023

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x0014
