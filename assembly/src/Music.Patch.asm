.headersize G_CODE_DELTA

; Replaces:
;   jr      ra
.org 0x80197D1C
    j       Music_AfterChannelInit_Hook

; Replaces:
;   LB      T1, 0x0004 (A1)
;   LBU     T4, 0x0000 (A0)
;   SLL     T2, T1, 4
;   ANDI    T3, T2, 0x0010
;   ANDI    T5, T4, 0xFFEF
;   OR      T6, T3, T5
;   B       0x801942AC
;   SB      T6, 0x0000 (A0)
.org 0x801941D8
    or      a2, s4, r0
    jal     Music_HandleChannelMute
    or      a3, s0, r0
    nop
    nop
    nop
    b       0x801942AC
    nop

; Replaces:
;   LW      T0, 0x0034 (SP)
;   LW      V0, 0x2860 (S3)
.org 0x8018FBE4
    jal     Music_SetLoadingSequenceId_Hook
    lw      a0, 0x0034 (sp)

; Replaces:
;   JAL     0x8018FCCC
.org 0x8018FC34
    jal     Music_LoadSequence_Hook

; Replaces:
;   LB      A1, 0x0019 (A0)
;   LW      T6, 0x0010 (V1)
.org 0x8018FFE8
    jal     Music_GetAudioLoadType_Hook
    lw      a1, 0x0058 (sp)

; Replaces:
;   LBU     T6, 0x0000 (V0)
;   LW      T5, 0x0034 (SP)
.org 0x801A7F1C
    jal     Music_SetLoadingSequenceId2_Hook
    lw      a0, 0x0044 (sp)

; Replaces:
;   LB      A1, 0x0019 (A0)
;   LW      T9, 0x0010 (V1)
.org 0x8019089C
    jal     Music_GetAudioLoadType2_Hook
    or      a1, s0, r0
