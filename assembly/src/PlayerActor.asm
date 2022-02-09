Player_BeforeUpdate_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     Player_BeforeUpdate
    nop

    ; Displaced code
    lw      t6, 0x0A74 (s0)
    addiu   at, r0, 0xFFEF

    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x18

Player_BeforeDamageProcess_Hook:
    ; Displaced code
    or      s0, a0, r0

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     Player_BeforeDamageProcess
    sw      a1, 0x0018 (sp)

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

@@caller_return:
    ; Will be returning from caller function, so restore S0
    addiu   sp, sp, 0x20
    lw      s0, 0x0028 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x002C (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x78

Player_BeforeHandleFrozenState_Hook:
    j       Player_BeforeHandleFrozenState
    or      s1, a1, r0 ;; Displaced code.

Player_BeforeHandleVoidingState_Hook:
    j       Player_BeforeHandleVoidingState
    or      s1, a1, r0 ;; Displaced code.

Player_ShouldIceVoidZora_Hook:
    addiu   at, r0, 0x0002      ;; AT = Zora form value.
    bnel    t9, at, @@not_zora  ;; If not Zora, return false early.
    or      v0, r0, r0          ;; V0 = 0 (false).

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    or      a0, s0, r0          ;; A0 = Link.
    jal     Player_ShouldIceVoidZora
    or      a1, s1, r0          ;; A1 = GlobalContext.

    lw      ra, 0x0014 (sp)
    addiu   sp, sp, 0x18
@@not_zora:
    jr      ra
    nop

Player_ShouldPreventRestoringSwimState_Hook:
    lw      a0, 0x0020 (sp) ;; A0 = GlobalContext.

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a1, 0x0010 (sp)

    jal     Player_ShouldPreventRestoringSwimState
    sw      v0, 0x0014 (sp)

    ; Move result to AT.
    or      at, v0, r0

    lw      a1, 0x0010 (sp)
    lw      v0, 0x0014 (sp)

    ; Displaced code.
    lw      t4, 0x0A6C (a1)
    sll     t5, t4, 4

    lw      ra, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20

DekuHop_GetSpeedModifier_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     DekuHop_GetSpeedModifier
    nop

    mov.s   f4, f0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Player_GetCollisionType_Hook:
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)

    jal     Player_GetCollisionType
    nop

    ; Displaced code.
    or      t0, r0, r0
    andi    t1, v0, 0x0008

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x14

