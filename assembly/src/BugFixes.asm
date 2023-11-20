BugFixes_PreventHessCrash_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     BugFixes_PreventHessCrash
    sw      v1, 0x0010 (sp)

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

BugFixes_PreventAudioCrash_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     BugFixes_PreventAudioCrash
    sw      a0, 0x0018 (sp)

    or      t0, v0, r0
    lw      t2, 0x0018 (sp)

    ; Displaced code
    addiu   s1, r0, 0x0080
    lui     t6, 0x801E

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
