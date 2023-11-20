;==================================================================================================
; Minifrog - Randomize
;==================================================================================================

.headersize G_EN_MINIFROG_DELTA

; Replaces:
;   LUI     V0, 0x808A
;   LUI     T2, 0x801F
;   SLL     T0, T9, 1
;   ADDU    V0, V0, T0
;   LHU     T3, 0x4D7C (V0)
;   ADDIU   T2, T2, 0xF670
;   SRA     T1, T3, 8
;   ADDU    V1, T1, T2
;   LBU     A0, 0x0EF8 (V1)
;   OR      T4, T3, A0
;   B       0x808A3F08
;   SB      T4, 0x0EF8 (V1)
.org 0x808A3E74
    or      a0, s0, r0
    lw      a1, 0x0024 (sp)
    jal     Minifrog_GiveReward
    or      a2, t9, r0
    b       0x808A3F08
    nop
    lui     at, hi(Minifrog_WaitForDespawn)
    addiu   at, at, lo(Minifrog_WaitForDespawn)
    sw      at, 0x02A8 (s0)
    sw      r0, 0x013C (s0)
    nop
    nop

; Fix relocations.
; Replaces:
;   .dw 0x45000804
;   .dw 0x46000814
.org 0x808A4EB8
    .dw 0x00000000
    .dw 0x00000000

; Replaces:
;   JAL     0x800B670C
;   OR      A0, S0, R0
.org 0x808A3EF8
    nop
    nop

; Replaces:
;   BEQZ    AT, 0x808A3EA4
.org 0x808A3E14
    beqz    at, 0x808A3E8C

; Replaces:
;   .dw 0x808A3EA4
.org 0x808A4DC4
    .dw 0x808A3E8C

;==================================================================================================
; Minifrog - Don't despawn unless have given item.
;==================================================================================================
; RAM + 4950C0
; Replaces:
;   LH      V0, 0x02B0 (S1)
;   LUI     V1, hi(isFrogReturnedFlags) ; 0x808A
;   BEQZ    V0, .+0x38
;   SLL     T9, V0, 1
;   ADDU    V1, V1, T9
;   LHU     V1, lo(isFrogReturnedFlags) (V1) ; 0x4D7C
;   LUI     T2, 0x801F
;   LUI     T5, hi(EnMinifrog_SpawnGrowAndShrink) ; 0x808A
;   SRA     T1, V1, 8
;   ADDU    T2, T2, T1
;   LBU     T2, 0x568 (T2)
;   ANDI    T0, V1, 0x00FF
;   ADDIU   T4, R0, 0x001E
;   AND     T3, T0, T2
;   BEQZ    T3, .+0x18
;   ADDIU   T5, T5, lo(EnMinifrog_SpawnGrowAndShrink) ; 0x3F88
;   JAL     z2_ActorUnload
;   OR      A0, S1, R0
;   B       0x808A38D0
;   LW      RA, 0x0034 (SP)
.org 0x808A37A4 ; 8040E6E4
.area 0x50, 0
    or      a0, s1, r0
    jal     Minifrog_HasGivenReward
    lw      a1, 0x003C (sp)
    beqzl   v0, .+0x18
    lui     t5, 0x808A ; hi(EnMinifrog_SpawnGrowAndShrink)
    jal     z2_ActorUnload
    or      a0, s1, r0
    b       0x808A38D0
    lw      ra, 0x0034 (sp)
    addiu   t5, t5, 0x3F88 ; lo(EnMinifrog_SpawnGrowAndShrink)
    addiu   t4, r0, 0x001E
.endarea

; Fix relocations.
; Replaces:
;   .dw 0x45000138
;   .dw 0x46000148
;   .dw 0x45000150
;   .dw 0x46000170
.org 0x808A4E54
    .dw 0x00000000
    .dw 0x00000000
    .dw 0x45000144
    .dw 0x46000158
