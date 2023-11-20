;==================================================================================================
; Majora
;==================================================================================================

.headersize G_BOSS_07_DELTA

; Replaces:
;   LW      T2, 0x00CC (SP)
;   LUI     AT, 0x0002
;   LH      T8, 0x0A90 (T7)
;   ADDIU   T1, R0, 0x5400
;   ORI     T3, R0, 0xFFF7
;   ADDIU   T9, T8, 0x01B8
;   BNE     V0, T9, .+0x2C
;   ADDU    AT, AT, T2
;   SH      T1, 0x887A (AT)
;   LW      T5, 0x00CC (SP)
;   LUI     AT, 0x801F
;   SH      T3, 0x35BA (AT)
;   LUI     AT, 0x0002
;   ADDIU   T4, R0, 0x0014
;   ADDU    AT, AT, T5
;   SB      T4, 0x8875 (AT)
;   LW      V0, 0x2BC8 (S2)
.org 0x809F6E94
.area 0x44, 0
    lh      t8, 0x0A90 (t7)
    addiu   t9, t8, 0x01B8
    bne     v0, t9, .+0x34
    nop
    jal     Boss07_TriggerCredits
    lw      a0, 0x00CC (sp)
    lw      v0, 0x2BC8 (s2)
.endarea
