@MiscFlags equ (MISC_CONFIG + 0x18)

Scarecrow_CheckSongFlag_Hook:
    ; Check Scarecrow's Song flag, check Free Scarecrow bit, and return the OR result.
    lbu     t6, 0x05B7 (t6)
    andi    t7, t6, 0x0008
    lui     at, hi(@MiscFlags)
    lw      at, lo(@MiscFlags) (at)
    andi    at, 0x1000 ; Check Free Scarecrow bit.
    jr      ra
    or      v0, t7, at

Scarecrow_ShouldActivate_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0024 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      t3, 0x0018 (sp)
    sw      t4, 0x001C (sp)

    jal     Scarecrow_ShouldActivate
    sw      t5, 0x0020 (sp)

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      t3, 0x0018 (sp)
    lw      t4, 0x001C (sp)
    lw      t5, 0x0020 (sp)
    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x28

Scarecrow_ShouldSpawn_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a1, 0x0010 (sp)

    ; Perform original call to ActorCutscene_GetCanPlayNext.
    jal     0x800F1BE4
    lh      a0, 0x01AE (v1)

    beqz    v0, @@Return

    or      a0, s0, r0      ; A0 = Actor
    jal     Scarecrow_ShouldSpawn
    lw      a1, 0x0010 (sp) ; A1 = GlobalContext

@@Return:
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
