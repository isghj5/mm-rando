;==================================================================================================
; Write Get-Item Index on Init
;==================================================================================================

.headersize G_EN_BOX_DELTA

; Resolve get-item table index from chest table, and write to En_Box actor field.
; Replaces:
;   lh      a1, 0x001C (s1)       ;; A1 = Actor Variable (for later function call)
;   sra     t8, t7, 5             ;;
;   andi    t9, t8, 0x007F        ;; T9 = (variable >> 5) & 0x7F
;   sw      t9, 0x021C (s1)       ;; Store T9 as gi-table index in actor field.
;   sh      t6, 0x00BC (s1)       ;;
.org 0x808681C0 ; Offset: 0x5F0
    sh      t6, 0x00BC (s1)       ;;
    or      a0, s1, r0            ;; A0 = Actor
    jal     Chest_WriteGiIndex    ;; Call function to find & write gi-table index to actor field.
    lw      a1, 0x005C (sp)       ;; A1 = GlobalContext
    lh      a1, 0x001C (s1)       ;; A1 = Actor Variable

;==================================================================================================
; Remove setting collectible flag
;==================================================================================================

.headersize G_EN_BOX_DELTA

; Always branch to while-opening code (see below).
; Replaces:
;   ADDIU   AT, R0, 0x000C
;   BEQ     V0, AT, 0x80868E88
;   LW      A0, 0x0084 (SP)
;   ADDIU   AT, R0, 0x005A
;   BNEL    V0, AT, .+0x14
;   LH      A1, 0x001C (S0)
;   JAL     0x800B5DB0 ; Flags_SetCollectible
;   LW      A1, 0x0220 (S0)
.org 0x80868E70
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop

; Always branch to before-opening code (see below).
; Replaces:
;   beq     v0, at, 0x80868F5C
.org 0x80868F4C
    b       0x80868F5C

; Called before opening to check flags.
; Replaces:
;   jal     0x800B5D6C             ;; Call: Actor_GetCollectibleFlag
;   lw      a1, 0x0220(s0)         ;; Collectible flag index
;   beqz    v0, 0x80868F70         ;; Branch if flag not set
;   addiu   v1, r0, 0x000A         ;; V1 = 0xA
;   sw      v1, 0x021C(s0)         ;; If flag already set, replace gi-table index with hardcoded value: 0xA
;   lw      v0, 0x021C(s0)         ;;
.org 0x80868F5C
    nop
    nop
    nop
    nop
    nop
    nop

;==================================================================================================
; Fix Ice Trap Sound Effect
;==================================================================================================

.headersize G_EN_BOX_DELTA

; Use the correct sound effect Id for ice traps.
; Replaces:
;   addiu   a1, r0, 0x31F1
.org 0x80869344 ; Offset: 0x1774
    addiu   a1, r0, 0x31A4

;==================================================================================================
; Fix Large Chest Ice Trap
;==================================================================================================

.headersize G_EN_BOX_DELTA

; Replaces:
;   LUI     AT, 0x4234
;   MTC1    AT, F4
;   LWC1    F6, 0x0174 (S0)
;   C.LT.S  F4, F6
;   NOP
;   BC1FL   0x808695D8
.org 0x8086959C
    nop
    nop
    nop
    jal     Chest_ShouldIceSmokeSpawn
    or      a0, s0, r0
    beqz    v0, 0x808695D8
