;==================================================================================================
; After crow has spawned a rupee
;==================================================================================================

.headersize G_EN_RUPPECROW_DELTA

; Replaces:
;   jr	    ra
.org 0x80BE2B78
    j       Rupeecrow_AfterRupeeSpawn_Hook