Player_HandleFormSpeed_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0020 (sp)
    sw      a1, 0x0024 (sp)
    sw      a2, 0x0028 (sp)
    sw      a3, 0x002C (sp)

    jal     Player_HandleFormSpeed
    nop

    lw      a0, 0x0020 (sp)
    lw      a1, 0x0024 (sp)
    lw      a2, 0x0028 (sp)
    lw      a3, 0x002C (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_GetWallCollisionHeight_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    swc1    f2, 0x0010 (sp)

    jal     Player_GetWallCollisionHeight
    or      a0, s0, r0

    mfc1    a2, f0

    lwc1    f2, 0x0010 (sp)
    lw      s0, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_GetDiveDepth_Hook:
    addiu   sp, sp, -0x24
    sw      ra, 0x0020 (sp)
    swc1    f16, 0x001C (sp)
    swc1    f14, 0x0018 (sp)
    swc1    f2, 0x0014 (sp)
    sw      a0, 0x0024 (sp)

    jal     Player_GetDiveDepth
    nop

    lw      a0, 0x0024 (sp)
    lwc1    f2, 0x0014 (sp)
    lwc1    f14, 0x0018 (sp)
    lwc1    f16, 0x001C (sp)

    c.lt.s  f0, f14

    lw      ra, 0x0020 (sp)
    jr      ra
    addiu   sp, sp, 0x24

Player_GetLedgeClimbFactorFromSwim_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    swc1    f0, 0x0018 (sp)

    jal     Player_GetLedgeClimbFactorFromSwim
    swc1    f2, 0x0014 (sp)

    mov.s   f18, f0

    lwc1    f2, 0x0014 (sp)
    lwc1    f0, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_GetLedgeClimbFactor_Hook:
    addiu   sp, sp, -0x24
    sw      ra, 0x0020 (sp)
    sw      v0, 0x0018 (sp)
    swc1    f0, 0x0014 (sp)
    swc1    f2, 0x001C (sp)

    jal     Player_GetLedgeClimbFactor
    nop

    mfc1    at, f0

    lw      v0, 0x0018 (sp)

    ; Displaced code
    lwc1    f8, 0x0018 (v0)

    lwc1    f2, 0x001C (sp)
    lwc1    f0, 0x0014 (sp)
    lw      ra, 0x0020 (sp)
    jr      ra
    addiu   sp, sp, 0x24

Player_GetLedgeClimbFactor2_Hook:
    addiu   sp, sp, -0x24
    sw      ra, 0x0020 (sp)
    sw      v0, 0x0018 (sp)
    swc1    f0, 0x0014 (sp)
    swc1    f2, 0x001C (sp)

    jal     Player_GetLedgeClimbFactor2
    nop

    mov.s   f10, f0

    lwc1    f2, 0x001C (sp)
    lwc1    f0, 0x0014 (sp)
    lw      v0, 0x0018 (sp)
    lw      ra, 0x0020 (sp)
    jr      ra
    addiu   sp, sp, 0x24

Player_GetInvertedLedgeClimbFactor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    swc1    f2, 0x0018 (sp)
    swc1    f0, 0x0014 (sp)

    jal     Player_GetInvertedLedgeClimbFactor
    nop

    mov.s   f16, f0

    lwc1    f0, 0x0014 (sp)
    lwc1    f2, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_GetLedgeJumpSpeed_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    swc1    f4, 0x0018 (sp)
    swc1    f6, 0x0014 (sp)

    jal     Player_GetLedgeJumpSpeed
    nop

    mov.s   f8, f0

    lwc1    f6, 0x0014 (sp)
    lwc1    f4, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_GetMidAirJumpSlashHeight_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)
    sw      a2, 0x0020 (sp)

    jal     Player_GetMidAirJumpSlashHeight
    addiu   a0, sp, 0x0010

    lwc1    f4, 0x0010 (sp)

    lw      a2, 0x0020 (sp)
    lw      a1, 0x001C (sp)
    lw      a0, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_ModifyJumpHeight_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Player_GetJumpHeightModifier
    swc1    f0, 0x0010 (sp)

    lwc1    f4, 0x0010 (sp)
    mul.s   f0, f4, f0

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_ModifyJumpVelocity_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Player_ModifyJumpVelocity
    sw      v0, 0x0010 (sp)

    ; Displaced code:
    lw      t2, 0x0A70 (s0)

    lw      v0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_GetJumpSlashHeight_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Player_GetJumpSlashHeight
    sw      a0, 0x0018 (sp)

    lw      a0, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_GetRunDeceleration_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)
    sw      a1, 0x001C (sp)
    sw      a2, 0x0020 (sp)

    jal     Player_GetRunDeceleration
    sw      a3, 0x0024 (sp)

    lw      a3, 0x0024 (sp)
    lw      a2, 0x0020 (sp)
    lw      a1, 0x001C (sp)
    lw      a0, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_GetMidAirAcceleration_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    sw      a0, 0x0020 (sp)
    sw      a1, 0x0024 (sp)
    swc1    f10, 0x0018 (sp)
    swc1    f8, 0x0014 (sp)

    addiu   a0, sp, 0x0014

    jal     Player_GetMidAirAcceleration
    addiu   a1, sp, 0x0018

    ; Displaced code:
    addiu   t9, r0, 0x00C8
    lui     a3, 0x3F80

    lwc1    f8, 0x0014 (sp)
    lwc1    f10, 0x0018 (sp)
    lw      a1, 0x0024 (sp)
    lw      a0, 0x0020 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_GetLedgeGrabDistance_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    swc1    f0, 0x0010 (sp)

    jal     Player_GetLedgeGrabDistance
    nop

    mov.s   f12, f0

    lwc1    f0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_AfterJumpSlashGravity_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    sw      a0, 0x0018 (sp)

    jal     Player_AfterJumpSlashGravity
    sw      a1, 0x001C (sp)

    lw      a0, 0x0018 (sp)
    lw      a1, 0x001C (sp)
    jal     0x801360E0
    addiu   a1, a1, 0x0240

    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18

Player_GetSpinChargeWalkSpeedFactor_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x001C (sp)
    swc1    f0, 0x0018 (sp)

    jal     Player_GetSpinChargeWalkSpeedFactor
    sw      v1, 0x0014 (sp)

    mov.s   f14, f0

    lw      v1, 0x0014 (sp)
    lwc1    f0, 0x0018 (sp)
    lw      ra, 0x001C (sp)
    jr      ra
    addiu   sp, sp, 0x20

Player_UseItem_CheckCeiling_Hook:
    lw      t5, 0x0014 (sp)
    lw      t6, 0x0070 (sp)
    j       Player_UseItem_CheckCeiling
    sw      t6, 0x0000 (t5)

Player_ShouldCheckItemUsabilityWhileSwimming_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     Player_ShouldCheckItemUsabilityWhileSwimming
    sw      a2, 0x0020 (sp)

    ; Displaced code:
    lb      t1, 0x0145 (s0)
    slti    at, t1, 0x0005

    lw      a2, 0x0020 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
