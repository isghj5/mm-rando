.n64
.relativeinclude on

.create "../roms/patched.z64", 0
.incbin "../roms/base.z64"

;==================================================================================================
; Constants
;==================================================================================================

.include "constants.asm"
.include "dmadata.asm"

;==================================================================================================
; RAM translation for "code" (file 31)
;==================================================================================================

.headersize (G_CODE_RAM - G_CODE_FILE) // ROM != VROM for all tools

;==================================================================================================
; Base game editing region
;==================================================================================================

.include "boot.asm"
.include "hacks.asm"
.include "hooks/actor.asm"
.include "hooks/arrow_magic.asm"
.include "hooks/business_scrub.asm"
.include "hooks/cows.asm"
.include "hooks/dpad.asm"
.include "hooks/extended_objects.asm"
.include "hooks/file_select.asm"
.include "hooks/general.asm"
.include "hooks/hud_colors.asm"
.include "hooks/items.asm"
.include "hooks/misc.asm"
.include "hooks/models.asm"
.include "hooks/pause.asm"
.include "hooks/pushblock.asm"
.include "hooks/quest_items.asm"
.include "hooks/rooms.asm"
.include "hooks/savedata.asm"
.include "hooks/scene.asm"
.include "hooks/world_colors.asm"

.if MMR_MODS == 1
    .include "hooks/mmr.asm"
.endif

;==================================================================================================
; New code region
;==================================================================================================

.headersize (G_PAYLOAD_ADDR - G_PAYLOAD_VROM)

.org G_PAYLOAD_ADDR
.area (G_PAYLOAD_SIZE - G_C_HEAP_SIZE) // Payload max memory
PAYLOAD_START:

.include "init.asm"
.include "actor.asm"
.include "arrow_magic.asm"
.include "business_scrub.asm"
.include "cows.asm"
.include "damage.asm"
.include "dpad.asm"
.include "extended_objects.asm"
.include "file_select.asm"
.include "hud_colors.asm"
.include "items.asm"
.include "misc.asm"
.include "mmr.asm"
.include "models.asm"
.include "pause.asm"
.include "player_actor.asm"
.include "pushblock.asm"
.include "quest_items.asm"
.include "rooms.asm"
.include "savedata.asm"
.include "world_colors.asm"
.importobj "../build/bundle.o"
.align 8
DPAD_TEXTURE:
.incbin("../resources/dpad32.bin")

.align 0x10
PAYLOAD_END:
.endarea // Payload max memory

.close
