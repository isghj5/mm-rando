;==================================================================================================
; Override floor physics type hook
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   jal     0x800C99D4
.org 0x80843B10 ; In RDRAM: 0x80760FA0
    jal     FloorPhysics_GetOverrideType
