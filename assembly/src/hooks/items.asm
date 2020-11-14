;==================================================================================================
; Item Receive & Removal
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Hook after item receive.
; Replaces:
;   jr      ra
.org 0x801143C4
    j       items_after_receive_hook

; Hook after item removal.
; Replaces:
;   jr      ra
.org 0x80114A94
    j       items_after_removal_hook

;==================================================================================================
; Fix Item Receive for Custom Items
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Function begins at: 0x80112E80
; Prevent overwriting Ocarina inventory byte if item Id 0xA4 or higher.
; Replaces:
;   lui     t0, 0x801F
;   lbu     t8, 0x0047 (sp)
;   addiu   t0, t0, 0xF670
;   addu    t9, t0, t3
.org 0x801143A0
    jal     items_should_try_write_to_inventory_hook
    lui     t0, 0x801F
    beqzl   v0, 0x801143BC
    addiu   v0, r0, 0x00FF
