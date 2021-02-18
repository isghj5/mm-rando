;==================================================================================================
; Igos gives item instead of starting cutscene if not vanilla layout
;==================================================================================================

.headersize (G_EN_KNIGHT_VRAM - G_EN_KNIGHT_FILE)

; Check if player can receive item before triggering the item cutscene.
; Replaces:
;   jal     0x800F1BE4
;   lb      a0, 0x0038 (s0)
;   beq     v0, r0, 0x809B5890
;   or      a1, s0, r0
;   jal     0x800F1C68
;   lb      a0, 0x0038 (s0)
.org 0x809B5868
    jal     knight_give_item_hook
    lw      a0, 0x0054 (sp)
    beq     v0, r0, 0x809B58A4
    nop
    nop
    nop