; =========================================================
; File: 0x00B3C000, Address: 0x00C72CD0, Offset: 0x00136CD0
; Name: code
; =========================================================

.headersize G_CODE_DELTA

; Replaces (. is \x00):

; Replaces:
; week_event_reg[7
; 6]..week_event_r
; eg[77]..
.org 0x801DC710
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    or      a0, a1, r0
    lw      a1, 0x0018 (sp)
    jal     MMR_ProcessItem
    or      a2, r0, r0
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

; Replaces:
;   nt_reg[82]..week
;   _event_reg[83]..
;   week_event_reg[8
;   4]..week_event_r
;   eg[85]..week_eve
;   nt_reg[86]..week
;   _event_reg[87]..
;   week_event_reg[8
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
    jal     MMR_GetNewGiIndex
    or      a0, s6, r0
    lw      a3, 0x0008 (sp)
    lw      a2, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20
; 0x801DC7D8
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     MMR_QueueItem
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
; 0x801DC7F4
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     MMR_GetNewGiEntry
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
