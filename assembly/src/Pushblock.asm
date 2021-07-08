Misc_GetPushBlockSpeed_Hook:
    addiu   sp, sp, -0x20

    sw      ra, 0x0010 (sp)
    sw      at, 0x0014 (sp)
    sw      a0, 0x0018 (sp)

    jal     Misc_GetPushBlockSpeed
    sw      a1, 0x001C (sp)

    ; Place return value from F0 into F6
    mfc1    v0, f0
    mtc1    v0, f6

    lw      ra, 0x0010 (sp)
    lw      at, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)

    ; Displaced code
    mtc1    r0, f4

    jr      ra
    addiu   sp, sp, 0x20

Misc_GetIceblockPushSpeed_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    swc1    f0, 0x0014 (sp)

    jal     Misc_GetIceblockPushSpeed
    addiu   a2, sp, 0x18

    ; Move return values to F6, F18 and F12 from stack
    lwc1    f6, 0x0018 (sp)
    lwc1    f18, 0x001C (sp)
    lwc1    f12, 0x0020 (sp)

    lw      ra, 0x0010 (sp)
    lwc1    f0, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x28

Misc_GetGreatBayTempleFaucetSpeed_Hook:
    addiu   sp, sp, -0x20

    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     Misc_GetGreatBayTempleFaucetSpeed
    sw      a1, 0x0018 (sp)

    ; Place bitfield results into T8 and T9
    srl     t8, v0, 16
    andi    t9, v0, 0xFFFF

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    ; Displaced code
    or      s0, a0, r0

    jr      ra
    addiu   sp, sp, 0x20

Misc_GetSpiderHouseShelvesSpeed_Hook:
    addiu   sp, sp, -0x28

    sw      ra, 0x0020 (sp)
    swc1    f0, 0x0010 (sp)
    sw      a2, 0x0014 (sp)

    or      a3, a2, r0
    jal     Misc_GetSpiderHouseShelvesSpeed
    addiu   a2, sp, 0x18

    lw      at, 0x0014 (sp)
    bnez    at, @@large_shelf
    nop

@@small_shelf:
    ; Move return values to F16 and F4 from stack
    lwc1    f16, 0x0018 (sp)
    b       @@tail
    lwc1    f4, 0x001C (sp)
@@large_shelf:
    ; Move return values to F18 and F6 from stack
    lwc1    f18, 0x0018 (sp)
    lwc1    f6, 0x001C (sp)
@@tail:
    lw      ra, 0x0020 (sp)
    lwc1    f0, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x28

Misc_GetSpiderHouseShelvesOutwardSpeed_Hook:
    lw      a1, 0x004C (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Misc_GetSpiderHouseShelvesOutwardSpeed
    swc1    f0, 0x0010 (sp)

    ; Place return value in F12
    mfc1    at, f0
    mtc1    at, f12

    lwc1    f0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Misc_GetIkanaPushblockSpeed_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)

    jal     Misc_GetIkanaPushblockSpeed
    addiu   a2, sp, 0x10

    ; Move results from stack into A1 and A2
    lw      a1, 0x0010 (sp)
    lw      a2, 0x0014 (sp)

    lw      ra, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

Misc_GetPzlblockSpeed_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Misc_GetPzlblockSpeed
    nop

    ; Place return value from F0 into A2
    mfc1    a2, f0

    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x18

Misc_GetGravestoneSpeed_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Misc_GetGravestoneSpeed
    nop

    ; Place return value in A2
    or      a2, v0, r0

    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x18

Misc_GetInWaterPushSpeed_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    swc1    f0, 0x0010 (sp)

    jal     Misc_GetInWaterPushSpeed
    or      a1, s0, r0

    ; Place return value in F16
    mfc1    at, f0
    mtc1    at, f16

    lwc1    f0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
