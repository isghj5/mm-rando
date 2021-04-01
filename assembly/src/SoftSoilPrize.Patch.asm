;==================================================================================================
; Soft Soil Prize replace triple item spawn
;==================================================================================================

.headersize G_OBJ_SWPRIZE_DELTA

; Replaces:
;   or      a1, s3, r0
;   jal     0x800A7730
.org 0x80C25484
    or      a1, s4, r0
    jal     SoftSoilPrize_ItemSpawn

;==================================================================================================
; Soft Soil Prize replace single item spawn
;==================================================================================================

.headersize G_OBJ_SWPRIZE_DELTA

; Replaces:
;   addiu   a1, s4, 0x0024
;   jal     0x800A7730
.org 0x80C25520
    or      a1, s4, r0
    jal     SoftSoilPrize_ItemSpawn
