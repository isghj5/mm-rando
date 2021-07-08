; DMA Table constants
.definelabel G_DMA_TABLE, 0x1A500

; Overlay: code
.definelabel G_CODE_FILE, 0xB3C000
.definelabel G_CODE_RAM, 0x800A5AC0
.definelabel G_CODE_DELTA, (G_CODE_RAM - G_CODE_FILE)

; Overlay: title
.definelabel G_TITLE_FILE, 0xC7A4E0
.definelabel G_TITLE_VRAM, 0x80800000
.definelabel G_TITLE_DELTA, (G_TITLE_VRAM - G_TITLE_FILE)

; Overlay: select
.definelabel G_SELECT_FILE, 0xC7ADF0
.definelabel G_SELECT_VRAM, 0x80800910
.definelabel G_SELECT_DELTA, (G_SELECT_VRAM - G_SELECT_FILE)

; Overlay: opening
.definelabel G_OPENING_FILE, 0xC7E2D0
.definelabel G_OPENING_VRAM, 0x80803DF0
.definelabel G_OPENING_DELTA, (G_OPENING_VRAM - G_OPENING_FILE)

; Overlay: file_choose
.definelabel G_FILE_CHOOSE_FILE, 0xC7E4F0
.definelabel G_FILE_CHOOSE_VRAM, 0x80804010
.definelabel G_FILE_CHOOSE_DELTA, (G_FILE_CHOOSE_VRAM - G_FILE_CHOOSE_FILE)

; Overlay: daytelop :: Day Transition Overlays
.definelabel G_DAYTELOP_FILE, 0xC8F360
.definelabel G_DAYTELOP_VRAM, 0x80814EB0
.definelabel G_DAYTELOP_DELTA, (G_DAYTELOP_VRAM - G_DAYTELOP_FILE)

; Overlay: kaleido_scope
.definelabel G_KALEIDO_SCOPE_FILE, 0xC90550
.definelabel G_KALEIDO_SCOPE_VRAM, 0x808160A0
.definelabel G_KALEIDO_SCOPE_DELTA, (G_KALEIDO_SCOPE_VRAM - G_KALEIDO_SCOPE_FILE)

; Overlay: player_actor
.definelabel G_PLAYER_ACTOR_FILE, 0xCA7F00
.definelabel G_PLAYER_ACTOR_VRAM, 0x8082DA90
.definelabel G_PLAYER_ACTOR_DELTA, (G_PLAYER_ACTOR_VRAM - G_PLAYER_ACTOR_FILE)

; Overlay: En_Test
.definelabel G_EN_TEST_FILE, 0xCDCF60
.definelabel G_EN_TEST_VRAM, 0x80862B70
.definelabel G_EN_TEST_DELTA, (G_EN_TEST_VRAM - G_EN_TEST_FILE)

; Overlay: En_GirlA :: Shop Inventory Data
.definelabel G_EN_GIRLA_FILE, 0xCDDC60
.definelabel G_EN_GIRLA_VRAM, 0x80863870
.definelabel G_EN_GIRLA_DELTA, (G_EN_GIRLA_VRAM - G_EN_GIRLA_FILE)

; Overlay: En_Part
.definelabel G_EN_PART_FILE, 0xCDF760
.definelabel G_EN_PART_VRAM, 0x80865370
.definelabel G_EN_PART_DELTA, (G_EN_PART_VRAM - G_EN_PART_FILE)

; Overlay: En_Light :: Flame
.definelabel G_EN_LIGHT_FILE, 0xCDFD80
.definelabel G_EN_LIGHT_VRAM, 0x80865990
.definelabel G_EN_LIGHT_DELTA, (G_EN_LIGHT_VRAM - G_EN_LIGHT_FILE)

; Overlay: En_Door :: Wooden Door
.definelabel G_EN_DOOR_FILE, 0xCE0BF0
.definelabel G_EN_DOOR_VRAM, 0x80866800
.definelabel G_EN_DOOR_DELTA, (G_EN_DOOR_VRAM - G_EN_DOOR_FILE)

; Overlay: En_Box :: Treasure Chest
.definelabel G_EN_BOX_FILE, 0xCE1FB0
.definelabel G_EN_BOX_VRAM, 0x80867BD0
.definelabel G_EN_BOX_DELTA, (G_EN_BOX_VRAM - G_EN_BOX_FILE)

; Overlay: En_Pametfrog :: Gekko (Mini-Boss)
.definelabel G_EN_PAMETFROG_FILE, 0xCE4170
.definelabel G_EN_PAMETFROG_VRAM, 0x80869D90
.definelabel G_EN_PAMETFROG_DELTA, (G_EN_PAMETFROG_VRAM - G_EN_PAMETFROG_FILE)

; Overlay: En_Okuta :: Octorok (Field Enemy)
.definelabel G_EN_OKUTA_FILE, 0xCE8200
.definelabel G_EN_OKUTA_VRAM, 0x8086DE20
.definelabel G_EN_OKUTA_DELTA, (G_EN_OKUTA_VRAM - G_EN_OKUTA_FILE)

; Overlay: En_Bom :: Powder Keg
.definelabel G_EN_BOM_FILE, 0xCEB190
.definelabel G_EN_BOM_VRAM, 0x80870DB0
.definelabel G_EN_BOM_DELTA, (G_EN_BOM_VRAM - G_EN_BOM_FILE)

; Overlay: En_Wallmas :: Wallmaster
.definelabel G_EN_WALLMAS_FILE, 0xCEEA30
.definelabel G_EN_WALLMAS_VRAM, 0x80874810
.definelabel G_EN_WALLMAS_DELTA, (G_EN_WALLMAS_VRAM - G_EN_WALLMAS_FILE)

; Overlay: En_Dodongo :: Dodongo
.definelabel G_EN_DODONGO_FILE, 0xCF0890
.definelabel G_EN_DODONGO_VRAM, 0x80876670
.definelabel G_EN_DODONGO_DELTA, (G_EN_DODONGO_VRAM - G_EN_DODONGO_FILE)

; Overlay: En_Firefly :: Keese
.definelabel G_EN_FIREFLY_FILE, 0xCF3910
.definelabel G_EN_FIREFLY_VRAM, 0x808796F0
.definelabel G_EN_FIREFLY_DELTA, (G_EN_FIREFLY_VRAM - G_EN_FIREFLY_FILE)

; Overlay: En_Horse :: Child Epona (Cutscenes)
.definelabel G_EN_HORSE_FILE, 0xCF5950
.definelabel G_EN_HORSE_VRAM, 0x8087B730
.definelabel G_EN_HORSE_DELTA, (G_EN_HORSE_VRAM - G_EN_HORSE_FILE)

; Overlay: En_Arrow :: Arrow
.definelabel G_EN_ARROW_FILE, 0xD04460
.definelabel G_EN_ARROW_VRAM, 0x8088A240
.definelabel G_EN_ARROW_DELTA, (G_EN_ARROW_VRAM - G_EN_ARROW_FILE)

; Overlay: En_Elf :: Tatl (Gameplay) & Healing Fairy
.definelabel G_EN_ELF_FILE, 0xD06730
.definelabel G_EN_ELF_VRAM, 0x8088C510
.definelabel G_EN_ELF_DELTA, (G_EN_ELF_VRAM - G_EN_ELF_FILE)

; Overlay: En_Niw :: Cucco (Friendly)
.definelabel G_EN_NIW_FILE, 0xD0B280
.definelabel G_EN_NIW_VRAM, 0x80891060
.definelabel G_EN_NIW_DELTA, (G_EN_NIW_VRAM - G_EN_NIW_FILE)

; Overlay: En_Tite :: Tektite
.definelabel G_EN_TITE_FILE, 0xD0DA10
.definelabel G_EN_TITE_VRAM, 0x808937F0
.definelabel G_EN_TITE_DELTA, (G_EN_TITE_VRAM - G_EN_TITE_FILE)

; Overlay: En_Peehat :: Peahat
.definelabel G_EN_PEEHAT_FILE, 0xD11150
.definelabel G_EN_PEEHAT_VRAM, 0x80896F30
.definelabel G_EN_PEEHAT_DELTA, (G_EN_PEEHAT_VRAM - G_EN_PEEHAT_FILE)

; Overlay: En_Holl :: Room Transition Plane
.definelabel G_EN_HOLL_FILE, 0xD13B80
.definelabel G_EN_HOLL_VRAM, 0x80899960
.definelabel G_EN_HOLL_DELTA, (G_EN_HOLL_VRAM - G_EN_HOLL_FILE)

; Overlay: En_Dinofos :: Dinolfos
.definelabel G_EN_DINOFOS_FILE, 0xD14900
.definelabel G_EN_DINOFOS_VRAM, 0x8089A6E0
.definelabel G_EN_DINOFOS_DELTA, (G_EN_DINOFOS_VRAM - G_EN_DINOFOS_FILE)

; Overlay: En_Hata :: Flagpole
.definelabel G_EN_HATA_FILE, 0xD18B00
.definelabel G_EN_HATA_VRAM, 0x8089E8E0
.definelabel G_EN_HATA_DELTA, (G_EN_HATA_VRAM - G_EN_HATA_FILE)

; Overlay: En_Zl1 :: Child Zelda [OoT]
.definelabel G_EN_ZL1_FILE, 0xD18FB0
.definelabel G_EN_ZL1_VRAM, 0x8089ED90
.definelabel G_EN_ZL1_DELTA, (G_EN_ZL1_VRAM - G_EN_ZL1_FILE)

; Overlay: En_Viewer
.definelabel G_EN_VIEWER_FILE, 0xD19040
.definelabel G_EN_VIEWER_VRAM, 0x8089EE20
.definelabel G_EN_VIEWER_DELTA, (G_EN_VIEWER_VRAM - G_EN_VIEWER_FILE)

; Overlay: En_Bubble :: Shabom
.definelabel G_EN_BUBBLE_FILE, 0xD196F0
.definelabel G_EN_BUBBLE_VRAM, 0x8089F4E0
.definelabel G_EN_BUBBLE_DELTA, (G_EN_BUBBLE_VRAM - G_EN_BUBBLE_FILE)

; Overlay: Door_Shutter :: Door Shutter
.definelabel G_DOOR_SHUTTER_FILE, 0xD1AB00
.definelabel G_DOOR_SHUTTER_VRAM, 0x808A08F0
.definelabel G_DOOR_SHUTTER_DELTA, (G_DOOR_SHUTTER_VRAM - G_DOOR_SHUTTER_FILE)

; Overlay: En_Boom :: Zora Fins
.definelabel G_EN_BOOM_FILE, 0xD1C6E0
.definelabel G_EN_BOOM_VRAM, 0x808A24D0
.definelabel G_EN_BOOM_DELTA, (G_EN_BOOM_VRAM - G_EN_BOOM_FILE)

; Overlay: En_Torch2 :: Elegy Statues
.definelabel G_EN_TORCH2_FILE, 0xD1D3C0
.definelabel G_EN_TORCH2_VRAM, 0x808A31B0
.definelabel G_EN_TORCH2_DELTA, (G_EN_TORCH2_VRAM - G_EN_TORCH2_FILE)

; Overlay: En_Minifrog :: Frog I [?]
.definelabel G_EN_MINIFROG_FILE, 0xD1D880
.definelabel G_EN_MINIFROG_VRAM, 0x808A3670
.definelabel G_EN_MINIFROG_DELTA, (G_EN_MINIFROG_VRAM - G_EN_MINIFROG_FILE)

; Overlay: En_St :: Skulltula
.definelabel G_EN_ST_FILE, 0xD1F260
.definelabel G_EN_ST_VRAM, 0x808A5050
.definelabel G_EN_ST_DELTA, (G_EN_ST_VRAM - G_EN_ST_FILE)

; Overlay: Obj_Wturn :: Stone Tower Temple Inverter
.definelabel G_OBJ_WTURN_FILE, 0xD21B40
.definelabel G_OBJ_WTURN_VRAM, 0x808A7930
.definelabel G_OBJ_WTURN_DELTA, (G_OBJ_WTURN_VRAM - G_OBJ_WTURN_FILE)

; Overlay: En_River_Sound :: Sound Effects I
.definelabel G_EN_RIVER_SOUND_FILE, 0xD22040
.definelabel G_EN_RIVER_SOUND_VRAM, 0x808A7E30
.definelabel G_EN_RIVER_SOUND_DELTA, (G_EN_RIVER_SOUND_VRAM - G_EN_RIVER_SOUND_FILE)

; Overlay: En_Ossan
.definelabel G_EN_OSSAN_FILE, 0xD222B0
.definelabel G_EN_OSSAN_VRAM, 0x808A80A0
.definelabel G_EN_OSSAN_DELTA, (G_EN_OSSAN_VRAM - G_EN_OSSAN_FILE)

; Overlay: En_Famos :: Death Armos
.definelabel G_EN_FAMOS_FILE, 0xD26B30
.definelabel G_EN_FAMOS_VRAM, 0x808AC920
.definelabel G_EN_FAMOS_DELTA, (G_EN_FAMOS_VRAM - G_EN_FAMOS_FILE)

; Overlay: En_Bombf :: Bomb Flower
.definelabel G_EN_BOMBF_FILE, 0xD28AD0
.definelabel G_EN_BOMBF_VRAM, 0x808AE8C0
.definelabel G_EN_BOMBF_DELTA, (G_EN_BOMBF_VRAM - G_EN_BOMBF_FILE)

; Overlay: En_Am :: Armos
.definelabel G_EN_AM_FILE, 0xD29EE0
.definelabel G_EN_AM_VRAM, 0x808AFCD0
.definelabel G_EN_AM_DELTA, (G_EN_AM_VRAM - G_EN_AM_FILE)

; Overlay: En_Dekubaba :: Deku Baba
.definelabel G_EN_DEKUBABA_FILE, 0xD2B540
.definelabel G_EN_DEKUBABA_VRAM, 0x808B1330
.definelabel G_EN_DEKUBABA_DELTA, (G_EN_DEKUBABA_VRAM - G_EN_DEKUBABA_FILE)

; Overlay: En_M_Fire1 :: Deku Nut Effect
.definelabel G_EN_M_FIRE1_FILE, 0xD2F440
.definelabel G_EN_M_FIRE1_VRAM, 0x808B5230
.definelabel G_EN_M_FIRE1_DELTA, (G_EN_M_FIRE1_VRAM - G_EN_M_FIRE1_FILE)

; Overlay: En_M_Thunder :: Spin Attack & Sword Beam Effects
.definelabel G_EN_M_THUNDER_FILE, 0xD2F5D0
.definelabel G_EN_M_THUNDER_VRAM, 0x808B53C0
.definelabel G_EN_M_THUNDER_DELTA, (G_EN_M_THUNDER_VRAM - G_EN_M_THUNDER_FILE)

; Overlay: Bg_Breakwall
.definelabel G_BG_BREAKWALL_FILE, 0xD31570
.definelabel G_BG_BREAKWALL_VRAM, 0x808B7360
.definelabel G_BG_BREAKWALL_DELTA, (G_BG_BREAKWALL_VRAM - G_BG_BREAKWALL_FILE)

; Overlay: Door_Warp1 :: Blue Warp & Boss Lair Warps
.definelabel G_DOOR_WARP1_FILE, 0xD326A0
.definelabel G_DOOR_WARP1_VRAM, 0x808B8490
.definelabel G_DOOR_WARP1_DELTA, (G_DOOR_WARP1_VRAM - G_DOOR_WARP1_FILE)

; Overlay: Obj_Syokudai :: Torch Stand (Generic)
.definelabel G_OBJ_SYOKUDAI_FILE, 0xD36210
.definelabel G_OBJ_SYOKUDAI_VRAM, 0x808BC010
.definelabel G_OBJ_SYOKUDAI_DELTA, (G_OBJ_SYOKUDAI_VRAM - G_OBJ_SYOKUDAI_FILE)

; Overlay: Item_B_Heart :: Heart Container
.definelabel G_ITEM_B_HEART_FILE, 0xD36FE0
.definelabel G_ITEM_B_HEART_VRAM, 0x808BCDF0
.definelabel G_ITEM_B_HEART_DELTA, (G_ITEM_B_HEART_VRAM - G_ITEM_B_HEART_FILE)

; Overlay: En_Dekunuts :: Mad Scrub (Field Enemy)
.definelabel G_EN_DEKUNUTS_FILE, 0xD373D0
.definelabel G_EN_DEKUNUTS_VRAM, 0x808BD1E0
.definelabel G_EN_DEKUNUTS_DELTA, (G_EN_DEKUNUTS_VRAM - G_EN_DEKUNUTS_FILE)

; Overlay: En_Bbfall :: Red Bubble
.definelabel G_EN_BBFALL_FILE, 0xD39410
.definelabel G_EN_BBFALL_VRAM, 0x808BF220
.definelabel G_EN_BBFALL_DELTA, (G_EN_BBFALL_VRAM - G_EN_BBFALL_FILE)

; Overlay: Arms_Hook :: Hookshot
.definelabel G_ARMS_HOOK_FILE, 0xD3B220
.definelabel G_ARMS_HOOK_VRAM, 0x808C1030
.definelabel G_ARMS_HOOK_DELTA, (G_ARMS_HOOK_VRAM - G_ARMS_HOOK_FILE)

; Overlay: En_Bb :: Blue Bubble
.definelabel G_EN_BB_FILE, 0xD3BF30
.definelabel G_EN_BB_VRAM, 0x808C1D40
.definelabel G_EN_BB_DELTA, (G_EN_BB_VRAM - G_EN_BB_FILE)

; Overlay: Bg_Keikoku_Spr :: Fountain Water
.definelabel G_BG_KEIKOKU_SPR_FILE, 0xD3DC40
.definelabel G_BG_KEIKOKU_SPR_VRAM, 0x808C3A50
.definelabel G_BG_KEIKOKU_SPR_DELTA, (G_BG_KEIKOKU_SPR_VRAM - G_BG_KEIKOKU_SPR_FILE)

; Overlay: En_Wood02 :: Trees & Bushes
.definelabel G_EN_WOOD02_FILE, 0xD3DDF0
.definelabel G_EN_WOOD02_VRAM, 0x808C3C00
.definelabel G_EN_WOOD02_DELTA, (G_EN_WOOD02_VRAM - G_EN_WOOD02_FILE)

; Overlay: En_Death :: Gomess
.definelabel G_EN_DEATH_FILE, 0xD3F160
.definelabel G_EN_DEATH_VRAM, 0x808C4F80
.definelabel G_EN_DEATH_DELTA, (G_EN_DEATH_VRAM - G_EN_DEATH_FILE)

; Overlay: En_Minideath :: Gomess' Bats
.definelabel G_EN_MINIDEATH_FILE, 0xD44290
.definelabel G_EN_MINIDEATH_VRAM, 0x808CA0B0
.definelabel G_EN_MINIDEATH_DELTA, (G_EN_MINIDEATH_VRAM - G_EN_MINIDEATH_FILE)

; Overlay: En_Vm :: Beamos
.definelabel G_EN_VM_FILE, 0xD46430
.definelabel G_EN_VM_VRAM, 0x808CC260
.definelabel G_EN_VM_DELTA, (G_EN_VM_VRAM - G_EN_VM_FILE)

; Overlay: Demo_Effect
.definelabel G_DEMO_EFFECT_FILE, 0xD47910
.definelabel G_DEMO_EFFECT_VRAM, 0x808CD740
.definelabel G_DEMO_EFFECT_DELTA, (G_DEMO_EFFECT_VRAM - G_DEMO_EFFECT_FILE)

; Overlay: Demo_Kankyo :: Environment Effects
.definelabel G_DEMO_KANKYO_FILE, 0xD48620
.definelabel G_DEMO_KANKYO_VRAM, 0x808CE450
.definelabel G_DEMO_KANKYO_DELTA, (G_DEMO_KANKYO_VRAM - G_DEMO_KANKYO_FILE)

; Overlay: En_Floormas :: Floormaster
.definelabel G_EN_FLOORMAS_FILE, 0xD4A850
.definelabel G_EN_FLOORMAS_VRAM, 0x808D0680
.definelabel G_EN_FLOORMAS_DELTA, (G_EN_FLOORMAS_VRAM - G_EN_FLOORMAS_FILE)

; Overlay: En_Rd :: Redead
.definelabel G_EN_RD_FILE, 0xD4DFF0
.definelabel G_EN_RD_VRAM, 0x808D3E20
.definelabel G_EN_RD_DELTA, (G_EN_RD_VRAM - G_EN_RD_FILE)

; Overlay: Bg_F40_Flift :: Stone Tower Temple Elevator [Early]
.definelabel G_BG_F40_FLIFT_FILE, 0xD51720
.definelabel G_BG_F40_FLIFT_VRAM, 0x808D7550
.definelabel G_BG_F40_FLIFT_DELTA, (G_BG_F40_FLIFT_VRAM - G_BG_F40_FLIFT_FILE)

; Overlay: Obj_Mure :: Group of Bugs / Fish / Butterflies
.definelabel G_OBJ_MURE_FILE, 0xD51AA0
.definelabel G_OBJ_MURE_VRAM, 0x808D78D0
.definelabel G_OBJ_MURE_DELTA, (G_OBJ_MURE_VRAM - G_OBJ_MURE_FILE)

; Overlay: En_Sw :: Skullwalltula
.definelabel G_EN_SW_FILE, 0xD52B10
.definelabel G_EN_SW_VRAM, 0x808D8940
.definelabel G_EN_SW_DELTA, (G_EN_SW_VRAM - G_EN_SW_FILE)

; Overlay: Object_Kankyo
.definelabel G_OBJECT_KANKYO_FILE, 0xD56050
.definelabel G_OBJECT_KANKYO_VRAM, 0x808DBE80
.definelabel G_OBJECT_KANKYO_DELTA, (G_OBJECT_KANKYO_VRAM - G_OBJECT_KANKYO_FILE)

; Overlay: En_Horse_Link_Child :: Child Epona (Gameplay)
.definelabel G_EN_HORSE_LINK_CHILD_FILE, 0xD58780
.definelabel G_EN_HORSE_LINK_CHILD_VRAM, 0x808DE5C0
.definelabel G_EN_HORSE_LINK_CHILD_DELTA, (G_EN_HORSE_LINK_CHILD_VRAM - G_EN_HORSE_LINK_CHILD_FILE)

; Overlay: Door_Ana :: Grotto Hole
.definelabel G_DOOR_ANA_FILE, 0xD5A360
.definelabel G_DOOR_ANA_VRAM, 0x808E01A0
.definelabel G_DOOR_ANA_DELTA, (G_DOOR_ANA_VRAM - G_DOOR_ANA_FILE)

; Overlay: En_Encount1
.definelabel G_EN_ENCOUNT1_FILE, 0xD5A9F0
.definelabel G_EN_ENCOUNT1_VRAM, 0x808E0830
.definelabel G_EN_ENCOUNT1_DELTA, (G_EN_ENCOUNT1_VRAM - G_EN_ENCOUNT1_FILE)

; Overlay: Demo_Tre_Lgt :: Treasure Chest Glow
.definelabel G_DEMO_TRE_LGT_FILE, 0xD5B000
.definelabel G_DEMO_TRE_LGT_VRAM, 0x808E0E40
.definelabel G_DEMO_TRE_LGT_DELTA, (G_DEMO_TRE_LGT_VRAM - G_DEMO_TRE_LGT_FILE)

; Overlay: En_Encount2 :: Majora Balloon (Astral Observatory)
.definelabel G_EN_ENCOUNT2_FILE, 0xD5B720
.definelabel G_EN_ENCOUNT2_VRAM, 0x808E1560
.definelabel G_EN_ENCOUNT2_DELTA, (G_EN_ENCOUNT2_VRAM - G_EN_ENCOUNT2_FILE)

; Overlay: En_Fire_Rock :: Rock & Beam of Light [OoT]
.definelabel G_EN_FIRE_ROCK_FILE, 0xD5C1A0
.definelabel G_EN_FIRE_ROCK_VRAM, 0x808E1FE0
.definelabel G_EN_FIRE_ROCK_DELTA, (G_EN_FIRE_ROCK_VRAM - G_EN_FIRE_ROCK_FILE)

; Overlay: Bg_Ctower_Rot :: Clock Tower Helix Path
.definelabel G_BG_CTOWER_ROT_FILE, 0xD5C230
.definelabel G_BG_CTOWER_ROT_VRAM, 0x808E2070
.definelabel G_BG_CTOWER_ROT_DELTA, (G_BG_CTOWER_ROT_VRAM - G_BG_CTOWER_ROT_FILE)

; Overlay: Mir_Ray :: Reflectable Ray of Light [?]
.definelabel G_MIR_RAY_FILE, 0xD5C7C0
.definelabel G_MIR_RAY_VRAM, 0x808E2600
.definelabel G_MIR_RAY_DELTA, (G_MIR_RAY_VRAM - G_MIR_RAY_FILE)

; Overlay: En_Sb :: Shellblade
.definelabel G_EN_SB_FILE, 0xD5E0B0
.definelabel G_EN_SB_VRAM, 0x808E3EF0
.definelabel G_EN_SB_DELTA, (G_EN_SB_VRAM - G_EN_SB_FILE)

; Overlay: En_Bigslime :: Mad Jelly
.definelabel G_EN_BIGSLIME_FILE, 0xD5F180
.definelabel G_EN_BIGSLIME_VRAM, 0x808E4FC0
.definelabel G_EN_BIGSLIME_DELTA, (G_EN_BIGSLIME_VRAM - G_EN_BIGSLIME_FILE)

; Overlay: En_Karebaba :: Withered Deku Baba & Mini Baba
.definelabel G_EN_KAREBABA_FILE, 0xD6B3C0
.definelabel G_EN_KAREBABA_VRAM, 0x808F1200
.definelabel G_EN_KAREBABA_DELTA, (G_EN_KAREBABA_VRAM - G_EN_KAREBABA_FILE)

; Overlay: En_In :: Gorman Bros.
.definelabel G_EN_IN_FILE, 0xD6D270
.definelabel G_EN_IN_VRAM, 0x808F30B0
.definelabel G_EN_IN_DELTA, (G_EN_IN_VRAM - G_EN_IN_FILE)

; Overlay: En_Bom_Chu :: Bombchu
.definelabel G_EN_BOM_CHU_FILE, 0xD71670
.definelabel G_EN_BOM_CHU_VRAM, 0x808F74B0
.definelabel G_EN_BOM_CHU_DELTA, (G_EN_BOM_CHU_VRAM - G_EN_BOM_CHU_FILE)

; Overlay: En_Horse_Game_Check
.definelabel G_EN_HORSE_GAME_CHECK_FILE, 0xD72C60
.definelabel G_EN_HORSE_GAME_CHECK_VRAM, 0x808F8AA0
.definelabel G_EN_HORSE_GAME_CHECK_DELTA, (G_EN_HORSE_GAME_CHECK_VRAM - G_EN_HORSE_GAME_CHECK_FILE)

; Overlay: En_Rr :: Like Like
.definelabel G_EN_RR_FILE, 0xD73FC0
.definelabel G_EN_RR_VRAM, 0x808F9E00
.definelabel G_EN_RR_DELTA, (G_EN_RR_VRAM - G_EN_RR_FILE)

