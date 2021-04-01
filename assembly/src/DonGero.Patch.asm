;==================================================================================================
; Hook cutscene starting
;==================================================================================================

.headersize G_EN_GEG_DELTA

; Replaces:
;   jal     0x800F1CE0
.org 0x80BB2598
    jal     DonGero_StartCutscene

; Replaces
;   lh      v0, 0x04D8 (s0)
;   addiu   a0, s0, 0x0024
;   lui     t7, 0x80BB ; relocation removed
;   slti    at, v0, 0x0002
;   beqz    at, 0x80BB340C
;   sll     t6, v0, 2
;   subu    t6, t6, v0
;   sll     t6, t6, 2
;   addiu   t7, t7, 0x4044 ; relocation removed
;   addu    a1, t6, t7
.org 0x80BB332C
    jal     DonGero_GetRollTarget
    lh      a0, 0x04D8 (s0)
    beqz    v0, 0x80BB340C
    nop
    addiu   a0, s0, 0x0024
    or      a1, v0, r0
    nop
    nop
    nop
    nop

; Replaces
;   lh      t8, 0x04D8 (s0)
;   lui     t0, 0x80BB ; relocation removed
;   addiu   t0, t0, 0x4044 ; relocation removed
;   sll     t9, t8, 2
;   subu    t9, t9, t8
;   sll     t9, t9, 2
;   addu    a1, t9, t0
.org 0x80BB3360
    jal     DonGero_GetRollTarget
    lh      a0, 0x04D8 (s0)
    or      a1, v0, r0
    nop
    nop
    nop
    nop

; Fix relocations.
; Replaces:
;   .dw 0x45001C64
;   .dw 0x46001C7C
;   .dw 0x45001C94
;   .dw 0x46001C98
.org 0x80BB4428
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x00000000

; Rest of the modifications are in ~/Resources/mods/shorten-cutscene-don-gero.bin
