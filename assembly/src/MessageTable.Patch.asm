;==================================================================================================
; Extended Message Table - Get Data File VROM Address
;==================================================================================================

.headersize G_CODE_DELTA

; Called when getting data for get-item message.
; Other potential areas to patch: 0x801511DC, 0x801516C4
; Branches here if: *(u8*)(0x801EF670 +0x3F42) != 0
; Replaces:
;   lui     t8, 0x00AD
;   sw      t7, 0x1F10 (at)
;   lw      t6, 0x1D80 (v0)
;   lui     at, 0x0001
;   ori     at, at, 0x1880
;   lw      a2, 0x1D84 (v0)
;   addiu   t8, t8, 0x1000
;   sw      a3, 0x0024 (sp)
;   addu    a0, a3, at
;   jal     0x80080C90
;   addu    a1, t6, t8
.org 0x80151248
.area 0x2C
    sw      t7, 0x1F10 (at)
    lw      t6, 0x1D80 (v0)
    lui     at, 0x0001
    ori     at, at, 0x1880
    lw      a2, 0x1D84 (v0)
    jal     MessageTable_GetDataFileVrom_Hook
    sw      a3, 0x0024 (sp)
    lw      a3, 0x0024 (sp)
    addu    a0, a3, at
    jal     0x80080C90
    addu    a1, t6, v0
.endarea

;==================================================================================================
; Extended Message Table - Determine Data File Type
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lui     at, 0x3F80
;   mtc1    at, f0
;   lbu     t3, 0x6F20 (t2)
;   slti    at, t1, 0x4E20
.org 0x80150F5C
    jal     MessageTable_IsPrimaryMessage_Hook
    lbu     t3, 0x6F20 (t2)
    lui     t9, 0x3F80
    mtc1    t9, f0

;==================================================================================================
; Extended Message Table - Extend Lookup
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   addiu   v0, a0, 0x4A70
;   lui     at, 0x0001
;   lw      a1, 0x0004 (v1)
;   lw      a3, 0x000C (v1)
;   addu    at, at, v0
.org 0x80158950
    sw      ra, 0x0000 (sp)  ;; Store return address
    jal     MessageTable_LookupExtended_Hook
    nop
    bnez    t9, 0x80158980   ;; Branch to end if function returned true
    lw      ra, 0x0000 (sp)  ;; Load return address before returning
