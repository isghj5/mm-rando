;==================================================================================================
; Elegy Speedup - Elegy Statue
;==================================================================================================

.headersize G_EN_TORCH2_DELTA

; Get the speed at which the Elegy statue fades in or out.
; Replaces:
;   sll     a1, v0, 16
;   sra     a1, a1, 16
;   addiu   a0, s0, 0x0192
;   jal     0x800FEF2C
;   addiu   a2, r0, 0x0008
.org 0x808A3400 ; Offset: 0x250
    jal     ElegySpeedup_GetStatueFadeSpeed_Hook
    or      a1, v0, r0
    addiu   a0, s0, 0x0192
    jal     0x800FEF2C
    or      a2, v0, r0

.headersize G_EFF_CHANGE_DELTA

; Sync position of Eff_Change effect with the position of the Elegy statue.
; Replaces:
;   sw      s0, 0x0020 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x0024 (sp)
;   sw      a1, 0x002C (sp)
.org 0x80A4C5D0 ; Offset: 0x140
    sw      ra, 0x0024 (sp)
    sw      s0, 0x0020 (sp)
    jal     ElegySpeedup_UpdateEffectPosition_Hook
    sw      a1, 0x002C (sp)

; Handle whether or not to darken the scene during Elegy animation.
; Replaces:
;   jal     0x800FD2B4
.org 0x80A4C734
    jal     ElegySpeedup_HandleDarken_Hook

; Handle whether or not to update the camera during Elegy animation.
; Replaces:
;   jal     0x800F1D84
.org 0x80A4C760
    jal     ElegySpeedup_HandleCamera_Hook

.headersize G_PLAYER_ACTOR_DELTA

; Get frames to wait before beginning to despawn current Elegy statue.
; Replaces:
;   lw      v1, 0x004C (sp)
;   lh      t8, 0x00BE (s0)
;   addiu   t9, r0, 0x0014
.org 0x80848684 ; Offset: 0x1ABF4
    jal     ElegySpeedup_GetStatueDespawnCounter_Hook
    lw      v1, 0x004C (sp)
    lh      t8, 0x00BE (s0)

; Get fields regarding how long Link is locked in place, and which frame to handle the Elegy statue.
; Replaces:
;   or      a2, a0, r0
;   or      a3, a1, r0
.org 0x80855A84 ; Offset: 0x27FF4
    jal     ElegySpeedup_GetLockParams_Hook
    or      a2, a0, r0

; Set how many frames until Link is "unlocked".
; Replaces:
;   slti    v1, v0, 0x005B
.org 0x80855A98
    slt     v1, v0, t0

; Set which frame should call the function to handle the Elegy statue.
; Replaces:
;   addiu   at, r0, 0x000A
.org 0x80855ACC
    addu    at, r0, t1

;==================================================================================================
; Elegy Speedup - Stone Tower Blocks
;==================================================================================================

.headersize G_BG_F40_BLOCK_DELTA

; Get speed of block moving to new area (normally moving slow).
; Replaces:
;   lui     at, 0x41A0
;   mtc1    at, f4
;   lw      t6, 0x015C (s0)
;   lw      v0, 0x0160 (s0)
;   swc1    f4, 0x0070 (s0)
.org 0x80BC4248 ; Offset: 0x8C8
    lw      a1, 0x0024 (sp)
    jal     ElegySpeedup_GetBlockSpeed_Hook
    ori     a2, r0, 0
    lw      t6, 0x015C (s0)
    lw      v0, 0x0160 (s0)

; Get speed of block returning to old area (normally moving fast).
; Replaces:
;   lui     at, 0x4220
;   lw      v0, 0x0160 (s0)
;   mtc1    at, f4
;   lui     t7, 0x80BC
;   blez    v0, 0x80BC448C
;   swc1    f4, 0x0070 (s0)
.org 0x80BC4468 ; Offset: 0xAE8
.area 0x18
    lw      a1, 0x0024 (sp)
    jal     ElegySpeedup_GetBlockSpeed_Hook
    ori     a2, r0, 1
    lw      v0, 0x0160 (s0)
    blez    v0, 0x80BC448C
    lui     t7, 0x80BC ; Relocated hiword for: 0x80BC4380 (offset 0xA00)
.endarea

; Fix relocations.
; Replaces:
;   .dw 0x45000AF4
.org 0x80BC4754 ; Offset: 0xDD4
    .dw 0x45000AFC
