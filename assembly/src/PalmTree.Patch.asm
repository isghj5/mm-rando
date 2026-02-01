;==================================================================================================
; Spawn nut
;==================================================================================================

.headersize G_OBJ_YASI_DELTA

; Replaces:
;   JAL     0x800A7730 ; Item_DropCollectible
.org 0x80BB4C1C
    jal     ObjYasi_ItemSpawn

; Replaces:
;   JAL     Gfx_DrawDListOpa
.org 0x80BB4D4C
    jal     Models_DrawPalmTree_Hook
