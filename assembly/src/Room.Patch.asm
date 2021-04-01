;==================================================================================================
; Room Load Hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Before room load.
; Replaces:
;   sw      s0, 0x0028 (sp)
;   or      s0, a1, r0
;   sw      ra, 0x002C (sp)
.org 0x8012E970
    sw      ra, 0x002C (sp)
    jal     Room_BeforeLoad_Hook
    sw      s0, 0x0028 (sp)

; After room load.
; Replaces:
;   jr      ra
.org 0x8012EAA0
    j       Room_AfterLoad_Hook

;==================================================================================================
; Room Unload Hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Before room unload.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C (sp)
.org 0x8012EBFC
    sw      ra, 0x001C (sp)
    jal     Room_BeforeUnload_Hook
    sw      s0, 0x0018 (sp)

; After room unload.
; Replaces:
;   jr      ra
.org 0x8012EC74
    j       Room_AfterUnload_Hook
