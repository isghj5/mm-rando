;==================================================================================================
; Savedata Hooks
;==================================================================================================

.headersize(G_CODE_RAM - G_CODE_FILE)

; Hook after loading savedata into z2_file from buffer after flash.
; Replaces:
;   jal     0x800FEC90
.org 0x80145024
    jal     savedata_after_load_hook

; Hook after preparing savedata.
; Replaces:
;   lw      s5, 0x0028 (sp)
;   jr      ra
;   addiu   sp, sp, 0x30
.org 0x80144568
    or      a0, s5, r0
    j       savedata_after_prepare_hook
    lw      s5, 0x0028 (sp)

; Hook after writing savedata to buffer before flash (owl statue).
; Replaces:
;   jal     0x800FEC90
.org 0x80145578
    jal     savedata_after_write_owl_hook

; Hook after writing savedata to buffer before flash (Song of Time).
; Replaces:
;   jal     0x800FEC90
.org 0x80145664
    jal     savedata_after_write_sot_hook
