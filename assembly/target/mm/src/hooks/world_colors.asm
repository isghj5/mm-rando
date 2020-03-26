;==================================================================================================
; Blue Bubble color
;==================================================================================================

.headersize (G_EN_BB_VRAM - G_EN_BB_FILE)

; Replaces:
;   lui     t8, 0xFB00
;   ori     t9, r0, 0xFF00
;   addiu   t7, v0, 0x0008
;   sw      t7, 0x02C0 (s0)
;   sw      t9, 0x0004 (v0)
;   sw      t8, 0x0000 (v0)
.org 0x808C361C ; Offset: 0x18DC‬
.area 0x18
    lui     t8, 0xFB00
    addiu   t7, v0, 0x0008
    sw      t7, 0x02C0 (s0)
    jal     world_colors_get_blue_bubble_color_hook
    sw      t8, 0x0000 (v0)
    sw      t9, 0x0004 (v0)
.endarea
