.headersize G_EN_DOOR_DELTA

;==================================================================================================
; Check is player is close enough to interact
;==================================================================================================

; Replaces:
;   JAL     0x800B6E6C
;   ADDIU   A2, A2, 0x0024
.org 0x80866C68
    jal     Door_PlayerCloseEnoughToOpen
    nop

; Replaces:
;   LWC1    F0, 0x005C (SP)
.org 0x80866C7C
    nop

; Replaces:
;   LUI     AT, 0x41A0
;   MTC1    AT, F2
;   ABS.S   F0, F0
;   C.LT.S  F0, F2
;   LWC1    F0, 0x0058 (SP)
;   BC1FL   0x80866F40
;   LBU     T7, 0x01A4 (A3)
;   ABS.S   F0, F0
;   LUI     AT, 0x4248
;   C.LT.S  F0, F2
;   LWC1    F0, 0x0060 (SP)
;   BC1FL   0x80866F40
;   LBU     T7, 0x01A4 (A3)
;   MTC1    AT, F4
;   ABS.S   F0, F0
;   C.LT.S  F0, F4
;   NOP
;   BC1FL   0x80866F40
;   LBU     T7, 0x01A4 (A3)
.org 0x80866C88
.area 0x4C
    beqzl   v0, 0x80866F40
    lbu     t7, 0x01A4 (a3)
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
.endarea
