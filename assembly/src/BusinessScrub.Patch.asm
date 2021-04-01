;==================================================================================================
; Business Scrub Item Consume
;==================================================================================================

.headersize G_EN_AKINDONUTS_DELTA

; Consume quest item while getting index of get-item table for scrub item.
; Replaces:
;   lh      v0, 0x001C (a0)
;   addiu   at, r0, 0x0001
.org 0x80BECFC4
    jal     BusinessScrub_ConsumeItem_Hook
    nop

;==================================================================================================
; Business Scrub Before Giving Item (Clock Town)
;==================================================================================================

.headersize G_EN_SELLNUTS_DELTA

; Optionally consume item before receiving the new item.
; Replaces:
;   sw      a1, 0x0024 (sp)
;   lw      a1, 0x0024 (sp)
.org 0x80ADBBF4
    jal     BusinessScrub_BeforeGiveItemClockTown_Hook
    sw      a1, 0x0024 (sp)

;==================================================================================================
; Business Scrub Initial Dialogue (Swamp Scrub)
;==================================================================================================

.headersize G_EN_AKINDONUTS_DELTA

; Check if deed has already been given.
; Replaces:
;   addiu   t9, r0, 0x15e0
;   sh      t9, 0x033c (a2)
.org 0x80BED420
    jal     BusinessScrub_InitialDialogue_Hook
    nop

;==================================================================================================
; Business Scrub Initial Dialogue (Mountain Scrub)
;==================================================================================================

.headersize G_EN_AKINDONUTS_DELTA

; Check if deed has already been given.
; Replaces:
;   addiu   t8, r0, 0x15f4
;   bne     v0, at, ????
;   andi    a3, t8, 0xffff
;   b       ????
;   sh      t8, 0x033c (a2)
.org 0x80BED8FC
    bne     v0, at, 0x80BED910
    nop
    jal     BusinessScrub_InitialDialogue_Hook
    nop
    b       0x80BEDB6C

;==================================================================================================
; Business Scrub Initial Dialogue (Ocean Scrub)
;==================================================================================================

.headersize G_EN_AKINDONUTS_DELTA

; Check if deed has already been given.
; Replaces:
;   addiu   t8, r0, 0x1607
;   bne     v0, at, ????
;   andi    a3, t8, 0xffff
;   b       ????
;   sh      t8, 0x033c (a2)
.org 0x80BEDE04
    bne     v0, at, 0x80BEDE18
    nop
    jal     BusinessScrub_InitialDialogue_Hook
    nop
    b       0x80BEE054

;==================================================================================================
; Business Scrub Initial Dialogue (Canyon Scrub)
;==================================================================================================

.headersize G_EN_AKINDONUTS_DELTA

; Check if deed has already been given.
; Replaces:
;   addiu   t9, r0, 0x161b
;   sh      t9, 0x033c (a2)
.org 0x80BEE308
    jal     BusinessScrub_InitialDialogue_Hook
    nop
