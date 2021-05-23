;==================================================================================================
; Copy input before PlayUpdate is run, so we can use it.
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lw      t1, 0x0000 (t9)
;   swl     t1, 0x0000 (v0)
.org 0x80169008
    jal     Input_BeforePlayUpdate_Hook
    nop
