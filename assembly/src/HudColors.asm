HudColors_GetMagicMeterColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      v0, 0x0018 (sp)
    sw      t3, 0x001C (sp)

    ; A bit gross, RA will be restored from stack after call
    lui     ra, 0x0400
    ori     ra, ra, 0x0400
    sw      ra, 0x0024 (sp)

    jal     HudColors_GetMagicMeterColor
    sw      t5, 0x0020 (sp)

    ; Also a bit gross, put return value in T6
    or      t6, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      v0, 0x0018 (sp)
    lw      t3, 0x001C (sp)
    lw      t5, 0x0020 (sp)
    addiu   sp, sp, 0x28

    lw      v1, 0x00DC (sp)
    lw      t1, 0x004C (sp)
    lh      t2, 0x00E2 (sp)

    jr      ra
    nop

HudColors_GetMagicMeterChargingColor_Hook:
    addiu   sp, sp, -0x38
    sw      ra, 0x0030 (sp)
    sw      a2, 0x0010 (sp)
    sw      a3, 0x0014 (sp)
    sw      v0, 0x0018 (sp)
    sw      v1, 0x001C (sp)
    sw      t1, 0x0020 (sp)
    sw      t2, 0x0024 (sp)
    sw      t3, 0x0028 (sp)

    ; RA will be restored from stack after call
    lui     ra, 0x0400
    ori     ra, ra, 0x0400
    sw      ra, 0x0034 (sp)

    jal     HudColors_GetMagicMeterColor
    sw      t5, 0x002C (sp)

    ; Place return value in T9
    or      t9, v0, r0

    lw      a2, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      v0, 0x0018 (sp)
    lw      v1, 0x001C (sp)
    lw      t1, 0x0020 (sp)
    lw      t2, 0x0024 (sp)
    lw      t3, 0x0028 (sp)
    lw      t5, 0x002C (sp)

    lw      ra, 0x0030 (sp)
    jr      ra
    addiu   sp, sp, 0x38

HudColors_GetMapColor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a1, 0x0014 (sp)
    sw      a2, 0x0018 (sp)

    jal     HudColors_GetMapColor
    sw      v1, 0x001C (sp)

    ; Put return value in T5
    or      t5, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      v1, 0x001C (sp)

    jr      ra
    addiu   sp, sp, 0x20

HudColors_GetMapPlayerCursorColor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      t0, 0x0018 (sp)

    jal     HudColors_GetMapPlayerCursorColor
    sw      t8, 0x001C (sp)

    ; Put return value in T7
    or      t7, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      t0, 0x0018 (sp)
    lw      t8, 0x001C (sp)

    jr      ra
    addiu   sp, sp, 0x20

HudColors_GetMapEntranceCursorColor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      t0, 0x0018 (sp)

    jal     HudColors_GetMapEntranceCursorColor
    sw      t8, 0x001C (sp)

    ; Put return value in T5
    or      t5, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      t0, 0x0018 (sp)
    lw      t8, 0x001C (sp)

    jr      ra
    addiu   sp, sp, 0x20

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_GetClockEmblemColor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)

    jal     HudColors_GetClockEmblemColor
    sw      v0, 0x0014 (sp)

    ; Put return value in T8
    or      t8, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)

    jr      ra
    addiu   sp, sp, 0x20

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_GetInvertedClockEmblemColorR_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      t2, 0x0018 (sp)

    jal     HudColors_GetClockEmblemInvertedColor
    ori     a0, r0, 0

    ; Put return value in T0
    or      t0, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      t2, 0x0018 (sp)

    ; Displaced code
    lui     t5, 0x801C

    jr      ra
    addiu   sp, sp, 0x20

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_GetInvertedClockEmblemColorG_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      t2, 0x0018 (sp)
    sw      t5, 0x001C (sp)

    jal     HudColors_GetClockEmblemInvertedColor
    ori     a0, r0, 1

    ; Put return value in A2
    or      a2, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      t2, 0x0018 (sp)
    lw      t5, 0x001C (sp)

    ; Displaced code
    lui     a3, 0x801C
    lui     t3, 0x801C

    jr      ra
    addiu   sp, sp, 0x28

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_GetInvertedClockEmblemColorB_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      a3, 0x0014 (sp)
    sw      t2, 0x0018 (sp)
    sw      t4, 0x001C (sp)
    sw      t5, 0x0020 (sp)

    jal     HudColors_GetClockEmblemInvertedColor
    ori     a0, r0, 2

    ; Put return value in A0
    or      a0, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a3, 0x0014 (sp)
    lw      t2, 0x0018 (sp)
    lw      t4, 0x001C (sp)
    lw      t5, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_FixInvertedClockEmblemColorCalc_Hook:
    ; Displaced code
    sll     t7, t9, 24
    or      t9, t7, t6

    ; Properly OR blue value
    lui     t6, 0x801C
    lh      t6, 0xFBD4 (t6)
    sll     t6, t6, 8

    jr      ra
    ; Put return value in T8
    or      t8, t9, t6

