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
