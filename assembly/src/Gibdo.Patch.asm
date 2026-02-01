.headersize G_EN_TALK_GIBUD_DELTA

/*
.macro GibdoRequestedItem,itemAction,item,amount,isBottledItem
    .byte itemAction
    .byte item
    .byte amount
    .byte isBottledItem
    .word 0
.endmacro

.org 0x80B013A8
    GibdoRequestedItem 36, 21,  1, 1 ; 0x00 - Blue Potion
    GibdoRequestedItem 46, 10,  5, 0 ; 0x01 - 5 Magic Beans
    GibdoRequestedItem 23, 31,  1, 1 ; 0x02 - Spring Water
    GibdoRequestedItem 22, 26,  1, 1 ; 0x03 - Fish
    GibdoRequestedItem 32, 27,  1, 1 ; 0x04 - Bugs
    GibdoRequestedItem 18,  9, 10, 0 ; 0x05 - 10 Deku Nuts
    GibdoRequestedItem 14,  6, 10, 0 ; 0x06 - 10 Bombs
    GibdoRequestedItem 24, 32,  1, 1 ; 0x07 - Hot Spring Water
    GibdoRequestedItem 34, 30,  1, 1 ; 0x08 - Big Poe
    GibdoRequestedItem 38, 24,  1, 1 ; 0x09 - Milk
    GibdoRequestedItem 32, 27,  1, 1 ; 0x0a - Bugs
    GibdoRequestedItem 32, 27,  1, 1 ; 0x0b - Bugs
    GibdoRequestedItem 22, 26,  1, 1 ; 0x0c - Fish
    GibdoRequestedItem 32, 27,  1, 1 ; 0x0d - Bugs
    GibdoRequestedItem 32, 27,  1, 1 ; 0x0e - Bugs
    GibdoRequestedItem 32, 27,  1, 1 ; 0x0f - Bugs
*/

; Replaces:
;   li      t5, 0x9
.org 0x80afea90
    li      t5, 0xf

; Replaces:
;   slti    at, v0, 0xa
.org 0x80afea98
    slti    at, v0, 0x10

; Replaces:
;   slti    at, v0, 0xa
.org 0x80afeaa4
    slti    at, v0, 0x10

.org 0x80affab0
.area 0x80affc10 - 0x80affab0
    addiu   sp, sp, -0x20
    sw      s0, 0x0018 (sp)
    or      s0, a0, r0
    sw      ra, 0x001c (sp)
    or      a3, a1, r0
    lw      t6, 0x0290 (a0)
Gibdo_GetTextIdForRequestedItem_Upper:
    lui     t8, 0x80b0
Gibdo_GetTextIdForRequestedItem_Lower:
    addiu   t8, t8, 0x13a8
    sll     t7, t6, 3
    addu    v0, t7, t8
    lhu     a1, 0x0004 (v0)
    sh      a1, 0x03dc (s0)
    or      a0, a3, r0
    jal     z2_ShowMessage
    or      a2, s0, r0
    lw      ra, 0x001c (sp)
    lw      s0, 0x0018 (sp)
    jr      ra
    addiu   sp, sp, 0x20
.endarea

; Replaces
;   .word 0x45000000 + Gibdo_GetTextIdForRequestedItem_Upper - G_EN_TALK_GIBUD_VRAM ; 0x45001234
;   .word 0x46000000 + Gibdo_GetTextIdForRequestedItem_Lower - G_EN_TALK_GIBUD_VRAM ; 0x4600123c
.org 0x80B016EC
    .word 0x45000000 + Gibdo_GetTextIdForRequestedItem_Upper - G_EN_TALK_GIBUD_VRAM
    .word 0x46000000 + Gibdo_GetTextIdForRequestedItem_Lower - G_EN_TALK_GIBUD_VRAM

; Replaces:
;   sltiu   at, t7, 0xe
;   beq     at, zero, 0x80affc8c
;   _sll    t7, t7, 0x2
; Gibdo_TextSwitch_Upper:
;   lui     at, 0x80b0
;   addu    at, at, t7
; Gibdo_TextSwitch_Lower:
;   lw      t7, 0x14b0(at)
;   jr      t7
;   _nop
.org 0x80affc40
    nop
    bnez     t7, 0x80affc74
     nop
    nop
    nop
    nop
    nop
    nop

; Replaces
;   .word 0x45000000 + Gibdo_TextSwitch_Upper - G_EN_TALK_GIBUD_VRAM ; 0x450013ac
;   .word 0x46000000 + Gibdo_TextSwitch_Lower - G_EN_TALK_GIBUD_VRAM ; 0x460013b4
.org 0x80B016F4
    .word 0
    .word 0

; Replaces:
;   sll     t7, t6, 4
.org 0x80affcb4
    sll     t7, t6, 3

; Replaces:
;   lw      t9, 0x0000 (v0) ; itemAction
.org 0x80affcbc
    lbu     t9, 0x0000 (v0) ; itemAction

; Replaces:
;   lh      t0, 0x000c (v0) ; isBottledItem
.org 0x80affcc8
    lbu     t0, 0x0003 (v0) ; isBottledItem

; Replaces:
;   lw      t1, 0x0004 (v0) ; item
.org 0x80affcd4
    lbu     t1, 0x0001 (v0) ; item

; Replaces:
;   lw      t4, 0x0008 (v0) ; amount
.org 0x80affce8
    lbu     t4, 0x0002 (v0) ; amount

; Replaces:
;   jal     z2_Inventory_HasItemInBottle
;   _lbu    a0, 0x0007 (v0) ; item
.org 0x80affd10
    jal     Gibdo_CheckPlayerHasItem
     or     a0, v0, r0

; Replaces:
;   sll     t0, t9, 4
.org 0x80b00040
    sll     t0, t9, 3

; Replaces:
;   lh      t2, 0x000c(v0) ; isBottledItem
.org 0x80b00048
    lbu     t2, 0x0003(v0) ; isBottledItem

; Replaces:
;   _li     a2, 0x12
;   lw      a1, 0x0008 (v0) ; amount
;   lh      a0, 0x0006 (v0) ; item
.org 0x80b00058
     or     a2, v0, r0
    lbu     a1, 0x0002 (v0) ; amount
    lbu     a0, 0x0001 (v0) ; item

; Replaces:
;   jal     z2_Player_UpdateBottleHeld
;   _li     a3, 0x15
.org 0x80b0007c
    jal     Gibdo_TakePlayerItem
     nop
