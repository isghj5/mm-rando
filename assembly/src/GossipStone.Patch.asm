;==================================================================================================
; Spawn fairy
;==================================================================================================

.headersize G_EN_GS_DELTA

; Replaces:
;   ADDIU   A0, S1, 0x1CA0
.org 0x80998118
    or      a0, s0, r0

; Replaces:
;   jal     0x800BAC60 ; Actor_Spawn
.org 0x8099813C
    jal     GossipStone_FairySpawn
