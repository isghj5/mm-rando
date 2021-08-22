Music_AfterChannelInit_Hook:
    addiu   sp, sp, -0x28

    or      a0, s2, r0
    jal     Music_AfterChannelInit
    lw      a1, 0x002C (sp)

    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x28

Music_SetLoadingSequenceId_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     Music_SetLoadingSequenceId
    nop

    ; Displaced code
    lw      t0, 0x0014 (sp)
    lw      v0, 0x2860 (s3)

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Music_LoadSequence_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     0x8018FCCC
    nop

    sw      v0, 0x0010 (sp)

    jal     Music_SetLoadingSequenceId
    or      a0, r0, r0

    lw      v0, 0x0010 (sp)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Music_GetAudioLoadType_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     Music_GetAudioLoadType
    nop

    or      a1, v0, r0
    lw      v1, 0x0010 (sp)
    lw      t6, 0x0010 (v1)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Music_SetLoadingSequenceId2_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Music_SetLoadingSequenceId
    sw      v0, 0x0010 (sp)

    lw      v0, 0x0010 (sp)

    lw      ra, 0x0014 (sp)
    addiu   sp, sp, 0x18

    ; Displaced code
    lbu     t6, 0x0000 (v0)
    jr      ra
    lw      t5, 0x0034 (sp)

Music_GetAudioLoadType2_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     Music_GetAudioLoadType
    nop

    or      a1, v0, r0
    lw      v1, 0x0010 (sp)
    lw      t9, 0x0010 (v1)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
