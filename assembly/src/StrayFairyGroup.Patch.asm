;==================================================================================================
; Stray Fairy group gives item instead of starting cutscene if not vanilla layout
;==================================================================================================

.headersize G_EN_ELFGRP_DELTA

; Check if player can receive item before triggering the item cutscene.
; Replaces:
;   lb      a0, 0x0038 (s0)
;   jal     0x800F1C68
;   or      a1, s0, r0
;   lh      a1, 0x001C (s0)
;   lui     t6, 0x80A4
;   addiu   t6, t6, 0xA274
;   sw      t6, 0x14C (s0)
.org 0x80A3A3BC
    or      a0, s0, r0
    lw      a1, 0x0024 (sp)
    lui     a2, 0x80A4
    jal     StrayFairyGroup_GiveReward_Hook
    addiu   a2, a2, 0xA274
    bne     v0, r0, 0x80A3A470
    nop

; Alter relocations.
.org 0x80A3ABC8
    .dw 0x45000D14 ; Replaces: 0x45000D1C
.org 0x80A3ABCC
    .dw 0x46000D1C ; Replaces: 0x46000D20
