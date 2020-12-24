before_player_actor_update_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     before_player_actor_update
    nop

    ; Displaced code
    lw      t6, 0x0A74 (s0)
    addiu   at, r0, 0xFFEF

    lw      ra, 0x0010 (sp)

    jr      ra
    addiu   sp, sp, 0x18

player_before_handle_frozen_state_hook:
    j       player_before_handle_frozen_state
    or      s1, a1, r0 ;; Displaced code.

player_before_handle_void_state_hook:
    j       player_before_handle_void_state
    or      s1, a1, r0 ;; Displaced code.

player_should_ice_void_zora_hook:
    addiu   at, r0, 0x0002      ;; AT = Zora form value.
    bnel    t9, at, @@not_zora  ;; If not Zora, return false early.
    or      v0, r0, r0          ;; V0 = 0 (false).

    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    or      a0, s0, r0          ;; A0 = Link.
    jal     player_should_ice_void_zora
    or      a1, s1, r0          ;; A1 = GlobalContext.

    lw      ra, 0x0014 (sp)
    addiu   sp, sp, 0x18
@@not_zora:
    jr      ra
    nop

player_should_prevent_restoring_swim_state_hook:
    lw      a0, 0x0020 (sp) ;; A0 = GlobalContext.

    addiu   sp, sp, -0x20
    sw      ra, 0x0018 (sp)
    sw      a1, 0x0010 (sp)

    jal     player_should_prevent_restoring_swim_state
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
