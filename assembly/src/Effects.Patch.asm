;==================================================================================================
; Replaces the ShieldParticle effect destructor function pointer with our replacement to fix
;==================================================================================================
.headersize G_CODE_DELTA

; Replaces:
;   .word      EffectShieldParticle_Destroy 
.org 0x801AE374
    .word      ShieldParticle_Destroy2

