Game_AfterPrepareDisplayBuffers_Hook:
    j       Game_AfterPrepareDisplayBuffers
    lw      a0, 0x0070 (sp) ;; Load pointer to Graphics context.

Game_DrawOverlay_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Game_DrawOverlay
    nop

    lw      ra, 0x0010 (sp)
    addiu   sp, sp, 0x18

    jr      ra
    ; Displaced code
    lw      t6, 0x0068 (sp)
