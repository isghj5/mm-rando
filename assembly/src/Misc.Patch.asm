;==================================================================================================
; Crit Wiggle
;==================================================================================================

.headersize G_CODE_DELTA

; Enables crit wiggle movement.
; Replaces:
;   lh      v1, 0xF6A6 (v1)
;   slti    at, v1, 0x0011
;   beqzl   at, 0x800D06F0
;   slti    at, v1, 0x0011
.org 0x800D062C
    jal     Misc_CritWiggleCheck_Hook
    lh      v1, 0xF6A6 (v1)
    beqzl   at, 0x800D06F0
    nop

; Prevent from branching into delay slot, and prevent bad V1 deref after branch.
; Replaces:
;   b       0x800D06EC
;   lh      v1, 0xF6A6 (v1)
.org 0x800D0668
    b       0x800D06E8
    nop

; Enables crit wiggle camera.
; Replaces:
;   lh      v1, 0xF6A6 (v1)
;   slti    at, v1, 0x0011
.org 0x800D06E8
    jal     Misc_CritWiggleCheck_Hook
    lh      v1, 0xF6A6 (v1)

; Replaces:
;   lh      t9, 0xF6A6 (t9)
;   or      a0, r0, r0
;   slti    at, t9, 0x0011
.org 0x800D07D0
    jal     Misc_CritWiggleCheck_Hook
    lh      v1, 0xF6A6 (t9)
    or      a0, r0, r0

;==================================================================================================
; Underwater Ocarina
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   andi    t7, a2, 0x00FF
;   slti    at, t7, 0x0012
.org 0x80110914
    jal     Misc_UnderwaterOcarinaCheck_Hook
    andi    t7, a2, 0x00FF
