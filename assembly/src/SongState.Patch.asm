;==================================================================================================
; Song State - Skip Song Playback
;==================================================================================================

.headersize G_CODE_DELTA

; Tweak advancement of state machine for deciding if song playback & message box should be skipped.
; Replaces:
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t2, r0, 0x0012
;   sb      t2, 0x1F22 (at)
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t4, r0, 0x0003
;   sb      t4, 0x1F0A (at)
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t6, r0, 0x0002
;   sb      t6, 0x2023 (at)
.org 0x80154CC8
.area 0x30
    nop
    or      a0, s2, r0
    jal     SongState_HandlePlayback
    or      a1, s0, r0
    lui     at, 0x0001
    addu    at, at, s0
    srl     t2, v0, 24
    sb      t2, 0x1F22 (at)
    srl     t4, v0, 16
    sb      t4, 0x1F0A (at)
    srl     t6, v0, 8
    sb      t6, 0x2023 (at)
.endarea

; Prevent check of u16 field at 0x202C, unsure what exactly this does.
; Replaces:
;   lhu     v1, 0x202C (s1)
;   addiu   at, r0, 0x0032
;   bnel    v1, at, 0x8015553C
;   slti    at, v1, 0x0022
.org 0x8015543C
    nop
    nop
    nop
    nop

;==================================================================================================
; Song State - Skip Preset Song Playback
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t4, r0, 0x0012
;   sb      t4, 0x1F22 (at)
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t6, r0, 0x0003
;   sb      t6, 0x1F0A (at)
;   lui     at, 0x0001
;   addu    at, at, s0
;   addiu   t5, r0, 0x0001
;   b       0x80154FD4
;   sb      t5, 0x2023 (at)
.org 0x80154EF0
.area 0x34
    nop
    or      a0, s2, r0
    jal     SongState_HandlePresetPlayback
    or      a1, s0, r0
    lui     at, 0x0001
    addu    at, at, s0
    srl     t4, v0, 24
    sb      t4, 0x1F22 (at)
    srl     t6, v0, 16
    sb      t6, 0x1F0A (at)
    srl     t5, v0, 8
    b       0x80154FD4
    sb      t5, 0x2023 (at)
.endarea
