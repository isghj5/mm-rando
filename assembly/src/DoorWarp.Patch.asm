.headersize G_DOOR_WARP1_DELTA

;==================================================================================================
; Give Item right before the Boss Warp warps you.
;==================================================================================================

; Replaces:
;   LH	    T6, 0x01CE (A0)
;   ADDIU	T7, T6, 0xFFFF
;   SH	    T7, 0x01CE (A0)
.org 0x808B9EE0
    jal     DoorWarp_GiveItem_Hook
    nop
    nop

; Replaces
;   ANDI    V0, T5, 0xFFFF
;   SLT     AT, T6, V0
.org 0x808BA6E0
    jal     DoorWarp_GiveItem2_Hook
    nop

;==================================================================================================
; Replace remains checks with get-item checks.
;==================================================================================================

; Replaces:
;   SW	    A0, 0x0000 (SP)
;   LH      V0, 0x00A4 (A1)
.org 0x808B849C
    J       DoorWarp_GetSpawnItem
    NOP

;==================================================================================================
; Don't keep track of which Giants cutscenes have been seen.
; It's now based on number of remains the player has.
;==================================================================================================

; Replaces:
;   SW      T5, 0x0ED0 (V0)
.org 0x808B9E58
    nop

.org 0x808BA1A8
.area 0x90, 0
    addiu   a0, t6, 0xFFFF
    lui     v1, 0x801F
    addiu   v1, v1, 0xF670
.endarea
