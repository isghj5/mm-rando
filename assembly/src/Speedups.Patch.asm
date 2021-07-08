;==================================================================================================
; Check Speedups - Sound Check (Actor Cutscene Index 0xD)
;==================================================================================================

.headersize G_EN_TOTO_DELTA

; Call hook before beginning of function which may advance Toto cutscene state.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   sw      a1, 0x0024 (sp)
.org 0x80BA4A08 ; Offset: 0x1348
    jal     Toto_BeforeAdvanceFormalReplay_Hook
    sw      s0, 0x0018 (sp)

; Update behavior when advancing Toto cutscene state (1).
; Replaces:
;   b       0x80BA4B10
;   addiu   v0, r0, 0x0001
.org 0x80BA4AE0 ; Offset: 0x1420
    jal     Toto_HandleAdvanceFormalReplay_Hook
    nop

; Update behavior when advancing Toto cutscene state (2).
; Replaces:
;   b       0x80BA4B10
;   addiu   v0, r0, 0x0001
.org 0x80BA4AF4 ; Offset: 0x1434
    jal     Toto_HandleAdvanceFormalReplay_Hook
    nop

; Update behavior when advancing Toto cutscene state (3).
; Replaces:
;   b       0x80BA4B10
;   addiu   v0, r0, 0x0001
.org 0x80BA4B04 ; Offset: 0x1444
    jal     Toto_HandleAdvanceFormalReplay_Hook
    nop

;==================================================================================================
; Check Speedups - Saving Bomb Shop Lady
;==================================================================================================

.headersize G_EN_SUTTARI_DELTA

; Prevent branching into delay slot due to following patch.
; Replaces:
;   b       0x80BAD554
.org 0x80BAD4C4 ; Offset: 0x2DF4
    b       0x80BAD550

; Call hook function to check if escape sequence should be ended.
; Replaces:
;   lw      t3, 0x01F8 (s0)
;   addiu   at, r0, 0xFF9D
;   addiu   a0, s0, 0x0070
;   bne     t3, at, 0x80BAD5B8
;   lui     a1, 0x4080
.org 0x80BAD550 ; Offset: 0x2E80
    jal     Sakon_ShouldEndThiefEscape_Hook
    or      a0, s0, r0
    addiu   a0, s0, 0x0070
    beqz    v0, 0x80BAD5B8
    lui     a1, 0x4080

; Prevent setting Sakon velocity to 0 when escape sequence ends.
; Replaces:
;   swc1    f16, 0x0070 (s0)
.org 0x80BAD594 ; Offset: 0x2EC0
    nop

; Remove branch to allow Sakon to continue running after escape sequence ends, and prepare for
; function call to update velocity.
; Replaces:
;   b       0x80BAD5E8
;   lw      ra, 0x0034 (sp)
.org 0x80BAD5B0 ; Offset: 0x2EE0
    addiu   a0, s0, 0x0070
    lui     a1, 0x4080

;==================================================================================================
; Check Speedups - Fisherman's Game
;==================================================================================================

.headersize G_EN_JGAME_TSN_DELTA

; Ignore timer check if speedup is enabled.
; Replaces:
;   lw      t1, 0x3E04 (v1)
;   lw      t0, 0x3E00 (v1)
;   bnez    t0, 0x80C13C1C
;   addiu   a0, s0, 0x0190
;   beqz    t1, 0x80C13C48
;   lui     a1, 0x80C1
.org 0x80C13C08 ; Offset: 0x2D8
.area 0x18
    lw      a3, 0x3E04 (v1)
    lw      a2, 0x3E00 (v1)
    jal     Fisherman_ShouldPassTimerCheck_Hook
    lw      a1, 0x0034 (sp)
    bnez    v0, 0x80C13C48
    lui     a1, 0x80C1 ; Preparing to load: 0x80C15030
.endarea

; Check if Fisherman's Game should end. Used to end the game early if the player has enough points.
; Replaces:
;   lui     t2, 0x801F
;   lw      t2, 0x3470 (t2)
;   lui     t3, 0x801F
;   lw      t3, 0x3474 (t3)
;   bnezl   t2, 0x80C144D4
;   lw      ra, 0x001C (sp)
;   bnez    t3, 0x80C144D0
;   lw      a0, 0x002C (sp)
.org 0x80C14464 ; Offset: 0xB34
.area 0x20
    lui     a2, 0x801F
    lw      a2, 0x3470 (a2)
    lui     a3, 0x801F
    lw      a3, 0x3474 (a3)
    jal     Fisherman_ShouldEndGame_Hook
    lw      a1, 0x002C (sp)
    beqz    v0, 0x80C144D0
    lw      a0, 0x002C (sp)
