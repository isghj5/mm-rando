;==================================================================================================
; Fix HESS crash
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   LW      T7, 0x0260 (S0)
.org 0x801260FC
    lw      a0, 0x0260 (s0)

; Replaces:
;   LW      T7, 0x0260 (S0)
;   LH      V0, 0x0084 (T7)
;   ANDI    V0, V0, 0x0F00
;   BEQZ    V0, 0x80126168
;   SRA     V0, V0, 8
.org 0x80126130
    lw      a0, 0x0260 (s0)
    jal     BugFixes_PreventHessCrash_Hook
    sw      a2, 0x0030 (sp)
    lw      a2, 0x0030 (sp)
    beqz    v0, 0x80126168

;==================================================================================================
; Fix Action Swap crash
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   ADDIU   S1, R0, 0x0080
;   LUI     T6, 0x801E
.org 0x801A5FC0
    jal     BugFixes_PreventAudioCrash_Hook
    or      a0, t2, r0

;==================================================================================================
; Fix Hookslide
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   JAL     0x800CB7CC
.org 0x800CBD98
    jal     BugFixes_PreventHookslideCrash
