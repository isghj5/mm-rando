;==================================================================================================
; Before Handle Player Frozen State
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Replaces:
;   or      s0, a0, r0
;   or      s1, a1, r0
;   sw      ra, 0x001C (sp)
.org 0x808546DC ; RDRAM: 0x80771B6C, Offset: 0x26C4C
    sw      ra, 0x001C (sp)
    jal     player_before_handle_frozen_state_hook
    or      s0, a0, r0

;==================================================================================================
; Before Handle Player Voiding State
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Replaces:
;   or      s0, a0, r0
;   or      s1, a1, r0
;   sw      ra, 0x001C (sp)
.org 0x80854228 ; RDRAM: 0x807716B8, Offset: 0x26798
    sw      ra, 0x001C (sp)
    jal     player_before_handle_void_state_hook
    or      s0, a0, r0

;==================================================================================================
; Should Ice Void Zora
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Call function to determine if Zora should void during freeze.
; Replaces:
;   lbu     t9, 0x014B (s0)
;   addiu   at, r0, 0x0002
;   lui     t2, 0x0002
;   bne     t9, at, 0x8085479C
;   addu    t2, t2, s1
.org 0x8085475C ; RDRAM: 0x80771BEC, Offset: 0x26CCC
    jal     player_should_ice_void_zora_hook
    lbu     t9, 0x014B (s0)  ;; T9 = Link form value.
    lui     t2, 0x0002       ;;
    beqz    v0, 0x8085479C   ;; If returned false (0), jump past code which sets void flag.
    addu    t2, t2, s1       ;;

;==================================================================================================
; Should Prevent Restoring Swim State
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Fix branch into patched code, jump into the branch (instead of the delay slot).
; Replaces:
;   bnel    v1, at, 0x8083BE0C
.org 0x8083BCC4
    bnel    v1, at, 0x8083BE08

; Branch to function end early if not restoring swim state.
; Replaces:
;   lw      t4, 0xA6C (a1)
;   sll     t5, t4, 4
;   bgez    t5, 0x8083BEB4
;   nop
;   lb      t6, 0x0145 (a1)
.org 0x8083BE08 ; RDRAM: 0x80759298
    jal     player_should_prevent_restoring_swim_state_hook
    or      a2, v0, r0      ;; A2 = Function pointer.
    bnez    at, 0x8083BF44  ;; Branch to function end if not restoring swim state.
    lb      t6, 0x0145 (a1) ;; Displaced code.
    bgez    t5, 0x8083BEB4  ;; Original branch if swim flag not set.
