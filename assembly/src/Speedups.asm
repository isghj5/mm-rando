BoatCruise_BeforeCruiseEnd_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     BoatCruise_BeforeCruiseEnd
    sw      a1, 0x0014 (sp)

    ; Displaced code
    lui     v0, 0x801F
    addiu   v0, v0, 0xF670

    lw      a0, 0x0010 (sp)
    lw      a1, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

BoatCruise_GetBoatSpeedArchery_Hook:
    j       BoatCruise_GetBoatSpeed_Hook
    ori     a1, r0, 0x0001

BoatCruise_GetBoatSpeedCruise_Hook:
    j       BoatCruise_GetBoatSpeed_Hook
    or      a1, r0, r0

BoatCruise_GetBoatSpeed_Hook:
    ; Displaced code
    sw      t0, 0x0180 (a0)

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a0, 0x0010 (sp)

    jal     BoatCruise_GetBoatSpeed
    sw      t5, 0x0014 (sp)

    lw      a0, 0x0010 (sp)
    lw      t5, 0x0014 (sp)
    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

BoatCruise_ShouldEndArchery_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     BoatCruise_ShouldEndArchery
    or      a0, s0, r0

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Fisherman_BoatGetAccelSpeed_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Fisherman_BoatGetAccelSpeed
    or      a0, s0, r0

    ; Move result into A2.
    mfc1    a2, f0

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Fisherman_BoatGetTopSpeed_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Fisherman_BoatGetTopSpeed
    or      a0, s0, r0

    ; Move result into AT.
    mfc1    at, f0

    ; Displaced code
    lh      t9, 0x001C (s0)

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Fisherman_ShouldEndGame_Hook:
    j       Fisherman_ShouldEndGame
    or      a0, s0, r0

Fisherman_ShouldPassTimerCheck_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0010 (sp)

    jal     Fisherman_ShouldPassTimerCheck
    or      a0, s0, r0

    ; Displaced code
    addiu   a0, s0, 0x0190

    lw      v1, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Sakon_ShouldEndThiefEscape_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Sakon_ShouldEndThiefEscape
    or      a1, s1, r0

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Toto_BeforeAdvanceFormalReplay_Hook:
    sw      a1, 0x0024 (sp)

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Toto_BeforeAdvanceFormalReplay
    sw      a0, 0x0010 (sp)

    lw      a0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Toto_HandleAdvanceFormalReplay_Hook:
    ; Load params from stack for calling hook function.
    lw      a0, 0x0020 (sp)
    jal     Toto_HandleAdvanceFormalReplay
    lw      a1, 0x0024 (sp)

    ; Return from caller function early.
    lw      ra, 0x001C (sp)
    lw      s0, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
