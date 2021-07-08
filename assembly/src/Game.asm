Game_AfterPrepareDisplayBuffers_Hook:
    j       Game_AfterPrepareDisplayBuffers
    lw      a0, 0x0070 (sp) ;; Load pointer to Graphics context.