HudColors_GetClockEmblemSunColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)
    sw      a2, 0x0020 (sp)

    ; Hook stub passes alpha in A3
    jal     HudColors_GetClockEmblemSunColor
    or      a0, a3, r0

    ; Put return value in T6
    or      t6, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)
    lw      a2, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28

HudColors_GetClockSunColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)

    jal     HudColors_GetClockSunColor
    sw      a2, 0x0020 (sp)

    ; Put return value in T7
    or      t7, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)
    lw      a2, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28

HudColors_GetClockMoonColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)

    jal     HudColors_GetClockMoonColor
    sw      a2, 0x0020 (sp)

    ; Put return value in T6
    or      t6, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)
    lw      a2, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28

HudColors_GetAButtonColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      v0, 0x0014 (sp)
    sw      a2, 0x0018 (sp)
    sw      t2, 0x001C (sp)
    sw      t7, 0x0020 (sp)

    jal     HudColors_GetAButtonColor
    or      a0, t4, r0

    ; Put return value in T5
    or      t5, v0, r0

    lw      ra, 0x0010 (sp)
    lw      v0, 0x0014 (sp)
    lw      a2, 0x0018 (sp)
    lw      t2, 0x001C (sp)
    lw      t7, 0x0020 (sp)

    jr      ra
    addiu   sp, sp, 0x28

HudColors_GetBButtonColor_Hook:
    addiu   sp, sp, -0x38
    sw      ra, 0x0010 (sp)
    sw      at, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)
    sw      a2, 0x0020 (sp)
    sw      v0, 0x0024 (sp)
    sw      v1, 0x0028 (sp)
    sw      t4, 0x002C (sp)

    jal     HudColors_GetBButtonColor
    sw      t9, 0x0030 (sp)

    ; Move individual RGB values into t7, t6 and t8
    ; T6 and T8 will be properly masked by our stub in the caller
    srl     t7, v0, 24
    srl     t6, v0, 16
    srl     t8, v0, 8

    lw      ra, 0x0010 (sp)
    lw      at, 0x0014 (sp)
    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)
    lw      a2, 0x0020 (sp)
    lw      v0, 0x0024 (sp)
    lw      v1, 0x0028 (sp)
    lw      t4, 0x002C (sp)
    lw      t9, 0x0030 (sp)

    jr      ra
    addiu   sp, sp, 0x38

; Note: Final 4 bytes of stack frame reserved for caller stub.
HudColors_GetCStartButtonColor_Hook:
    ; If blue value is non-0, is Start button
    lh      t7, 0x0026 (sp)

    addiu   sp, sp, -0x40
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)
    sw      a2, 0x001C (sp)
    sw      a3, 0x0020 (sp)
    sw      v0, 0x0024 (sp)
    sw      v1, 0x0028 (sp)
    sw      t0, 0x002C (sp)
    sw      t2, 0x0030 (sp)
    sw      t3, 0x0034 (sp)
    sw      t4, 0x0038 (sp)

    bnez    t7, @@start_button
    nop

@@c_button:
    ; Use alpha in T8 as argument
    jal     HudColors_GetCButtonColor
    or      a0, t8, r0
    b       @@tail
    nop

@@start_button:
    jal     HudColors_GetStartButtonColor
    or      a0, t8, r0

@@tail:
    ; Put return value in T7
    or      t7, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)
    lw      a2, 0x001C (sp)
    lw      a3, 0x0020 (sp)
    lw      v0, 0x0024 (sp)
    lw      v1, 0x0028 (sp)
    lw      t0, 0x002C (sp)
    lw      t2, 0x0030 (sp)
    lw      t3, 0x0034 (sp)
    lw      t4, 0x0038 (sp)

    jr      ra
    addiu   sp, sp, 0x40

HudColors_GetCButtonTriangleColor_Hook:
    addiu   sp, sp, -0x28
    sw      ra, 0x0010 (sp)
    sw      a2, 0x0014 (sp)
    sw      a3, 0x0018 (sp)
    sw      v1, 0x001C (sp)
    sw      t1, 0x0020 (sp)
    sw      t4, 0x0024 (sp)

    ; Use alpha in T6 as argument
    jal     HudColors_GetCButtonColor
    or      a0, t6, r0

    ; Put return value in T9
    or      t9, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a2, 0x0014 (sp)
    lw      a3, 0x0018 (sp)
    lw      v1, 0x001C (sp)
    lw      t1, 0x0020 (sp)
    lw      t4, 0x0024 (sp)

    jr      ra
    addiu   sp, sp, 0x28
