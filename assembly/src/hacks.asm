;==================================================================================================
; DPad Not-on-C-Buttons Fixes
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Remove C button check for equipped mask, fixes using B button item (usually sword).
; Replaces:
;   jal     0x8074D19C
;.org 0x8082FE84 ; In RDRAM: 0x8074D314
;    addiu   v0, r0, 1

; For some items, if UseItem is called without said item being on the C-buttons,
; it will be automatically called again to unequip. Prevents this.
; Replaces:
;   jal     0x8074EE20
;.org 0x8082FF1C ; In RDRAM: 0x8074D3AC
;    nop

; Remove relocations.
;.org 0x8085E908
;    .dw 0x00000000 ; Replaces: 0x440023F4
;.org 0x8085E910
;    .dw 0x00000000 ; Replaces: 0x4400248C

;==================================================================================================
; Draw C Button Amounts Fix
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Fixes B & C button item amounts and text being drawn in green if magic has not yet been obtained.

; Fix for B button icon.
; Replaces:
;   jal     0x80118084
.org 0x80120214
    jal     draw_b_button_icon_color_fix

; Fix for C button icons.
; Replaces:
;   jal     0x80118890
.org 0x8012021C
    jal     draw_c_button_icons_color_fix