.endarea

;==================================================================================================
; Speedups - Fisherman Boat
;==================================================================================================

.headersize G_OBJ_BOAT_DELTA

; Store A1 (context pointer) on stack for later usage.
; Replaces:
;   jal     0x800CAF70
;   sw      t6, 0x0044 (sp)
;   mtc1    r0, f4
;   or      v1, v0, r0
;   or      a0, s0, r0
;   swc1    f4, 0x003C (sp)
.org 0x80B9B1D0 ; Offset: 0x280
.area 0x18
    sw      a1, 0x0010 (sp)
    jal     0x800CAF70
    sw      t6, 0x0044 (sp)
    or      v1, v0, r0
    or      a0, s0, r0
    sw      r0, 0x003C (sp)
.endarea

; Fix branch to top speed function instead of delay slot.
; Replaces:
;   bc1fl   0x80B9B33C
.org 0x80B9B2D4 ; Offset: 0x384
    bc1fl   0x80B9B338

; Get max speed of boat.
; Replaces:
;   lh      t9, 0x001C (s0)
;   lui     at, 0x4040
.org 0x80B9B338 ; Offset: 0x3E8
    jal     Fisherman_BoatGetTopSpeed_Hook
    lw      a1, 0x0010 (sp)

; Get acceleration speed of boat.
; Replaces:
;   lh      t5, 0x00BE (s0)
;   lui     a2, 0x3D4C
;   ori     a2, a2, 0xCCCD
.org 0x80B9B3C4 ; Offset: 0x474
    jal     Fisherman_BoatGetAccelSpeed_Hook
    lw      a1, 0x0010 (sp)
    lh      t5, 0x00BE (s0)

;==================================================================================================
; Check Speedups - Boat Archery
;==================================================================================================

.headersize G_BG_INGATE_DELTA

; Get whether or not archery should end.
; Replaces:
;   andi    t8, t7, 0x0002
;   beqz    t8, 0x809540F0
;   nop
;   lw      t9, 0x0164 (s0)
.org 0x8095403C ; Offset: 0x5AC
    jal     BoatCruise_ShouldEndArchery_Hook
    lw      a1, 0x002C (sp)
    beqz    v0, 0x809540F0
    lw      t9, 0x0164 (s0)

; Hook start of function for idling (does nothing).
; Replaces:
;   sw      a0, 0x0000 (sp)
;   sw      a1, 0x0004 (sp)
;   jr      ra
;   nop
.org 0x80953F8C ; Offset: 0x4FC
    j       BoatCruise_HandleIdle
    nop
    nop
    nop

; Hook start of function for handling cruise end.
; Replaces:
;   lui     v0, 0x801F
;   addiu   v0, v0, 0xF670
;   sw      ra, 0x0014 (sp)
.org 0x809542A4 ; Offset: 0x814
    sw      ra, 0x0014 (sp)
    jal     BoatCruise_BeforeCruiseEnd_Hook
    nop

;==================================================================================================
; Speedups - Cruise Boat
;==================================================================================================

.headersize G_BG_INGATE_DELTA

; Get boat speed during cruise.
; Replaces:
;   bnezl   t7, 0x80953B70
;   addiu   t0, r0, 0x07D0
;   addiu   t8, r0, 0x0FA0
;   addiu   t9, r0, 0x0004
;   sw      t8, 0x0180 (a0)
;   b       0x80953B7C
;   sh      t9, 0x0168 (a0)
.org 0x80953B50 ; Offset: 0xC0
.area 0x1C
    bnezl   t7, 0x80953B6C
    addiu   t0, r0, 0x07D0
    sw      ra, 0x0000 (sp)
    jal     BoatCruise_GetBoatSpeedCruise_Hook
    addiu   t0, r0, 0x0FA0
    b       0x80953B7C
    sh      v0, 0x0168 (a0)
.endarea

; Get boat speed during archery.
; Replaces:
;   addiu   t0, r0, 0x07D0 (unused)
;   addiu   t1, r0, 0x0001
;   sw      t0, 0x0180 (a0)
;   sh      t1, 0x0168 (a0)
.org 0x80953B6C ; Offset: 0xDC
    sw      ra, 0x0000 (sp)
    jal     BoatCruise_GetBoatSpeedArchery_Hook
    sw      t0, 0x0180 (a0)
    sh      v0, 0x0168 (a0)

; Restore RA before returning.
; Replaces:
;   sh      t9, 0x0160 (a0)
;   jr      ra
;   nop
.org 0x80953BE0 ; Offset: 0x150
    lw      ra, 0x0000 (sp)
    jr      ra
    sh      t9, 0x0160 (a0)
