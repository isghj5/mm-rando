.headersize G_EN_ARROW_DELTA

;==================================================================================================
; After arrow draw
;==================================================================================================

; Replaces:
;   ADDIU   A0, A3, 0x0048
;   ADDIU   A1, S0, 0x0234
;   JAL     Matrix_MultVec3f
.org 0x8088B8A4
    nop
    nop
    jal     Arrow_AfterDraw

;==================================================================================================
; After arrow hits something
;==================================================================================================

; Replaces:
;   OR      A1, R0, R0
;   ADDIU   A2, R0, 0x0096
;   JAL     0x800B26A8 ; EffectSsHitmark_SpawnCustomScale
;   SW      V0, 0x0050 (SP)
;   LW      V0, 0x0050 (SP)
.org 0x8088AFD0
    or      a1, s0, r0
    or      a2, v0, r0
    jal     Arrow_OnHit
    sw      v0, 0x0050 (sp)
    nop
