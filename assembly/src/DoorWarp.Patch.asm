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
