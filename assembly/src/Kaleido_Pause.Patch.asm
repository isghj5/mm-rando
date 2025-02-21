;==================================================================================================
; Pause Menu (Select Item subscreen) - Icon Draw
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Hook draw for item textures on "Select Item" screen.
; Replaces:
;   jal     0x80821AD4
.org 0x8081BB70 ; In RDRAM: 0x807509F0
    jal     Kaleido_PauseMenu_SelectItemDrawIcon_Hook

; Remove relocations.
.org 0x8082C944
    .dw 0x00000000 ; Removes: 0x44005AD0

;==================================================================================================
; Pause Menu (Select Item subscreen) - Process
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; After Select Item subscreen processing complete.
; Replaces:
;   jr      ra
.org 0x8081C67C ; In RDRAM: 0x807514FC
    j       Kaleido_PauseMenu_SelectItemSubscreenAfterProcess_Hook

;==================================================================================================
; Pause Menu (Select Item subscreen) - Process A Button
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Replaces:
;   addiu   at, r0, 0x03E7
;   sh      t6, 0x025E (s0)
;   lhu     t7, 0x002E (sp)
;   beq     s1, at, 0x8081C61C
.org 0x8081C250 ; In RDRAM: 0x807510D0
    jal     Kaleido_PauseMenu_SelectItemProcessAButton_Hook
    sh      t6, 0x025E (s0)
    lhu     t7, 0x002E (sp)
    bnez    v0, 0x8081C61C

;==================================================================================================
; Pause Menu (Select Item subscreen) - Set Button Enabled
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Replaces:
;   lui     t9, 0x0001
;   addu    t9, t9, t8
;   lw      t9, 0x6818 (t9)
;   bnezl   t9, 0x8081C224
.org 0x8081C1E0 ; In RDRAM: 0x80751060
    nop
    jal     Kaleido_PauseMenu_SelectItemShowAButtonEnabled_Hook
    or      a0, t8, r0
    beqzl    v0, 0x8081C224

;==================================================================================================
; Pause Menu Update
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Replaces:
;   sw      s0, 0x0014 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C (sp)
.org 0x808283DC
    sw      ra, 0x001C (sp)
    jal     Kaleido_PauseMenu_BeforeUpdate_Hook
    sw      s0, 0x0014 (sp)