; Overlay: En_Fr
.definelabel G_EN_FR_FILE, 0xD76710
.definelabel G_EN_FR_VRAM, 0x808FC550
.definelabel G_EN_FR_DELTA, (G_EN_FR_VRAM - G_EN_FR_FILE)

; Overlay: En_Fishing :: Fishing Pond & Proprietor [OoT]
.definelabel G_EN_FISHING_FILE, 0xD76880
.definelabel G_EN_FISHING_VRAM, 0x808FC6C0
.definelabel G_EN_FISHING_DELTA, (G_EN_FISHING_VRAM - G_EN_FISHING_FILE)

; Overlay: Obj_Oshihiki :: Pushblock
.definelabel G_OBJ_OSHIHIKI_FILE, 0xD8A370
.definelabel G_OBJ_OSHIHIKI_VRAM, 0x80917290
.definelabel G_OBJ_OSHIHIKI_DELTA, (G_OBJ_OSHIHIKI_VRAM - G_OBJ_OSHIHIKI_FILE)

; Overlay: Eff_Dust :: Spin Attack Charge Particles
.definelabel G_EFF_DUST_FILE, 0xD8BC20
.definelabel G_EFF_DUST_VRAM, 0x80918B40
.definelabel G_EFF_DUST_DELTA, (G_EFF_DUST_VRAM - G_EFF_DUST_FILE)

; Overlay: Bg_Umajump :: Horse Jumping Fence
.definelabel G_BG_UMAJUMP_FILE, 0xD8D010
.definelabel G_BG_UMAJUMP_VRAM, 0x80919F30
.definelabel G_BG_UMAJUMP_DELTA, (G_BG_UMAJUMP_VRAM - G_BG_UMAJUMP_FILE)

; Overlay: En_Insect :: Bug
.definelabel G_EN_INSECT_FILE, 0xD8D980
.definelabel G_EN_INSECT_VRAM, 0x8091A8A0
.definelabel G_EN_INSECT_DELTA, (G_EN_INSECT_VRAM - G_EN_INSECT_FILE)

; Overlay: En_Butte :: Butterfly
.definelabel G_EN_BUTTE_FILE, 0xD8F180
.definelabel G_EN_BUTTE_VRAM, 0x8091C0A0
.definelabel G_EN_BUTTE_DELTA, (G_EN_BUTTE_VRAM - G_EN_BUTTE_FILE)

; Overlay: En_Fish :: Fish
.definelabel G_EN_FISH_FILE, 0xD90710
.definelabel G_EN_FISH_VRAM, 0x8091D630
.definelabel G_EN_FISH_DELTA, (G_EN_FISH_VRAM - G_EN_FISH_FILE)

; Overlay: Item_Etcetera
.definelabel G_ITEM_ETCETERA_FILE, 0xD92FD0
.definelabel G_ITEM_ETCETERA_VRAM, 0x8091FEF0
.definelabel G_ITEM_ETCETERA_DELTA, (G_ITEM_ETCETERA_VRAM - G_ITEM_ETCETERA_FILE)

; Overlay: Arrow_Fire :: Collectible Fire Arrows
.definelabel G_ARROW_FIRE_FILE, 0xD93420
.definelabel G_ARROW_FIRE_VRAM, 0x80920340
.definelabel G_ARROW_FIRE_DELTA, (G_ARROW_FIRE_VRAM - G_ARROW_FIRE_FILE)

; Overlay: Arrow_Ice :: Collectible Ice Arrows
.definelabel G_ARROW_ICE_FILE, 0xD95500
.definelabel G_ARROW_ICE_VRAM, 0x80922430
.definelabel G_ARROW_ICE_DELTA, (G_ARROW_ICE_VRAM - G_ARROW_ICE_FILE)

; Overlay: Arrow_Light :: Collectible Light Arrows
.definelabel G_ARROW_LIGHT_FILE, 0xD973C0
.definelabel G_ARROW_LIGHT_VRAM, 0x80924300
.definelabel G_ARROW_LIGHT_DELTA, (G_ARROW_LIGHT_VRAM - G_ARROW_LIGHT_FILE)

; Overlay: Obj_Kibako :: Wooden Crate (Small)
.definelabel G_OBJ_KIBAKO_FILE, 0xD99260
.definelabel G_OBJ_KIBAKO_VRAM, 0x809261B0
.definelabel G_OBJ_KIBAKO_DELTA, (G_OBJ_KIBAKO_VRAM - G_OBJ_KIBAKO_FILE)

; Overlay: Obj_Tsubo :: Pot
.definelabel G_OBJ_TSUBO_FILE, 0xD9A670
.definelabel G_OBJ_TSUBO_VRAM, 0x809275C0
.definelabel G_OBJ_TSUBO_DELTA, (G_OBJ_TSUBO_VRAM - G_OBJ_TSUBO_FILE)

; Overlay: En_Ik :: Iron Knuckle
.definelabel G_EN_IK_FILE, 0xD9C9C0
.definelabel G_EN_IK_VRAM, 0x80929910
.definelabel G_EN_IK_DELTA, (G_EN_IK_VRAM - G_EN_IK_FILE)

; Overlay: Demo_Shd
.definelabel G_DEMO_SHD_FILE, 0xD9F5E0
.definelabel G_DEMO_SHD_VRAM, 0x8092C530
.definelabel G_DEMO_SHD_DELTA, (G_DEMO_SHD_VRAM - G_DEMO_SHD_FILE)

