;==================================================================================================
; After arrow draw
;==================================================================================================

.headersize G_EN_ARROW_DELTA

; Replaces:
;   ADDIU   A0, A3, 0x0048
;   ADDIU   A1, S0, 0x0234
;   JAL     Matrix_MultVec3f
.org 0x8088B8A4
    nop
    nop
    jal     Arrow_AfterDraw
