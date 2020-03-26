;==================================================================================================
; Room Load Hooks
;==================================================================================================

.headersize(G_CODE_RAM - G_CODE_FILE)

; Before room load.
; Replaces:
;   sw      s0, 0x0028 (sp)
;   or      s0, a1, r0
;   sw      ra, 0x002C (sp)
.org 0x8012E970
    sw      ra, 0x002C (sp)
    jal     room_before_load_hook
    sw      s0, 0x0028 (sp)

; After room load.
; Replaces:
;   jr      ra
.org 0x8012EAA0
    j       room_after_load_hook

;==================================================================================================
; Room Unload Hooks
;==================================================================================================

; Before room unload.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C (sp)
.org 0x8012EBFC
    sw      ra, 0x001C (sp)
    jal     room_before_unload_hook
    sw      s0, 0x0018 (sp)

; After room unload.
; Replaces:
;   jr      ra
.org 0x8012EC74
    j       room_after_unload_hook