; Overlay: En_Dns :: Deku Scrub Guard (Deku King's Chamber)
.definelabel G_EN_DNS_FILE, 0xD9F670
.definelabel G_EN_DNS_VRAM, 0x8092C5C0
.definelabel G_EN_DNS_DELTA, (G_EN_DNS_VRAM - G_EN_DNS_FILE)

; Overlay: Elf_Msg :: Tatl Text Trigger Spot
.definelabel G_ELF_MSG_FILE, 0xDA1040
.definelabel G_ELF_MSG_VRAM, 0x8092DF90
.definelabel G_ELF_MSG_DELTA, (G_ELF_MSG_VRAM - G_ELF_MSG_FILE)

; Overlay: En_Honotrap
.definelabel G_EN_HONOTRAP_FILE, 0xDA15C0
.definelabel G_EN_HONOTRAP_VRAM, 0x8092E510
.definelabel G_EN_HONOTRAP_DELTA, (G_EN_HONOTRAP_VRAM - G_EN_HONOTRAP_FILE)

; Overlay: En_Tubo_Trap :: Flying Pot
.definelabel G_EN_TUBO_TRAP_FILE, 0xDA3890
.definelabel G_EN_TUBO_TRAP_VRAM, 0x809307E0
.definelabel G_EN_TUBO_TRAP_DELTA, (G_EN_TUBO_TRAP_VRAM - G_EN_TUBO_TRAP_FILE)

; Overlay: Obj_Ice_Poly :: Ice Sparkle Effect
.definelabel G_OBJ_ICE_POLY_FILE, 0xDA4610
.definelabel G_OBJ_ICE_POLY_VRAM, 0x80931560
.definelabel G_OBJ_ICE_POLY_DELTA, (G_OBJ_ICE_POLY_VRAM - G_OBJ_ICE_POLY_FILE)

; Overlay: En_Fz :: Freezard
.definelabel G_EN_FZ_FILE, 0xDA5540
.definelabel G_EN_FZ_VRAM, 0x80932490
.definelabel G_EN_FZ_DELTA, (G_EN_FZ_VRAM - G_EN_FZ_FILE)

; Overlay: En_Kusa :: Grass
.definelabel G_EN_KUSA_FILE, 0xDA7A90
.definelabel G_EN_KUSA_VRAM, 0x809349E0
.definelabel G_EN_KUSA_DELTA, (G_EN_KUSA_VRAM - G_EN_KUSA_FILE)

; Overlay: Obj_Bean :: Magic Bean Plant
.definelabel G_OBJ_BEAN_FILE, 0xDA9B80
.definelabel G_OBJ_BEAN_VRAM, 0x80936CF0
.definelabel G_OBJ_BEAN_DELTA, (G_OBJ_BEAN_VRAM - G_OBJ_BEAN_FILE)

; Overlay: Obj_Bombiwa :: Bombable Rock
.definelabel G_OBJ_BOMBIWA_FILE, 0xDAC240
.definelabel G_OBJ_BOMBIWA_VRAM, 0x809393B0
.definelabel G_OBJ_BOMBIWA_DELTA, (G_OBJ_BOMBIWA_VRAM - G_OBJ_BOMBIWA_FILE)

; Overlay: Obj_Switch :: Switches
.definelabel G_OBJ_SWITCH_FILE, 0xDADA60
.definelabel G_OBJ_SWITCH_VRAM, 0x8093ABD0
.definelabel G_OBJ_SWITCH_DELTA, (G_OBJ_SWITCH_VRAM - G_OBJ_SWITCH_FILE)

; Overlay: Obj_Lift :: Dampé's House Elevator
.definelabel G_OBJ_LIFT_FILE, 0xDB0240
.definelabel G_OBJ_LIFT_VRAM, 0x8093D3C0
.definelabel G_OBJ_LIFT_DELTA, (G_OBJ_LIFT_VRAM - G_OBJ_LIFT_FILE)

; Overlay: Obj_Hsblock :: Stone Hookshot Pillar
.definelabel G_OBJ_HSBLOCK_FILE, 0xDB0D20
.definelabel G_OBJ_HSBLOCK_VRAM, 0x8093DEA0
.definelabel G_OBJ_HSBLOCK_DELTA, (G_OBJ_HSBLOCK_VRAM - G_OBJ_HSBLOCK_FILE)

; Overlay: En_Okarina_Tag :: Ocarina Song Spot
.definelabel G_EN_OKARINA_TAG_FILE, 0xDB12A0
.definelabel G_EN_OKARINA_TAG_VRAM, 0x8093E420
.definelabel G_EN_OKARINA_TAG_DELTA, (G_EN_OKARINA_TAG_VRAM - G_EN_OKARINA_TAG_FILE)

; Overlay: En_Goroiwa :: Snowball & Rolling Boulder [?]
.definelabel G_EN_GOROIWA_FILE, 0xDB1720
.definelabel G_EN_GOROIWA_VRAM, 0x8093E8A0
.definelabel G_EN_GOROIWA_DELTA, (G_EN_GOROIWA_VRAM - G_EN_GOROIWA_FILE)

; Overlay: En_Daiku :: Carpenter (Clock Town)
.definelabel G_EN_DAIKU_FILE, 0xDB6330
.definelabel G_EN_DAIKU_VRAM, 0x809434B0
.definelabel G_EN_DAIKU_DELTA, (G_EN_DAIKU_VRAM - G_EN_DAIKU_FILE)

; Overlay: En_Nwc :: Cucco Chick
.definelabel G_EN_NWC_FILE, 0xDB7060
.definelabel G_EN_NWC_VRAM, 0x809441E0
.definelabel G_EN_NWC_DELTA, (G_EN_NWC_VRAM - G_EN_NWC_FILE)

; Overlay: Item_Inbox
.definelabel G_ITEM_INBOX_FILE, 0xDB8370
.definelabel G_ITEM_INBOX_VRAM, 0x809454F0
.definelabel G_ITEM_INBOX_DELTA, (G_ITEM_INBOX_VRAM - G_ITEM_INBOX_FILE)

; Overlay: En_Ge1 :: Pirate Lieutenant
.definelabel G_EN_GE1_FILE, 0xDB84D0
.definelabel G_EN_GE1_VRAM, 0x80945650
.definelabel G_EN_GE1_DELTA, (G_EN_GE1_VRAM - G_EN_GE1_FILE)

; Overlay: Obj_Blockstop
.definelabel G_OBJ_BLOCKSTOP_FILE, 0xDB9520
.definelabel G_OBJ_BLOCKSTOP_VRAM, 0x809466A0
.definelabel G_OBJ_BLOCKSTOP_DELTA, (G_OBJ_BLOCKSTOP_VRAM - G_OBJ_BLOCKSTOP_FILE)

; Overlay: En_Sda :: Dynamic Shadow
.definelabel G_EN_SDA_FILE, 0xDB9750
.definelabel G_EN_SDA_VRAM, 0x809468D0
.definelabel G_EN_SDA_DELTA, (G_EN_SDA_VRAM - G_EN_SDA_FILE)

; Overlay: En_Clear_Tag
.definelabel G_EN_CLEAR_TAG_FILE, 0xDBAD20
.definelabel G_EN_CLEAR_TAG_VRAM, 0x80947F60
.definelabel G_EN_CLEAR_TAG_DELTA, (G_EN_CLEAR_TAG_VRAM - G_EN_CLEAR_TAG_FILE)

; Overlay: En_Gm :: Gorman
.definelabel G_EN_GM_FILE, 0xDC0CA0
.definelabel G_EN_GM_VRAM, 0x8094DEE0
.definelabel G_EN_GM_DELTA, (G_EN_GM_VRAM - G_EN_GM_FILE)

; Overlay: En_Ms :: Magic Bean Seller
.definelabel G_EN_MS_FILE, 0xDC53E0
.definelabel G_EN_MS_VRAM, 0x80952620
.definelabel G_EN_MS_DELTA, (G_EN_MS_VRAM - G_EN_MS_FILE)

; Overlay: En_Hs :: Grog
.definelabel G_EN_HS_FILE, 0xDC5A10
.definelabel G_EN_HS_VRAM, 0x80952C50
.definelabel G_EN_HS_DELTA, (G_EN_HS_VRAM - G_EN_HS_FILE)

; Overlay: Bg_Ingate :: Boat Cruise Canoe
.definelabel G_BG_INGATE_FILE, 0xDC6850
.definelabel G_BG_INGATE_VRAM, 0x80953A90
.definelabel G_BG_INGATE_DELTA, (G_BG_INGATE_VRAM - G_BG_INGATE_FILE)

; Overlay: En_Kanban :: Square Sign
.definelabel G_EN_KANBAN_FILE, 0xDC7720
.definelabel G_EN_KANBAN_VRAM, 0x80954960
.definelabel G_EN_KANBAN_DELTA, (G_EN_KANBAN_VRAM - G_EN_KANBAN_FILE)

; Overlay: En_Attack_Niw :: Cucco (Attacking)
.definelabel G_EN_ATTACK_NIW_FILE, 0xDCAE80
.definelabel G_EN_ATTACK_NIW_VRAM, 0x809580C0
.definelabel G_EN_ATTACK_NIW_DELTA, (G_EN_ATTACK_NIW_VRAM - G_EN_ATTACK_NIW_FILE)

; Overlay: En_Mk :: Marine Scientist
.definelabel G_EN_MK_FILE, 0xDCC0A0
.definelabel G_EN_MK_VRAM, 0x809592E0
.definelabel G_EN_MK_DELTA, (G_EN_MK_VRAM - G_EN_MK_FILE)

; Overlay: En_Owl :: Owl
.definelabel G_EN_OWL_FILE, 0xDCD2D0
.definelabel G_EN_OWL_VRAM, 0x8095A510
.definelabel G_EN_OWL_DELTA, (G_EN_OWL_VRAM - G_EN_OWL_FILE)

; Overlay: En_Ishi :: Rock
.definelabel G_EN_ISHI_FILE, 0xDD04A0
.definelabel G_EN_ISHI_VRAM, 0x8095D6E0
.definelabel G_EN_ISHI_DELTA, (G_EN_ISHI_VRAM - G_EN_ISHI_FILE)

; Overlay: Obj_Hana :: Orange Graveyard Flower
.definelabel G_OBJ_HANA_FILE, 0xDD28D0
.definelabel G_OBJ_HANA_VRAM, 0x8095FB10
.definelabel G_OBJ_HANA_DELTA, (G_OBJ_HANA_VRAM - G_OBJ_HANA_FILE)

; Overlay: Obj_Lightswitch :: Sun Switch
.definelabel G_OBJ_LIGHTSWITCH_FILE, 0xDD29B0
.definelabel G_OBJ_LIGHTSWITCH_VRAM, 0x8095FBF0
.definelabel G_OBJ_LIGHTSWITCH_DELTA, (G_OBJ_LIGHTSWITCH_VRAM - G_OBJ_LIGHTSWITCH_FILE)

; Overlay: Obj_Mure2 :: Grass & Rock Cluster
.definelabel G_OBJ_MURE2_FILE, 0xDD3AB0
.definelabel G_OBJ_MURE2_VRAM, 0x80960CF0
.definelabel G_OBJ_MURE2_DELTA, (G_OBJ_MURE2_VRAM - G_OBJ_MURE2_FILE)

; Overlay: En_Fu :: Honey & Darling (Gameplay)
.definelabel G_EN_FU_FILE, 0xDD44A0
.definelabel G_EN_FU_VRAM, 0x809616E0
.definelabel G_EN_FU_DELTA, (G_EN_FU_VRAM - G_EN_FU_FILE)

; Overlay: En_Stream :: Water Spout
.definelabel G_EN_STREAM_FILE, 0xDD8410
.definelabel G_EN_STREAM_VRAM, 0x80965650
.definelabel G_EN_STREAM_DELTA, (G_EN_STREAM_VRAM - G_EN_STREAM_FILE)

; Overlay: En_Mm :: Rock Sirloin
.definelabel G_EN_MM_FILE, 0xDD8970
.definelabel G_EN_MM_VRAM, 0x80965BB0
.definelabel G_EN_MM_DELTA, (G_EN_MM_VRAM - G_EN_MM_FILE)

; Overlay: En_Weather_Tag
.definelabel G_EN_WEATHER_TAG_FILE, 0xDD91D0
.definelabel G_EN_WEATHER_TAG_VRAM, 0x80966410
.definelabel G_EN_WEATHER_TAG_DELTA, (G_EN_WEATHER_TAG_VRAM - G_EN_WEATHER_TAG_FILE)

; Overlay: En_Ani :: Part-Timer
.definelabel G_EN_ANI_FILE, 0xDDA790
.definelabel G_EN_ANI_VRAM, 0x809679D0
.definelabel G_EN_ANI_DELTA, (G_EN_ANI_VRAM - G_EN_ANI_FILE)

; Overlay: En_Js :: Moon Child
.definelabel G_EN_JS_FILE, 0xDDB570
.definelabel G_EN_JS_VRAM, 0x809687B0
.definelabel G_EN_JS_DELTA, (G_EN_JS_VRAM - G_EN_JS_FILE)

; Overlay: En_Okarina_Effect :: Song of Storms Effect I [?]
.definelabel G_EN_OKARINA_EFFECT_FILE, 0xDDDE60
.definelabel G_EN_OKARINA_EFFECT_VRAM, 0x8096B0A0
.definelabel G_EN_OKARINA_EFFECT_DELTA, (G_EN_OKARINA_EFFECT_VRAM - G_EN_OKARINA_EFFECT_FILE)

; Overlay: En_Mag :: Title Logo
.definelabel G_EN_MAG_FILE, 0xDDE0D0
.definelabel G_EN_MAG_VRAM, 0x8096B310
.definelabel G_EN_MAG_DELTA, (G_EN_MAG_VRAM - G_EN_MAG_FILE)

; Overlay: Elf_Msg2 :: Green Tatl Target Spot
.definelabel G_ELF_MSG2_FILE, 0xDE1A00
.definelabel G_ELF_MSG2_VRAM, 0x8096EC40
.definelabel G_ELF_MSG2_DELTA, (G_ELF_MSG2_VRAM - G_ELF_MSG2_FILE)

; Overlay: Bg_F40_Swlift :: Stone Tower Temple Platform [Early]
.definelabel G_BG_F40_SWLIFT_FILE, 0xDE1F20
.definelabel G_BG_F40_SWLIFT_VRAM, 0x8096F160
.definelabel G_BG_F40_SWLIFT_DELTA, (G_BG_F40_SWLIFT_VRAM - G_BG_F40_SWLIFT_FILE)

; Overlay: En_Kakasi :: Scarecrow
.definelabel G_EN_KAKASI_FILE, 0xDE2390
.definelabel G_EN_KAKASI_VRAM, 0x8096F5E0
.definelabel G_EN_KAKASI_DELTA, (G_EN_KAKASI_VRAM - G_EN_KAKASI_FILE)

; Overlay: Obj_Makeoshihiki
.definelabel G_OBJ_MAKEOSHIHIKI_FILE, 0xDE5100
.definelabel G_OBJ_MAKEOSHIHIKI_VRAM, 0x80972350
.definelabel G_OBJ_MAKEOSHIHIKI_DELTA, (G_OBJ_MAKEOSHIHIKI_VRAM - G_OBJ_MAKEOSHIHIKI_FILE)

; Overlay: Oceff_Spot :: Sun's Song Effect
.definelabel G_OCEFF_SPOT_FILE, 0xDE5430
.definelabel G_OCEFF_SPOT_VRAM, 0x80972680
.definelabel G_OCEFF_SPOT_DELTA, (G_OCEFF_SPOT_VRAM - G_OCEFF_SPOT_FILE)

; Overlay: En_Torch :: Treasure Chest (Grotto)
.definelabel G_EN_TORCH_FILE, 0xDE6300
.definelabel G_EN_TORCH_VRAM, 0x80973550
.definelabel G_EN_TORCH_DELTA, (G_EN_TORCH_VRAM - G_EN_TORCH_FILE)

; Overlay: Shot_Sun
.definelabel G_SHOT_SUN_FILE, 0xDE63F0
.definelabel G_SHOT_SUN_VRAM, 0x80973640
.definelabel G_SHOT_SUN_DELTA, (G_SHOT_SUN_VRAM - G_SHOT_SUN_FILE)

; Overlay: Obj_Roomtimer
.definelabel G_OBJ_ROOMTIMER_FILE, 0xDE6A00
.definelabel G_OBJ_ROOMTIMER_VRAM, 0x80973C50
.definelabel G_OBJ_ROOMTIMER_DELTA, (G_OBJ_ROOMTIMER_VRAM - G_OBJ_ROOMTIMER_FILE)

; Overlay: En_Ssh :: Cursed Skulltula Man
.definelabel G_EN_SSH_FILE, 0xDE6CA0
.definelabel G_EN_SSH_VRAM, 0x80973EF0
.definelabel G_EN_SSH_DELTA, (G_EN_SSH_VRAM - G_EN_SSH_FILE)

; Overlay: Oceff_Wipe :: Song of Time Effect
.definelabel G_OCEFF_WIPE_FILE, 0xDE9260
.definelabel G_OCEFF_WIPE_VRAM, 0x809764B0
.definelabel G_OCEFF_WIPE_DELTA, (G_OCEFF_WIPE_VRAM - G_OCEFF_WIPE_FILE)

; Overlay: Effect_Ss_Dust :: Particle - [Unknown]
.definelabel G_EFFECT_SS_DUST_FILE, 0xDE9FB0
.definelabel G_EFFECT_SS_DUST_VRAM, 0x80977210
.definelabel G_EFFECT_SS_DUST_DELTA, (G_EFFECT_SS_DUST_VRAM - G_EFFECT_SS_DUST_FILE)

; Overlay: Effect_Ss_Kirakira :: Particle - [Unknown]
.definelabel G_EFFECT_SS_KIRAKIRA_FILE, 0xDEA7A0
.definelabel G_EFFECT_SS_KIRAKIRA_VRAM, 0x80977A00
.definelabel G_EFFECT_SS_KIRAKIRA_DELTA, (G_EFFECT_SS_KIRAKIRA_VRAM - G_EFFECT_SS_KIRAKIRA_FILE)

; Overlay: Effect_Ss_Bomb2 :: Particle - [Unknown]
.definelabel G_EFFECT_SS_BOMB2_FILE, 0xDEAE10
.definelabel G_EFFECT_SS_BOMB2_VRAM, 0x80978070
.definelabel G_EFFECT_SS_BOMB2_DELTA, (G_EFFECT_SS_BOMB2_VRAM - G_EFFECT_SS_BOMB2_FILE)

; Overlay: Effect_Ss_Blast :: Particle - [Unknown]
.definelabel G_EFFECT_SS_BLAST_FILE, 0xDEB670
.definelabel G_EFFECT_SS_BLAST_VRAM, 0x809788D0
.definelabel G_EFFECT_SS_BLAST_DELTA, (G_EFFECT_SS_BLAST_VRAM - G_EFFECT_SS_BLAST_FILE)

; Overlay: Effect_Ss_G_Spk :: Particle - [Unknown]
.definelabel G_EFFECT_SS_G_SPK_FILE, 0xDEB9D0
.definelabel G_EFFECT_SS_G_SPK_VRAM, 0x80978C30
.definelabel G_EFFECT_SS_G_SPK_DELTA, (G_EFFECT_SS_G_SPK_VRAM - G_EFFECT_SS_G_SPK_FILE)

; Overlay: Effect_Ss_D_Fire :: Particle - [Unknown]
.definelabel G_EFFECT_SS_D_FIRE_FILE, 0xDEBF50
.definelabel G_EFFECT_SS_D_FIRE_VRAM, 0x809791B0
.definelabel G_EFFECT_SS_D_FIRE_DELTA, (G_EFFECT_SS_D_FIRE_VRAM - G_EFFECT_SS_D_FIRE_FILE)

; Overlay: Effect_Ss_Bubble :: Particle - [Unknown]
.definelabel G_EFFECT_SS_BUBBLE_FILE, 0xDEC360
.definelabel G_EFFECT_SS_BUBBLE_VRAM, 0x809795C0
.definelabel G_EFFECT_SS_BUBBLE_DELTA, (G_EFFECT_SS_BUBBLE_VRAM - G_EFFECT_SS_BUBBLE_FILE)

; Overlay: Effect_Ss_G_Ripple :: Particle - [Unknown]
.definelabel G_EFFECT_SS_G_RIPPLE_FILE, 0xDEC8D0
.definelabel G_EFFECT_SS_G_RIPPLE_VRAM, 0x80979B30
.definelabel G_EFFECT_SS_G_RIPPLE_DELTA, (G_EFFECT_SS_G_RIPPLE_VRAM - G_EFFECT_SS_G_RIPPLE_FILE)

; Overlay: Effect_Ss_G_Splash :: Particle - [Unknown]
.definelabel G_EFFECT_SS_G_SPLASH_FILE, 0xDECDF0
.definelabel G_EFFECT_SS_G_SPLASH_VRAM, 0x8097A050
.definelabel G_EFFECT_SS_G_SPLASH_DELTA, (G_EFFECT_SS_G_SPLASH_VRAM - G_EFFECT_SS_G_SPLASH_FILE)

; Overlay: Effect_Ss_G_Fire :: Particle - [Unknown]
.definelabel G_EFFECT_SS_G_FIRE_FILE, 0xDED210
.definelabel G_EFFECT_SS_G_FIRE_VRAM, 0x8097A470
.definelabel G_EFFECT_SS_G_FIRE_DELTA, (G_EFFECT_SS_G_FIRE_VRAM - G_EFFECT_SS_G_FIRE_FILE)

; Overlay: Effect_Ss_Lightning :: Particle - [Unknown]
.definelabel G_EFFECT_SS_LIGHTNING_FILE, 0xDED420
.definelabel G_EFFECT_SS_LIGHTNING_VRAM, 0x8097A680
.definelabel G_EFFECT_SS_LIGHTNING_DELTA, (G_EFFECT_SS_LIGHTNING_VRAM - G_EFFECT_SS_LIGHTNING_FILE)

; Overlay: Effect_Ss_Dt_Bubble :: Particle - [Unknown]
.definelabel G_EFFECT_SS_DT_BUBBLE_FILE, 0xDEDB00
.definelabel G_EFFECT_SS_DT_BUBBLE_VRAM, 0x8097AD60
.definelabel G_EFFECT_SS_DT_BUBBLE_DELTA, (G_EFFECT_SS_DT_BUBBLE_VRAM - G_EFFECT_SS_DT_BUBBLE_FILE)

; Overlay: Effect_Ss_Hahen :: Particle - [Unknown]
.definelabel G_EFFECT_SS_HAHEN_FILE, 0xDEE010
.definelabel G_EFFECT_SS_HAHEN_VRAM, 0x8097B270
.definelabel G_EFFECT_SS_HAHEN_DELTA, (G_EFFECT_SS_HAHEN_VRAM - G_EFFECT_SS_HAHEN_FILE)

; Overlay: Effect_Ss_Stick :: Particle - [Unknown]
.definelabel G_EFFECT_SS_STICK_FILE, 0xDEE5B0
.definelabel G_EFFECT_SS_STICK_VRAM, 0x8097B810
.definelabel G_EFFECT_SS_STICK_DELTA, (G_EFFECT_SS_STICK_VRAM - G_EFFECT_SS_STICK_FILE)

; Overlay: Effect_Ss_Sibuki :: Particle - [Unknown]
.definelabel G_EFFECT_SS_SIBUKI_FILE, 0xDEE870
.definelabel G_EFFECT_SS_SIBUKI_VRAM, 0x8097BAD0
.definelabel G_EFFECT_SS_SIBUKI_DELTA, (G_EFFECT_SS_SIBUKI_VRAM - G_EFFECT_SS_SIBUKI_FILE)

; Overlay: Effect_Ss_Stone1 :: Particle - [Unknown]
.definelabel G_EFFECT_SS_STONE1_FILE, 0xDEEED0
.definelabel G_EFFECT_SS_STONE1_VRAM, 0x8097C130
.definelabel G_EFFECT_SS_STONE1_DELTA, (G_EFFECT_SS_STONE1_VRAM - G_EFFECT_SS_STONE1_FILE)

; Overlay: Effect_Ss_Hitmark :: Particle - [Unknown]
.definelabel G_EFFECT_SS_HITMARK_FILE, 0xDEF230
.definelabel G_EFFECT_SS_HITMARK_VRAM, 0x8097C490
.definelabel G_EFFECT_SS_HITMARK_DELTA, (G_EFFECT_SS_HITMARK_VRAM - G_EFFECT_SS_HITMARK_FILE)

; Overlay: Effect_Ss_Fhg_Flash :: Particle - [Unknown]
.definelabel G_EFFECT_SS_FHG_FLASH_FILE, 0xDEF730
.definelabel G_EFFECT_SS_FHG_FLASH_VRAM, 0x8097C990
.definelabel G_EFFECT_SS_FHG_FLASH_DELTA, (G_EFFECT_SS_FHG_FLASH_VRAM - G_EFFECT_SS_FHG_FLASH_FILE)

; Overlay: Effect_Ss_K_Fire :: Particle - [Unknown]
.definelabel G_EFFECT_SS_K_FIRE_FILE, 0xDF01B0
.definelabel G_EFFECT_SS_K_FIRE_VRAM, 0x8097D410
.definelabel G_EFFECT_SS_K_FIRE_DELTA, (G_EFFECT_SS_K_FIRE_VRAM - G_EFFECT_SS_K_FIRE_FILE)

; Overlay: Effect_Ss_Solder_Srch_Ball :: Particle - [Unknown]
.definelabel G_EFFECT_SS_SOLDER_SRCH_BALL_FILE, 0xDF05F0
.definelabel G_EFFECT_SS_SOLDER_SRCH_BALL_VRAM, 0x8097D850
.definelabel G_EFFECT_SS_SOLDER_SRCH_BALL_DELTA, (G_EFFECT_SS_SOLDER_SRCH_BALL_VRAM - G_EFFECT_SS_SOLDER_SRCH_BALL_FILE)

; Overlay: Effect_Ss_Kakera :: Particle - [Unknown]
.definelabel G_EFFECT_SS_KAKERA_FILE, 0xDF0A40
.definelabel G_EFFECT_SS_KAKERA_VRAM, 0x8097DCA0
.definelabel G_EFFECT_SS_KAKERA_DELTA, (G_EFFECT_SS_KAKERA_VRAM - G_EFFECT_SS_KAKERA_FILE)

; Overlay: Effect_Ss_Ice_Piece :: Particle - [Unknown]
.definelabel G_EFFECT_SS_ICE_PIECE_FILE, 0xDF1A70
.definelabel G_EFFECT_SS_ICE_PIECE_VRAM, 0x8097ECD0
.definelabel G_EFFECT_SS_ICE_PIECE_DELTA, (G_EFFECT_SS_ICE_PIECE_VRAM - G_EFFECT_SS_ICE_PIECE_FILE)

; Overlay: Effect_Ss_En_Ice :: Particle - [Unknown]
.definelabel G_EFFECT_SS_EN_ICE_FILE, 0xDF1E70
.definelabel G_EFFECT_SS_EN_ICE_VRAM, 0x8097F0D0
.definelabel G_EFFECT_SS_EN_ICE_DELTA, (G_EFFECT_SS_EN_ICE_VRAM - G_EFFECT_SS_EN_ICE_FILE)

; Overlay: Effect_Ss_Fire_Tail :: Particle - [Unknown]
.definelabel G_EFFECT_SS_FIRE_TAIL_FILE, 0xDF2620
.definelabel G_EFFECT_SS_FIRE_TAIL_VRAM, 0x8097F880
.definelabel G_EFFECT_SS_FIRE_TAIL_DELTA, (G_EFFECT_SS_FIRE_TAIL_VRAM - G_EFFECT_SS_FIRE_TAIL_FILE)

; Overlay: Effect_Ss_En_Fire :: Particle - [Unknown]
.definelabel G_EFFECT_SS_EN_FIRE_FILE, 0xDF2D00
.definelabel G_EFFECT_SS_EN_FIRE_VRAM, 0x8097FF60
.definelabel G_EFFECT_SS_EN_FIRE_DELTA, (G_EFFECT_SS_EN_FIRE_VRAM - G_EFFECT_SS_EN_FIRE_FILE)

; Overlay: Effect_Ss_Extra :: Particle - [Unknown]
.definelabel G_EFFECT_SS_EXTRA_FILE, 0xDF3370
.definelabel G_EFFECT_SS_EXTRA_VRAM, 0x809805D0
.definelabel G_EFFECT_SS_EXTRA_DELTA, (G_EFFECT_SS_EXTRA_VRAM - G_EFFECT_SS_EXTRA_FILE)

; Overlay: Effect_Ss_Dead_Db :: Particle - [Unknown]
.definelabel G_EFFECT_SS_DEAD_DB_FILE, 0xDF36E0
.definelabel G_EFFECT_SS_DEAD_DB_VRAM, 0x80980940
.definelabel G_EFFECT_SS_DEAD_DB_DELTA, (G_EFFECT_SS_DEAD_DB_VRAM - G_EFFECT_SS_DEAD_DB_FILE)

; Overlay: Effect_Ss_Dead_Dd :: Particle - [Unknown]
.definelabel G_EFFECT_SS_DEAD_DD_FILE, 0xDF3AF0
.definelabel G_EFFECT_SS_DEAD_DD_VRAM, 0x80980D50
.definelabel G_EFFECT_SS_DEAD_DD_DELTA, (G_EFFECT_SS_DEAD_DD_VRAM - G_EFFECT_SS_DEAD_DD_FILE)

; Overlay: Effect_Ss_Dead_Ds :: Particle - [Unknown]
.definelabel G_EFFECT_SS_DEAD_DS_FILE, 0xDF4080
.definelabel G_EFFECT_SS_DEAD_DS_VRAM, 0x809812E0
.definelabel G_EFFECT_SS_DEAD_DS_DELTA, (G_EFFECT_SS_DEAD_DS_VRAM - G_EFFECT_SS_DEAD_DS_FILE)

; Overlay: Oceff_Storm :: Song of Storms Effect II [?]
.definelabel G_OCEFF_STORM_FILE, 0xDF4500
.definelabel G_OCEFF_STORM_VRAM, 0x80981760
.definelabel G_OCEFF_STORM_DELTA, (G_OCEFF_STORM_VRAM - G_OCEFF_STORM_FILE)

; Overlay: Obj_Demo
.definelabel G_OBJ_DEMO_FILE, 0xDF62C0
.definelabel G_OBJ_DEMO_VRAM, 0x80983520
.definelabel G_OBJ_DEMO_DELTA, (G_OBJ_DEMO_VRAM - G_OBJ_DEMO_FILE)

; Overlay: En_Minislime :: Jelly Droplets
.definelabel G_EN_MINISLIME_FILE, 0xDF6690
.definelabel G_EN_MINISLIME_VRAM, 0x809838F0
.definelabel G_EN_MINISLIME_DELTA, (G_EN_MINISLIME_VRAM - G_EN_MINISLIME_FILE)

; Overlay: En_Nutsball :: Deku Nut Projectile
.definelabel G_EN_NUTSBALL_FILE, 0xDF89E0
.definelabel G_EN_NUTSBALL_VRAM, 0x80985C40
.definelabel G_EN_NUTSBALL_DELTA, (G_EN_NUTSBALL_VRAM - G_EN_NUTSBALL_FILE)

; Overlay: Oceff_Wipe2 :: Epona's Song Effect
.definelabel G_OCEFF_WIPE2_FILE, 0xDF9010
.definelabel G_OCEFF_WIPE2_VRAM, 0x80986270
.definelabel G_OCEFF_WIPE2_DELTA, (G_OCEFF_WIPE2_VRAM - G_OCEFF_WIPE2_FILE)

; Overlay: Oceff_Wipe3 :: Saria's Song Effect
.definelabel G_OCEFF_WIPE3_FILE, 0xDFA770
.definelabel G_OCEFF_WIPE3_VRAM, 0x809879E0
.definelabel G_OCEFF_WIPE3_DELTA, (G_OCEFF_WIPE3_VRAM - G_OCEFF_WIPE3_FILE)

; Overlay: En_Dg :: Dog (Generic)
.definelabel G_EN_DG_FILE, 0xDFBEC0
.definelabel G_EN_DG_VRAM, 0x80989140
.definelabel G_EN_DG_DELTA, (G_EN_DG_VRAM - G_EN_DG_FILE)

; Overlay: En_Si :: Gold Skulltula Token
.definelabel G_EN_SI_FILE, 0xDFF7A0
.definelabel G_EN_SI_VRAM, 0x8098CA20
.definelabel G_EN_SI_DELTA, (G_EN_SI_VRAM - G_EN_SI_FILE)

; Overlay: Obj_Comb :: Beehive
.definelabel G_OBJ_COMB_FILE, 0xDFFBC0
.definelabel G_OBJ_COMB_VRAM, 0x8098CE40
.definelabel G_OBJ_COMB_DELTA, (G_OBJ_COMB_VRAM - G_OBJ_COMB_FILE)

; Overlay: Obj_Kibako2 :: Wooden Crate (Large)
.definelabel G_OBJ_KIBAKO2_FILE, 0xE01340
.definelabel G_OBJ_KIBAKO2_VRAM, 0x8098E5C0
.definelabel G_OBJ_KIBAKO2_DELTA, (G_OBJ_KIBAKO2_VRAM - G_OBJ_KIBAKO2_FILE)

; Overlay: En_Hs2
.definelabel G_EN_HS2_FILE, 0xE01CE0
.definelabel G_EN_HS2_VRAM, 0x8098EF60
.definelabel G_EN_HS2_DELTA, (G_EN_HS2_VRAM - G_EN_HS2_FILE)

; Overlay: Obj_Mure3 :: Rupee Cluster
.definelabel G_OBJ_MURE3_FILE, 0xE01DC0
.definelabel G_OBJ_MURE3_VRAM, 0x8098F040
.definelabel G_OBJ_MURE3_DELTA, (G_OBJ_MURE3_VRAM - G_OBJ_MURE3_FILE)

; Overlay: En_Tg :: Honey & Darling (Cutscenes)
.definelabel G_EN_TG_FILE, 0xE02580
.definelabel G_EN_TG_VRAM, 0x8098F800
.definelabel G_EN_TG_DELTA, (G_EN_TG_VRAM - G_EN_TG_FILE)

; Overlay: En_Wf :: White Wolfos & Wolfos (Field Enemy)
.definelabel G_EN_WF_FILE, 0xE03090
.definelabel G_EN_WF_VRAM, 0x80990310
.definelabel G_EN_WF_DELTA, (G_EN_WF_VRAM - G_EN_WF_FILE)

; Overlay: En_Skb :: Stalchild (Generic)
.definelabel G_EN_SKB_FILE, 0xE07530
.definelabel G_EN_SKB_VRAM, 0x809947B0
.definelabel G_EN_SKB_DELTA, (G_EN_SKB_VRAM - G_EN_SKB_FILE)

; Overlay: En_Gs :: Gossip Stone
.definelabel G_EN_GS_FILE, 0xE0A810
.definelabel G_EN_GS_VRAM, 0x80997A90
.definelabel G_EN_GS_DELTA, (G_EN_GS_VRAM - G_EN_GS_FILE)

; Overlay: Obj_Sound :: Sound Effects II
.definelabel G_OBJ_SOUND_FILE, 0xE0D6A0
.definelabel G_OBJ_SOUND_VRAM, 0x8099A920
.definelabel G_OBJ_SOUND_DELTA, (G_OBJ_SOUND_VRAM - G_OBJ_SOUND_FILE)

; Overlay: En_Crow :: Guay (Field Enemy)
.definelabel G_EN_CROW_FILE, 0xE0D8B0
.definelabel G_EN_CROW_VRAM, 0x8099AB30
.definelabel G_EN_CROW_DELTA, (G_EN_CROW_VRAM - G_EN_CROW_FILE)

; Overlay: En_Cow :: Cow
.definelabel G_EN_COW_FILE, 0xE0F010
.definelabel G_EN_COW_VRAM, 0x8099C290
.definelabel G_EN_COW_DELTA, (G_EN_COW_VRAM - G_EN_COW_FILE)

; Overlay: Oceff_Wipe4 :: Scarecrow's Song Effect
.definelabel G_OCEFF_WIPE4_FILE, 0xE10500
.definelabel G_OCEFF_WIPE4_VRAM, 0x8099D780
.definelabel G_OCEFF_WIPE4_DELTA, (G_OCEFF_WIPE4_VRAM - G_OCEFF_WIPE4_FILE)

; Overlay: En_Zo :: Zora [Early]
.definelabel G_EN_ZO_FILE, 0xE11500
.definelabel G_EN_ZO_VRAM, 0x8099E790
.definelabel G_EN_ZO_DELTA, (G_EN_ZO_VRAM - G_EN_ZO_FILE)

; Overlay: Effect_Ss_Ice_Smoke :: Particle - [Unknown]
.definelabel G_EFFECT_SS_ICE_SMOKE_FILE, 0xE124A0
.definelabel G_EFFECT_SS_ICE_SMOKE_VRAM, 0x8099F730
.definelabel G_EFFECT_SS_ICE_SMOKE_DELTA, (G_EFFECT_SS_ICE_SMOKE_VRAM - G_EFFECT_SS_ICE_SMOKE_FILE)

; Overlay: Obj_Makekinsuta
.definelabel G_OBJ_MAKEKINSUTA_FILE, 0xE127B0
.definelabel G_OBJ_MAKEKINSUTA_VRAM, 0x8099FA40
.definelabel G_OBJ_MAKEKINSUTA_DELTA, (G_OBJ_MAKEKINSUTA_VRAM - G_OBJ_MAKEKINSUTA_FILE)

; Overlay: En_Ge3 :: Aveil
.definelabel G_EN_GE3_FILE, 0xE12C20
.definelabel G_EN_GE3_VRAM, 0x8099FEB0
.definelabel G_EN_GE3_DELTA, (G_EN_GE3_VRAM - G_EN_GE3_FILE)

; Overlay: Obj_Hamishi :: Bronze Boulder
.definelabel G_OBJ_HAMISHI_FILE, 0xE13C90
.definelabel G_OBJ_HAMISHI_VRAM, 0x809A0F20
.definelabel G_OBJ_HAMISHI_DELTA, (G_OBJ_HAMISHI_VRAM - G_OBJ_HAMISHI_FILE)

; Overlay: En_Zl4
.definelabel G_EN_ZL4_FILE, 0xE14920
.definelabel G_EN_ZL4_VRAM, 0x809A1BB0
.definelabel G_EN_ZL4_DELTA, (G_EN_ZL4_VRAM - G_EN_ZL4_FILE)

; Overlay: En_Mm2 :: Postman's Letter to Himself
.definelabel G_EN_MM2_FILE, 0xE14DA0
.definelabel G_EN_MM2_VRAM, 0x809A2030
.definelabel G_EN_MM2_DELTA, (G_EN_MM2_VRAM - G_EN_MM2_FILE)

; Overlay: Door_Spiral
.definelabel G_DOOR_SPIRAL_FILE, 0xE158D0
.definelabel G_DOOR_SPIRAL_VRAM, 0x809A2B60
.definelabel G_DOOR_SPIRAL_DELTA, (G_DOOR_SPIRAL_VRAM - G_DOOR_SPIRAL_FILE)

; Overlay: Obj_Pzlblock :: Majora Pushblock
.definelabel G_OBJ_PZLBLOCK_FILE, 0xE16150
.definelabel G_OBJ_PZLBLOCK_VRAM, 0x809A33E0
.definelabel G_OBJ_PZLBLOCK_DELTA, (G_OBJ_PZLBLOCK_VRAM - G_OBJ_PZLBLOCK_FILE)

; Overlay: Obj_Toge :: Blade Trap
.definelabel G_OBJ_TOGE_FILE, 0xE16F30
.definelabel G_OBJ_TOGE_VRAM, 0x809A41C0
.definelabel G_OBJ_TOGE_DELTA, (G_OBJ_TOGE_VRAM - G_OBJ_TOGE_FILE)

; Overlay: Obj_Armos :: Armos Statue
.definelabel G_OBJ_ARMOS_FILE, 0xE17B70
.definelabel G_OBJ_ARMOS_VRAM, 0x809A4E00
.definelabel G_OBJ_ARMOS_DELTA, (G_OBJ_ARMOS_VRAM - G_OBJ_ARMOS_FILE)

; Overlay: Obj_Boyo :: Bumper
.definelabel G_OBJ_BOYO_FILE, 0xE18A80
.definelabel G_OBJ_BOYO_VRAM, 0x809A5D10
.definelabel G_OBJ_BOYO_DELTA, (G_OBJ_BOYO_VRAM - G_OBJ_BOYO_FILE)

; Overlay: En_Grasshopper :: Dragonfly
.definelabel G_EN_GRASSHOPPER_FILE, 0xE18FF0
.definelabel G_EN_GRASSHOPPER_VRAM, 0x809A6280
.definelabel G_EN_GRASSHOPPER_DELTA, (G_EN_GRASSHOPPER_VRAM - G_EN_GRASSHOPPER_FILE)

; Overlay: Obj_Grass
.definelabel G_OBJ_GRASS_FILE, 0xE1BE80
.definelabel G_OBJ_GRASS_VRAM, 0x809A9110
.definelabel G_OBJ_GRASS_DELTA, (G_OBJ_GRASS_VRAM - G_OBJ_GRASS_FILE)

; Overlay: Obj_Grass_Carry
.definelabel G_OBJ_GRASS_CARRY_FILE, 0xE1DB10
.definelabel G_OBJ_GRASS_CARRY_VRAM, 0x809AAE60
.definelabel G_OBJ_GRASS_CARRY_DELTA, (G_OBJ_GRASS_CARRY_VRAM - G_OBJ_GRASS_CARRY_FILE)

; Overlay: Obj_Grass_Unit :: Grass Cluster
.definelabel G_OBJ_GRASS_UNIT_FILE, 0xE1EA90
.definelabel G_OBJ_GRASS_UNIT_VRAM, 0x809ABDE0
.definelabel G_OBJ_GRASS_UNIT_DELTA, (G_OBJ_GRASS_UNIT_VRAM - G_OBJ_GRASS_UNIT_FILE)

; Overlay: Bg_Fire_Wall
.definelabel G_BG_FIRE_WALL_FILE, 0xE1F160
.definelabel G_BG_FIRE_WALL_VRAM, 0x809AC4B0
.definelabel G_BG_FIRE_WALL_DELTA, (G_BG_FIRE_WALL_VRAM - G_BG_FIRE_WALL_FILE)

; Overlay: En_Bu
.definelabel G_EN_BU_FILE, 0xE1FA40
.definelabel G_EN_BU_VRAM, 0x809ACD90
.definelabel G_EN_BU_DELTA, (G_EN_BU_VRAM - G_EN_BU_FILE)

; Overlay: En_Encount3 :: Circle of Light [?]
.definelabel G_EN_ENCOUNT3_FILE, 0xE1FBF0
.definelabel G_EN_ENCOUNT3_VRAM, 0x809ACF40
.definelabel G_EN_ENCOUNT3_DELTA, (G_EN_ENCOUNT3_VRAM - G_EN_ENCOUNT3_FILE)

; Overlay: En_Jso :: Garo Master I [?]
.definelabel G_EN_JSO_FILE, 0xE20590
.definelabel G_EN_JSO_VRAM, 0x809AD8E0
.definelabel G_EN_JSO_DELTA, (G_EN_JSO_VRAM - G_EN_JSO_FILE)

; Overlay: Obj_Chikuwa :: Falling Block Row
.definelabel G_OBJ_CHIKUWA_FILE, 0xE24200
.definelabel G_OBJ_CHIKUWA_VRAM, 0x809B1550
.definelabel G_OBJ_CHIKUWA_DELTA, (G_OBJ_CHIKUWA_VRAM - G_OBJ_CHIKUWA_FILE)

; Overlay: En_Knight :: Igos du Ikana & Henchmen [?]
.definelabel G_EN_KNIGHT_FILE, 0xE24DA0
.definelabel G_EN_KNIGHT_VRAM, 0x809B20F0
.definelabel G_EN_KNIGHT_DELTA, (G_EN_KNIGHT_VRAM - G_EN_KNIGHT_FILE)

; Overlay: En_Warp_tag :: Warp to Trial Entrance
.definelabel G_EN_WARP_TAG_FILE, 0xE31C80
.definelabel G_EN_WARP_TAG_VRAM, 0x809C0760
.definelabel G_EN_WARP_TAG_DELTA, (G_EN_WARP_TAG_VRAM - G_EN_WARP_TAG_FILE)

; Overlay: En_Aob_01 :: Mamamu Yan
.definelabel G_EN_AOB_01_FILE, 0xE325D0
.definelabel G_EN_AOB_01_VRAM, 0x809C10B0
.definelabel G_EN_AOB_01_DELTA, (G_EN_AOB_01_VRAM - G_EN_AOB_01_FILE)

; Overlay: En_Boj_01
.definelabel G_EN_BOJ_01_FILE, 0xE352A0
.definelabel G_EN_BOJ_01_VRAM, 0x809C3D80
.definelabel G_EN_BOJ_01_DELTA, (G_EN_BOJ_01_VRAM - G_EN_BOJ_01_FILE)

; Overlay: En_Boj_02
.definelabel G_EN_BOJ_02_FILE, 0xE35330
.definelabel G_EN_BOJ_02_VRAM, 0x809C3E10
.definelabel G_EN_BOJ_02_DELTA, (G_EN_BOJ_02_VRAM - G_EN_BOJ_02_FILE)

; Overlay: En_Boj_03
.definelabel G_EN_BOJ_03_FILE, 0xE353C0
.definelabel G_EN_BOJ_03_VRAM, 0x809C3EA0
.definelabel G_EN_BOJ_03_DELTA, (G_EN_BOJ_03_VRAM - G_EN_BOJ_03_FILE)

; Overlay: En_Encount4
.definelabel G_EN_ENCOUNT4_FILE, 0xE35450
.definelabel G_EN_ENCOUNT4_VRAM, 0x809C3F30
.definelabel G_EN_ENCOUNT4_DELTA, (G_EN_ENCOUNT4_VRAM - G_EN_ENCOUNT4_FILE)

; Overlay: En_Bom_Bowl_Man :: Bomber I [?]
.definelabel G_EN_BOM_BOWL_MAN_FILE, 0xE35CB0
.definelabel G_EN_BOM_BOWL_MAN_VRAM, 0x809C4790
.definelabel G_EN_BOM_BOWL_MAN_DELTA, (G_EN_BOM_BOWL_MAN_VRAM - G_EN_BOM_BOWL_MAN_FILE)

; Overlay: En_Syateki_Man :: Shooting Gallery Proprietors [?]
.definelabel G_EN_SYATEKI_MAN_FILE, 0xE379E0
.definelabel G_EN_SYATEKI_MAN_VRAM, 0x809C64C0
.definelabel G_EN_SYATEKI_MAN_DELTA, (G_EN_SYATEKI_MAN_VRAM - G_EN_SYATEKI_MAN_FILE)

; Overlay: Bg_Icicle :: Icicle
.definelabel G_BG_ICICLE_FILE, 0xE3AF80
.definelabel G_BG_ICICLE_VRAM, 0x809C9A60
.definelabel G_BG_ICICLE_DELTA, (G_BG_ICICLE_VRAM - G_BG_ICICLE_FILE)

; Overlay: En_Syateki_Crow :: Guay (Shooting Gallery)
.definelabel G_EN_SYATEKI_CROW_FILE, 0xE3B910
.definelabel G_EN_SYATEKI_CROW_VRAM, 0x809CA3F0
.definelabel G_EN_SYATEKI_CROW_DELTA, (G_EN_SYATEKI_CROW_VRAM - G_EN_SYATEKI_CROW_FILE)

; Overlay: En_Boj_04
.definelabel G_EN_BOJ_04_FILE, 0xE3C720
.definelabel G_EN_BOJ_04_VRAM, 0x809CB200
.definelabel G_EN_BOJ_04_DELTA, (G_EN_BOJ_04_VRAM - G_EN_BOJ_04_FILE)

; Overlay: En_Cne_01 :: Thin Woman in Blue Dress [OoT]
.definelabel G_EN_CNE_01_FILE, 0xE3C7B0
.definelabel G_EN_CNE_01_VRAM, 0x809CB290
.definelabel G_EN_CNE_01_DELTA, (G_EN_CNE_01_VRAM - G_EN_CNE_01_FILE)

; Overlay: En_Bba_01 :: Bomb Shop Proprietor's Mother [Early]
.definelabel G_EN_BBA_01_FILE, 0xE3D580
.definelabel G_EN_BBA_01_VRAM, 0x809CC060
.definelabel G_EN_BBA_01_DELTA, (G_EN_BBA_01_VRAM - G_EN_BBA_01_FILE)

; Overlay: En_Bji_01 :: Shikashi
.definelabel G_EN_BJI_01_FILE, 0xE3E300
.definelabel G_EN_BJI_01_VRAM, 0x809CCDE0
.definelabel G_EN_BJI_01_DELTA, (G_EN_BJI_01_VRAM - G_EN_BJI_01_FILE)

; Overlay: Bg_Spdweb :: Spiderweb
.definelabel G_BG_SPDWEB_FILE, 0xE3F3E0
.definelabel G_BG_SPDWEB_VRAM, 0x809CDEC0
.definelabel G_BG_SPDWEB_DELTA, (G_BG_SPDWEB_VRAM - G_BG_SPDWEB_FILE)

; Overlay: En_Mt_tag
.definelabel G_EN_MT_TAG_FILE, 0xE40870
.definelabel G_EN_MT_TAG_VRAM, 0x809CF350
.definelabel G_EN_MT_TAG_DELTA, (G_EN_MT_TAG_VRAM - G_EN_MT_TAG_FILE)

; Overlay: Boss_01 :: Odolwa
.definelabel G_BOSS_01_FILE, 0xE41A50
.definelabel G_BOSS_01_VRAM, 0x809D0530
.definelabel G_BOSS_01_DELTA, (G_BOSS_01_VRAM - G_BOSS_01_FILE)

; Overlay: Boss_02 :: Twinmold
.definelabel G_BOSS_02_FILE, 0xE49F30
.definelabel G_BOSS_02_VRAM, 0x809DA1D0
.definelabel G_BOSS_02_DELTA, (G_BOSS_02_VRAM - G_BOSS_02_FILE)

; Overlay: Boss_03 :: Gyorg
.definelabel G_BOSS_03_FILE, 0xE50180
.definelabel G_BOSS_03_VRAM, 0x809E2760
.definelabel G_BOSS_03_DELTA, (G_BOSS_03_VRAM - G_BOSS_03_FILE)

; Overlay: Boss_04 :: Wart
.definelabel G_BOSS_04_FILE, 0xE57260
.definelabel G_BOSS_04_VRAM, 0x809EC040
.definelabel G_BOSS_04_DELTA, (G_BOSS_04_VRAM - G_BOSS_04_FILE)

; Overlay: Boss_05 :: Bio Deku Baba
.definelabel G_BOSS_05_FILE, 0xE596F0
.definelabel G_BOSS_05_VRAM, 0x809EE4E0
.definelabel G_BOSS_05_DELTA, (G_BOSS_05_VRAM - G_BOSS_05_FILE)

; Overlay: Boss_06 :: Igos du Ikana [?]
.definelabel G_BOSS_06_FILE, 0xE5D320
.definelabel G_BOSS_06_VRAM, 0x809F2120
.definelabel G_BOSS_06_DELTA, (G_BOSS_06_VRAM - G_BOSS_06_FILE)

; Overlay: Boss_07 :: Majora
.definelabel G_BOSS_07_FILE, 0xE5F570
.definelabel G_BOSS_07_VRAM, 0x809F4980
.definelabel G_BOSS_07_DELTA, (G_BOSS_07_VRAM - G_BOSS_07_FILE)

; Overlay: Bg_Dy_Yoseizo :: Great Fairy
.definelabel G_BG_DY_YOSEIZO_FILE, 0xE74630
.definelabel G_BG_DY_YOSEIZO_VRAM, 0x80A0A8A0
.definelabel G_BG_DY_YOSEIZO_DELTA, (G_BG_DY_YOSEIZO_VRAM - G_BG_DY_YOSEIZO_FILE)

; Overlay: En_Boj_05
.definelabel G_EN_BOJ_05_FILE, 0xE76510
.definelabel G_EN_BOJ_05_VRAM, 0x80A0C780
.definelabel G_EN_BOJ_05_DELTA, (G_EN_BOJ_05_VRAM - G_EN_BOJ_05_FILE)

; Overlay: En_Sob1
.definelabel G_EN_SOB1_FILE, 0xE765A0
.definelabel G_EN_SOB1_VRAM, 0x80A0C810
.definelabel G_EN_SOB1_DELTA, (G_EN_SOB1_VRAM - G_EN_SOB1_FILE)

; Overlay: En_Go :: Goron (Generic) [?]
.definelabel G_EN_GO_FILE, 0xE7AD60
.definelabel G_EN_GO_VRAM, 0x80A10FD0
.definelabel G_EN_GO_DELTA, (G_EN_GO_VRAM - G_EN_GO_FILE)

; Overlay: En_Raf :: Carnivorous Lilypad
.definelabel G_EN_RAF_FILE, 0xE80AD0
.definelabel G_EN_RAF_VRAM, 0x80A16D40
.definelabel G_EN_RAF_DELTA, (G_EN_RAF_VRAM - G_EN_RAF_FILE)

; Overlay: Obj_Funen :: Stone Tower Smoke Plume [Early]
.definelabel G_OBJ_FUNEN_FILE, 0xE834D0
.definelabel G_OBJ_FUNEN_VRAM, 0x80A19740
.definelabel G_OBJ_FUNEN_DELTA, (G_OBJ_FUNEN_VRAM - G_OBJ_FUNEN_FILE)

; Overlay: Obj_Raillift :: Elevator (Deku Palace & Woodfall Temple) [?]
.definelabel G_OBJ_RAILLIFT_FILE, 0xE836A0
.definelabel G_OBJ_RAILLIFT_VRAM, 0x80A19910
.definelabel G_OBJ_RAILLIFT_DELTA, (G_OBJ_RAILLIFT_VRAM - G_OBJ_RAILLIFT_FILE)

; Overlay: Bg_Numa_Hana :: Wooden Flower
.definelabel G_BG_NUMA_HANA_FILE, 0xE84290
.definelabel G_BG_NUMA_HANA_VRAM, 0x80A1A500
.definelabel G_BG_NUMA_HANA_DELTA, (G_BG_NUMA_HANA_VRAM - G_BG_NUMA_HANA_FILE)

; Overlay: Obj_Flowerpot :: Potted Plant
.definelabel G_OBJ_FLOWERPOT_FILE, 0xE85160
.definelabel G_OBJ_FLOWERPOT_VRAM, 0x80A1B3D0
.definelabel G_OBJ_FLOWERPOT_DELTA, (G_OBJ_FLOWERPOT_VRAM - G_OBJ_FLOWERPOT_FILE)

; Overlay: Obj_Spinyroll :: Spiked Log (Horizontal)
.definelabel G_OBJ_SPINYROLL_FILE, 0xE875C0
.definelabel G_OBJ_SPINYROLL_VRAM, 0x80A1DA50
.definelabel G_OBJ_SPINYROLL_DELTA, (G_OBJ_SPINYROLL_VRAM - G_OBJ_SPINYROLL_FILE)

; Overlay: Dm_Hina :: Boss Remains
.definelabel G_DM_HINA_FILE, 0xE88F80
.definelabel G_DM_HINA_VRAM, 0x80A1F410
.definelabel G_DM_HINA_DELTA, (G_DM_HINA_VRAM - G_DM_HINA_FILE)

; Overlay: En_Syateki_Wf :: Wolfos (Shooting Gallery)
.definelabel G_EN_SYATEKI_WF_FILE, 0xE899C0
.definelabel G_EN_SYATEKI_WF_VRAM, 0x80A1FE50
.definelabel G_EN_SYATEKI_WF_DELTA, (G_EN_SYATEKI_WF_VRAM - G_EN_SYATEKI_WF_FILE)

; Overlay: Obj_Skateblock :: Ice Pushblock
.definelabel G_OBJ_SKATEBLOCK_FILE, 0xE8ACC0
.definelabel G_OBJ_SKATEBLOCK_VRAM, 0x80A21150
.definelabel G_OBJ_SKATEBLOCK_DELTA, (G_OBJ_SKATEBLOCK_VRAM - G_OBJ_SKATEBLOCK_FILE)

; Overlay: Effect_En_Ice_Block :: Particle - [Unknown]
.definelabel G_EFFECT_EN_ICE_BLOCK_FILE, 0xE8C8B0
.definelabel G_EFFECT_EN_ICE_BLOCK_VRAM, 0x80A22D40
.definelabel G_EFFECT_EN_ICE_BLOCK_DELTA, (G_EFFECT_EN_ICE_BLOCK_VRAM - G_EFFECT_EN_ICE_BLOCK_FILE)

; Overlay: Obj_Iceblock :: Frozen Enemy Ice Block
.definelabel G_OBJ_ICEBLOCK_FILE, 0xE8CC00
.definelabel G_OBJ_ICEBLOCK_VRAM, 0x80A23090
.definelabel G_OBJ_ICEBLOCK_DELTA, (G_OBJ_ICEBLOCK_VRAM - G_OBJ_ICEBLOCK_FILE)

; Overlay: En_Bigpamet :: Snapper (Mini-Boss)
.definelabel G_EN_BIGPAMET_FILE, 0xE91090
.definelabel G_EN_BIGPAMET_VRAM, 0x80A27520
.definelabel G_EN_BIGPAMET_DELTA, (G_EN_BIGPAMET_VRAM - G_EN_BIGPAMET_FILE)

; Overlay: Bg_Dblue_Movebg :: Great Bay Temple Gears
.definelabel G_BG_DBLUE_MOVEBG_FILE, 0xE935F0
.definelabel G_BG_DBLUE_MOVEBG_VRAM, 0x80A29A80
.definelabel G_BG_DBLUE_MOVEBG_DELTA, (G_BG_DBLUE_MOVEBG_VRAM - G_BG_DBLUE_MOVEBG_FILE)

; Overlay: En_Syateki_Dekunuts :: Mad Scrub (Shooting Gallery)
.definelabel G_EN_SYATEKI_DEKUNUTS_FILE, 0xE95760
.definelabel G_EN_SYATEKI_DEKUNUTS_VRAM, 0x80A2BC00
.definelabel G_EN_SYATEKI_DEKUNUTS_DELTA, (G_EN_SYATEKI_DEKUNUTS_VRAM - G_EN_SYATEKI_DEKUNUTS_FILE)

; Overlay: Elf_Msg3
.definelabel G_ELF_MSG3_FILE, 0xE96870
.definelabel G_ELF_MSG3_VRAM, 0x80A2CD10
.definelabel G_ELF_MSG3_DELTA, (G_ELF_MSG3_VRAM - G_ELF_MSG3_FILE)

; Overlay: En_Fg :: Frog II [?]
.definelabel G_EN_FG_FILE, 0xE96DE0
.definelabel G_EN_FG_VRAM, 0x80A2D280
.definelabel G_EN_FG_DELTA, (G_EN_FG_VRAM - G_EN_FG_FILE)

; Overlay: Dm_Ravine :: Tree Trunk
.definelabel G_DM_RAVINE_FILE, 0xE98300
.definelabel G_DM_RAVINE_VRAM, 0x80A2E7A0
.definelabel G_DM_RAVINE_DELTA, (G_DM_RAVINE_VRAM - G_DM_RAVINE_FILE)

; Overlay: Dm_Sa
.definelabel G_DM_SA_FILE, 0xE984C0
.definelabel G_DM_SA_VRAM, 0x80A2E960
.definelabel G_DM_SA_DELTA, (G_DM_SA_VRAM - G_DM_SA_FILE)

; Overlay: En_Slime :: Chuchu
.definelabel G_EN_SLIME_FILE, 0xE98900
.definelabel G_EN_SLIME_VRAM, 0x80A2EDA0
.definelabel G_EN_SLIME_DELTA, (G_EN_SLIME_VRAM - G_EN_SLIME_FILE)

; Overlay: En_Pr :: Desbreko
.definelabel G_EN_PR_FILE, 0xE9BD60
.definelabel G_EN_PR_VRAM, 0x80A32210
.definelabel G_EN_PR_DELTA, (G_EN_PR_VRAM - G_EN_PR_FILE)

; Overlay: Obj_Toudai :: Clock Tower Spotlight
.definelabel G_OBJ_TOUDAI_FILE, 0xE9D650
.definelabel G_OBJ_TOUDAI_VRAM, 0x80A33B00
.definelabel G_OBJ_TOUDAI_DELTA, (G_OBJ_TOUDAI_VRAM - G_OBJ_TOUDAI_FILE)

; Overlay: Obj_Entotu :: Clock Town 2D Chimney Backdrop
.definelabel G_OBJ_ENTOTU_FILE, 0xE9E250
.definelabel G_OBJ_ENTOTU_VRAM, 0x80A34700
.definelabel G_OBJ_ENTOTU_DELTA, (G_OBJ_ENTOTU_VRAM - G_OBJ_ENTOTU_FILE)

; Overlay: Obj_Bell :: Stock Pot Inn Bell
.definelabel G_OBJ_BELL_FILE, 0xE9F060
.definelabel G_OBJ_BELL_VRAM, 0x80A35510
.definelabel G_OBJ_BELL_DELTA, (G_OBJ_BELL_VRAM - G_OBJ_BELL_FILE)

; Overlay: En_Syateki_Okuta :: Octorok (Shooting Gallery)
.definelabel G_EN_SYATEKI_OKUTA_FILE, 0xE9FB40
.definelabel G_EN_SYATEKI_OKUTA_VRAM, 0x80A35FF0
.definelabel G_EN_SYATEKI_OKUTA_DELTA, (G_EN_SYATEKI_OKUTA_VRAM - G_EN_SYATEKI_OKUTA_FILE)

; Overlay: Obj_Shutter :: West Clock Town Bank Shutter
.definelabel G_OBJ_SHUTTER_FILE, 0xEA1A20
.definelabel G_OBJ_SHUTTER_VRAM, 0x80A37ED0
.definelabel G_OBJ_SHUTTER_DELTA, (G_OBJ_SHUTTER_VRAM - G_OBJ_SHUTTER_FILE)

; Overlay: Dm_Zl :: Child Zelda (Cutscenes)
.definelabel G_DM_ZL_FILE, 0xEA1CE0
.definelabel G_DM_ZL_VRAM, 0x80A38190
.definelabel G_DM_ZL_DELTA, (G_DM_ZL_VRAM - G_DM_ZL_FILE)

; Overlay: En_Ru :: Adult Ruto [OoT]
.definelabel G_EN_RU_FILE, 0xEA24F0
.definelabel G_EN_RU_VRAM, 0x80A389A0
.definelabel G_EN_RU_DELTA, (G_EN_RU_VRAM - G_EN_RU_FILE)

; Overlay: En_Elfgrp :: Group of Stray Fairies
.definelabel G_EN_ELFGRP_FILE, 0xEA3200
.definelabel G_EN_ELFGRP_VRAM, 0x80A396B0
.definelabel G_EN_ELFGRP_DELTA, (G_EN_ELFGRP_VRAM - G_EN_ELFGRP_FILE)

; Overlay: Dm_Tsg
.definelabel G_DM_TSG_FILE, 0xEA47B0
.definelabel G_DM_TSG_VRAM, 0x80A3AC60
.definelabel G_DM_TSG_DELTA, (G_DM_TSG_VRAM - G_DM_TSG_FILE)

; Overlay: En_Baguo :: Nejiron
.definelabel G_EN_BAGUO_FILE, 0xEA4BD0
.definelabel G_EN_BAGUO_VRAM, 0x80A3B080
.definelabel G_EN_BAGUO_DELTA, (G_EN_BAGUO_VRAM - G_EN_BAGUO_FILE)

; Overlay: Obj_Vspinyroll :: Spiked Log (Vertical)
.definelabel G_OBJ_VSPINYROLL_FILE, 0xEA6030
.definelabel G_OBJ_VSPINYROLL_VRAM, 0x80A3C4E0
.definelabel G_OBJ_VSPINYROLL_DELTA, (G_OBJ_VSPINYROLL_VRAM - G_OBJ_VSPINYROLL_FILE)

; Overlay: Obj_Smork :: Romani Ranch Chimney Smoke
.definelabel G_OBJ_SMORK_FILE, 0xEA71D0
.definelabel G_OBJ_SMORK_VRAM, 0x80A3D680
.definelabel G_OBJ_SMORK_DELTA, (G_OBJ_SMORK_VRAM - G_OBJ_SMORK_FILE)

; Overlay: En_Test2
.definelabel G_EN_TEST2_FILE, 0xEA7EE0
.definelabel G_EN_TEST2_VRAM, 0x80A3E390
.definelabel G_EN_TEST2_DELTA, (G_EN_TEST2_VRAM - G_EN_TEST2_FILE)

; Overlay: En_Test3 :: Kafei
.definelabel G_EN_TEST3_FILE, 0xEA8330
.definelabel G_EN_TEST3_VRAM, 0x80A3E7E0
.definelabel G_EN_TEST3_DELTA, (G_EN_TEST3_VRAM - G_EN_TEST3_FILE)

; Overlay: En_Test4 :: Three-Day Timer
.definelabel G_EN_TEST4_FILE, 0xEAB870
.definelabel G_EN_TEST4_VRAM, 0x80A41D70
.definelabel G_EN_TEST4_DELTA, (G_EN_TEST4_VRAM - G_EN_TEST4_FILE)

; Overlay: En_Bat :: Bad Bat
.definelabel G_EN_BAT_FILE, 0xEACFD0
.definelabel G_EN_BAT_VRAM, 0x80A434E0
.definelabel G_EN_BAT_DELTA, (G_EN_BAT_VRAM - G_EN_BAT_FILE)

; Overlay: En_Sekihi :: Mikau's Grave & Song Pedestals [Early]
.definelabel G_EN_SEKIHI_FILE, 0xEAE760
.definelabel G_EN_SEKIHI_VRAM, 0x80A44C80
.definelabel G_EN_SEKIHI_DELTA, (G_EN_SEKIHI_VRAM - G_EN_SEKIHI_FILE)

; Overlay: En_Wiz :: Wizzrobe
.definelabel G_EN_WIZ_FILE, 0xEAEE40
.definelabel G_EN_WIZ_VRAM, 0x80A45360
.definelabel G_EN_WIZ_DELTA, (G_EN_WIZ_VRAM - G_EN_WIZ_FILE)

; Overlay: En_Wiz_Brock :: Wizzrobe Warp Platform
.definelabel G_EN_WIZ_BROCK_FILE, 0xEB2AC0
.definelabel G_EN_WIZ_BROCK_VRAM, 0x80A48FE0
.definelabel G_EN_WIZ_BROCK_DELTA, (G_EN_WIZ_BROCK_VRAM - G_EN_WIZ_BROCK_FILE)

; Overlay: En_Wiz_Fire :: Wizzrobe Fire Attack
.definelabel G_EN_WIZ_FIRE_FILE, 0xEB3180
.definelabel G_EN_WIZ_FIRE_VRAM, 0x80A496A0
.definelabel G_EN_WIZ_FIRE_DELTA, (G_EN_WIZ_FIRE_VRAM - G_EN_WIZ_FIRE_FILE)

; Overlay: Eff_Change :: Camera Refocuser
.definelabel G_EFF_CHANGE_FILE, 0xEB5F70
.definelabel G_EFF_CHANGE_VRAM, 0x80A4C490
.definelabel G_EFF_CHANGE_DELTA, (G_EFF_CHANGE_VRAM - G_EFF_CHANGE_FILE)

; Overlay: Dm_Statue :: Elegy Statue Light Beam [?]
.definelabel G_DM_STATUE_FILE, 0xEB6490
.definelabel G_DM_STATUE_VRAM, 0x80A4C9B0
.definelabel G_DM_STATUE_DELTA, (G_DM_STATUE_VRAM - G_DM_STATUE_FILE)

; Overlay: Obj_Fireshield :: Circle of Flames
.definelabel G_OBJ_FIRESHIELD_FILE, 0xEB6570
.definelabel G_OBJ_FIRESHIELD_VRAM, 0x80A4CA90
.definelabel G_OBJ_FIRESHIELD_DELTA, (G_OBJ_FIRESHIELD_VRAM - G_OBJ_FIRESHIELD_FILE)

; Overlay: Bg_Ladder :: Ladder
.definelabel G_BG_LADDER_FILE, 0xEB74D0
.definelabel G_BG_LADDER_VRAM, 0x80A4D9F0
.definelabel G_BG_LADDER_DELTA, (G_BG_LADDER_VRAM - G_BG_LADDER_FILE)

; Overlay: En_Mkk :: Black & White Boes
.definelabel G_EN_MKK_FILE, 0xEB79B0
.definelabel G_EN_MKK_VRAM, 0x80A4DED0
.definelabel G_EN_MKK_DELTA, (G_EN_MKK_VRAM - G_EN_MKK_FILE)

; Overlay: Demo_Getitem :: Great Fairy's Mask & Great Fairy's Sword
.definelabel G_DEMO_GETITEM_FILE, 0xEB9520
.definelabel G_DEMO_GETITEM_VRAM, 0x80A4FA40
.definelabel G_DEMO_GETITEM_DELTA, (G_DEMO_GETITEM_VRAM - G_DEMO_GETITEM_FILE)

; Overlay: En_Dnb
.definelabel G_EN_DNB_FILE, 0xEB98B0
.definelabel G_EN_DNB_VRAM, 0x80A4FDD0
.definelabel G_EN_DNB_DELTA, (G_EN_DNB_VRAM - G_EN_DNB_FILE)

; Overlay: En_Dnh :: Boat Cruise Target Spot
.definelabel G_EN_DNH_FILE, 0xEBA820
.definelabel G_EN_DNH_VRAM, 0x80A50D40
.definelabel G_EN_DNH_DELTA, (G_EN_DNH_VRAM - G_EN_DNH_FILE)

; Overlay: En_Dnk :: Hallucinatory Mad Scrubs
.definelabel G_EN_DNK_FILE, 0xEBAFD0
.definelabel G_EN_DNK_VRAM, 0x80A514F0
.definelabel G_EN_DNK_DELTA, (G_EN_DNK_VRAM - G_EN_DNK_FILE)

; Overlay: En_Dnq :: Deku King
.definelabel G_EN_DNQ_FILE, 0xEBC010
.definelabel G_EN_DNQ_VRAM, 0x80A52530
.definelabel G_EN_DNQ_DELTA, (G_EN_DNQ_VRAM - G_EN_DNQ_FILE)

; Overlay: Bg_Keikoku_Saku :: Spiked Iron Fence
.definelabel G_BG_KEIKOKU_SAKU_FILE, 0xEBD2B0
.definelabel G_BG_KEIKOKU_SAKU_VRAM, 0x80A537D0
.definelabel G_BG_KEIKOKU_SAKU_DELTA, (G_BG_KEIKOKU_SAKU_VRAM - G_BG_KEIKOKU_SAKU_FILE)

; Overlay: Obj_Hugebombiwa :: Powder Keg Boulder
.definelabel G_OBJ_HUGEBOMBIWA_FILE, 0xEBD6C0
.definelabel G_OBJ_HUGEBOMBIWA_VRAM, 0x80A53BE0
.definelabel G_OBJ_HUGEBOMBIWA_DELTA, (G_OBJ_HUGEBOMBIWA_VRAM - G_OBJ_HUGEBOMBIWA_FILE)

; Overlay: En_Firefly2
.definelabel G_EN_FIREFLY2_FILE, 0xEBFBA0
.definelabel G_EN_FIREFLY2_VRAM, 0x80A560C0
.definelabel G_EN_FIREFLY2_DELTA, (G_EN_FIREFLY2_VRAM - G_EN_FIREFLY2_FILE)

; Overlay: En_Rat :: Real Bombchu
.definelabel G_EN_RAT_FILE, 0xEBFC30
.definelabel G_EN_RAT_VRAM, 0x80A56150
.definelabel G_EN_RAT_DELTA, (G_EN_RAT_VRAM - G_EN_RAT_FILE)

; Overlay: En_Water_Effect :: Dripping Water
.definelabel G_EN_WATER_EFFECT_FILE, 0xEC2280
.definelabel G_EN_WATER_EFFECT_VRAM, 0x80A587A0
.definelabel G_EN_WATER_EFFECT_DELTA, (G_EN_WATER_EFFECT_VRAM - G_EN_WATER_EFFECT_FILE)

; Overlay: En_Kusa2 :: Keaton Grass Cluster
.definelabel G_EN_KUSA2_FILE, 0xEC4C40
.definelabel G_EN_KUSA2_VRAM, 0x80A5B160
.definelabel G_EN_KUSA2_DELTA, (G_EN_KUSA2_VRAM - G_EN_KUSA2_FILE)

; Overlay: Bg_Spout_Fire :: Proximity-Activated Fire Wall
.definelabel G_BG_SPOUT_FIRE_FILE, 0xEC8CA0
.definelabel G_BG_SPOUT_FIRE_VRAM, 0x80A60B20
.definelabel G_BG_SPOUT_FIRE_DELTA, (G_BG_SPOUT_FIRE_VRAM - G_BG_SPOUT_FIRE_FILE)

; Overlay: En_Dy_Extra :: Great Fairy Healing Beam
.definelabel G_EN_DY_EXTRA_FILE, 0xEC9430
.definelabel G_EN_DY_EXTRA_VRAM, 0x80A612B0
.definelabel G_EN_DY_EXTRA_DELTA, (G_EN_DY_EXTRA_VRAM - G_EN_DY_EXTRA_FILE)

; Overlay: En_Bal :: Tingle (Gameplay)
.definelabel G_EN_BAL_FILE, 0xEC9990
.definelabel G_EN_BAL_VRAM, 0x80A61810
.definelabel G_EN_BAL_DELTA, (G_EN_BAL_VRAM - G_EN_BAL_FILE)

; Overlay: En_Ginko_Man :: Bank Teller
.definelabel G_EN_GINKO_MAN_FILE, 0xECC620
.definelabel G_EN_GINKO_MAN_VRAM, 0x80A644A0
.definelabel G_EN_GINKO_MAN_DELTA, (G_EN_GINKO_MAN_VRAM - G_EN_GINKO_MAN_FILE)

; Overlay: En_Warp_Uzu :: Pirates' Fortress Telescope
.definelabel G_EN_WARP_UZU_FILE, 0xECE300
.definelabel G_EN_WARP_UZU_VRAM, 0x80A66180
.definelabel G_EN_WARP_UZU_DELTA, (G_EN_WARP_UZU_VRAM - G_EN_WARP_UZU_FILE)

; Overlay: Obj_Driftice :: Drifting Ice Platform
.definelabel G_OBJ_DRIFTICE_FILE, 0xECE6F0
.definelabel G_OBJ_DRIFTICE_VRAM, 0x80A66570
.definelabel G_OBJ_DRIFTICE_DELTA, (G_OBJ_DRIFTICE_VRAM - G_OBJ_DRIFTICE_FILE)

; Overlay: En_Look_Nuts :: Deku Scrub Guard (Palace Gardens)
.definelabel G_EN_LOOK_NUTS_FILE, 0xECFA30
.definelabel G_EN_LOOK_NUTS_VRAM, 0x80A678B0
.definelabel G_EN_LOOK_NUTS_DELTA, (G_EN_LOOK_NUTS_VRAM - G_EN_LOOK_NUTS_FILE)

; Overlay: En_Mushi2
.definelabel G_EN_MUSHI2_FILE, 0xED0920
.definelabel G_EN_MUSHI2_VRAM, 0x80A687A0
.definelabel G_EN_MUSHI2_DELTA, (G_EN_MUSHI2_VRAM - G_EN_MUSHI2_FILE)

; Overlay: En_Fall
.definelabel G_EN_FALL_FILE, 0xED4110
.definelabel G_EN_FALL_VRAM, 0x80A6BF90
.definelabel G_EN_FALL_DELTA, (G_EN_FALL_VRAM - G_EN_FALL_FILE)

; Overlay: En_Mm3 :: Postman (Counting Game)
.definelabel G_EN_MM3_FILE, 0xED6B10
.definelabel G_EN_MM3_VRAM, 0x80A6F0A0
.definelabel G_EN_MM3_DELTA, (G_EN_MM3_VRAM - G_EN_MM3_FILE)

; Overlay: Bg_Crace_Movebg :: Deku Shrine Door Shutters
.definelabel G_BG_CRACE_MOVEBG_FILE, 0xED8160
.definelabel G_BG_CRACE_MOVEBG_VRAM, 0x80A706F0
.definelabel G_BG_CRACE_MOVEBG_DELTA, (G_BG_CRACE_MOVEBG_VRAM - G_BG_CRACE_MOVEBG_FILE)

; Overlay: En_Dno :: Deku Butler
.definelabel G_EN_DNO_FILE, 0xED8C20
.definelabel G_EN_DNO_VRAM, 0x80A711D0
.definelabel G_EN_DNO_DELTA, (G_EN_DNO_VRAM - G_EN_DNO_FILE)

; Overlay: En_Pr2 :: Skullfish
.definelabel G_EN_PR2_FILE, 0xEDB9F0
.definelabel G_EN_PR2_VRAM, 0x80A73FA0
.definelabel G_EN_PR2_DELTA, (G_EN_PR2_VRAM - G_EN_PR2_FILE)

; Overlay: En_Prz :: Skullfish (Defeated)
.definelabel G_EN_PRZ_FILE, 0xEDD810
.definelabel G_EN_PRZ_VRAM, 0x80A75DC0
.definelabel G_EN_PRZ_DELTA, (G_EN_PRZ_VRAM - G_EN_PRZ_FILE)

; Overlay: En_Jso2 :: Garo Master II [?]
.definelabel G_EN_JSO2_FILE, 0xEDEE10
.definelabel G_EN_JSO2_VRAM, 0x80A773C0
.definelabel G_EN_JSO2_DELTA, (G_EN_JSO2_VRAM - G_EN_JSO2_FILE)

; Overlay: Obj_Etcetera :: Deku Flower
.definelabel G_OBJ_ETCETERA_FILE, 0xEE36C0
.definelabel G_OBJ_ETCETERA_VRAM, 0x80A7BC70
.definelabel G_OBJ_ETCETERA_DELTA, (G_OBJ_ETCETERA_VRAM - G_OBJ_ETCETERA_FILE)

; Overlay: En_Egol :: Eyegore
.definelabel G_EN_EGOL_FILE, 0xEE43E0
.definelabel G_EN_EGOL_VRAM, 0x80A7C990
.definelabel G_EN_EGOL_DELTA, (G_EN_EGOL_VRAM - G_EN_EGOL_FILE)

; Overlay: Obj_Mine :: Spiked Metal Mine
.definelabel G_OBJ_MINE_FILE, 0xEE8C20
.definelabel G_OBJ_MINE_VRAM, 0x80A811D0
.definelabel G_OBJ_MINE_DELTA, (G_OBJ_MINE_VRAM - G_OBJ_MINE_FILE)

; Overlay: Obj_Purify
.definelabel G_OBJ_PURIFY_FILE, 0xEEC420
.definelabel G_OBJ_PURIFY_VRAM, 0x80A84CD0
.definelabel G_OBJ_PURIFY_DELTA, (G_OBJ_PURIFY_VRAM - G_OBJ_PURIFY_FILE)

; Overlay: En_Tru :: Koume (Gameplay) [?]
.definelabel G_EN_TRU_FILE, 0xEECD70
.definelabel G_EN_TRU_VRAM, 0x80A85620
.definelabel G_EN_TRU_DELTA, (G_EN_TRU_VRAM - G_EN_TRU_FILE)

; Overlay: En_Trt :: Kotake (No Broom) [?]
.definelabel G_EN_TRT_FILE, 0xEF2EC0
.definelabel G_EN_TRT_VRAM, 0x80A8B770
.definelabel G_EN_TRT_DELTA, (G_EN_TRT_VRAM - G_EN_TRT_FILE)

; Overlay: En_Test5 :: Spring Water
.definelabel G_EN_TEST5_FILE, 0xEF7B00
.definelabel G_EN_TEST5_VRAM, 0x80A903B0
.definelabel G_EN_TEST5_DELTA, (G_EN_TEST5_VRAM - G_EN_TEST5_FILE)

; Overlay: En_Test6 :: Song of Time Cutscene Effects
.definelabel G_EN_TEST6_FILE, 0xEF7E80
.definelabel G_EN_TEST6_VRAM, 0x80A90730
.definelabel G_EN_TEST6_DELTA, (G_EN_TEST6_VRAM - G_EN_TEST6_FILE)

; Overlay: En_Az :: Beaver Bros.
.definelabel G_EN_AZ_FILE, 0xEFC060
.definelabel G_EN_AZ_VRAM, 0x80A94A30
.definelabel G_EN_AZ_DELTA, (G_EN_AZ_VRAM - G_EN_AZ_FILE)

; Overlay: En_Estone :: Eyegore Rubble
.definelabel G_EN_ESTONE_FILE, 0xF014B0
.definelabel G_EN_ESTONE_VRAM, 0x80A99EA0
.definelabel G_EN_ESTONE_DELTA, (G_EN_ESTONE_VRAM - G_EN_ESTONE_FILE)

; Overlay: Bg_Hakugin_Post :: Snowhead Temple Central Pillar
.definelabel G_BG_HAKUGIN_POST_FILE, 0xF022E0
.definelabel G_BG_HAKUGIN_POST_VRAM, 0x80A9ACD0
.definelabel G_BG_HAKUGIN_POST_DELTA, (G_BG_HAKUGIN_POST_VRAM - G_BG_HAKUGIN_POST_FILE)

; Overlay: Dm_Opstage :: Opening Cutscene Objects
.definelabel G_DM_OPSTAGE_FILE, 0xF053D0
.definelabel G_DM_OPSTAGE_VRAM, 0x80A9F950
.definelabel G_DM_OPSTAGE_DELTA, (G_DM_OPSTAGE_VRAM - G_DM_OPSTAGE_FILE)

; Overlay: Dm_Stk :: Skull Kid
.definelabel G_DM_STK_FILE, 0xF05830
.definelabel G_DM_STK_VRAM, 0x80A9FDB0
.definelabel G_DM_STK_DELTA, (G_DM_STK_VRAM - G_DM_STK_FILE)

; Overlay: Dm_Char00 :: Tatl & Tael (Cutscenes) II [?]
.definelabel G_DM_CHAR00_FILE, 0xF0B000
.definelabel G_DM_CHAR00_VRAM, 0x80AA5580
.definelabel G_DM_CHAR00_DELTA, (G_DM_CHAR00_VRAM - G_DM_CHAR00_FILE)

; Overlay: Dm_Char01 :: Woodfall Temple Rises Cutscene Objects
.definelabel G_DM_CHAR01_FILE, 0xF0DC60
.definelabel G_DM_CHAR01_VRAM, 0x80AA81E0
.definelabel G_DM_CHAR01_DELTA, (G_DM_CHAR01_VRAM - G_DM_CHAR01_FILE)

; Overlay: Dm_Char02 :: Ocarina of Time & Majora's Mask (Clock Tower Roof) [?]
.definelabel G_DM_CHAR02_FILE, 0xF108A0
.definelabel G_DM_CHAR02_VRAM, 0x80AAAE30
.definelabel G_DM_CHAR02_DELTA, (G_DM_CHAR02_VRAM - G_DM_CHAR02_FILE)

; Overlay: Dm_Char03 :: Happy Mask Salesman (Cutscenes)
.definelabel G_DM_CHAR03_FILE, 0xF10F10
.definelabel G_DM_CHAR03_VRAM, 0x80AAB4A0
.definelabel G_DM_CHAR03_DELTA, (G_DM_CHAR03_VRAM - G_DM_CHAR03_FILE)

; Overlay: Dm_Char04 :: Tatl & Tael (Cutscenes) I [?]
.definelabel G_DM_CHAR04_FILE, 0xF116B0
.definelabel G_DM_CHAR04_VRAM, 0x80AABC40
.definelabel G_DM_CHAR04_DELTA, (G_DM_CHAR04_VRAM - G_DM_CHAR04_FILE)

; Overlay: Dm_Char05 :: Masks (Cutscenes)
.definelabel G_DM_CHAR05_FILE, 0xF12010
.definelabel G_DM_CHAR05_VRAM, 0x80AAC5A0
.definelabel G_DM_CHAR05_DELTA, (G_DM_CHAR05_VRAM - G_DM_CHAR05_FILE)

; Overlay: Dm_Char06 :: Mountain Village Cutscene Objects [?]
.definelabel G_DM_CHAR06_FILE, 0xF140F0
.definelabel G_DM_CHAR06_VRAM, 0x80AAE680
.definelabel G_DM_CHAR06_DELTA, (G_DM_CHAR06_VRAM - G_DM_CHAR06_FILE)

; Overlay: Dm_Char07 :: Milk Bar Stage (Cutscenes)
.definelabel G_DM_CHAR07_FILE, 0xF14430
.definelabel G_DM_CHAR07_VRAM, 0x80AAE9C0
.definelabel G_DM_CHAR07_DELTA, (G_DM_CHAR07_VRAM - G_DM_CHAR07_FILE)

; Overlay: Dm_Char08 :: Turtle (Cutscenes) [?]
.definelabel G_DM_CHAR08_FILE, 0xF14AC0
.definelabel G_DM_CHAR08_VRAM, 0x80AAF050
.definelabel G_DM_CHAR08_DELTA, (G_DM_CHAR08_VRAM - G_DM_CHAR08_FILE)

; Overlay: Dm_Char09 :: Giant Bee (Cutscenes)
.definelabel G_DM_CHAR09_FILE, 0xF17880
.definelabel G_DM_CHAR09_VRAM, 0x80AB1E10
.definelabel G_DM_CHAR09_DELTA, (G_DM_CHAR09_VRAM - G_DM_CHAR09_FILE)

; Overlay: Obj_Tokeidai :: Clock Tower & Light Beam
.definelabel G_OBJ_TOKEIDAI_FILE, 0xF18200
.definelabel G_OBJ_TOKEIDAI_VRAM, 0x80AB2790
.definelabel G_OBJ_TOKEIDAI_DELTA, (G_OBJ_TOKEIDAI_VRAM - G_OBJ_TOKEIDAI_FILE)

; Overlay: En_Mnk :: Monkey
.definelabel G_EN_MNK_FILE, 0xF1A780
.definelabel G_EN_MNK_VRAM, 0x80AB4D10
.definelabel G_EN_MNK_DELTA, (G_EN_MNK_VRAM - G_EN_MNK_FILE)

; Overlay: En_Egblock :: Eyegore Block
.definelabel G_EN_EGBLOCK_FILE, 0xF20210
.definelabel G_EN_EGBLOCK_VRAM, 0x80ABA7A0
.definelabel G_EN_EGBLOCK_DELTA, (G_EN_EGBLOCK_VRAM - G_EN_EGBLOCK_FILE)

; Overlay: En_Guard_Nuts :: Deku Scrub Guard (Palace Entrance) [?]
.definelabel G_EN_GUARD_NUTS_FILE, 0xF20B50
.definelabel G_EN_GUARD_NUTS_VRAM, 0x80ABB0E0
.definelabel G_EN_GUARD_NUTS_DELTA, (G_EN_GUARD_NUTS_VRAM - G_EN_GUARD_NUTS_FILE)

; Overlay: Bg_Hakugin_Bombwall :: Snowhead Temple Bombable Wall
.definelabel G_BG_HAKUGIN_BOMBWALL_FILE, 0xF21A30
.definelabel G_BG_HAKUGIN_BOMBWALL_VRAM, 0x80ABBFC0
.definelabel G_BG_HAKUGIN_BOMBWALL_DELTA, (G_BG_HAKUGIN_BOMBWALL_VRAM - G_BG_HAKUGIN_BOMBWALL_FILE)

; Overlay: Obj_Tokei_Tobira :: Clock Tower Doors
.definelabel G_OBJ_TOKEI_TOBIRA_FILE, 0xF22C40
.definelabel G_OBJ_TOKEI_TOBIRA_VRAM, 0x80ABD1D0
.definelabel G_OBJ_TOKEI_TOBIRA_DELTA, (G_OBJ_TOKEI_TOBIRA_VRAM - G_OBJ_TOKEI_TOBIRA_FILE)

; Overlay: Bg_Hakugin_Elvpole :: Snowhead Temple Punchable Pillar Inserts
.definelabel G_BG_HAKUGIN_ELVPOLE_FILE, 0xF232A0
.definelabel G_BG_HAKUGIN_ELVPOLE_VRAM, 0x80ABD830
.definelabel G_BG_HAKUGIN_ELVPOLE_DELTA, (G_BG_HAKUGIN_ELVPOLE_VRAM - G_BG_HAKUGIN_ELVPOLE_FILE)

; Overlay: En_Ma4 :: Romani I [?]
.definelabel G_EN_MA4_FILE, 0xF23710
.definelabel G_EN_MA4_VRAM, 0x80ABDCA0
.definelabel G_EN_MA4_DELTA, (G_EN_MA4_VRAM - G_EN_MA4_FILE)

; Overlay: En_Twig :: Beaver Race Ring
.definelabel G_EN_TWIG_FILE, 0xF262A0
.definelabel G_EN_TWIG_VRAM, 0x80AC0830
.definelabel G_EN_TWIG_DELTA, (G_EN_TWIG_VRAM - G_EN_TWIG_FILE)

; Overlay: En_Po_Fusen :: Poe Balloon
.definelabel G_EN_PO_FUSEN_FILE, 0xF26CA0
.definelabel G_EN_PO_FUSEN_VRAM, 0x80AC1270
.definelabel G_EN_PO_FUSEN_DELTA, (G_EN_PO_FUSEN_VRAM - G_EN_PO_FUSEN_FILE)

; Overlay: En_Door_Etc
.definelabel G_EN_DOOR_ETC_FILE, 0xF27900
.definelabel G_EN_DOOR_ETC_VRAM, 0x80AC1ED0
.definelabel G_EN_DOOR_ETC_DELTA, (G_EN_DOOR_ETC_VRAM - G_EN_DOOR_ETC_FILE)

; Overlay: En_Bigokuta :: Big Octo
.definelabel G_EN_BIGOKUTA_FILE, 0xF28120
.definelabel G_EN_BIGOKUTA_VRAM, 0x80AC26F0
.definelabel G_EN_BIGOKUTA_DELTA, (G_EN_BIGOKUTA_VRAM - G_EN_BIGOKUTA_FILE)

; Overlay: Bg_Icefloe :: Ice Arrow Platform
.definelabel G_BG_ICEFLOE_FILE, 0xF2A320
.definelabel G_BG_ICEFLOE_VRAM, 0x80AC48F0
.definelabel G_BG_ICEFLOE_DELTA, (G_BG_ICEFLOE_VRAM - G_BG_ICEFLOE_FILE)

; Overlay: Effect_Ss_Sbn :: Particle - Deku Link's Magic Bubble
.definelabel G_EFFECT_SS_SBN_FILE, 0xF2E570
.definelabel G_EFFECT_SS_SBN_VRAM, 0x80AC8B50
.definelabel G_EFFECT_SS_SBN_DELTA, (G_EFFECT_SS_SBN_VRAM - G_EFFECT_SS_SBN_FILE)

; Overlay: Obj_Ocarinalift :: Triforce Elevator
.definelabel G_OBJ_OCARINALIFT_FILE, 0xF2EEE0
.definelabel G_OBJ_OCARINALIFT_VRAM, 0x80AC94C0
.definelabel G_OBJ_OCARINALIFT_DELTA, (G_OBJ_OCARINALIFT_VRAM - G_OBJ_OCARINALIFT_FILE)

; Overlay: En_Time_Tag
.definelabel G_EN_TIME_TAG_FILE, 0xF2F8C0
.definelabel G_EN_TIME_TAG_VRAM, 0x80AC9EA0
.definelabel G_EN_TIME_TAG_DELTA, (G_EN_TIME_TAG_VRAM - G_EN_TIME_TAG_FILE)

; Overlay: Bg_Open_Shutter :: Deku Emblem Door
.definelabel G_BG_OPEN_SHUTTER_FILE, 0xF30530
.definelabel G_BG_OPEN_SHUTTER_VRAM, 0x80ACAB10
.definelabel G_BG_OPEN_SHUTTER_DELTA, (G_BG_OPEN_SHUTTER_VRAM - G_BG_OPEN_SHUTTER_FILE)

; Overlay: Bg_Open_Spot :: Skull Kid Spotlights
.definelabel G_BG_OPEN_SPOT_FILE, 0xF30C00
.definelabel G_BG_OPEN_SPOT_VRAM, 0x80ACB1E0
.definelabel G_BG_OPEN_SPOT_DELTA, (G_BG_OPEN_SPOT_VRAM - G_BG_OPEN_SPOT_FILE)

; Overlay: Bg_Fu_Kaiten :: Honey & Darling's Shop Rotating Platform
.definelabel G_BG_FU_KAITEN_FILE, 0xF30E20
.definelabel G_BG_FU_KAITEN_VRAM, 0x80ACB400
.definelabel G_BG_FU_KAITEN_DELTA, (G_BG_FU_KAITEN_VRAM - G_BG_FU_KAITEN_FILE)

; Overlay: Obj_Aqua :: Poured Water
.definelabel G_OBJ_AQUA_FILE, 0xF310C0
.definelabel G_OBJ_AQUA_VRAM, 0x80ACB6A0
.definelabel G_OBJ_AQUA_DELTA, (G_OBJ_AQUA_VRAM - G_OBJ_AQUA_FILE)

; Overlay: En_Elforg :: Stray Fairy
.definelabel G_EN_ELFORG_FILE, 0xF31E90
.definelabel G_EN_ELFORG_VRAM, 0x80ACC470
.definelabel G_EN_ELFORG_DELTA, (G_EN_ELFORG_VRAM - G_EN_ELFORG_FILE)

; Overlay: En_Elfbub :: Stray Fairy Bubble
.definelabel G_EN_ELFBUB_FILE, 0xF336F0
.definelabel G_EN_ELFBUB_VRAM, 0x80ACDCD0
.definelabel G_EN_ELFBUB_DELTA, (G_EN_ELFBUB_VRAM - G_EN_ELFBUB_FILE)

; Overlay: En_Fu_Mato :: Honey & Darling's Shop Target
.definelabel G_EN_FU_MATO_FILE, 0xF33D50
.definelabel G_EN_FU_MATO_VRAM, 0x80ACE330
.definelabel G_EN_FU_MATO_DELTA, (G_EN_FU_MATO_VRAM - G_EN_FU_MATO_FILE)

; Overlay: En_Fu_Kago :: Honey & Darling's Shop Basket
.definelabel G_EN_FU_KAGO_FILE, 0xF351A0
.definelabel G_EN_FU_KAGO_VRAM, 0x80ACF780
.definelabel G_EN_FU_KAGO_DELTA, (G_EN_FU_KAGO_VRAM - G_EN_FU_KAGO_FILE)

; Overlay: En_Osn :: Happy Mask Salesman (Gameplay)
.definelabel G_EN_OSN_FILE, 0xF36250
.definelabel G_EN_OSN_VRAM, 0x80AD0830
.definelabel G_EN_OSN_DELTA, (G_EN_OSN_VRAM - G_EN_OSN_FILE)

; Overlay: Bg_Ctower_Gear :: Clock Tower Gear
.definelabel G_BG_CTOWER_GEAR_FILE, 0xF38590
.definelabel G_BG_CTOWER_GEAR_VRAM, 0x80AD2B70
.definelabel G_BG_CTOWER_GEAR_DELTA, (G_BG_CTOWER_GEAR_VRAM - G_BG_CTOWER_GEAR_FILE)

; Overlay: En_Trt2 :: Kotake (Broom) [?]
.definelabel G_EN_TRT2_FILE, 0xF38DA0
.definelabel G_EN_TRT2_VRAM, 0x80AD3380
.definelabel G_EN_TRT2_DELTA, (G_EN_TRT2_VRAM - G_EN_TRT2_FILE)

; Overlay: Obj_Tokei_Step :: Clock Tower Roof Door
.definelabel G_OBJ_TOKEI_STEP_FILE, 0xF3B5D0
.definelabel G_OBJ_TOKEI_STEP_VRAM, 0x80AD5BB0
.definelabel G_OBJ_TOKEI_STEP_DELTA, (G_OBJ_TOKEI_STEP_VRAM - G_OBJ_TOKEI_STEP_FILE)

; Overlay: Bg_Lotus :: Lilypad
.definelabel G_BG_LOTUS_FILE, 0xF3C180
.definelabel G_BG_LOTUS_VRAM, 0x80AD6760
.definelabel G_BG_LOTUS_DELTA, (G_BG_LOTUS_VRAM - G_BG_LOTUS_FILE)

; Overlay: En_Kame :: Snapper (Enemy)
.definelabel G_EN_KAME_FILE, 0xF3C7F0
.definelabel G_EN_KAME_VRAM, 0x80AD6DD0
.definelabel G_EN_KAME_DELTA, (G_EN_KAME_VRAM - G_EN_KAME_FILE)

; Overlay: Obj_Takaraya_Wall :: Treasure Chest Game Proximity-Activated Wall
.definelabel G_OBJ_TAKARAYA_WALL_FILE, 0xF3EC60
.definelabel G_OBJ_TAKARAYA_WALL_VRAM, 0x80AD9240
.definelabel G_OBJ_TAKARAYA_WALL_DELTA, (G_OBJ_TAKARAYA_WALL_VRAM - G_OBJ_TAKARAYA_WALL_FILE)

; Overlay: Bg_Fu_Mizu :: Honey & Darling's Shop Moat
.definelabel G_BG_FU_MIZU_FILE, 0xF3FE70
.definelabel G_BG_FU_MIZU_VRAM, 0x80ADAAF0
.definelabel G_BG_FU_MIZU_DELTA, (G_BG_FU_MIZU_VRAM - G_BG_FU_MIZU_FILE)

; Overlay: En_Sellnuts :: Business Scrub (Flying) [?]
.definelabel G_EN_SELLNUTS_FILE, 0xF40150
.definelabel G_EN_SELLNUTS_VRAM, 0x80ADADD0
.definelabel G_EN_SELLNUTS_DELTA, (G_EN_SELLNUTS_VRAM - G_EN_SELLNUTS_FILE)

; Overlay: Bg_Dkjail_Ivy :: Woodfall Prison Ivy
.definelabel G_BG_DKJAIL_IVY_FILE, 0xF435B0
.definelabel G_BG_DKJAIL_IVY_VRAM, 0x80ADE230
.definelabel G_BG_DKJAIL_IVY_DELTA, (G_BG_DKJAIL_IVY_VRAM - G_BG_DKJAIL_IVY_FILE)

; Overlay: Obj_Visiblock :: Lens of Truth Platform
.definelabel G_OBJ_VISIBLOCK_FILE, 0xF43DF0
.definelabel G_OBJ_VISIBLOCK_VRAM, 0x80ADEA70
.definelabel G_OBJ_VISIBLOCK_DELTA, (G_OBJ_VISIBLOCK_VRAM - G_OBJ_VISIBLOCK_FILE)

; Overlay: En_Takaraya :: Treasure Chest Game Employee
.definelabel G_EN_TAKARAYA_FILE, 0xF43F10
.definelabel G_EN_TAKARAYA_VRAM, 0x80ADEB90
.definelabel G_EN_TAKARAYA_DELTA, (G_EN_TAKARAYA_VRAM - G_EN_TAKARAYA_FILE)

; Overlay: En_Tsn :: Fisherman (Great Bay Village)
.definelabel G_EN_TSN_FILE, 0xF45020
.definelabel G_EN_TSN_VRAM, 0x80ADFCA0
.definelabel G_EN_TSN_DELTA, (G_EN_TSN_VRAM - G_EN_TSN_FILE)

; Overlay: En_Ds2n :: Potion Shop Proprietor (Updated) [OoT]
.definelabel G_EN_DS2N_FILE, 0xF469D0
.definelabel G_EN_DS2N_VRAM, 0x80AE1650
.definelabel G_EN_DS2N_DELTA, (G_EN_DS2N_VRAM - G_EN_DS2N_FILE)

; Overlay: En_Fsn :: Curiosity Shop Proprietor
.definelabel G_EN_FSN_FILE, 0xF46EF0
.definelabel G_EN_FSN_VRAM, 0x80AE1B70
.definelabel G_EN_FSN_DELTA, (G_EN_FSN_VRAM - G_EN_FSN_FILE)

; Overlay: En_Shn :: Swamp Tourist Center Guide
.definelabel G_EN_SHN_FILE, 0xF4B4B0
.definelabel G_EN_SHN_VRAM, 0x80AE6130
.definelabel G_EN_SHN_DELTA, (G_EN_SHN_VRAM - G_EN_SHN_FILE)

; Overlay: En_Stop_heishi :: Soldier (Gate Guard)
.definelabel G_EN_STOP_HEISHI_FILE, 0xF4C720
.definelabel G_EN_STOP_HEISHI_VRAM, 0x80AE73A0
.definelabel G_EN_STOP_HEISHI_DELTA, (G_EN_STOP_HEISHI_VRAM - G_EN_STOP_HEISHI_FILE)

; Overlay: Obj_Bigicicle :: Ice Block
.definelabel G_OBJ_BIGICICLE_FILE, 0xF4DEF0
.definelabel G_OBJ_BIGICICLE_VRAM, 0x80AE8B70
.definelabel G_OBJ_BIGICICLE_DELTA, (G_OBJ_BIGICICLE_VRAM - G_OBJ_BIGICICLE_FILE)

; Overlay: En_Lift_Nuts :: Deku Scrub Playground Employee
.definelabel G_EN_LIFT_NUTS_FILE, 0xF4EDA0
.definelabel G_EN_LIFT_NUTS_VRAM, 0x80AE9A20
.definelabel G_EN_LIFT_NUTS_DELTA, (G_EN_LIFT_NUTS_VRAM - G_EN_LIFT_NUTS_FILE)

; Overlay: En_Tk :: Dampé
.definelabel G_EN_TK_FILE, 0xF517E0
.definelabel G_EN_TK_VRAM, 0x80AEC460
.definelabel G_EN_TK_DELTA, (G_EN_TK_VRAM - G_EN_TK_FILE)

; Overlay: Bg_Market_Step :: West Clock Town Steps
.definelabel G_BG_MARKET_STEP_FILE, 0xF553D0
.definelabel G_BG_MARKET_STEP_VRAM, 0x80AF0060
.definelabel G_BG_MARKET_STEP_DELTA, (G_BG_MARKET_STEP_VRAM - G_BG_MARKET_STEP_FILE)

; Overlay: Obj_Lupygamelift :: Deku Scrub Playground Elevator
.definelabel G_OBJ_LUPYGAMELIFT_FILE, 0xF554E0
.definelabel G_OBJ_LUPYGAMELIFT_VRAM, 0x80AF0170
.definelabel G_OBJ_LUPYGAMELIFT_DELTA, (G_OBJ_LUPYGAMELIFT_VRAM - G_OBJ_LUPYGAMELIFT_FILE)

; Overlay: En_Test7 :: Song of Soaring Cutscene Activator
.definelabel G_EN_TEST7_FILE, 0xF55B90
.definelabel G_EN_TEST7_VRAM, 0x80AF0820
.definelabel G_EN_TEST7_DELTA, (G_EN_TEST7_VRAM - G_EN_TEST7_FILE)

; Overlay: Obj_Lightblock :: Dissolvable Light Block
.definelabel G_OBJ_LIGHTBLOCK_FILE, 0xF58C20
.definelabel G_OBJ_LIGHTBLOCK_VRAM, 0x80AF3910
.definelabel G_OBJ_LIGHTBLOCK_DELTA, (G_OBJ_LIGHTBLOCK_VRAM - G_OBJ_LIGHTBLOCK_FILE)

; Overlay: Mir_Ray2 :: Reflectable Ray of Light [?]
.definelabel G_MIR_RAY2_FILE, 0xF59280
.definelabel G_MIR_RAY2_VRAM, 0x80AF3F70
.definelabel G_MIR_RAY2_DELTA, (G_MIR_RAY2_VRAM - G_MIR_RAY2_FILE)

; Overlay: En_Wdhand :: Dexihand
.definelabel G_EN_WDHAND_FILE, 0xF59700
.definelabel G_EN_WDHAND_VRAM, 0x80AF43F0
.definelabel G_EN_WDHAND_DELTA, (G_EN_WDHAND_VRAM - G_EN_WDHAND_FILE)

; Overlay: En_Gamelupy :: Deku Scrub Playground Rupee
.definelabel G_EN_GAMELUPY_FILE, 0xF5BA70
.definelabel G_EN_GAMELUPY_VRAM, 0x80AF6760
.definelabel G_EN_GAMELUPY_DELTA, (G_EN_GAMELUPY_VRAM - G_EN_GAMELUPY_FILE)

; Overlay: Bg_Danpei_Movebg :: Dampé's House Objects
.definelabel G_BG_DANPEI_MOVEBG_FILE, 0xF5C0F0
.definelabel G_BG_DANPEI_MOVEBG_VRAM, 0x80AF6DE0
.definelabel G_BG_DANPEI_MOVEBG_DELTA, (G_BG_DANPEI_MOVEBG_VRAM - G_BG_DANPEI_MOVEBG_FILE)

; Overlay: En_Snowwd :: Snow-Covered Tree
.definelabel G_EN_SNOWWD_FILE, 0xF5C950
.definelabel G_EN_SNOWWD_VRAM, 0x80AF7640
.definelabel G_EN_SNOWWD_DELTA, (G_EN_SNOWWD_VRAM - G_EN_SNOWWD_FILE)

; Overlay: En_Pm :: Postman (Delivering Letters)
.definelabel G_EN_PM_FILE, 0xF5CE50
.definelabel G_EN_PM_VRAM, 0x80AF7B40
.definelabel G_EN_PM_DELTA, (G_EN_PM_VRAM - G_EN_PM_FILE)

; Overlay: En_Gakufu :: 2D Music Staff
.definelabel G_EN_GAKUFU_FILE, 0xF61C70
.definelabel G_EN_GAKUFU_VRAM, 0x80AFC960
.definelabel G_EN_GAKUFU_DELTA, (G_EN_GAKUFU_VRAM - G_EN_GAKUFU_FILE)

; Overlay: Elf_Msg4
.definelabel G_ELF_MSG4_FILE, 0xF62690
.definelabel G_ELF_MSG4_VRAM, 0x80AFD380
.definelabel G_ELF_MSG4_DELTA, (G_ELF_MSG4_VRAM - G_ELF_MSG4_FILE)

; Overlay: Elf_Msg5
.definelabel G_ELF_MSG5_FILE, 0xF62CA0
.definelabel G_ELF_MSG5_VRAM, 0x80AFD990
.definelabel G_ELF_MSG5_DELTA, (G_ELF_MSG5_VRAM - G_ELF_MSG5_FILE)

; Overlay: En_Col_Man :: Piece of Heart
.definelabel G_EN_COL_MAN_FILE, 0xF62F50
.definelabel G_EN_COL_MAN_VRAM, 0x80AFDC40
.definelabel G_EN_COL_MAN_DELTA, (G_EN_COL_MAN_VRAM - G_EN_COL_MAN_FILE)

; Overlay: En_Talk_Gibud :: Gibdo (Ikana Well)
.definelabel G_EN_TALK_GIBUD_FILE, 0xF63BB0
.definelabel G_EN_TALK_GIBUD_VRAM, 0x80AFE8A0
.definelabel G_EN_TALK_GIBUD_DELTA, (G_EN_TALK_GIBUD_VRAM - G_EN_TALK_GIBUD_FILE)

; Overlay: En_Giant :: Giant
.definelabel G_EN_GIANT_FILE, 0xF66CA0
.definelabel G_EN_GIANT_VRAM, 0x80B01990
.definelabel G_EN_GIANT_DELTA, (G_EN_GIANT_VRAM - G_EN_GIANT_FILE)

; Overlay: Obj_Snowball :: Large Snowball
.definelabel G_OBJ_SNOWBALL_FILE, 0xF67FE0
.definelabel G_OBJ_SNOWBALL_VRAM, 0x80B02CD0
.definelabel G_OBJ_SNOWBALL_DELTA, (G_OBJ_SNOWBALL_VRAM - G_OBJ_SNOWBALL_FILE)

; Overlay: Boss_Hakugin :: Goht
.definelabel G_BOSS_HAKUGIN_FILE, 0xF6A5A0
.definelabel G_BOSS_HAKUGIN_VRAM, 0x80B05290
.definelabel G_BOSS_HAKUGIN_DELTA, (G_BOSS_HAKUGIN_VRAM - G_BOSS_HAKUGIN_FILE)

; Overlay: En_Gb2 :: Ghost Hut Proprietor & Ikana Guard
.definelabel G_EN_GB2_FILE, 0xF748F0
.definelabel G_EN_GB2_VRAM, 0x80B0F5E0
.definelabel G_EN_GB2_DELTA, (G_EN_GB2_VRAM - G_EN_GB2_FILE)

; Overlay: En_Onpuman :: Monkey Instrument Prompt
.definelabel G_EN_ONPUMAN_FILE, 0xF77170
.definelabel G_EN_ONPUMAN_VRAM, 0x80B11E60
.definelabel G_EN_ONPUMAN_DELTA, (G_EN_ONPUMAN_VRAM - G_EN_ONPUMAN_FILE)

; Overlay: Bg_Tobira01 :: Goron Shrine Gate
.definelabel G_BG_TOBIRA01_FILE, 0xF77740
.definelabel G_BG_TOBIRA01_VRAM, 0x80B12430
.definelabel G_BG_TOBIRA01_DELTA, (G_BG_TOBIRA01_VRAM - G_BG_TOBIRA01_FILE)

; Overlay: En_Tag_Obj
.definelabel G_EN_TAG_OBJ_FILE, 0xF77B80
.definelabel G_EN_TAG_OBJ_VRAM, 0x80B12870
.definelabel G_EN_TAG_OBJ_DELTA, (G_EN_TAG_OBJ_VRAM - G_EN_TAG_OBJ_FILE)

; Overlay: Obj_Dhouse :: Dampé's House Facade
.definelabel G_OBJ_DHOUSE_FILE, 0xF77C90
.definelabel G_OBJ_DHOUSE_VRAM, 0x80B12980
.definelabel G_OBJ_DHOUSE_DELTA, (G_OBJ_DHOUSE_VRAM - G_OBJ_DHOUSE_FILE)

; Overlay: Obj_Hakaisi :: Gravestone
.definelabel G_OBJ_HAKAISI_FILE, 0xF79490
.definelabel G_OBJ_HAKAISI_VRAM, 0x80B14180
.definelabel G_OBJ_HAKAISI_DELTA, (G_OBJ_HAKAISI_VRAM - G_OBJ_HAKAISI_FILE)

; Overlay: Bg_Hakugin_Switch :: Goron Switch
.definelabel G_BG_HAKUGIN_SWITCH_FILE, 0xF7AAA0
.definelabel G_BG_HAKUGIN_SWITCH_VRAM, 0x80B15790
.definelabel G_BG_HAKUGIN_SWITCH_DELTA, (G_BG_HAKUGIN_SWITCH_VRAM - G_BG_HAKUGIN_SWITCH_FILE)

; Overlay: En_Snowman :: Big & Small Eeno
.definelabel G_EN_SNOWMAN_FILE, 0xF7BE00
.definelabel G_EN_SNOWMAN_VRAM, 0x80B16B00
.definelabel G_EN_SNOWMAN_DELTA, (G_EN_SNOWMAN_VRAM - G_EN_SNOWMAN_FILE)

; Overlay: TG_Sw
.definelabel G_TG_SW_FILE, 0xF7F260
.definelabel G_TG_SW_VRAM, 0x80B19F60
.definelabel G_TG_SW_DELTA, (G_TG_SW_VRAM - G_TG_SW_FILE)

; Overlay: En_Po_Sisters :: Poe Sisters
.definelabel G_EN_PO_SISTERS_FILE, 0xF7F6B0
.definelabel G_EN_PO_SISTERS_VRAM, 0x80B1A3B0
.definelabel G_EN_PO_SISTERS_DELTA, (G_EN_PO_SISTERS_VRAM - G_EN_PO_SISTERS_FILE)

; Overlay: En_Pp :: Hiploop
.definelabel G_EN_PP_FILE, 0xF831B0
.definelabel G_EN_PP_VRAM, 0x80B1DEB0
.definelabel G_EN_PP_DELTA, (G_EN_PP_VRAM - G_EN_PP_FILE)

; Overlay: En_Hakurock :: Goht Debris
.definelabel G_EN_HAKUROCK_FILE, 0xF86E00
.definelabel G_EN_HAKUROCK_VRAM, 0x80B21B00
.definelabel G_EN_HAKUROCK_DELTA, (G_EN_HAKUROCK_VRAM - G_EN_HAKUROCK_FILE)

; Overlay: En_Hanabi :: Fireworks
.definelabel G_EN_HANABI_FILE, 0xF87F00
.definelabel G_EN_HANABI_VRAM, 0x80B22C00
.definelabel G_EN_HANABI_DELTA, (G_EN_HANABI_VRAM - G_EN_HANABI_FILE)

; Overlay: Obj_Dowsing
.definelabel G_OBJ_DOWSING_FILE, 0xF89050
.definelabel G_OBJ_DOWSING_VRAM, 0x80B23D50
.definelabel G_OBJ_DOWSING_DELTA, (G_OBJ_DOWSING_VRAM - G_OBJ_DOWSING_FILE)

; Overlay: Obj_Wind :: Wind Funnel
.definelabel G_OBJ_WIND_FILE, 0xF891D0
.definelabel G_OBJ_WIND_VRAM, 0x80B23ED0
.definelabel G_OBJ_WIND_DELTA, (G_OBJ_WIND_VRAM - G_OBJ_WIND_FILE)

; Overlay: En_Racedog :: Dog (Doggie Racetrack)
.definelabel G_EN_RACEDOG_FILE, 0xF89930
.definelabel G_EN_RACEDOG_VRAM, 0x80B24630
.definelabel G_EN_RACEDOG_DELTA, (G_EN_RACEDOG_VRAM - G_EN_RACEDOG_FILE)

; Overlay: En_Kendo_Js :: Swordsman
.definelabel G_EN_KENDO_JS_FILE, 0xF8B5A0
.definelabel G_EN_KENDO_JS_VRAM, 0x80B262A0
.definelabel G_EN_KENDO_JS_DELTA, (G_EN_KENDO_JS_VRAM - G_EN_KENDO_JS_FILE)

; Overlay: Bg_Botihasira :: Captain Keeta Race Gatepost
.definelabel G_BG_BOTIHASIRA_FILE, 0xF8D380
.definelabel G_BG_BOTIHASIRA_VRAM, 0x80B28080
.definelabel G_BG_BOTIHASIRA_DELTA, (G_BG_BOTIHASIRA_VRAM - G_BG_BOTIHASIRA_FILE)

; Overlay: En_Fish2 :: Marine Research Lab Fish
.definelabel G_EN_FISH2_FILE, 0xF8D670
.definelabel G_EN_FISH2_VRAM, 0x80B28370
.definelabel G_EN_FISH2_DELTA, (G_EN_FISH2_VRAM - G_EN_FISH2_FILE)

; Overlay: En_Pst :: Postbox
.definelabel G_EN_PST_FILE, 0xF90B30
.definelabel G_EN_PST_VRAM, 0x80B2B830
.definelabel G_EN_PST_DELTA, (G_EN_PST_VRAM - G_EN_PST_FILE)

; Overlay: En_Poh :: Poe
.definelabel G_EN_POH_FILE, 0xF919F0
.definelabel G_EN_POH_VRAM, 0x80B2C6F0
.definelabel G_EN_POH_DELTA, (G_EN_POH_VRAM - G_EN_POH_FILE)

; Overlay: Obj_Spidertent :: Tent-Shaped Spider Web
.definelabel G_OBJ_SPIDERTENT_FILE, 0xF94E10
.definelabel G_OBJ_SPIDERTENT_VRAM, 0x80B2FB10
.definelabel G_OBJ_SPIDERTENT_DELTA, (G_OBJ_SPIDERTENT_VRAM - G_OBJ_SPIDERTENT_FILE)

; Overlay: En_Zoraegg :: Zora Egg
.definelabel G_EN_ZORAEGG_FILE, 0xF96890
.definelabel G_EN_ZORAEGG_VRAM, 0x80B31590
.definelabel G_EN_ZORAEGG_DELTA, (G_EN_ZORAEGG_VRAM - G_EN_ZORAEGG_FILE)

; Overlay: En_Kbt :: Zubora
.definelabel G_EN_KBT_FILE, 0xF99030
.definelabel G_EN_KBT_VRAM, 0x80B33D30
.definelabel G_EN_KBT_DELTA, (G_EN_KBT_VRAM - G_EN_KBT_FILE)

; Overlay: En_Gg :: Darmani's Ghost I [?]
.definelabel G_EN_GG_FILE, 0xF9A270
.definelabel G_EN_GG_VRAM, 0x80B34F70
.definelabel G_EN_GG_DELTA, (G_EN_GG_VRAM - G_EN_GG_FILE)

; Overlay: En_Maruta :: Swordsman's School Practice Log
.definelabel G_EN_MARUTA_FILE, 0xF9C380
.definelabel G_EN_MARUTA_VRAM, 0x80B37080
.definelabel G_EN_MARUTA_DELTA, (G_EN_MARUTA_VRAM - G_EN_MARUTA_FILE)

; Overlay: Obj_Snowball2 :: Small Snowball
.definelabel G_OBJ_SNOWBALL2_FILE, 0xF9E120
.definelabel G_OBJ_SNOWBALL2_VRAM, 0x80B38E20
.definelabel G_OBJ_SNOWBALL2_DELTA, (G_OBJ_SNOWBALL2_VRAM - G_OBJ_SNOWBALL2_FILE)

; Overlay: En_Gg2 :: Darmani's Ghost II [?]
.definelabel G_EN_GG2_FILE, 0xF9FF50
.definelabel G_EN_GG2_VRAM, 0x80B3AC50
.definelabel G_EN_GG2_DELTA, (G_EN_GG2_VRAM - G_EN_GG2_FILE)

; Overlay: Obj_Ghaka :: Darmani's Gravestone
.definelabel G_OBJ_GHAKA_FILE, 0xFA1560
.definelabel G_OBJ_GHAKA_VRAM, 0x80B3C260
.definelabel G_OBJ_GHAKA_DELTA, (G_OBJ_GHAKA_VRAM - G_OBJ_GHAKA_FILE)

; Overlay: En_Dnp :: Deku Princess
.definelabel G_EN_DNP_FILE, 0xFA1D20
.definelabel G_EN_DNP_VRAM, 0x80B3CA20
.definelabel G_EN_DNP_DELTA, (G_EN_DNP_VRAM - G_EN_DNP_FILE)

; Overlay: En_Dai :: Biggoron
.definelabel G_EN_DAI_FILE, 0xFA32F0
.definelabel G_EN_DAI_VRAM, 0x80B3DFF0
.definelabel G_EN_DAI_DELTA, (G_EN_DAI_VRAM - G_EN_DAI_FILE)

; Overlay: Bg_Goron_Oyu :: Hot Spring Water
.definelabel G_BG_GORON_OYU_FILE, 0xFA5380
.definelabel G_BG_GORON_OYU_VRAM, 0x80B40080
.definelabel G_BG_GORON_OYU_DELTA, (G_BG_GORON_OYU_VRAM - G_BG_GORON_OYU_FILE)

; Overlay: En_Kgy :: Gabora
.definelabel G_EN_KGY_FILE, 0xFA5B00
.definelabel G_EN_KGY_VRAM, 0x80B40800
.definelabel G_EN_KGY_DELTA, (G_EN_KGY_VRAM - G_EN_KGY_FILE)

; Overlay: En_Invadepoh
.definelabel G_EN_INVADEPOH_FILE, 0xFA8CB0
.definelabel G_EN_INVADEPOH_VRAM, 0x80B439B0
.definelabel G_EN_INVADEPOH_DELTA, (G_EN_INVADEPOH_VRAM - G_EN_INVADEPOH_FILE)

; Overlay: En_Gk :: Goron Elder's Son
.definelabel G_EN_GK_FILE, 0xFB55A0
.definelabel G_EN_GK_VRAM, 0x80B50410
.definelabel G_EN_GK_DELTA, (G_EN_GK_VRAM - G_EN_GK_FILE)

; Overlay: En_An :: Anju (Gameplay)
.definelabel G_EN_AN_FILE, 0xFB89D0
.definelabel G_EN_AN_VRAM, 0x80B53840
.definelabel G_EN_AN_DELTA, (G_EN_AN_VRAM - G_EN_AN_FILE)

; Overlay: En_Bee :: Giant Bee (Gameplay)
.definelabel G_EN_BEE_FILE, 0xFBF8B0
.definelabel G_EN_BEE_VRAM, 0x80B5A720
.definelabel G_EN_BEE_DELTA, (G_EN_BEE_VRAM - G_EN_BEE_FILE)

; Overlay: En_Ot :: Seahorse
.definelabel G_EN_OT_FILE, 0xFC0470
.definelabel G_EN_OT_VRAM, 0x80B5B2E0
.definelabel G_EN_OT_DELTA, (G_EN_OT_VRAM - G_EN_OT_FILE)

; Overlay: En_Dragon :: Deep Python
.definelabel G_EN_DRAGON_FILE, 0xFC3A10
.definelabel G_EN_DRAGON_VRAM, 0x80B5E890
.definelabel G_EN_DRAGON_DELTA, (G_EN_DRAGON_VRAM - G_EN_DRAGON_FILE)

; Overlay: Obj_Dora :: Swordsman's School Gong
.definelabel G_OBJ_DORA_FILE, 0xFC5C50
.definelabel G_OBJ_DORA_VRAM, 0x80B60AD0
.definelabel G_OBJ_DORA_DELTA, (G_OBJ_DORA_VRAM - G_OBJ_DORA_FILE)

; Overlay: En_Bigpo :: Big Poe
.definelabel G_EN_BIGPO_FILE, 0xFC6760
.definelabel G_EN_BIGPO_VRAM, 0x80B615E0
.definelabel G_EN_BIGPO_DELTA, (G_EN_BIGPO_VRAM - G_EN_BIGPO_FILE)

; Overlay: Obj_Kendo_Kanban :: Swordsman's School Wooden Board
.definelabel G_OBJ_KENDO_KANBAN_FILE, 0xFCA640
.definelabel G_OBJ_KENDO_KANBAN_VRAM, 0x80B654C0
.definelabel G_OBJ_KENDO_KANBAN_DELTA, (G_OBJ_KENDO_KANBAN_VRAM - G_OBJ_KENDO_KANBAN_FILE)

; Overlay: Obj_Hariko :: Cow Figurine
.definelabel G_OBJ_HARIKO_FILE, 0xFCBBA0
.definelabel G_OBJ_HARIKO_VRAM, 0x80B66A20
.definelabel G_OBJ_HARIKO_DELTA, (G_OBJ_HARIKO_VRAM - G_OBJ_HARIKO_FILE)

; Overlay: En_Sth
.definelabel G_EN_STH_FILE, 0xFCBEB0
.definelabel G_EN_STH_VRAM, 0x80B66D30
.definelabel G_EN_STH_DELTA, (G_EN_STH_VRAM - G_EN_STH_FILE)

; Overlay: Bg_Sinkai_Kabe
.definelabel G_BG_SINKAI_KABE_FILE, 0xFD27E0
.definelabel G_BG_SINKAI_KABE_VRAM, 0x80B6D660
.definelabel G_BG_SINKAI_KABE_DELTA, (G_BG_SINKAI_KABE_VRAM - G_BG_SINKAI_KABE_FILE)

; Overlay: Bg_Haka_Curtain :: Beneath the Grave Curtain
.definelabel G_BG_HAKA_CURTAIN_FILE, 0xFD2D60
.definelabel G_BG_HAKA_CURTAIN_VRAM, 0x80B6DBE0
.definelabel G_BG_HAKA_CURTAIN_DELTA, (G_BG_HAKA_CURTAIN_VRAM - G_BG_HAKA_CURTAIN_FILE)

; Overlay: Bg_Kin2_Bombwall :: Oceanside Spider House Bombable Wall
.definelabel G_BG_KIN2_BOMBWALL_FILE, 0xFD31A0
.definelabel G_BG_KIN2_BOMBWALL_VRAM, 0x80B6E020
.definelabel G_BG_KIN2_BOMBWALL_DELTA, (G_BG_KIN2_BOMBWALL_VRAM - G_BG_KIN2_BOMBWALL_FILE)

; Overlay: Bg_Kin2_Fence :: Oceanside Spider House Fireplace Grate
.definelabel G_BG_KIN2_FENCE_FILE, 0xFD39A0
.definelabel G_BG_KIN2_FENCE_VRAM, 0x80B6E820
.definelabel G_BG_KIN2_FENCE_DELTA, (G_BG_KIN2_FENCE_VRAM - G_BG_KIN2_FENCE_FILE)

; Overlay: Bg_Kin2_Picture :: Oceanside Spider House Skull Kid Painting
.definelabel G_BG_KIN2_PICTURE_FILE, 0xFD4120
.definelabel G_BG_KIN2_PICTURE_VRAM, 0x80B6EFA0
.definelabel G_BG_KIN2_PICTURE_DELTA, (G_BG_KIN2_PICTURE_VRAM - G_BG_KIN2_PICTURE_FILE)

; Overlay: Bg_Kin2_Shelf :: Oceanside Spider House Drawers & Bookshelf
.definelabel G_BG_KIN2_SHELF_FILE, 0xFD4CB0
.definelabel G_BG_KIN2_SHELF_VRAM, 0x80B6FB30
.definelabel G_BG_KIN2_SHELF_DELTA, (G_BG_KIN2_SHELF_VRAM - G_BG_KIN2_SHELF_FILE)

; Overlay: En_Rail_Skb :: Circle of Stalchildren
.definelabel G_EN_RAIL_SKB_FILE, 0xFD5A40
.definelabel G_EN_RAIL_SKB_VRAM, 0x80B708C0
.definelabel G_EN_RAIL_SKB_DELTA, (G_EN_RAIL_SKB_VRAM - G_EN_RAIL_SKB_FILE)

; Overlay: En_Jg :: Goron Elder
.definelabel G_EN_JG_FILE, 0xFD8C10
.definelabel G_EN_JG_VRAM, 0x80B73A90
.definelabel G_EN_JG_DELTA, (G_EN_JG_VRAM - G_EN_JG_FILE)

; Overlay: En_Tru_Mt :: Koume (Boat Cruise) [?]
.definelabel G_EN_TRU_MT_FILE, 0xFDB1B0
.definelabel G_EN_TRU_MT_VRAM, 0x80B76030
.definelabel G_EN_TRU_MT_DELTA, (G_EN_TRU_MT_VRAM - G_EN_TRU_MT_FILE)

; Overlay: Obj_Um :: Cremia's Cart
.definelabel G_OBJ_UM_FILE, 0xFDC8F0
.definelabel G_OBJ_UM_VRAM, 0x80B77770
.definelabel G_OBJ_UM_DELTA, (G_OBJ_UM_VRAM - G_OBJ_UM_FILE)

; Overlay: En_Neo_Reeba :: Leever
.definelabel G_EN_NEO_REEBA_FILE, 0xFE1A10
.definelabel G_EN_NEO_REEBA_VRAM, 0x80B7C890
.definelabel G_EN_NEO_REEBA_DELTA, (G_EN_NEO_REEBA_VRAM - G_EN_NEO_REEBA_FILE)

; Overlay: Bg_Mbar_Chair :: Milk Bar Chair
.definelabel G_BG_MBAR_CHAIR_FILE, 0xFE3AB0
.definelabel G_BG_MBAR_CHAIR_VRAM, 0x80B7E930
.definelabel G_BG_MBAR_CHAIR_DELTA, (G_BG_MBAR_CHAIR_VRAM - G_BG_MBAR_CHAIR_FILE)

; Overlay: Bg_Ikana_Block
.definelabel G_BG_IKANA_BLOCK_FILE, 0xFE3BE0
.definelabel G_BG_IKANA_BLOCK_VRAM, 0x80B7EA60
.definelabel G_BG_IKANA_BLOCK_DELTA, (G_BG_IKANA_BLOCK_VRAM - G_BG_IKANA_BLOCK_FILE)

; Overlay: Bg_Ikana_Mirror :: Stone Tower Temple Mirror
.definelabel G_BG_IKANA_MIRROR_FILE, 0xFE48B0
.definelabel G_BG_IKANA_MIRROR_VRAM, 0x80B7F730
.definelabel G_BG_IKANA_MIRROR_DELTA, (G_BG_IKANA_MIRROR_VRAM - G_BG_IKANA_MIRROR_FILE)

; Overlay: Bg_Ikana_Rotaryroom :: Stone Tower Temple Rotating Room
.definelabel G_BG_IKANA_ROTARYROOM_FILE, 0xFE5460
.definelabel G_BG_IKANA_ROTARYROOM_VRAM, 0x80B802E0
.definelabel G_BG_IKANA_ROTARYROOM_DELTA, (G_BG_IKANA_ROTARYROOM_VRAM - G_BG_IKANA_ROTARYROOM_FILE)

; Overlay: Bg_Dblue_Balance :: Great Bay Temple See-Saw
.definelabel G_BG_DBLUE_BALANCE_FILE, 0xFE7530
.definelabel G_BG_DBLUE_BALANCE_VRAM, 0x80B823B0
.definelabel G_BG_DBLUE_BALANCE_DELTA, (G_BG_DBLUE_BALANCE_VRAM - G_BG_DBLUE_BALANCE_FILE)

; Overlay: Bg_Dblue_Waterfall :: Great Bay Temple Water Spout
.definelabel G_BG_DBLUE_WATERFALL_FILE, 0xFE8DF0
.definelabel G_BG_DBLUE_WATERFALL_VRAM, 0x80B83C80
.definelabel G_BG_DBLUE_WATERFALL_DELTA, (G_BG_DBLUE_WATERFALL_VRAM - G_BG_DBLUE_WATERFALL_FILE)

; Overlay: En_Kaizoku :: Pirate [?]
.definelabel G_EN_KAIZOKU_FILE, 0xFEA700
.definelabel G_EN_KAIZOKU_VRAM, 0x80B85590
.definelabel G_EN_KAIZOKU_DELTA, (G_EN_KAIZOKU_VRAM - G_EN_KAIZOKU_FILE)

; Overlay: En_Ge2 :: Patrolling Pirate Guard
.definelabel G_EN_GE2_FILE, 0xFF0440
.definelabel G_EN_GE2_VRAM, 0x80B8B2D0
.definelabel G_EN_GE2_DELTA, (G_EN_GE2_VRAM - G_EN_GE2_FILE)

; Overlay: En_Ma_Yts :: Romani II [?]
.definelabel G_EN_MA_YTS_FILE, 0xFF21A0
.definelabel G_EN_MA_YTS_VRAM, 0x80B8D030
.definelabel G_EN_MA_YTS_DELTA, (G_EN_MA_YTS_VRAM - G_EN_MA_YTS_FILE)

; Overlay: En_Ma_Yto :: Cremia
.definelabel G_EN_MA_YTO_FILE, 0xFF3690
.definelabel G_EN_MA_YTO_VRAM, 0x80B8E520
.definelabel G_EN_MA_YTO_DELTA, (G_EN_MA_YTO_VRAM - G_EN_MA_YTO_FILE)

; Overlay: Obj_Tokei_Turret :: South Clock Town Objects
.definelabel G_OBJ_TOKEI_TURRET_FILE, 0xFF6E30
.definelabel G_OBJ_TOKEI_TURRET_VRAM, 0x80B91CC0
.definelabel G_OBJ_TOKEI_TURRET_DELTA, (G_OBJ_TOKEI_TURRET_VRAM - G_OBJ_TOKEI_TURRET_FILE)

; Overlay: Bg_Dblue_Elevator :: Great Bay Temple Elevator
.definelabel G_BG_DBLUE_ELEVATOR_FILE, 0xFF7090
.definelabel G_BG_DBLUE_ELEVATOR_VRAM, 0x80B91F20
.definelabel G_BG_DBLUE_ELEVATOR_DELTA, (G_BG_DBLUE_ELEVATOR_VRAM - G_BG_DBLUE_ELEVATOR_FILE)

; Overlay: Obj_Warpstone :: Owl Statue
.definelabel G_OBJ_WARPSTONE_FILE, 0xFF7C80
.definelabel G_OBJ_WARPSTONE_VRAM, 0x80B92B10
.definelabel G_OBJ_WARPSTONE_DELTA, (G_OBJ_WARPSTONE_VRAM - G_OBJ_WARPSTONE_FILE)

; Overlay: En_Zog :: Mikau
.definelabel G_EN_ZOG_FILE, 0xFF8480
.definelabel G_EN_ZOG_VRAM, 0x80B93310
.definelabel G_EN_ZOG_DELTA, (G_EN_ZOG_VRAM - G_EN_ZOG_FILE)

; Overlay: Obj_Rotlift :: Deku Moon Trial Rotating Platform
.definelabel G_OBJ_ROTLIFT_FILE, 0xFFAF80
.definelabel G_OBJ_ROTLIFT_VRAM, 0x80B95E20
.definelabel G_OBJ_ROTLIFT_DELTA, (G_OBJ_ROTLIFT_VRAM - G_OBJ_ROTLIFT_FILE)

; Overlay: Obj_Jg_Gakki :: Goron Elder's Drum
.definelabel G_OBJ_JG_GAKKI_FILE, 0xFFB340
.definelabel G_OBJ_JG_GAKKI_VRAM, 0x80B961E0
.definelabel G_OBJ_JG_GAKKI_DELTA, (G_OBJ_JG_GAKKI_VRAM - G_OBJ_JG_GAKKI_FILE)

; Overlay: Bg_Inibs_Movebg :: Twinmold's Lair Objects [?]
.definelabel G_BG_INIBS_MOVEBG_FILE, 0xFFB570
.definelabel G_BG_INIBS_MOVEBG_VRAM, 0x80B96410
.definelabel G_BG_INIBS_MOVEBG_DELTA, (G_BG_INIBS_MOVEBG_VRAM - G_BG_INIBS_MOVEBG_FILE)

; Overlay: En_Zot :: Zora (Land)
.definelabel G_EN_ZOT_FILE, 0xFFB730
.definelabel G_EN_ZOT_VRAM, 0x80B965D0
.definelabel G_EN_ZOT_DELTA, (G_EN_ZOT_VRAM - G_EN_ZOT_FILE)

; Overlay: Obj_Tree :: Fork-Branched Tree
.definelabel G_OBJ_TREE_FILE, 0xFFF210
.definelabel G_OBJ_TREE_VRAM, 0x80B9A0B0
.definelabel G_OBJ_TREE_DELTA, (G_OBJ_TREE_VRAM - G_OBJ_TREE_FILE)

; Overlay: Obj_Y2lift :: Pirates' Fortress Mesh Elevator
.definelabel G_OBJ_Y2LIFT_FILE, 0xFFF7B0
.definelabel G_OBJ_Y2LIFT_VRAM, 0x80B9A650
.definelabel G_OBJ_Y2LIFT_DELTA, (G_OBJ_Y2LIFT_VRAM - G_OBJ_Y2LIFT_FILE)

; Overlay: Obj_Y2shutter :: Pirates' Fortress Interior Door
.definelabel G_OBJ_Y2SHUTTER_FILE, 0xFFFAE0
.definelabel G_OBJ_Y2SHUTTER_VRAM, 0x80B9A980
.definelabel G_OBJ_Y2SHUTTER_DELTA, (G_OBJ_Y2SHUTTER_VRAM - G_OBJ_Y2SHUTTER_FILE)

; Overlay: Obj_Boat :: Pirates' Fortress Boat
.definelabel G_OBJ_BOAT_FILE, 0x10000B0
.definelabel G_OBJ_BOAT_VRAM, 0x80B9AF50
.definelabel G_OBJ_BOAT_DELTA, (G_OBJ_BOAT_VRAM - G_OBJ_BOAT_FILE)

; Overlay: Obj_Taru :: Barrel
.definelabel G_OBJ_TARU_FILE, 0x1000840
.definelabel G_OBJ_TARU_VRAM, 0x80B9B6E0
.definelabel G_OBJ_TARU_DELTA, (G_OBJ_TARU_VRAM - G_OBJ_TARU_FILE)

; Overlay: Obj_Hunsui :: Geyser
.definelabel G_OBJ_HUNSUI_FILE, 0x10015B0
.definelabel G_OBJ_HUNSUI_VRAM, 0x80B9C450
.definelabel G_OBJ_HUNSUI_DELTA, (G_OBJ_HUNSUI_VRAM - G_OBJ_HUNSUI_FILE)

; Overlay: En_Jc_Mato :: Boat Cruise Target
.definelabel G_EN_JC_MATO_FILE, 0x1003030
.definelabel G_EN_JC_MATO_VRAM, 0x80B9DEE0
.definelabel G_EN_JC_MATO_DELTA, (G_EN_JC_MATO_VRAM - G_EN_JC_MATO_FILE)

; Overlay: Mir_Ray3 :: Reflectable Ray of Light [?]
.definelabel G_MIR_RAY3_FILE, 0x1003410
.definelabel G_MIR_RAY3_VRAM, 0x80B9E2C0
.definelabel G_MIR_RAY3_DELTA, (G_MIR_RAY3_VRAM - G_MIR_RAY3_FILE)

; Overlay: En_Zob :: Japas
.definelabel G_EN_ZOB_FILE, 0x10046C0
.definelabel G_EN_ZOB_VRAM, 0x80B9F570
.definelabel G_EN_ZOB_DELTA, (G_EN_ZOB_VRAM - G_EN_ZOB_FILE)

; Overlay: Elf_Msg6
.definelabel G_ELF_MSG6_FILE, 0x10066F0
.definelabel G_ELF_MSG6_VRAM, 0x80BA15A0
.definelabel G_ELF_MSG6_DELTA, (G_ELF_MSG6_VRAM - G_ELF_MSG6_FILE)

; Overlay: Obj_Nozoki
.definelabel G_OBJ_NOZOKI_FILE, 0x1007570
.definelabel G_OBJ_NOZOKI_VRAM, 0x80BA2420
.definelabel G_OBJ_NOZOKI_DELTA, (G_OBJ_NOZOKI_VRAM - G_OBJ_NOZOKI_FILE)

; Overlay: En_Toto :: Toto
.definelabel G_EN_TOTO_FILE, 0x1008800
.definelabel G_EN_TOTO_VRAM, 0x80BA36C0
.definelabel G_EN_TOTO_DELTA, (G_EN_TOTO_VRAM - G_EN_TOTO_FILE)

; Overlay: En_Railgibud :: Gibdo (Ikana Canyon)
.definelabel G_EN_RAILGIBUD_FILE, 0x100A540
.definelabel G_EN_RAILGIBUD_VRAM, 0x80BA5400
.definelabel G_EN_RAILGIBUD_DELTA, (G_EN_RAILGIBUD_VRAM - G_EN_RAILGIBUD_FILE)

; Overlay: En_Baba :: Bomb Shop Proprietor's Mother
.definelabel G_EN_BABA_FILE, 0x100D960
.definelabel G_EN_BABA_VRAM, 0x80BA8820
.definelabel G_EN_BABA_DELTA, (G_EN_BABA_VRAM - G_EN_BABA_FILE)

; Overlay: En_Suttari :: Sakon
.definelabel G_EN_SUTTARI_FILE, 0x100F810
.definelabel G_EN_SUTTARI_VRAM, 0x80BAA6D0
.definelabel G_EN_SUTTARI_DELTA, (G_EN_SUTTARI_VRAM - G_EN_SUTTARI_FILE)

; Overlay: En_Zod :: Tijo
.definelabel G_EN_ZOD_FILE, 0x10140B0
.definelabel G_EN_ZOD_VRAM, 0x80BAEF70
.definelabel G_EN_ZOD_DELTA, (G_EN_ZOD_VRAM - G_EN_ZOD_FILE)

; Overlay: En_Kujiya :: Lottery Shop Kiosk
.definelabel G_EN_KUJIYA_FILE, 0x1015A20
.definelabel G_EN_KUJIYA_VRAM, 0x80BB08E0
.definelabel G_EN_KUJIYA_DELTA, (G_EN_KUJIYA_VRAM - G_EN_KUJIYA_FILE)

; Overlay: En_Geg :: Don Gero
.definelabel G_EN_GEG_FILE, 0x1016810
.definelabel G_EN_GEG_VRAM, 0x80BB16D0
.definelabel G_EN_GEG_DELTA, (G_EN_GEG_VRAM - G_EN_GEG_FILE)

; Overlay: Obj_Kinoko :: Mushroom Scent Cloud
.definelabel G_OBJ_KINOKO_FILE, 0x1019840
.definelabel G_OBJ_KINOKO_VRAM, 0x80BB4700
.definelabel G_OBJ_KINOKO_DELTA, (G_OBJ_KINOKO_VRAM - G_OBJ_KINOKO_FILE)

; Overlay: Obj_Yasi :: Palm Tree
.definelabel G_OBJ_YASI_FILE, 0x1019C30
.definelabel G_OBJ_YASI_VRAM, 0x80BB4AF0
.definelabel G_OBJ_YASI_DELTA, (G_OBJ_YASI_VRAM - G_OBJ_YASI_FILE)

; Overlay: En_Tanron1 :: Swarm of Moths
.definelabel G_EN_TANRON1_FILE, 0x1019F40
.definelabel G_EN_TANRON1_VRAM, 0x80BB4E00
.definelabel G_EN_TANRON1_DELTA, (G_EN_TANRON1_VRAM - G_EN_TANRON1_FILE)

; Overlay: En_Tanron2 :: Wart's Bubbles
.definelabel G_EN_TANRON2_FILE, 0x101B910
.definelabel G_EN_TANRON2_VRAM, 0x80BB67D0
.definelabel G_EN_TANRON2_DELTA, (G_EN_TANRON2_VRAM - G_EN_TANRON2_FILE)

; Overlay: En_Tanron3 :: Gyorg's Fish
.definelabel G_EN_TANRON3_FILE, 0x101D590
.definelabel G_EN_TANRON3_VRAM, 0x80BB85A0
.definelabel G_EN_TANRON3_DELTA, (G_EN_TANRON3_VRAM - G_EN_TANRON3_FILE)

; Overlay: Obj_Chan :: Goron Village Chandelier
.definelabel G_OBJ_CHAN_FILE, 0x101E8D0
.definelabel G_OBJ_CHAN_VRAM, 0x80BB98E0
.definelabel G_OBJ_CHAN_DELTA, (G_OBJ_CHAN_VRAM - G_OBJ_CHAN_FILE)

; Overlay: En_Zos :: Evan
.definelabel G_EN_ZOS_FILE, 0x101FC80
.definelabel G_EN_ZOS_VRAM, 0x80BBACA0
.definelabel G_EN_ZOS_DELTA, (G_EN_ZOS_VRAM - G_EN_ZOS_FILE)

; Overlay: En_S_Goro :: Goron (Goron Shrine & Bomb Shop) [?]
.definelabel G_EN_S_GORO_FILE, 0x1021A60
.definelabel G_EN_S_GORO_VRAM, 0x80BBCA80
.definelabel G_EN_S_GORO_DELTA, (G_EN_S_GORO_VRAM - G_EN_S_GORO_FILE)

; Overlay: En_Nb :: Anju's Grandmother (Gameplay)
.definelabel G_EN_NB_FILE, 0x1024D90
.definelabel G_EN_NB_VRAM, 0x80BBFDB0
.definelabel G_EN_NB_DELTA, (G_EN_NB_VRAM - G_EN_NB_FILE)

; Overlay: En_Ja :: Jugglers
.definelabel G_EN_JA_FILE, 0x10268E0
.definelabel G_EN_JA_VRAM, 0x80BC1900
.definelabel G_EN_JA_DELTA, (G_EN_JA_VRAM - G_EN_JA_FILE)

; Overlay: Bg_F40_Block :: Stone Tower Temple Shifting Block
.definelabel G_BG_F40_BLOCK_FILE, 0x1028960
.definelabel G_BG_F40_BLOCK_VRAM, 0x80BC3980
.definelabel G_BG_F40_BLOCK_DELTA, (G_BG_F40_BLOCK_VRAM - G_BG_F40_BLOCK_FILE)

; Overlay: Bg_F40_Switch :: Elegy Statue Switch
.definelabel G_BG_F40_SWITCH_FILE, 0x1029790
.definelabel G_BG_F40_SWITCH_VRAM, 0x80BC47B0
.definelabel G_BG_F40_SWITCH_DELTA, (G_BG_F40_SWITCH_VRAM - G_BG_F40_SWITCH_FILE)

; Overlay: En_Po_Composer :: Composer Brothers
.definelabel G_EN_PO_COMPOSER_FILE, 0x1029F00
.definelabel G_EN_PO_COMPOSER_VRAM, 0x80BC4F30
.definelabel G_EN_PO_COMPOSER_DELTA, (G_EN_PO_COMPOSER_VRAM - G_EN_PO_COMPOSER_FILE)

; Overlay: En_Guruguru :: Guru-Guru
.definelabel G_EN_GURUGURU_FILE, 0x102BBC0
.definelabel G_EN_GURUGURU_VRAM, 0x80BC6BF0
.definelabel G_EN_GURUGURU_DELTA, (G_EN_GURUGURU_VRAM - G_EN_GURUGURU_FILE)

; Overlay: Oceff_Wipe5 :: Sonata of Awakening Effect
.definelabel G_OCEFF_WIPE5_FILE, 0x102CAA0
.definelabel G_OCEFF_WIPE5_VRAM, 0x80BC7AD0
.definelabel G_OCEFF_WIPE5_DELTA, (G_OCEFF_WIPE5_VRAM - G_OCEFF_WIPE5_FILE)

; Overlay: En_Stone_heishi :: Shiro
.definelabel G_EN_STONE_HEISHI_FILE, 0x102E230
.definelabel G_EN_STONE_HEISHI_VRAM, 0x80BC9270
.definelabel G_EN_STONE_HEISHI_DELTA, (G_EN_STONE_HEISHI_VRAM - G_EN_STONE_HEISHI_FILE)

; Overlay: Oceff_Wipe6 :: Song of Soaring Effect
.definelabel G_OCEFF_WIPE6_FILE, 0x102F560
.definelabel G_OCEFF_WIPE6_VRAM, 0x80BCA5A0
.definelabel G_OCEFF_WIPE6_DELTA, (G_OCEFF_WIPE6_VRAM - G_OCEFF_WIPE6_FILE)

; Overlay: En_Scopenuts :: Business Scrub (Observatory Telescope) & Grotto
.definelabel G_EN_SCOPENUTS_FILE, 0x102FBB0
.definelabel G_EN_SCOPENUTS_VRAM, 0x80BCABF0
.definelabel G_EN_SCOPENUTS_DELTA, (G_EN_SCOPENUTS_VRAM - G_EN_SCOPENUTS_FILE)

; Overlay: En_Scopecrow :: Guay (Observatory Telescope)
.definelabel G_EN_SCOPECROW_FILE, 0x1031FC0
.definelabel G_EN_SCOPECROW_VRAM, 0x80BCD000
.definelabel G_EN_SCOPECROW_DELTA, (G_EN_SCOPECROW_VRAM - G_EN_SCOPECROW_FILE)

; Overlay: Oceff_Wipe7 :: Song of Healing Effect
.definelabel G_OCEFF_WIPE7_FILE, 0x1032C70
.definelabel G_OCEFF_WIPE7_VRAM, 0x80BCDCB0
.definelabel G_OCEFF_WIPE7_DELTA, (G_OCEFF_WIPE7_VRAM - G_OCEFF_WIPE7_FILE)

; Overlay: Eff_Kamejima_Wave :: Turtle's Tsunami
.definelabel G_EFF_KAMEJIMA_WAVE_FILE, 0x1033AD0
.definelabel G_EFF_KAMEJIMA_WAVE_VRAM, 0x80BCEB20
.definelabel G_EFF_KAMEJIMA_WAVE_DELTA, (G_EFF_KAMEJIMA_WAVE_VRAM - G_EFF_KAMEJIMA_WAVE_FILE)

; Overlay: En_Hg :: Pamela's Father (Normal)
.definelabel G_EN_HG_FILE, 0x1034170
.definelabel G_EN_HG_VRAM, 0x80BCF1D0
.definelabel G_EN_HG_DELTA, (G_EN_HG_VRAM - G_EN_HG_FILE)

; Overlay: En_Hgo :: Pamela's Father (Cursed)
.definelabel G_EN_HGO_FILE, 0x1035250
.definelabel G_EN_HGO_VRAM, 0x80BD02B0
.definelabel G_EN_HGO_DELTA, (G_EN_HGO_VRAM - G_EN_HGO_FILE)

; Overlay: En_Zov :: Lulu
.definelabel G_EN_ZOV_FILE, 0x1036180
.definelabel G_EN_ZOV_VRAM, 0x80BD11E0
.definelabel G_EN_ZOV_DELTA, (G_EN_ZOV_VRAM - G_EN_ZOV_FILE)

; Overlay: En_Ah :: Anju's Mother (Gameplay)
.definelabel G_EN_AH_FILE, 0x10379D0
.definelabel G_EN_AH_VRAM, 0x80BD2A30
.definelabel G_EN_AH_DELTA, (G_EN_AH_VRAM - G_EN_AH_FILE)

; Overlay: Obj_Hgdoor :: Music Box House Cupboard Doors
.definelabel G_OBJ_HGDOOR_FILE, 0x1039030
.definelabel G_OBJ_HGDOOR_VRAM, 0x80BD4090
.definelabel G_OBJ_HGDOOR_DELTA, (G_OBJ_HGDOOR_VRAM - G_OBJ_HGDOOR_FILE)

; Overlay: Bg_Ikana_Bombwall :: Stone Tower Temple Bombable Floor Tile & Wall
.definelabel G_BG_IKANA_BOMBWALL_FILE, 0x10396C0
.definelabel G_BG_IKANA_BOMBWALL_VRAM, 0x80BD4720
.definelabel G_BG_IKANA_BOMBWALL_DELTA, (G_BG_IKANA_BOMBWALL_VRAM - G_BG_IKANA_BOMBWALL_FILE)

; Overlay: Bg_Ikana_Ray :: Stone Tower Temple Light Ray [?]
.definelabel G_BG_IKANA_RAY_FILE, 0x103A360
.definelabel G_BG_IKANA_RAY_VRAM, 0x80BD53C0
.definelabel G_BG_IKANA_RAY_DELTA, (G_BG_IKANA_RAY_VRAM - G_BG_IKANA_RAY_FILE)

; Overlay: Bg_Ikana_Shutter :: Stone Tower Temple Lattice Door
.definelabel G_BG_IKANA_SHUTTER_FILE, 0x103A630
.definelabel G_BG_IKANA_SHUTTER_VRAM, 0x80BD5690
.definelabel G_BG_IKANA_SHUTTER_DELTA, (G_BG_IKANA_SHUTTER_VRAM - G_BG_IKANA_SHUTTER_FILE)

; Overlay: Bg_Haka_Bombwall :: Beneath the Grave Bombable Wall
.definelabel G_BG_HAKA_BOMBWALL_FILE, 0x103ADA0
.definelabel G_BG_HAKA_BOMBWALL_VRAM, 0x80BD5E00
.definelabel G_BG_HAKA_BOMBWALL_DELTA, (G_BG_HAKA_BOMBWALL_VRAM - G_BG_HAKA_BOMBWALL_FILE)

; Overlay: Bg_Haka_Tomb :: Flat's Tomb
.definelabel G_BG_HAKA_TOMB_FILE, 0x103B520
.definelabel G_BG_HAKA_TOMB_VRAM, 0x80BD6580
.definelabel G_BG_HAKA_TOMB_DELTA, (G_BG_HAKA_TOMB_VRAM - G_BG_HAKA_TOMB_FILE)

; Overlay: En_Sc_Ruppe :: Large Rotating Green Rupee
.definelabel G_EN_SC_RUPPE_FILE, 0x103B8B0
.definelabel G_EN_SC_RUPPE_VRAM, 0x80BD6910
.definelabel G_EN_SC_RUPPE_DELTA, (G_EN_SC_RUPPE_VRAM - G_EN_SC_RUPPE_FILE)

; Overlay: Bg_Iknv_Doukutu :: Sharp's Cave
.definelabel G_BG_IKNV_DOUKUTU_FILE, 0x103BEB0
.definelabel G_BG_IKNV_DOUKUTU_VRAM, 0x80BD6F10
.definelabel G_BG_IKNV_DOUKUTU_DELTA, (G_BG_IKNV_DOUKUTU_VRAM - G_BG_IKNV_DOUKUTU_FILE)

; Overlay: Bg_Iknv_Obj :: Ikana Canyon Objects
.definelabel G_BG_IKNV_OBJ_FILE, 0x103CA50
.definelabel G_BG_IKNV_OBJ_VRAM, 0x80BD7AB0
.definelabel G_BG_IKNV_OBJ_DELTA, (G_BG_IKNV_OBJ_VRAM - G_BG_IKNV_OBJ_FILE)

; Overlay: En_Pamera :: Pamela
.definelabel G_EN_PAMERA_FILE, 0x103D250
.definelabel G_EN_PAMERA_VRAM, 0x80BD82B0
.definelabel G_EN_PAMERA_DELTA, (G_EN_PAMERA_VRAM - G_EN_PAMERA_FILE)

; Overlay: Obj_HsStump :: Hookshot Stump
.definelabel G_OBJ_HSSTUMP_FILE, 0x103F9D0
.definelabel G_OBJ_HSSTUMP_VRAM, 0x80BDAA30
.definelabel G_OBJ_HSSTUMP_DELTA, (G_OBJ_HSSTUMP_VRAM - G_OBJ_HSSTUMP_FILE)

; Overlay: En_Hidden_Nuts :: Mad Scrub (Sleeping)
.definelabel G_EN_HIDDEN_NUTS_FILE, 0x103FFE0
.definelabel G_EN_HIDDEN_NUTS_VRAM, 0x80BDB040
.definelabel G_EN_HIDDEN_NUTS_DELTA, (G_EN_HIDDEN_NUTS_VRAM - G_EN_HIDDEN_NUTS_FILE)

; Overlay: En_Zow :: Zora (Water)
.definelabel G_EN_ZOW_FILE, 0x1041210
.definelabel G_EN_ZOW_VRAM, 0x80BDC270
.definelabel G_EN_ZOW_DELTA, (G_EN_ZOW_VRAM - G_EN_ZOW_FILE)

; Overlay: En_Talk :: Green Check Spot
.definelabel G_EN_TALK_FILE, 0x1042F80
.definelabel G_EN_TALK_VRAM, 0x80BDDFE0
.definelabel G_EN_TALK_DELTA, (G_EN_TALK_VRAM - G_EN_TALK_FILE)

; Overlay: En_Al :: Madame Aroma (Gameplay)
.definelabel G_EN_AL_FILE, 0x1043140
.definelabel G_EN_AL_VRAM, 0x80BDE1A0
.definelabel G_EN_AL_DELTA, (G_EN_AL_VRAM - G_EN_AL_FILE)

; Overlay: En_Tab :: Mr. Barten
.definelabel G_EN_TAB_FILE, 0x1045480
.definelabel G_EN_TAB_VRAM, 0x80BE04E0
.definelabel G_EN_TAB_DELTA, (G_EN_TAB_VRAM - G_EN_TAB_FILE)

; Overlay: En_Nimotsu :: Bomb Shop Bag
.definelabel G_EN_NIMOTSU_FILE, 0x1046C20
.definelabel G_EN_NIMOTSU_VRAM, 0x80BE1C80
.definelabel G_EN_NIMOTSU_DELTA, (G_EN_NIMOTSU_VRAM - G_EN_NIMOTSU_FILE)

; Overlay: En_Hit_Tag
.definelabel G_EN_HIT_TAG_FILE, 0x1046FD0
.definelabel G_EN_HIT_TAG_VRAM, 0x80BE2030
.definelabel G_EN_HIT_TAG_DELTA, (G_EN_HIT_TAG_VRAM - G_EN_HIT_TAG_FILE)

; Overlay: En_Ruppecrow :: Guay (Circling Clock Town)
.definelabel G_EN_RUPPECROW_FILE, 0x1047200
.definelabel G_EN_RUPPECROW_VRAM, 0x80BE2260
.definelabel G_EN_RUPPECROW_DELTA, (G_EN_RUPPECROW_VRAM - G_EN_RUPPECROW_FILE)

; Overlay: En_Tanron4 :: Flock of Seagulls
.definelabel G_EN_TANRON4_FILE, 0x1048B20
.definelabel G_EN_TANRON4_VRAM, 0x80BE3B80
.definelabel G_EN_TANRON4_DELTA, (G_EN_TANRON4_VRAM - G_EN_TANRON4_FILE)

; Overlay: En_Tanron5
.definelabel G_EN_TANRON5_FILE, 0x10498D0
.definelabel G_EN_TANRON5_VRAM, 0x80BE4930
.definelabel G_EN_TANRON5_DELTA, (G_EN_TANRON5_VRAM - G_EN_TANRON5_FILE)

; Overlay: En_Tanron6 :: Swarm of Giant Bees
.definelabel G_EN_TANRON6_FILE, 0x104AFE0
.definelabel G_EN_TANRON6_VRAM, 0x80BE6040
.definelabel G_EN_TANRON6_DELTA, (G_EN_TANRON6_VRAM - G_EN_TANRON6_FILE)

; Overlay: En_Daiku2 :: Carpenter (Milk Road)
.definelabel G_EN_DAIKU2_FILE, 0x104B170
.definelabel G_EN_DAIKU2_VRAM, 0x80BE61D0
.definelabel G_EN_DAIKU2_DELTA, (G_EN_DAIKU2_VRAM - G_EN_DAIKU2_FILE)

; Overlay: En_Muto :: Mutoh (Gameplay)
.definelabel G_EN_MUTO_FILE, 0x104CAA0
.definelabel G_EN_MUTO_VRAM, 0x80BE7B00
.definelabel G_EN_MUTO_DELTA, (G_EN_MUTO_VRAM - G_EN_MUTO_FILE)

; Overlay: En_Baisen :: Viscen
.definelabel G_EN_BAISEN_FILE, 0x104D490
.definelabel G_EN_BAISEN_VRAM, 0x80BE84F0
.definelabel G_EN_BAISEN_DELTA, (G_EN_BAISEN_VRAM - G_EN_BAISEN_FILE)

; Overlay: En_Heishi :: Soldier (Mayor's Residence)
.definelabel G_EN_HEISHI_FILE, 0x104DEC0
.definelabel G_EN_HEISHI_VRAM, 0x80BE8F20
.definelabel G_EN_HEISHI_DELTA, (G_EN_HEISHI_VRAM - G_EN_HEISHI_FILE)

; Overlay: En_Demo_heishi :: Soldier (Cutscenes) I [?]
.definelabel G_EN_DEMO_HEISHI_FILE, 0x104E4B0
.definelabel G_EN_DEMO_HEISHI_VRAM, 0x80BE9510
.definelabel G_EN_DEMO_HEISHI_DELTA, (G_EN_DEMO_HEISHI_VRAM - G_EN_DEMO_HEISHI_FILE)

; Overlay: En_Dt :: Mayor Dotour (Gameplay)
.definelabel G_EN_DT_FILE, 0x104EAC0
.definelabel G_EN_DT_VRAM, 0x80BE9B20
.definelabel G_EN_DT_DELTA, (G_EN_DT_VRAM - G_EN_DT_FILE)

; Overlay: En_Cha :: Laundry Pool Bell [?]
.definelabel G_EN_CHA_FILE, 0x10504C0
.definelabel G_EN_CHA_VRAM, 0x80BEB520
.definelabel G_EN_CHA_DELTA, (G_EN_CHA_VRAM - G_EN_CHA_FILE)

; Overlay: Obj_Dinner :: Cremia & Romani's Dinner
.definelabel G_OBJ_DINNER_FILE, 0x10508E0
.definelabel G_OBJ_DINNER_VRAM, 0x80BEB940
.definelabel G_OBJ_DINNER_DELTA, (G_OBJ_DINNER_VRAM - G_OBJ_DINNER_FILE)

; Overlay: Eff_Lastday :: Moon Fall Effects
.definelabel G_EFF_LASTDAY_FILE, 0x1050A60
.definelabel G_EFF_LASTDAY_VRAM, 0x80BEBAC0
.definelabel G_EFF_LASTDAY_DELTA, (G_EFF_LASTDAY_VRAM - G_EFF_LASTDAY_FILE)

; Overlay: Bg_Ikana_Dharma :: Ancient Castle of Ikana Punchable Pillar Segments
.definelabel G_BG_IKANA_DHARMA_FILE, 0x10511E0
.definelabel G_BG_IKANA_DHARMA_VRAM, 0x80BEC240
.definelabel G_BG_IKANA_DHARMA_DELTA, (G_BG_IKANA_DHARMA_VRAM - G_BG_IKANA_DHARMA_FILE)

; Overlay: En_Akindonuts :: Business Scrub (Burrowed) [?]
.definelabel G_EN_AKINDONUTS_FILE, 0x1051B70
.definelabel G_EN_AKINDONUTS_VRAM, 0x80BECBE0
.definelabel G_EN_AKINDONUTS_DELTA, (G_EN_AKINDONUTS_VRAM - G_EN_AKINDONUTS_FILE)

; Overlay: Eff_Stk :: Skull Kid Moon-Summoning Effects [?]
.definelabel G_EFF_STK_FILE, 0x1055D20
.definelabel G_EFF_STK_VRAM, 0x80BF0D90
.definelabel G_EFF_STK_DELTA, (G_EFF_STK_VRAM - G_EFF_STK_FILE)

; Overlay: En_Ig :: Link the Goron
.definelabel G_EN_IG_FILE, 0x10560E0
.definelabel G_EN_IG_VRAM, 0x80BF1150
.definelabel G_EN_IG_DELTA, (G_EN_IG_VRAM - G_EN_IG_FILE)

; Overlay: En_Rg :: Goron (Goron Racetrack)
.definelabel G_EN_RG_FILE, 0x10588B0
.definelabel G_EN_RG_VRAM, 0x80BF3920
.definelabel G_EN_RG_DELTA, (G_EN_RG_VRAM - G_EN_RG_FILE)

; Overlay: En_Osk :: Igos du Ikana & Henchmen's Heads
.definelabel G_EN_OSK_FILE, 0x105ABA0
.definelabel G_EN_OSK_VRAM, 0x80BF5C20
.definelabel G_EN_OSK_DELTA, (G_EN_OSK_VRAM - G_EN_OSK_FILE)

; Overlay: En_Sth2
.definelabel G_EN_STH2_FILE, 0x105C460
.definelabel G_EN_STH2_VRAM, 0x80BF74E0
.definelabel G_EN_STH2_DELTA, (G_EN_STH2_VRAM - G_EN_STH2_FILE)

; Overlay: En_Yb :: Kamaro
.definelabel G_EN_YB_FILE, 0x105F080
.definelabel G_EN_YB_VRAM, 0x80BFA100
.definelabel G_EN_YB_DELTA, (G_EN_YB_VRAM - G_EN_YB_FILE)

; Overlay: En_Rz :: Rosa Sisters
.definelabel G_EN_RZ_FILE, 0x1060400
.definelabel G_EN_RZ_VRAM, 0x80BFB480
.definelabel G_EN_RZ_DELTA, (G_EN_RZ_VRAM - G_EN_RZ_FILE)

; Overlay: En_Scopecoin
.definelabel G_EN_SCOPECOIN_FILE, 0x1061F20
.definelabel G_EN_SCOPECOIN_VRAM, 0x80BFCFA0
.definelabel G_EN_SCOPECOIN_DELTA, (G_EN_SCOPECOIN_VRAM - G_EN_SCOPECOIN_FILE)

; Overlay: En_Bjt :: Hand in Toilet
.definelabel G_EN_BJT_FILE, 0x1062260
.definelabel G_EN_BJT_VRAM, 0x80BFD2E0
.definelabel G_EN_BJT_DELTA, (G_EN_BJT_VRAM - G_EN_BJT_FILE)

; Overlay: En_Bomjima :: Jim I [?]
.definelabel G_EN_BOMJIMA_FILE, 0x10630F0
.definelabel G_EN_BOMJIMA_VRAM, 0x80BFE170
.definelabel G_EN_BOMJIMA_DELTA, (G_EN_BOMJIMA_VRAM - G_EN_BOMJIMA_FILE)

; Overlay: En_Bomjimb :: Jim II [?]
.definelabel G_EN_BOMJIMB_FILE, 0x1065E20
.definelabel G_EN_BOMJIMB_VRAM, 0x80C00EA0
.definelabel G_EN_BOMJIMB_DELTA, (G_EN_BOMJIMB_VRAM - G_EN_BOMJIMB_FILE)

; Overlay: En_Bombers :: Bomber II [?]
.definelabel G_EN_BOMBERS_FILE, 0x10684B0
.definelabel G_EN_BOMBERS_VRAM, 0x80C03530
.definelabel G_EN_BOMBERS_DELTA, (G_EN_BOMBERS_VRAM - G_EN_BOMBERS_FILE)

; Overlay: En_Bombers2 :: Bomber (Hideout Guard)
.definelabel G_EN_BOMBERS2_FILE, 0x10698B0
.definelabel G_EN_BOMBERS2_VRAM, 0x80C04930
.definelabel G_EN_BOMBERS2_DELTA, (G_EN_BOMBERS2_VRAM - G_EN_BOMBERS2_FILE)

; Overlay: En_Bombal :: Majora Balloon (North Clock Town)
.definelabel G_EN_BOMBAL_FILE, 0x106A9F0
.definelabel G_EN_BOMBAL_VRAM, 0x80C05A70
.definelabel G_EN_BOMBAL_DELTA, (G_EN_BOMBAL_VRAM - G_EN_BOMBAL_FILE)

; Overlay: Obj_Moon_Stone :: Moon's Tear
.definelabel G_OBJ_MOON_STONE_FILE, 0x106B490
.definelabel G_OBJ_MOON_STONE_VRAM, 0x80C06510
.definelabel G_OBJ_MOON_STONE_DELTA, (G_OBJ_MOON_STONE_VRAM - G_OBJ_MOON_STONE_FILE)

; Overlay: Obj_Mu_Pict :: Music Box House Picture Checkspots
.definelabel G_OBJ_MU_PICT_FILE, 0x106BA20
.definelabel G_OBJ_MU_PICT_VRAM, 0x80C06AA0
.definelabel G_OBJ_MU_PICT_DELTA, (G_OBJ_MU_PICT_VRAM - G_OBJ_MU_PICT_FILE)

; Overlay: Bg_Ikninside :: Ancient Castle of Ikana Objects [?]
.definelabel G_BG_IKNINSIDE_FILE, 0x106C090
.definelabel G_BG_IKNINSIDE_VRAM, 0x80C07110
.definelabel G_BG_IKNINSIDE_DELTA, (G_BG_IKNINSIDE_VRAM - G_BG_IKNINSIDE_FILE)

; Overlay: Eff_Zoraband :: Blue Spotlight Effect
.definelabel G_EFF_ZORABAND_FILE, 0x106C6C0
.definelabel G_EFF_ZORABAND_VRAM, 0x80C07740
.definelabel G_EFF_ZORABAND_DELTA, (G_EFF_ZORABAND_VRAM - G_EFF_ZORABAND_FILE)

; Overlay: Obj_Kepn_Koya :: Gorman Track Buildings
.definelabel G_OBJ_KEPN_KOYA_FILE, 0x106CAA0
.definelabel G_OBJ_KEPN_KOYA_VRAM, 0x80C07B20
.definelabel G_OBJ_KEPN_KOYA_DELTA, (G_OBJ_KEPN_KOYA_VRAM - G_OBJ_KEPN_KOYA_FILE)

; Overlay: Obj_Usiyane :: Cow Barn Roof (Exterior)
.definelabel G_OBJ_USIYANE_FILE, 0x106CC00
.definelabel G_OBJ_USIYANE_VRAM, 0x80C07C80
.definelabel G_OBJ_USIYANE_DELTA, (G_OBJ_USIYANE_VRAM - G_OBJ_USIYANE_FILE)

; Overlay: En_Nnh :: Deku Butler's Son
.definelabel G_EN_NNH_FILE, 0x106D6E0
.definelabel G_EN_NNH_VRAM, 0x80C08760
.definelabel G_EN_NNH_DELTA, (G_EN_NNH_VRAM - G_EN_NNH_FILE)

; Overlay: Obj_Kzsaku :: Metal Portcullis
.definelabel G_OBJ_KZSAKU_FILE, 0x106DA00
.definelabel G_OBJ_KZSAKU_VRAM, 0x80C08A80
.definelabel G_OBJ_KZSAKU_DELTA, (G_OBJ_KZSAKU_VRAM - G_OBJ_KZSAKU_FILE)

; Overlay: Obj_Milk_Bin :: Chateau Romani Delivery Bottle
.definelabel G_OBJ_MILK_BIN_FILE, 0x106DDC0
.definelabel G_OBJ_MILK_BIN_VRAM, 0x80C08E40
.definelabel G_OBJ_MILK_BIN_DELTA, (G_OBJ_MILK_BIN_VRAM - G_OBJ_MILK_BIN_FILE)

; Overlay: En_Kitan :: Keaton
.definelabel G_EN_KITAN_FILE, 0x106E050
.definelabel G_EN_KITAN_VRAM, 0x80C090D0
.definelabel G_EN_KITAN_DELTA, (G_EN_KITAN_VRAM - G_EN_KITAN_FILE)

; Overlay: Bg_Astr_Bombwall :: Astral Observatory Bombable Wall
.definelabel G_BG_ASTR_BOMBWALL_FILE, 0x106EE50
.definelabel G_BG_ASTR_BOMBWALL_VRAM, 0x80C09ED0
.definelabel G_BG_ASTR_BOMBWALL_DELTA, (G_BG_ASTR_BOMBWALL_VRAM - G_BG_ASTR_BOMBWALL_FILE)

; Overlay: Bg_Iknin_Susceil :: Hot Checkered Ceiling [?]
.definelabel G_BG_IKNIN_SUSCEIL_FILE, 0x106F6C0
.definelabel G_BG_IKNIN_SUSCEIL_VRAM, 0x80C0A740
.definelabel G_BG_IKNIN_SUSCEIL_DELTA, (G_BG_IKNIN_SUSCEIL_VRAM - G_BG_IKNIN_SUSCEIL_FILE)

; Overlay: En_Bsb :: Captain Keeta
.definelabel G_EN_BSB_FILE, 0x1070210
.definelabel G_EN_BSB_VRAM, 0x80C0B290
.definelabel G_EN_BSB_DELTA, (G_EN_BSB_VRAM - G_EN_BSB_FILE)

; Overlay: En_Recepgirl :: Mayor's Receptionist
.definelabel G_EN_RECEPGIRL_FILE, 0x1074F50
.definelabel G_EN_RECEPGIRL_VRAM, 0x80C0FFD0
.definelabel G_EN_RECEPGIRL_DELTA, (G_EN_RECEPGIRL_VRAM - G_EN_RECEPGIRL_FILE)

; Overlay: En_Thiefbird :: Takkuri
.definelabel G_EN_THIEFBIRD_FILE, 0x10756F0
.definelabel G_EN_THIEFBIRD_VRAM, 0x80C10770
.definelabel G_EN_THIEFBIRD_DELTA, (G_EN_THIEFBIRD_VRAM - G_EN_THIEFBIRD_FILE)

; Overlay: En_Jgame_Tsn :: Fisherman (Fisherman's Jumping Game)
.definelabel G_EN_JGAME_TSN_FILE, 0x10788A0
.definelabel G_EN_JGAME_TSN_VRAM, 0x80C13930
.definelabel G_EN_JGAME_TSN_DELTA, (G_EN_JGAME_TSN_VRAM - G_EN_JGAME_TSN_FILE)

; Overlay: Obj_Jgame_Light :: Torch Stand (Fisherman's Jumping Game)
.definelabel G_OBJ_JGAME_LIGHT_FILE, 0x107A260
.definelabel G_OBJ_JGAME_LIGHT_VRAM, 0x80C152F0
.definelabel G_OBJ_JGAME_LIGHT_DELTA, (G_OBJ_JGAME_LIGHT_VRAM - G_OBJ_JGAME_LIGHT_FILE)

; Overlay: Obj_Yado :: Stockpot Inn Window
.definelabel G_OBJ_YADO_FILE, 0x107B150
.definelabel G_OBJ_YADO_VRAM, 0x80C161E0
.definelabel G_OBJ_YADO_DELTA, (G_OBJ_YADO_VRAM - G_OBJ_YADO_FILE)

; Overlay: Demo_Syoten :: Ikana Canyon Curse Lifted Effects
.definelabel G_DEMO_SYOTEN_FILE, 0x107B3E0
.definelabel G_DEMO_SYOTEN_VRAM, 0x80C16480
.definelabel G_DEMO_SYOTEN_DELTA, (G_DEMO_SYOTEN_VRAM - G_DEMO_SYOTEN_FILE)

; Overlay: Demo_Moonend :: Moon (Cutscenes)
.definelabel G_DEMO_MOONEND_FILE, 0x107C970
.definelabel G_DEMO_MOONEND_VRAM, 0x80C17A10
.definelabel G_DEMO_MOONEND_DELTA, (G_DEMO_MOONEND_VRAM - G_DEMO_MOONEND_FILE)

; Overlay: Bg_Lbfshot :: Rainbow Hookshot Pillar
.definelabel G_BG_LBFSHOT_FILE, 0x107D080
.definelabel G_BG_LBFSHOT_VRAM, 0x80C18120
.definelabel G_BG_LBFSHOT_DELTA, (G_BG_LBFSHOT_VRAM - G_BG_LBFSHOT_FILE)

; Overlay: Bg_Last_Bwall :: Link Moon Trial Bombable & Climbable Walls
.definelabel G_BG_LAST_BWALL_FILE, 0x107D1A0
.definelabel G_BG_LAST_BWALL_VRAM, 0x80C18240
.definelabel G_BG_LAST_BWALL_DELTA, (G_BG_LAST_BWALL_VRAM - G_BG_LAST_BWALL_FILE)

; Overlay: En_And :: Anju (Wedding Dress)
.definelabel G_EN_AND_FILE, 0x107DAF0
.definelabel G_EN_AND_VRAM, 0x80C18B90
.definelabel G_EN_AND_DELTA, (G_EN_AND_VRAM - G_EN_AND_FILE)

; Overlay: En_Invadepoh_Demo
.definelabel G_EN_INVADEPOH_DEMO_FILE, 0x107E200
.definelabel G_EN_INVADEPOH_DEMO_VRAM, 0x80C192A0
.definelabel G_EN_INVADEPOH_DEMO_DELTA, (G_EN_INVADEPOH_DEMO_VRAM - G_EN_INVADEPOH_DEMO_FILE)

; Overlay: Obj_Danpeilift :: Deku Shrine & Snowhead Temple Elevator [?]
.definelabel G_OBJ_DANPEILIFT_FILE, 0x107FCA0
.definelabel G_OBJ_DANPEILIFT_VRAM, 0x80C1ADC0
.definelabel G_OBJ_DANPEILIFT_DELTA, (G_OBJ_DANPEILIFT_VRAM - G_OBJ_DANPEILIFT_FILE)

; Overlay: En_Fall2 :: Falling Moon
.definelabel G_EN_FALL2_FILE, 0x1080520
.definelabel G_EN_FALL2_VRAM, 0x80C1B640
.definelabel G_EN_FALL2_DELTA, (G_EN_FALL2_VRAM - G_EN_FALL2_FILE)

; Overlay: Dm_Al :: Madame Aroma (Cutscenes)
.definelabel G_DM_AL_FILE, 0x1080C70
.definelabel G_DM_AL_VRAM, 0x80C1BD90
.definelabel G_DM_AL_DELTA, (G_DM_AL_VRAM - G_DM_AL_FILE)

; Overlay: Dm_An :: Anju Cutscene Animations
.definelabel G_DM_AN_FILE, 0x10812F0
.definelabel G_DM_AN_VRAM, 0x80C1C410
.definelabel G_DM_AN_DELTA, (G_DM_AN_VRAM - G_DM_AN_FILE)

; Overlay: Dm_Ah :: Anju's Mother (Cutscenes)
.definelabel G_DM_AH_FILE, 0x10822F0
.definelabel G_DM_AH_VRAM, 0x80C1D410
.definelabel G_DM_AH_DELTA, (G_DM_AH_VRAM - G_DM_AH_FILE)

; Overlay: Dm_Nb :: Anju's Grandmother (Cutscenes)
.definelabel G_DM_NB_FILE, 0x1082DB0
.definelabel G_DM_NB_VRAM, 0x80C1DED0
.definelabel G_DM_NB_DELTA, (G_DM_NB_VRAM - G_DM_NB_FILE)

; Overlay: En_Drs :: Wedding Dress Mannequin
.definelabel G_EN_DRS_FILE, 0x1083170
.definelabel G_EN_DRS_VRAM, 0x80C1E290
.definelabel G_EN_DRS_DELTA, (G_EN_DRS_VRAM - G_EN_DRS_FILE)

; Overlay: En_Ending_Hero :: Mayor Dotour (Cutscenes)
.definelabel G_EN_ENDING_HERO_FILE, 0x1083570
.definelabel G_EN_ENDING_HERO_VRAM, 0x80C1E690
.definelabel G_EN_ENDING_HERO_DELTA, (G_EN_ENDING_HERO_VRAM - G_EN_ENDING_HERO_FILE)

; Overlay: Dm_Bal :: Tingle (Cutscenes)
.definelabel G_DM_BAL_FILE, 0x10838C0
.definelabel G_DM_BAL_VRAM, 0x80C1E9E0
.definelabel G_DM_BAL_DELTA, (G_DM_BAL_VRAM - G_DM_BAL_FILE)

; Overlay: En_Paper :: Tingle Confetti
.definelabel G_EN_PAPER_FILE, 0x10842B0
.definelabel G_EN_PAPER_VRAM, 0x80C1F3D0
.definelabel G_EN_PAPER_DELTA, (G_EN_PAPER_VRAM - G_EN_PAPER_FILE)

; Overlay: En_Hint_Skb :: Stalchild (Oceanside Spider House)
.definelabel G_EN_HINT_SKB_FILE, 0x1084BD0
.definelabel G_EN_HINT_SKB_VRAM, 0x80C1FCF0
.definelabel G_EN_HINT_SKB_DELTA, (G_EN_HINT_SKB_VRAM - G_EN_HINT_SKB_FILE)

; Overlay: Dm_Tag
.definelabel G_DM_TAG_FILE, 0x1087230
.definelabel G_DM_TAG_VRAM, 0x80C22350
.definelabel G_DM_TAG_DELTA, (G_DM_TAG_VRAM - G_DM_TAG_FILE)

; Overlay: En_Bh :: Brown Bird
.definelabel G_EN_BH_FILE, 0x1087C20
.definelabel G_EN_BH_VRAM, 0x80C22D40
.definelabel G_EN_BH_DELTA, (G_EN_BH_VRAM - G_EN_BH_FILE)

; Overlay: En_Ending_Hero2 :: Viscen (Cutscenes)
.definelabel G_EN_ENDING_HERO2_FILE, 0x1088110
.definelabel G_EN_ENDING_HERO2_VRAM, 0x80C23230
.definelabel G_EN_ENDING_HERO2_DELTA, (G_EN_ENDING_HERO2_VRAM - G_EN_ENDING_HERO2_FILE)

; Overlay: En_Ending_Hero3 :: Mutoh (Cutscenes)
.definelabel G_EN_ENDING_HERO3_FILE, 0x1088340
.definelabel G_EN_ENDING_HERO3_VRAM, 0x80C23460
.definelabel G_EN_ENDING_HERO3_DELTA, (G_EN_ENDING_HERO3_VRAM - G_EN_ENDING_HERO3_FILE)

; Overlay: En_Ending_Hero4 :: Soldier (Cutscenes) II [?]
.definelabel G_EN_ENDING_HERO4_FILE, 0x1088570
.definelabel G_EN_ENDING_HERO4_VRAM, 0x80C23690
.definelabel G_EN_ENDING_HERO4_DELTA, (G_EN_ENDING_HERO4_VRAM - G_EN_ENDING_HERO4_FILE)

; Overlay: En_Ending_Hero5 :: Carpenter (Cutscenes)
.definelabel G_EN_ENDING_HERO5_FILE, 0x10887A0
.definelabel G_EN_ENDING_HERO5_VRAM, 0x80C238C0
.definelabel G_EN_ENDING_HERO5_DELTA, (G_EN_ENDING_HERO5_VRAM - G_EN_ENDING_HERO5_FILE)

; Overlay: En_Ending_Hero6
.definelabel G_EN_ENDING_HERO6_FILE, 0x1088B70
.definelabel G_EN_ENDING_HERO6_VRAM, 0x80C23C90
.definelabel G_EN_ENDING_HERO6_DELTA, (G_EN_ENDING_HERO6_VRAM - G_EN_ENDING_HERO6_FILE)

; Overlay: Dm_Gm
.definelabel G_DM_GM_FILE, 0x1089240
.definelabel G_DM_GM_VRAM, 0x80C24360
.definelabel G_DM_GM_DELTA, (G_DM_GM_VRAM - G_DM_GM_FILE)

; Overlay: Obj_Swprize
.definelabel G_OBJ_SWPRIZE_FILE, 0x108A240
.definelabel G_OBJ_SWPRIZE_VRAM, 0x80C25360
.definelabel G_OBJ_SWPRIZE_DELTA, (G_OBJ_SWPRIZE_VRAM - G_OBJ_SWPRIZE_FILE)

; Overlay: En_Invisible_Ruppe
.definelabel G_EN_INVISIBLE_RUPPE_FILE, 0x108A780
.definelabel G_EN_INVISIBLE_RUPPE_VRAM, 0x80C258A0
.definelabel G_EN_INVISIBLE_RUPPE_DELTA, (G_EN_INVISIBLE_RUPPE_VRAM - G_EN_INVISIBLE_RUPPE_FILE)

; Overlay: Obj_Ending :: Epilogue Cutscene Objects
.definelabel G_OBJ_ENDING_FILE, 0x108AAA0
.definelabel G_OBJ_ENDING_VRAM, 0x80C25BC0
.definelabel G_OBJ_ENDING_DELTA, (G_OBJ_ENDING_VRAM - G_OBJ_ENDING_FILE)

; Overlay: En_Rsn :: Bomb Shop Proprietor
.definelabel G_EN_RSN_FILE, 0x108AC20
.definelabel G_EN_RSN_VRAM, 0x80C25D40
.definelabel G_EN_RSN_DELTA, (G_EN_RSN_VRAM - G_EN_RSN_FILE)
