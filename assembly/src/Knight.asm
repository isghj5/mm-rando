Knight_GiveItem_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)

    jal     Misc_GetVanillaLayout
    nop
    bnez    v0, @@vanilla_layout
    nop

    jal     Player_CanReceiveItem
    nop

    beq     v0, r0, @@caller_return
    nop

    jal     0x801DC650
    addiu   a2, r0, 0x01CB

    b       @@caller_return
    addiu   v0, r0, 0x0001

@@vanilla_layout:
    jal     0x800F1BE4 ; check flag
    lb      a0, 0x0038 (s0)
    beq     v0, r0, @@set_flag
    or      a1, s0, r0
    jal     0x800F1C68 ; start cutscene
    lb      a0, 0x0038 (s0)
    b       @@caller_return
    addiu   v0, r0, 0x0001

@@set_flag:
    jal     0x800F1BA4 ; set flag
    lb      a0, 0x0038 (s0)
    or      v0, r0, r0
    
@@caller_return:
    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20
