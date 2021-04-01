;==================================================================================================
; Spawn rupee
;==================================================================================================

.headersize G_EN_KUSA2_DELTA

; Replaces:
;   lw      a2, 0xEB24 (a2)
.org 0x80A5BD60
    or      a2, v1, r0

; Replaces:
;   jal     0x800A7730
.org 0x80A5BD68
    jal     KeatonGrassCluster_RupeeSpawn

; Fix relocations.
; Replaces:
;   .dw 0x46000C00
.org 0x80A5ED48
    .dw 0x00000000