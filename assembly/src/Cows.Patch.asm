;==================================================================================================
; Get Item From Closest Cow
;==================================================================================================

.headersize G_EN_COW_DELTA

; Replaces:
;   lwc1    f4, 0x0098 (a0)
;   mtc1    at, f6
;   ori     t6, a1, 0x0004
;   c.lt.s  f4, f6
;   nop
;   bc1f    0x8099CE98
;   nop
.org 0x8099CE04
.area 0x1C
    jal     Cows_IsCloseEnoughToGiveMilk_Hook
    lw      a1, 0x001C (sp)
    lhu     a1, 0x026E (a0)
    ori     t6, a1, 0x0004
    beqz    v0, 0x8099CE98
    nop
    nop
.endarea
