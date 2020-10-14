; DMA Table constants
G_BASE equ                "../roms/base.z64"
.definelabel G_DMA_TABLE, 0x1A500

.macro DefineDmaFile, DmaVar, FileVar, Index
    .definelabel DmaVar,  G_DMA_TABLE + (0x10 * Index)
    .definelabel FileVar, (readbyte(G_BASE, (DmaVar + 8)) << 24 | readbyte(G_BASE, (DmaVar + 9)) << 16 | readbyte(G_BASE, (DmaVar + 10)) << 8 | readbyte(G_BASE, (DmaVar + 11)))
.endmacro

DefineDmaFile G_CODE_DMA, G_CODE_FILE, 31
.definelabel  G_CODE_RAM, 0x800A5AC0

; ovl_file_choose
DefineDmaFile G_FILE_CHOOSE_DMA, G_FILE_CHOOSE_FILE, 35
.definelabel  G_FILE_CHOOSE_VRAM, 0x80804010

; ovl_kaleido_scope (Pause menu)
DefineDmaFile G_KALEIDO_SCOPE_DMA, G_KALEIDO_SCOPE_FILE, 37
.definelabel  G_KALEIDO_SCOPE_VRAM, 0x808160A0

; ovl_player_actor
DefineDmaFile G_PLAYER_ACTOR_DMA, G_PLAYER_ACTOR_FILE, 38
.definelabel  G_PLAYER_ACTOR_VRAM, 0x8082DA90

; ovl_En_Torch2 (Elegy Statue)
DefineDmaFile G_EN_TORCH2_DMA, G_EN_TORCH2_FILE, 65
.definelabel  G_EN_TORCH2_VRAM, 0x808A31B0

; ovl_Item_B_Heart (Heart Container)
DefineDmaFile G_ITEM_B_HEART_DMA, G_ITEM_B_HEART_FILE, 80
.definelabel  G_ITEM_B_HEART_VRAM, 0x808BCDF0

; ovl_Obj_Oshihiki (Pushblock)
DefineDmaFile G_OBJ_OSHIHIKI_DMA, G_OBJ_OSHIHIKI_FILE, 115
.definelabel  G_OBJ_OSHIHIKI_VRAM, 0x80917290

; ovl_Bg_Ingate (Boat Cruise Canoe)
DefineDmaFile G_BG_INGATE_DMA, G_BG_INGATE_FILE, 153
.definelabel  G_BG_INGATE_VRAM, 0x80953A90

; ovl_En_Cow
DefineDmaFile G_EN_COW_DMA, G_EN_COW_FILE, 227
.definelabel  G_EN_COW_VRAM, 0x8099C290

