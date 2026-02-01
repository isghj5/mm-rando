.include "BuildPatch.asm"

.headersize G_EN_ZORAEGG_DELTA

; Add a flat value to the EnZoraegg_GetNumDeliveredEggs return value
; Replaces:
;   nop
.org 0x80b319cc
ZORA_EGG_BASELINE_ADD:
    addiu   v0, v0, 0x0000 ; Value is edited by Builder.cs

; Subtract the baseline value when incrementing the egg counter.
; Replaces:
;   addiu   a1, v0, 0x0001
.org 0x80b32214
ZORA_EGG_BASELINE_SUBTRACT:
    addiu   a1, v0, 0x0000 ; Value is edited by Builder.cs

.close
