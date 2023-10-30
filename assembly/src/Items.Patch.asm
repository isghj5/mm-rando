;==================================================================================================
; Item Receive & Removal
;==================================================================================================

.headersize G_CODE_DELTA

; Hook after item receive.
; Replaces:
;   jr      ra
.org 0x801143C4
    j       Items_AfterReceive_Hook

; Hook after item removal.
; Replaces:
;   jr      ra
.org 0x80114A94
    j       Items_AfterRemoval_Hook

;==================================================================================================
; Fix Item Receive for Custom Items
;==================================================================================================

.headersize G_CODE_DELTA

; Function begins at: 0x80112E80
; Prevent overwriting Ocarina inventory byte if item Id 0xA4 or higher.
; Replaces:
;   lui     t0, 0x801F
;   lbu     t8, 0x0047 (sp)
;   addiu   t0, t0, 0xF670
;   addu    t9, t0, t3
.org 0x801143A0
    jal     Items_ShouldTryWriteToInventory_Hook
    lui     t0, 0x801F
    beqzl   v0, 0x801143BC
    addiu   v0, r0, 0x00FF

;==================================================================================================
; Check if items usability should be checked while swimming
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   ANDI    T7, A2, 0x00FF
;   ADDIU   AT, R0, 0x0034
;   BEQL    T7, AT, 0x801109B8
.org 0x80110890
    jal     Items_ShouldCheckItemUsabilityWhileSwimming_Hook
    lw      a0, 0x0080 (sp)
    beqzl   v0, 0x801109B8
