; =========================================================
; File: 0x00B3C000, Address: 0x00C72CD0, Offset: 0x00136CD0
; Name: code
; =========================================================
.headersize (0x800A5AC0 - 0x00B3C000)
; Replaces (. is \x00):
;   nt_reg[82]..week
;   _event_reg[83]..
;   week_event_reg[8
;   4]..week_event_r
;   eg[85]..
.org 0x801DC790
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0010 (sp)
    sw      a3, 0x0008 (sp)
    or      a3, a1, r0
    or      a2, a0, r0
    or      a1, s0, r0
    jal     mmr_GetNewGiIndex
    or      a0, s6, r0
    lw      a3, 0x0008 (sp)
    lw      a2, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20