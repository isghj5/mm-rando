Savedata_AfterLoad_Hook:
    lw      t0, 0x0040 (sp)

    addiu   sp, sp, -0x28
    sw      ra, 0x0024 (sp)

    sw      t0, 0x0014 (sp)
    sw      a0, 0x001C (sp)
    sw      a1, 0x0020 (sp)

    ; Original memcpy call
    jal     0x800FEC90
    sw      a2, 0x0010 (sp)

    ; Call hook function
    lw      a0, 0x0014 (sp)
    lw      a1, 0x001C (sp)
    lw      a2, 0x0020 (sp)
    jal     Savedata_AfterLoad
    lw      a3, 0x0010 (sp)

    lw      ra, 0x0024 (sp)

    jr      ra
    addiu   sp, sp, 0x28

Savedata_AfterPrepare_Hook:
    j       Savedata_AfterPrepare
    ; Displaced code (stack adjustment)
    addiu   sp, sp, 0x30

Savedata_AfterWrite_Owl_Hook:
    j       Savedata_AfterWrite_Hook
    ori     a3, r0, 1

Savedata_AfterWrite_Sot_Hook:
    j       Savedata_AfterWrite_Hook
    ori     a3, r0, 0

Savedata_AfterWrite_Hook:
    addiu   sp, sp, -0x28

    sw      ra, 0x0020 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)

    ; Original memcpy call
    jal     0x800FEC90
    sw      a3, 0x001C (sp)

    ; Call hook function
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    jal     Savedata_AfterWrite
    lw      a3, 0x001C (sp)

    lw      ra, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28
