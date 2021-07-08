;==================================================================================================
; Arrow Magic Consumption
;==================================================================================================

.headersize G_CODE_DELTA

; Get initial magic consume state for elemental arrows.
; Replaces:
;   lh      v0, 0x3F28 (v1)
;   addiu   at, r0, 0x0007
;   addiu   t2, r0, 0x0001
.org 0x80115E44
    jal     ArrowMagic_GetInitialConsumeState_Hook
    or      a0, a3, r0
    lh      v0, 0x3F28 (v1)

; Get whether or not to set current magic cost to RDRAM.
; Replaces:
;   addiu   v0, r0, 0x0001
;   andi    t1, t0, 0x0008
;   beqzl   t1, 0x80115E88
.org 0x80115E70
    jal     ArrowMagic_ShouldSetMagicCost_Hook
    or      a0, a3, r0
    bnezl   at, 0x80115E88
