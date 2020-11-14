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
; Handle Linear Velocity
;==================================================================================================

.headersize (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Call function to handle player's linear velocity.
; Replaces:
;   jal     0x800FF2F8
.org 0x8083CC84
    jal     player_handle_linear_velocity_hook
