;==================================================================================================
; Actor Hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Replaced:
;   jalr    ra, v0
;   sw      a0, 0x0018 (sp)
;   lw      a0, 0x0018 (sp)
;   sw      r0, 0x0134 (a0)
.org 0x800B6968
    sw      a0, 0x0018 (sp)
    jal     Actor_AfterDtor_Hook
    sw      a1, 0x001C (sp)
    nop

; Replaced:
;   JALR    RA, T9
.org 0x800B6928
    jal     Actor_Init

; Replaced:
;   JALR    RA, T9
.org 0x800B9510
    jal     Actor_Init

; Replaced:
;   JALR    RA, T9
.org 0x800B9744
    jal     Actor_Update
