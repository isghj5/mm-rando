;==================================================================================================
; Spawn fairy
;==================================================================================================

.headersize G_EN_BUTTE_DELTA

; Replaces:
;   ADDIU   A0, A1, 0x1CA0
.org 0x8091D024
    or      a0, s0, r0

; Replaces:
;   jal     0x800BAC60 ; Actor_Spawn
.org 0x8091D03C
    jal     Butterfly_FairySpawn
