;==================================================================================================
; Quest Item Inventory Checks (Near Door)
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Lookup type 0 (Room Key)
; Replaces:
;   lui     t7, 0x801F
;   addiu   at, r0, 0x002D
;   addu    t7, t7, t6
;   lbu     t7, 0xF6E0 (t7)
;   beq     t7, at, 0x801329F8
.org 0x80132994
    sw      a1, 0x0024 (sp)
    addiu   a1, r0, 0x002D
    jal     quest_items_door_check_hook
    or      a2, t6, r0
    bnez    t0, 0x801329F8

; Lookup type 1 (Kafei Letter)
; Replaces:
;   lui     t9, 0x801F
;   addiu   at, r0, 0x002F
;   addu    t9, t9, t8
;   lbu     t9, 0xF6E0 (t9)
;   beq     t9, at, 0x801329F8
.org 0x801329B8
    sw      a1, 0x0024 (sp)
    addiu   a1, r0, 0x002F
    jal     quest_items_door_check_hook
    or      a2, t8, r0
    bnez    t0, 0x801329F8

;==================================================================================================
; Quest Item Inventory Checks (En_Time_Tag)
;==================================================================================================

.headersize (G_EN_TIME_TAG_VRAM - G_EN_TIME_TAG_FILE)

; Prevent from being kicked out of Inn if Room Key is in storage.
; Replaces:
;   lui     a1, 0x801F
;   addiu   a1, a1, 0xF670
;   addu    t8, a1, t7
;   lbu     t9, 0x0070 (t8)
;   addiu   at, r0, 0x002D
;   beq     t9, at, 0x80ACA998
.org 0x80ACA86C
    or      a1, a2, r0
    addiu   a2, r0, 0x002D
    jal     quest_items_time_tag_check_hook
    or      a3, t7, r0
    or      a2, a1, r0
    bnez    v0, 0x80ACA998

;==================================================================================================
; Quest Item Inventory Checks (Trade Prompt)
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Still show trade prompts for items which are in storage but not on inventory slot.
; Replaces:
;   addu    t3, t3, t2
;   lbu     t3, 0xF6E0 (t3)
;   or      a1, t0, t1
;   sll     a1, a1, 16
;   bne     a0, t3, 0x8010B8D8
.org 0x8010B8B8
    sw      ra, 0x0008 (sp)
    jal     quest_items_fix_trade_prompt_hook
    or      a1, t0, t1
    lw      ra, 0x0008 (sp)
    beqz    v0, 0x8010B8D8

;==================================================================================================
; Quest Inventory Slot Clear (Song of Time)
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Handle inventory quest slots being removed during Song of Time.
; Replaces:
;   addiu   a0, r0, 0x0005
;   jal     0x801149A0
;   addiu   a1, r0, 0x0005
;   addiu   a0, r0, 0x000B
;   jal     0x801149A0
;   addiu   a1, r0, 0x000B
;   addiu   a0, r0, 0x0011
;   jal     0x801149A0
;   addiu   a1, r0, 0x0011
.org 0x80144390
.area 0x24
    addiu   a0, r0, 0x0005
    jal     quest_items_clear_inventory_item_sot
    addiu   a1, r0, 0x0005
    addiu   a0, r0, 0x000B
    jal     quest_items_clear_inventory_item_sot
    addiu   a1, r0, 0x000B
    addiu   a0, r0, 0x0011
    jal     quest_items_clear_inventory_item_sot
    addiu   a1, r0, 0x0011
.endarea

;==================================================================================================
; Quest Item Removal (General)
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE)

; Handle removal of multiple different quest items.
; Used when giving: Letter to Mama, Letter to Kafei, Pendant.
; Replaces:
;   jal     0x801149A0
.org 0x8010BC60
    jal     quest_items_try_remove_item

;==================================================================================================
; Quest Item Removal (Hand in Toilet)
;==================================================================================================

.headersize (G_EN_BJT_VRAM - G_EN_BJT_FILE)

; Handle removal of Letter to Kafei.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD468
    jal     quest_items_try_remove_item

; Handle removal of Swamp Title Deed.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD480
    jal     quest_items_try_remove_item

; Handle removal of Mountain Title Deed.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD498
    jal     quest_items_try_remove_item

; Handle removal of Ocean Title Deed.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD4B0
    jal     quest_items_try_remove_item

; Handle removal of Town Title Deed.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD4C8
    jal     quest_items_try_remove_item

; Handle removal of Letter to Mama.
; Replaces:
;   jal     0x801149A0
.org 0x80BFD4E0
    jal     quest_items_try_remove_item
