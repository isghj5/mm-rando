game_after_prepare_display_buffers_hook:
    j       game_after_prepare_display_buffers
    lw      a0, 0x0070 (sp) ;; Load pointer to Graphics context.
