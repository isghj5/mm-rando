;==================================================================================================
; Business Scrub Item Consume
;==================================================================================================

.headersize (G_EN_AKINDONUTS_VRAM - G_EN_AKINDONUTS_FILE)

; Replaces:
;   addiu   sp, sp, -0x18
.org 0x80BECFBC
    j       business_scrub_consume_item

;==================================================================================================
; Business Scrub Before Giving Item (Clock Town)
;==================================================================================================

.headersize (G_EN_SELLNUTS_VRAM - G_EN_SELLNUTS_FILE)

; Optionally consume item before receiving the new item.
; Replaces:
;   sw      a1, 0x0024 (sp)
;   lw      a1, 0x0024 (sp)
.org 0x80ADBBF4
    jal     business_scrub_before_give_item_clock_town_hook
    sw      a1, 0x0024 (sp)
