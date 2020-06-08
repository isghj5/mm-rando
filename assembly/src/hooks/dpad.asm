;==================================================================================================
; DPAD Display
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Replaces:
;   sw      a0, 0x0068 (sp)
;   lw      t6, 0x0068 (sp)
.org 0x80118898 ; In rom: 0xAFE8F8
    jal     dpad_draw_hook
    sw      a0, 0x0068 (sp)

;==================================================================================================
; DPAD Handle (Safer)
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Replaces:
;   sw      s0, 0x0030 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x0034 (sp)
.org 0x8082FE10 ; In RDRAM: 0x8074D2A0
    sw      ra, 0x0034 (sp)
    jal     dpad_handle_hook
    sw      s0, 0x0030 (sp)

;==================================================================================================
; Transformation Mask Cutscene Skip
;==================================================================================================

; Replaces:
;   lhu     v0, 0x000C (t8)
;   andi    v0, v0, 0xC00F ; Input pad flags
.org 0x808555F0 ; In RDRAM: 0x80772A80
    jal     dpad_skip_transformation_check_hook
    lhu     v0, 0x000C (t8)
