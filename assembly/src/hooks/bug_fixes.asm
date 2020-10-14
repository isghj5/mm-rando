;==================================================================================================
; Fix Igos softlock
;==================================================================================================

.headersize (G_EN_KNIGHT_VRAM - G_EN_KNIGHT_FILE)

; Check if player can receive item before triggering the item cutscene.
; Replaces (MMR):
;   jal     0x801DC650
;   addiu   a2, r0, 0x0076
;   nop
;   nop
;   nop
;   nop
.org 0x809B5868
    jal     player_can_receive_item
    lw      a0, 0x0054 (sp)
    beq     v0, r0, 0x809B58C0
    nop
    jal     0x801DC650
    addiu   a2, r0, 0x0076

;==================================================================================================
; Make Great Fairy softlock prevention consistent with Igos softlock fix
;==================================================================================================

.headersize (G_EN_ELFGRP_VRAM - G_EN_ELFGRP_FILE)

; Check if player can receive item before triggering the item cutscene.
; Replaces (MMR):
;   lw      t0, 0x024A (a3)
;   addiu   at, r0, 0xD918
;   beq     t0, at, 0x80A3A3D8
;   addiu   at, r0, 0xDE40
;   beq     t0, at, 0x80A3A3D8
;   addiu   at, r0, 0xD920
;   bne     t0, at, 0x80A3A470
;   lb      a0, 0x0038 (s0)
.org 0x80A3A3BC
    jal     player_can_receive_item
    lw      a0, 0x0024 (sp)
    beq     v0, r0, 0x80A3A470
    nop
    nop
    nop
    nop
    nop
