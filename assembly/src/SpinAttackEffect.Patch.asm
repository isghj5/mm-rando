;==================================================================================================
; Before the main Spin function
;==================================================================================================

.headersize G_EN_M_THUNDER_DELTA

; Replaces:
;   lw      t6, 0x0034 (sp)
;   addiu   a0, s0, 0x01A4
;   addiu   a1, r0, 0x0000
;   lw      t7, 0x1CCC (t6)
.org 0x808B5F7C
    jal     SpinAttack_ShouldSpinMainRun_Hook
    nop
    beqz    v0, 0x808B60C4
    lw      ra, 0x0024 (sp)
