Models_BeforeStrayFairyMain_Hook:
    ; Store A1 for later usage if needed
    sw      a1, 0x001C (sp)

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     Models_BeforeStrayFairyMain
    sw      a1, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    lw      t9, 0x022C (a0)

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Models_DrawStrayFairy_Hook:
    ; Displaced code
    or      s0, a1, r0

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     Models_DrawStrayFairy
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0018 (sp)
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    ; Will be returning from caller function, so restore S0
    addiu   sp, sp, 0x20
    lw      s0, 0x0028 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x002C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x40

Models_DrawHeartContainer_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     Models_DrawHeartContainer
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    or      a3, r0, r0
    lw      a2, 0x0000 (a1)

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    addiu   sp, sp, 0x20

    ; Restore RA from caller's caller function
    lw      ra, 0x0014 (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x48

Models_DrawBossRemains_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    ; Shift arguments
    or      a2, a1, r0
    or      a1, a0, r0

    jal     Models_DrawBossRemains
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Models_BeforeMoonsTearMain_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     Models_BeforeMoonsTearMain
    sw      a1, 0x0014 (sp)

    ; Displaced code
    lui     at, 0x1000
    ori     at, at, 0x0282

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Models_DrawMoonsTear_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)
    jal     Models_DrawMoonsTear
    sw      a1, 0x0014 (sp)

    bnez    v0, @@caller_return
    nop

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)

    ; Displaced code
    or      s1, a1, r0

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    addiu   sp, sp, 0x20

    ; Restore RA from caller's caller function
    lw      ra, 0x001C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x38

Models_DrawLabFishHeartPiece_Hook:
    ; Displaced code
    sw      a1, 0x0034 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Models_DrawLabFishHeartPiece
    nop

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

@@caller_return:
    ; Restore S0
    addiu   sp, sp, 0x18
    lw      s0, 0x0018 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x001C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x30

Models_BeforeSeahorseMain_Hook:
    ; Displaced code
    or      s0, a0, r0

    addiu   sp, sp, 0x18
    sw      ra, 0x0014 (sp)

    jal     Models_BeforeSeahorseMain
    sw      a1, 0x0010 (sp)

    lw      a1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, -0x18

Models_DrawSeahorse_Hook:
    ; Displaced code
    sw      a1, 0x0054 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Models_DrawSeahorse
    nop

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

@@caller_return:
    ; Restore S0
    addiu   sp, sp, 0x18
    lw      s0, 0x0028 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x002C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x50

Models_DrawShopInventory_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    ; Shift arguments
    or      a2, a1, r0
    or      a1, a0, r0

    jal     Models_DrawShopInventory
    or      a0, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Models_BioBabaHeartPieceRotationFix_Hook:
    bnez    at, @@return
    nop
    addiu   at, r0, 0x0017
    beq     v0, at, @@return
    addiu   at, r0, 0x0001
    slti    at, v0, 0x001D
    xori    at, at, 0x0001

@@return:
    jr      ra
    nop

Models_DrawItem00_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)
    sw      a2, 0x001C (sp)

    jal     Models_DrawItem00
    nop

    beq     v0, r0, @@displaced_code
    nop

    lui     t9, 0x800A
    b       @@caller_return
    addiu   t9, t9, 0x729C

@@displaced_code:
    lhu     t9, 0x001C (s0)
    sll     t9, t9, 2
    lui     at, 0x801E
    addu    at, at, t9
    lw      t9, 0xBFF4 (at)

@@caller_return:
    lw      a2, 0x001C (sp)
    lw      a1, 0x0018 (sp)
    lw      a0, 0x0014 (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Models_Item00_SetActorSize_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     Models_Item00_SetActorSize
    nop

    beq     v0, r0, @@displaced_code
    nop

    lui     t7, 0x800A
    b       @@caller_return
    addiu   t7, t7, 0x5E80

@@displaced_code:
    lhu     t7, 0x001C (s0)
    sll     t7, t7, 2
    lui     at, 0x801E
    addu    at, at, t7
    lw      t7, 0xBDF4 (at)

@@caller_return:
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Models_DrawScopecoin_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      s0, 0x0038 (sp)
    sw      a0, 0x0058 (sp)
    sw      a1, 0x005C (sp)

    jal     Models_DrawScopecoin
    nop

    lw      t6, 0x005C (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Models_DrawScRuppe_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      s0, 0x0038 (sp)
    sw      a0, 0x0058 (sp)
    sw      a1, 0x005C (sp)

    jal     Models_DrawScRuppe
    nop

    lw      t6, 0x005C (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20

Models_RotateScRuppe_Hook:
    or      a0, s0, r0
    lw      a1, 0x001C (sp)
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     Models_RotateScRuppe
    nop

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Models_DrawDekuScrubPlaygroundRupee_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      s0, 0x0038 (sp)
    sw      a0, 0x0058 (sp)
    sw      a1, 0x005C (sp)

    jal     Models_DrawDekuScrubPlaygroundRupee
    nop

    lw      t6, 0x005C (sp)
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20
