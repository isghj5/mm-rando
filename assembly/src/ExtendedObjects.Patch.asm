;==================================================================================================
; Extended Objects Table
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

; Lookup for get-item when loading into special buffer.
; Vanilla relocated address: 0x80755CFC (offset 0xADDC)
; Replaces:
;   lui     t0, 0x801C
;   addiu   t0, t0, 0x2740
.org 0x8083886C
    jal     extended_objects_lookup_gi_hook
    nop

.headersize G_CODE_DELTA

; Lookup for loading objects when loading a new scene.
; Replaces:
;   lbu     t6, 0x0008 (a3)
;   lui     t1, 0x801C
;   addiu   t1, t1, 0x2740
.org 0x8012F2FC
    jal     extended_objects_lookup_load_hook
    lbu     t6, 0x0008 (a3)
    or      t1, v0, r0

; Unknown lookup, affects objects in object context table (including shop objects).
; Processed once per frame but only does anything if object index is negative.
; Replaces:
;   subu    s2, r0, v0
;   addiu   a0, s0, 0x0028
.org 0x8012F54C
    jal     extended_objects_lookup_unknown_hook
    subu    s2, r0, v0

; Lookup for reloading objects when unpausing.
; Replaces:
;   sll     t8, a3, 4
;   addu    t8, t8, a3
.org 0x8012F6C8
    jal     extended_objects_lookup_unpause_hook
    sll     t8, a3, 4

; Lookup for loading shop items?
; Replaces:
;   sll     t6, t6, 2
;   addu    v1, a0, t6
;   lui     t9, 0x801C
;   subu    t7, r0, a2
;   addiu   t9, t9, 0x2740
;   sll     t8, a2, 3
.org 0x8012F750
.area 0x18
    sw      ra, 0x0000 (sp)
    jal     extended_objects_lookup_shop_hook
    sub     t7, r0, a2
    lw      ra, 0x0000 (sp)
    or      t9, v0, r0
    sll     t8, a2, 3
.endarea
