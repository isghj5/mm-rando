.headersize G_CODE_DELTA

; Low Health SFX function
; Replaces:
;   JAL     0x8019F0C8
.org 0x801018E4
    jal     ExternalEffects_PlayLowHpSfx

; I-Frames glow
; Replaces:
;   JAL     0x8012BC50
.org 0x8012290C
    jal     ExternalEffects_IFrameGlow