; ovl_Obj_Pzlblock (Woodfall Temple pushblock, Sakon's Hideout pushblocks)
DefineDmaFile G_OBJ_PZLBLOCK_DMA, G_OBJ_PZLBLOCK_FILE, 237
.definelabel  G_OBJ_PZLBLOCK_VRAM, 0x809A33E0

; ovl_Obj_Iceblock
DefineDmaFile G_OBJ_ICEBLOCK_DMA, G_OBJ_ICEBLOCK_FILE, 288
.definelabel  G_OBJ_ICEBLOCK_VRAM, 0x80A23090

; ovl_Bg_Dblue_Movebg (Great Bay Temple faucets)
DefineDmaFile G_BG_DBLUE_MOVEBG_DMA, G_BG_DBLUE_MOVEBG_FILE, 290
.definelabel  G_BG_DBLUE_MOVEBG_VRAM, 0x80A29A80

; ovl_En_Si (Skulltula Token)
DefineDmaFile G_EN_SI_DMA, G_EN_SI_FILE, 216
.definelabel  G_EN_SI_VRAM, 0x8098CA20

; ovl_Dm_Hina (Boss Remains)
DefineDmaFile G_DM_HINA_DMA, G_DM_HINA_FILE, 284
.definelabel  G_DM_HINA_VRAM, 0x80A1F410

; ovl_Eff_Change (Camera Refocuser for Elegy)
DefineDmaFile G_EFF_CHANGE_DMA, G_EFF_CHANGE_FILE, 318
.definelabel  G_EFF_CHANGE_VRAM, 0x80A4C490

; ovl_En_Time_Tag
DefineDmaFile G_EN_TIME_TAG_DMA, G_EN_TIME_TAG_FILE, 392
.definelabel  G_EN_TIME_TAG_VRAM, 0x80AC9EA0

; ovl_En_Elforg (Stray Fairy)
DefineDmaFile G_EN_ELFORG_DMA, G_EN_ELFORG_FILE, 397
.definelabel  G_EN_ELFORG_VRAM, 0x80ACC470

; ovl_En_Sellnuts (Clock Town Business Scrub)
DefineDmaFile G_EN_SELLNUTS_DMA, G_EN_SELLNUTS_FILE, 409
.definelabel  G_EN_SELLNUTS_VRAM, 0x80ADADD0

; ovl_En_Col_Man
DefineDmaFile G_EN_COL_MAN_DMA, G_EN_COL_MAN_FILE, 434
.definelabel  G_EN_COL_MAN_VRAM, 0x80AFDC40

; ovl_Obj_Ghaka (Darmani's Gravestone)
DefineDmaFile G_OBJ_GHAKA_DMA, G_OBJ_GHAKA_FILE, 467
.definelabel  G_OBJ_GHAKA_VRAM, 0x80B3C260

; ovl_En_Ot (Seahorse)
DefineDmaFile G_EN_OT_DMA, G_EN_OT_FILE, 476
.definelabel  G_EN_OT_VRAM, 0x80B5B2E0

; ovl_Bg_Kin2_Shelf (Oceanside Skulltula House shelves)
DefineDmaFile G_BG_KIN2_SHELF_DMA, G_BG_KIN2_SHELF_FILE, 488
.definelabel  G_BG_KIN2_SHELF_VRAM, 0x80B6FB30

; ovl_Bg_Ikana_Block (Ikana pushblock)
DefineDmaFile G_BG_IKANA_BLOCK_DMA, G_BG_IKANA_BLOCK_FILE, 495
.definelabel  G_BG_IKANA_BLOCK_VRAM, 0x80B7EA60

; ovl_Obj_Boat (Pirates' Fortress Boat)
DefineDmaFile G_OBJ_BOAT_DMA, G_OBJ_BOAT_FILE, 515
.definelabel  G_OBJ_BOAT_VRAM, 0x80B9AF50

; ovl_En_Toto (Toto)
DefineDmaFile G_EN_TOTO_DMA, G_EN_TOTO_FILE, 523
.definelabel  G_EN_TOTO_VRAM, 0x80BA36C0

; ovl_En_Suttari (Sakon)
DefineDmaFile G_EN_SUTTARI_DMA, G_EN_SUTTARI_FILE, 526
.definelabel  G_EN_SUTTARI_VRAM, 0x80BAA6D0

; ovl_Bg_F40_Block (Stone Tower Temple Shifting Block)
DefineDmaFile G_BG_F40_BLOCK_DMA, G_BG_F40_BLOCK_FILE, 540
.definelabel  G_BG_F40_BLOCK_VRAM, 0x80BC3980

; ovl_En_Akindonuts (Traveling Business Scrub)
DefineDmaFile G_EN_AKINDONUTS_DMA, G_EN_AKINDONUTS_FILE, 587
.definelabel  G_EN_AKINDONUTS_VRAM, 0x80BECBE0

; ovl_En_Bjt (Hand in Toilet)
DefineDmaFile G_EN_BJT_DMA, G_EN_BJT_FILE, 596
.definelabel  G_EN_BJT_VRAM, 0x80BFD2E0

; ovl_Obj_Moon_Stone (Moon's Tear)
DefineDmaFile G_OBJ_MOON_STONE_DMA, G_OBJ_MOON_STONE_FILE, 602
.definelabel  G_OBJ_MOON_STONE_VRAM, 0x80C06510

; ovl_En_Jgame_Tsn (Fisherman - Fisherman's Jumping Game)
DefineDmaFile G_EN_JGAME_TSN_DMA, G_EN_JGAME_TSN_FILE, 617
.definelabel  G_EN_JGAME_TSN_VRAM, 0x80C13930

; ovl_En_Bb (Blue Bubble)
DefineDmaFile G_EN_BB_DMA, G_EN_BB_FILE, 84
.definelabel  G_EN_BB_VRAM, 0x808C1D40

; ovl_En_Knight (Igos du Ikana & Henchmen)
DefineDmaFile G_EN_KNIGHT_DMA, G_EN_KNIGHT_FILE, 250
.definelabel  G_EN_KNIGHT_VRAM, 0x809B20F0

; ovl_En_Elfgrp (Group of Stray Fairies)
DefineDmaFile G_EN_ELFGRP_DMA, G_EN_ELFGRP_FILE, 305
.definelabel  G_EN_ELFGRP_VRAM, 0x80A396B0
