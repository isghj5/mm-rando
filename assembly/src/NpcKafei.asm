NpcKafei_IsMaskActive_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x000C(sp)
    sw      a0, 0x0010(sp)

    jal     NpcKafei_KeatonMaskInit
    or      a0, s0, r0

    lw      ra, 0x000C(sp)
    lw      a0, 0x0010(sp)
    addiu   at, r0, 0x004F ;; displaced code
    jr      ra
    addiu   sp, sp, 0x18

NpcKafei_CheckHead_Hook:
    addiu   sp, sp, -0x0018
    sw      ra, 0x0004 (sp)
    jal     NpcKafei_CheckHead

    lw      ra, 0x0004 (sp)
    addiu   sp, sp, 0x0018

    jr      ra
    lw      t0, 0x0038 (sp)
