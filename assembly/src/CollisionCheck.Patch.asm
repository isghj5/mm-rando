;==================================================================================================
; Damage and Effect
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   JAL     z2_CollisionCheck_GetDamageAndEffectOnBumper
.org 0x800E3380
    JAL     CollisionCheck_GetDamageAndEffectOnBumper

; Replaces:
;   JAL     z2_CollisionCheck_GetDamageAndEffectOnBumper
.org 0x800E764C
    JAL     CollisionCheck_GetDamageAndEffectOnBumper
