;==================================================================================================
; Pause Menu (Select Item subscreen) - Icon Draw
;==================================================================================================

.headersize(G_KALEIDO_SCOPE_VRAM - G_KALEIDO_SCOPE_FILE)

; Hook draw for item textures on "Select Item" screen.
; Replaces:
;   jal     0x80821AD4
.org 0x8081BB70 ; In RDRAM: 0x807509F0
    jal     pause_menu_select_item_draw_icon_hook

; Remove relocations.
.org 0x8082C944
    .dw 0x00000000 ; Removes: 0x44005AD0

;==================================================================================================
; Pause Menu (Select Item subscreen) - Process
;==================================================================================================

; After Select Item subscreen processing complete.
; Replaces:
;   jr      ra
.org 0x8081C67C ; In RDRAM: 0x807514FC
    j       pause_menu_select_item_subscreen_after_process_hook

;==================================================================================================
; Pause Menu (Select Item subscreen) - Process A Button
;==================================================================================================

; Replaces:
;   addiu   at, r0, 0x03E7
;   sh      t6, 0x025E (s0)
;   lhu     t7, 0x002E (sp)
;   beq     s1, at, 0x8081C61C
.org 0x8081C250 ; In RDRAM: 0x807510D0
    jal     pause_menu_select_item_process_a_button_hook
    sh      t6, 0x025E (s0)
    lhu     t7, 0x002E (sp)
    bnez    v0, 0x8081C61C

;==================================================================================================
; Pause Menu (Select Item subscreen) - Set Button Enabled
;==================================================================================================

; Replaces:
;   lui     t9, 0x0001
;   addu    t9, t9, t8
;   lw      t9, 0x6818 (t9)
;   bnezl   t9, 0x8081C224
.org 0x8081C1E0 ; In RDRAM: 0x80751060
    nop
    jal     pause_menu_select_item_show_a_button_enabled_hook
    or      a0, t8, r0
    beqzl    v0, 0x8081C224
