;==================================================================================================
; R/Z input during bank deposit/withdraw
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lui     t0, 0x0001
;   addu    t6, a0, t0
;   lh      t7, 0x69AE (t6)
;   addiu   a3, a0, 0x4908
;   addu    s0, a3, t0
.org 0x80148D70
    jal     BankAmount_BeforeHandleInput_Hook
    nop
    bnez    v0, 0x80148FA8
    lui     t0, 0x0001
    lh      t7, 0x20A6 (s0)
