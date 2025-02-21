Kaleido_HudColors_Pause1GetNoteAColor_Hook:
    sw      ra, -0x0004 (sp)
    jal     Kaleido_HudColors_PauseGetNoteColor_Hook
    ori     t9, r0, 0xC8
    lw      ra, -0x0004 (sp)
    jr      ra
    or      t8, v0, r0

Kaleido_HudColors_Pause1GetNoteCColor_Hook:
    sw      ra, -0x0004 (sp)
    jal     Kaleido_HudColors_PauseGetNoteColor_Hook
    ori     t9, r0, 0xC8
    lw      ra, -0x0004 (sp)
    jr      ra
    or      t9, v0, r0

Kaleido_HudColors_Pause2GetNoteColor_Hook:
    sw      ra, -0x0004 (sp)
    jal     Kaleido_HudColors_PauseGetNoteColor_Hook
    ori     t9, r0, 0x00
    lw      ra, -0x0004 (sp)
    jr      ra
    or      at, v0, r0

Kaleido_HudColors_PauseGetNoteColor_Hook:
    addiu   sp, sp, -0x0030
    sw      ra, 0x0028 (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      a3, 0x001C (sp)
    sw      t0, 0x0020 (sp)
    sw      t2, 0x0024 (sp)

    ; Get color (uses index in AT, alpha in T9)
    or      a0, at, r0
    jal     Kaleido_HudColors_GetNoteButtonColor
    or      a1, t9, r0

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      a3, 0x001C (sp)
    lw      t0, 0x0020 (sp)
    lw      t2, 0x0024 (sp)
    lw      ra, 0x0028 (sp)
    jr      ra
    addiu   sp, sp, 0x0030

Kaleido_HudColors_GetMenuBorder1Color_Hook:
    addiu   sp, sp, -0x50
    sw      ra, 0x004C (sp)

    ; Store bulk registers.
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      a3, 0x001C (sp)
    sw      t0, 0x0020 (sp)
    sw      t1, 0x0024 (sp)
    sw      t2, 0x0028 (sp)
    sw      t3, 0x002C (sp)
    sw      t4, 0x0030 (sp)
    sw      t5, 0x0034 (sp)
    sw      t6, 0x0038 (sp)
    sw      t7, 0x003C (sp)
    sw      t8, 0x0040 (sp)
    sw      t9, 0x0044 (sp)

    jal     Kaleido_HudColors_GetMenuBorder1Color
    sw      v0, 0x0048 (sp)

    ; Store result in V1.
    or      v1, v0, r0

    ; Load bulk registers.
    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      a3, 0x001C (sp)
    lw      t0, 0x0020 (sp)
    lw      t1, 0x0024 (sp)
    lw      t2, 0x0028 (sp)
    lw      t3, 0x002C (sp)
    lw      t4, 0x0030 (sp)
    lw      t5, 0x0034 (sp)
    lw      t6, 0x0038 (sp)
    lw      t7, 0x003C (sp)
    lw      t8, 0x0040 (sp)
    lw      t9, 0x0044 (sp)
    lw      v0, 0x0048 (sp)

    lw      ra, 0x004C (sp)
    jr      ra
    addiu   sp, sp, 0x50

Kaleido_HudColors_GetMenuBorder2Color1_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     Kaleido_HudColors_GetMenuBorder2Color
    sw      v1, 0x0010 (sp)

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Kaleido_HudColors_GetMenuBorder2Color2_Hook:
    addiu   sp, sp, -0x30
    sw      ra, 0x002C (sp)
    sw      a0, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      a3, 0x001C (sp)
    sw      t0, 0x0020 (sp)
    sw      t9, 0x0024 (sp)

    jal     Kaleido_HudColors_GetMenuBorder2Color
    sw      v0, 0x0028 (sp)

    ; Clear alpha bits of color value, and store result in AT.
    lui     at, 0xFFFF
    ori     at, at, 0xFF00
    and     at, v0, at

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      a3, 0x001C (sp)
    lw      t0, 0x0020 (sp)
    lw      t9, 0x0024 (sp)
    lw      v0, 0x0028 (sp)
    lw      ra, 0x002C (sp)
    jr      ra
    addiu   sp, sp, 0x30

Kaleido_HudColors_GetMenuSubtitleTextColor_Hook:
    addiu   sp, sp, -0x30
    sw      ra, 0x0028 (sp)
    sw      a1, 0x0010 (sp)
    sw      a2, 0x0014 (sp)
    sw      a3, 0x0018 (sp)
    sw      t0, 0x001C (sp)
    sw      t7, 0x0020 (sp)

    jal     Kaleido_HudColors_GetMenuSubtitleTextColor
    sw      v0, 0x0024 (sp)

    ; Store result in T6.
    or      t6, v0, r0

    lw      a1, 0x0010 (sp)
    lw      a2, 0x0014 (sp)
    lw      a3, 0x0018 (sp)
    lw      t0, 0x001C (sp)
    lw      t7, 0x0020 (sp)
    lw      v0, 0x0024 (sp)
    lw      ra, 0x0028 (sp)
    jr      ra
    addiu   sp, sp, 0x30
