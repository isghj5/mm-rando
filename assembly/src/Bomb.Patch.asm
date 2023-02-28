;==================================================================================================
; After with parent updates
;==================================================================================================

.headersize G_EN_BOM_DELTA

; Replaces:
;   LW      A1, 0x0120 (S0)
;   ADDIU   A0, S0, 0x0014
;   JAL     Math_Vec3f_ToVec3s
;   ADDIU   A1, A1, 0x0024
.org 0x8087156C
    jal     Bomb_UpdateWithParent
    or      a0, s0, r0
    nop
    nop
