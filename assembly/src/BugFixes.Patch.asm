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

;==================================================================================================
; Fix Empty Weapon Sound
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Replaces:
;   LH      T5, 0x0B28 (S0)
.org 0x80848DA8
    or      t5, r0, r0

; Replaces:
;   JAL     0x800B8E58
;   LHU     A1, 0xD5FA (A1)
.org 0x80848DBC
    jal     BugFixes_PlayEmptyWeaponSound
    addiu   a1, a1, 0xD5FA
