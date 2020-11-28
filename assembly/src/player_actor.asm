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
