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
