;==================================================================================================
; Update dmadata for Get-Item & Chest Tables
;==================================================================================================

.definelabel G_GI_TABLE_VROM,     0x03900000
.definelabel G_GI_TABLE_SIZE,     0xE50

.definelabel G_CHEST_TABLE_VROM,  0x03901000
.definelabel G_CHEST_TABLE_SIZE,  0x100

.orga (G_DMA_TABLE + (0x10 * 1545))
.area 0x20
    .word G_GI_TABLE_VROM, G_GI_TABLE_VROM + G_GI_TABLE_SIZE, G_GI_TABLE_VROM, 0
    .word G_CHEST_TABLE_VROM, G_CHEST_TABLE_VROM + G_CHEST_TABLE_SIZE, G_CHEST_TABLE_VROM, 0
.endarea

;==================================================================================================
; Include Tables as Virtual Files
;==================================================================================================

.orga G_GI_TABLE_VROM
.incbin("../../../MMR.Randomizer/Resources/mods/gi-table")

.orga G_CHEST_TABLE_VROM
.incbin("../../../MMR.Randomizer/Resources/mods/chest-table")

;==================================================================================================
; Include Mod Files Converted to Assembly
;==================================================================================================

.include "../mods/fix-item-checks.asm"
.include "../mods/fix-fairies.asm"
.include "../mods/fix-skulltula-tokens.asm"
.include "../mods/standing-hearts.asm"
.include "../mods/replace-gi-table.asm"
.include "../mods/update-chests.asm"

;==================================================================================================
; Set File Indexes for Get-Item & Chest Tables
;==================================================================================================

.headersize(G_CODE_RAM - G_CODE_FILE)

.org 0x80144968
    .word 1546
    .word 1545
