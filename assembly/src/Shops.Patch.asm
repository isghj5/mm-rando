; =========================================================
; Trading Post
; =========================================================

.headersize G_EN_OSSAN_DELTA

; Replaces:
;   jalr    ra, t9
.org 0x808A9ED0
    jal     ShopInventoryData_CheckPurchase

; Replaces:
;   jalr    ra, t9
.org 0x808A9F78
    jal     ShopInventoryData_HandleInstantPurchase

; =========================================================
; Potion Shop
; =========================================================

.headersize G_EN_TRT_DELTA

; Replaces:
;   jalr    ra, t9
.org 0x80A8CD64
    jal     ShopInventoryData_CheckPurchase

; Replaces:
;   jalr    ra, t9
.org 0x80A8CE18
    jal     ShopInventoryData_HandleInstantPurchase

; =========================================================
; Goron Shop / Bomb Shop / Zora Shop
; =========================================================

.headersize G_EN_SOB1_DELTA

; Replaces:
;   jalr    ra, t9
.org 0x80A0E588
    jal     ShopInventoryData_CheckPurchase

; Replaces:
;   jalr    ra, t9
.org 0x80A0E63C
    jal     ShopInventoryData_HandleInstantPurchase
