;======================================================================
; Kafei - Keaton Mask Replacement
;======================================================================

.headersize G_EN_TEST3_DELTA

; Check if Keaton Mask is being worn when actor initiates
; Replaces:
;   addiu   at, r0, 0x004F
;   sb      t4, 0x0155 (s0)
.org 0x80A3EF28
    jal     NpcKafei_IsMaskActive_Hook
    sb      t4, 0x0155(s0)


; a1 - actor, a0 - ctxt, a2 - pointer to mask DLs
; Replaces:
;   lw      t2, 0x00a0 (sp)
;   lui     t4, 0xde00
;   lw      a0, 0x0000 (t2)
;   lw      v1, 0x02b0 (a0)
;   lui     t5, 0x0a00
;   addiu   t5, t5, 0x04a0
;   addiu   t3, v1, 0x0008
;   sw      t3, 0x02b0 (a0)
;   sw      t5, 0x0004 (v1)
;   sw      t4, 0x0000 (v1)
.org 0x80A41178
    lui     a2, 0x801C
    lw      a0, 0x00a0 (sp)
    jal     NpcKafei_CheckHead
    ori     a2, a2, 0x0B20
    lw      t0, 0x0038 (sp)
    lw      a3, 0x00b4 (sp)
    nop
    nop
    nop
    nop

; a0 - ctxt, a1, actor
; Replaces:
;   jal     0x80128640
;   or      a1, a3, r0
.org 0x80A40F9C
    jal     NpcKafei_CheckHand
    or      a1, a3, r0

;======================================================================
; Kafei - remove inventory pendant check
;======================================================================

; Pendant check GetItem can stop drawing if the pendant is found in its quest slot.
; Just change what item ID it wants to something impossible.
; offset 0x2A40
; Replaces:
;    addiu  at, r0, 0x0030
.org 0x80A41220
    addiu   at, r0, 0x0002
