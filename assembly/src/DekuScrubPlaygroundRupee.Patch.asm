;==================================================================================================
; Give Rupee
;==================================================================================================

.headersize G_EN_GAMELUPY_DELTA

; Replaces:
;   lw      t0, 0x0194 (a1)
;   bne     t0, at, 0x80AF6A04
;   nop
;   addiu   a0, r0, 0x0005
;   jal     0x801159EC
;   sw      a1, 0x0018 (sp)
;   b       0x80AF6A10
;   lw      a1, 0x0018 (sp)
;   jal     0x801159EC
;   sw      a1, 0x0018 (sp)
.org 0x80AF69E4
    lw      a0, 0x001C (sp)
    jal     DekuScrubPlaygroundRupee_GiveItem
    lw      a1, 0x0018 (sp)
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Remove call to PlaySfxAtActor. Handled in DekuScrubPlaygroundRupee_GiveItem
; Replaces
;   jal     0x800B8EC8
.org 0x80AF6A50
    nop

;==================================================================================================
; When the rupee is disappearing into link
;==================================================================================================

; Replaces
;   lw      v1, 0x1CCC (a2)
;   slti    at, v0, 0x001F
.org 0x80AF6A90
    jal     DekuScrubPlaygroundRupee_BeforeDisappearing_Hook
    nop
