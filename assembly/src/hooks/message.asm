;==================================================================================================
; Before a character in a raw message is processed
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Replaces:
;   LW T2 0x0070 (SP)
;   LH T6 0x00DA (SP)
;   ADDIU AT R0 0x0010
;   LHU T3 0x1FEC (T2)
;   ADDU T7 S6 T6
;   ADDU T8 T7 S7
;   ADDU T4 S3 T3
;   ADDU T5 T4 S7
;   LBU S2 0x1880 (T5)
;   SB S2 0x1F24 (T8)
.org 0x8015B27C
    or      a0, s4, r0
    jal     before_message_character_process
    or      a1, sp, r0
    addiu   at, r0, 0x00FF
    beq     v0, at, 0x8015E6E4
    addiu   at, r0, 0x0010
    or      s2, v0, r0
    nop
    nop
    nop
