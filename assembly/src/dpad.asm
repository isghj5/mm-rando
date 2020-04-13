dpad_draw_hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0010 (sp)

    jal     dpad_draw
    nop

    lw      ra, 0x0010 (sp)
    addiu   sp, sp, 0x18

    jr      ra
    ; Displaced code
    lw      t6, 0x0068 (sp)

dpad_handle_hook:
    ; Displaced code
    or      s0, a0, r0

    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)

    jal     dpad_handle
    sw      a1, 0x0018 (sp)

    bnez    v0, @@caller_return
    nop

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20

; Used to return early from the hooked function 0x8074D29C if we called z64_UseItem.
; This is because z64_UseItem sets a byte (0x803FFDB0 +0x14A) which is the item Link currently has out.
; Because our hook is at the start of the function, this value may be set earlier than expected.
;
; Specifically, the branch at 0x8074D408 won't be taken, and can lead to a crash in specific conditions.
; This has been noted to happen when not having an item on the B button while wearing a mask with a B button
; effect (Blast, Bremen, Kamaro), and calling z64_UseItem.
;
; To fix this, we return non-zero if z64_UseItem was called, and if so return from the caller function
; immediately.
@@caller_return:
    ; Will be returning from caller function, so restore S0
    addiu   sp, sp, 0x20
    lw      s0, 0x0030 (sp)

    ; Restore RA from caller's caller function
    lw      ra, 0x0034 (sp)

    ; Fix stack for caller and return
    jr      ra
    addiu   sp, sp, 0x50

dpad_skip_transformation_check_hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)
    sw      a0, 0x0014 (sp)
    sw      a1, 0x0018 (sp)

    or      a0, s0, r0
    lw      a1, 0x0074 (sp)
    jal     dpad_skip_transformation_check
    or      a2, v0, r0

    lw      ra, 0x0010 (sp)
    lw      a0, 0x0014 (sp)
    lw      a1, 0x0018 (sp)

    jr      ra
    addiu   sp, sp, 0x20
