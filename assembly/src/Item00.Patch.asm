;==================================================================================================
; Item00 Constructor after collectable flag has been checked and processed.
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lh      t4, 0x001C (s0)
;   addiu   at, r0, 0x0015
.org 0x800A5E1C
    jal     Item00_Constructor_Hook
    lw      a1, 0x004C (sp)

;==================================================================================================
; Give Item
;==================================================================================================

; Replaces:
;   lui     at, 0x801E
;   addu    at, at, t3
;   lw      t3, 0xBF24 (at)
.org 0x800A6E1C
    jal     Item00_GiveItem_Hook
    nop
    nop

;==================================================================================================
; Check if item can be spawned
;==================================================================================================

; Replaces:
;   andi    a0, s0, 0x00FF
;   sll     a0, a0, 16
;   sra     a0, a0, 16
.org 0x800A7984
    or      a0, s0, r0
    nop
    nop

; Replaces:
;   jal     0x800A7650
.org 0x800A7994
    jal     Item00_CanBeSpawned

;==================================================================================================
; Run before code block when item is being picked up
;==================================================================================================

; Replaces:
;   lw      t6, 0x1CCC (a1)
;   or      a0, s0, r0
.org 0x800A6A50
    jal     Item00_BeforeBeingPickedUp_Hook
    nop

;==================================================================================================
; Get how long item should delay before despawning
;==================================================================================================

; Replaces:
;   addiu   t1, r0, 0x000F
;   addiu   t2, r0, 0x0023
.org 0x800A70D8
    jal     Item00_GetDespawnDelayAmount_Hook
    nop

;==================================================================================================
; Replace Item00_GetAlteredDropId with a custom implementation
;==================================================================================================

; Replaces:
;   sw      a0, 0x0000 (sp)
;   sll     a0, a0, 16
;   sra     a0, a0, 16
;   addiu   at, r0, 0x0004
;   beq     a0, at, 0x800A767C
;   lui     t6, 0x801C
;   addiu   at, r0, 0x0019
.org 0x800A7650
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     Item00_GetAlteredDropId
    nop
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
