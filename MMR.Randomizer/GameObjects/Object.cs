using System;
using System.Collections.Generic;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Actor;

namespace MMR.Randomizer.GameObjects
{
    public enum Object
    {
        /// <summary>
        ///  Enum of in-game object list, for ObjectID => FileID
        /// </summary>

        Empty_0 = 0x000,

        [FileID(649)]  // gameplay_keep
        GameplayKeep = 0x001,

        [FileID(650)]  // gameplay_field_keep
        GameplayFieldKeep = 0x002,

        [FileID(651)]  // gameplay_dangeon_keep
        GameplayDangeonKeep = 0x003,

        [FileID(1027)]  // object_nb
        Nb = 0x004,

        [FileID(683)]  // object_okuta
        Okuta = 0x005,

        [FileID(833)]  // object_crow
        Crow = 0x006,

        [FileID(1030)]  // object_ah
        Ah = 0x007,

        [FileID(685)]  // object_dy_obj
        DyObj = 0x008,

        [FileID(684)]  // object_wallmaster
        Wallmaster = 0x009,

        [FileID(687)]  // object_dodongo
        Dodongo = 0x00A,

        [FileID(686)]  // object_firefly
        Firefly = 0x00B,

        [FileID(682)]  // object_box
        Box = 0x00C,

        [FileID(1043)]  // object_al
        Al = 0x00D,

        [FileID(693)]  // object_bubble
        Bubble = 0x00E,

        [FileID(688)]  // object_niw
        Niw = 0x00F,

        [FileID(653)]  // object_link_boy
        LinkBoy = 0x010,

        [FileID(654)]  // object_link_child
        LinkChild = 0x011,

        [FileID(689)]  // object_tite
        Tite = 0x012,

        [FileID(1044)]  // object_tab
        Tab = 0x013,

        [FileID(690)]  // object_ph
        Ph = 0x014,

        [FileID(1094)]  // object_and
        And = 0x015,

        [FileID(1097)]  // object_msmo
        Msmo = 0x016,

        [FileID(691)]  // object_dinofos
        Dinofos = 0x017,

        [FileID(1096)]  // object_drs
        Drs = 0x018,

        [FileID(692)]  // object_zl1
        Zl1 = 0x019,

        [FileID(1098)]  // object_an4
        An4 = 0x01A,

        Empty_1B = 0x01B,

        [FileID(694)]  // object_test3
        Test3 = 0x01C,

        [FileID(695)]  // object_famos
        Famos = 0x01D,

        Empty_1E = 0x01E,

        Empty_1F = 0x01F,

        [FileID(696)]  // object_st
        St = 0x020,

        Empty_21 = 0x021,

        [FileID(697)]  // object_thiefbird
        Thiefbird = 0x022,

        Empty_23 = 0x023,

        Empty_24 = 0x024,

        Empty_25 = 0x025,

        Empty_26 = 0x026,

        Empty_27 = 0x027,

        Empty_28 = 0x028,

        Empty_29 = 0x029,

        [FileID(698)]  // object_bombf
        Bombf = 0x02A,

        Empty_2B = 0x02B,

        Empty_2C = 0x02C,

        Empty_2D = 0x02D,

        Empty_2E = 0x02E,

        Empty_2F = 0x02F,

        [FileID(699)]  // object_am
        Am = 0x030,

        [FileID(700)]  // object_dekubaba
        Dekubaba = 0x031,

        Empty_32 = 0x032,

        Empty_33 = 0x033,

        Empty_34 = 0x034,

        Empty_35 = 0x035,

        Empty_36 = 0x036,

        Empty_37 = 0x037,

        Empty_38 = 0x038,

        Empty_39 = 0x039,

        Empty_3A = 0x03A,

        Empty_3B = 0x03B,

        Empty_3C = 0x03C,

        Empty_3D = 0x03D,

        [FileID(701)]  // object_warp1
        Warp1 = 0x03E,

        [FileID(702)]  // object_b_heart
        BHeart = 0x03F,

        [FileID(703)]  // object_dekunuts
        Dekunuts = 0x040,

        Empty_41 = 0x041,

        Empty_42 = 0x042,

        Empty_43 = 0x043,

        Empty_44 = 0x044,

        Empty_45 = 0x045,

        Empty_46 = 0x046,

        Empty_47 = 0x047,

        Empty_48 = 0x048,

        Empty_49 = 0x049,

        Empty_4A = 0x04A,

        Empty_4B = 0x04B,

        Empty_4C = 0x04C,

        Empty_4D = 0x04D,

        Empty_4E = 0x04E,

        Empty_4F = 0x04F,

        Empty_50 = 0x050,

        [FileID(704)]  // object_bb
        Bb = 0x051,

        [FileID(705)]  // object_death
        Death = 0x052,

        Empty_53 = 0x053,

        Empty_54 = 0x054,

        Empty_55 = 0x055,

        Empty_56 = 0x056,

        Empty_57 = 0x057,

        Empty_58 = 0x058,

        Empty_59 = 0x059,

        Empty_5A = 0x05A,

        Empty_5B = 0x05B,

        [FileID(824)]  // object_f40_obj
        F40Obj = 0x05C,

        [FileID(826)]  // object_po_composer
        PoComposer = 0x05D,

        Empty_5E = 0x05E,

        [FileID(706)]  // object_hata
        Hata = 0x05F,

        Empty_60 = 0x060,

        [FileID(707)]  // object_wood02
        Wood02 = 0x061,

        Empty_62 = 0x062,

        Empty_63 = 0x063,

        [FileID(708)]  // object_trap
        Trap = 0x064,

        Empty_65 = 0x065,

        Empty_66 = 0x066,

        Empty_67 = 0x067,

        Empty_68 = 0x068,

        Empty_69 = 0x069,

        [FileID(709)]  // object_vm
        Vm = 0x06A,

        Empty_6B = 0x06B,

        Empty_6C = 0x06C,

        Empty_6D = 0x06D,

        Empty_6E = 0x06E,

        Empty_6F = 0x06F,

        [FileID(710)]  // object_efc_star_field
        EfcStarField = 0x070,

        Empty_71 = 0x071,

        Empty_72 = 0x072,

        Empty_73 = 0x073,

        Empty_74 = 0x074,

        [FileID(711)]  // object_rd
        Rd = 0x075,

        [FileID(712)]  // object_yukimura_obj
        YukimuraObj = 0x076,

        Empty_77 = 0x077,

        Empty_78 = 0x078,

        Empty_79 = 0x079,

        Empty_7A = 0x07A,

        Empty_7B = 0x07B,

        Empty_7C = 0x07C,

        [FileID(713)]  // object_horse_link_child
        HorseLinkChild = 0x07D,

        Empty_7E = 0x07E,

        Empty_7F = 0x07F,

        [FileID(714)]  // object_syokudai
        Syokudai = 0x080,

        Empty_81 = 0x081,

        Empty_82 = 0x082,

        Empty_83 = 0x083,

        [FileID(715)]  // object_efc_tw
        EfcTw = 0x084,

        Empty_85 = 0x085,

        [FileID(716)]  // object_gi_key
        GiKey = 0x086,

        [FileID(717)]  // object_mir_ray
        MirRay = 0x087,

        [FileID(718)]  // object_ctower_rot
        CtowerRot = 0x088,

        Empty_89 = 0x089,

        [FileID(719)]  // object_bdoor
        Bdoor = 0x08A,

        Empty_8B = 0x08B,

        Empty_8C = 0x08C,

        Empty_8D = 0x08D,

        [FileID(720)]  // object_sb
        Sb = 0x08E,

        [FileID(721)]  // object_gi_melody
        GiMelody = 0x08F,

        [FileID(722)]  // object_gi_heart
        GiHeart = 0x090,

        [FileID(723)]  // object_gi_compass
        GiCompass = 0x091,

        [FileID(724)]  // object_gi_bosskey
        GiBosskey = 0x092,

        Empty_93 = 0x093,

        [FileID(725)]  // object_gi_nuts
        GiNuts = 0x094,

        Empty_95 = 0x095,

        [FileID(726)]  // object_gi_hearts
        GiHearts = 0x096,

        [FileID(727)]  // object_gi_arrowcase
        GiArrowcase = 0x097,

        [FileID(728)]  // object_gi_bombpouch
        GiBombpouch = 0x098,

        [FileID(729)]  // object_in
        In = 0x099,

        Empty_9A = 0x09A,

        Empty_9B = 0x09B,

        Empty_9C = 0x09C,

        [FileID(730)]  // object_os_anime
        OsAnime = 0x09D,

        [FileID(731)]  // object_gi_bottle
        GiBottle = 0x09E,

        [FileID(732)]  // object_gi_stick
        GiStick = 0x09F,

        [FileID(733)]  // object_gi_map
        GiMap = 0x0A0,

        [FileID(734)]  // object_oF1d_map
        Of1dMap = 0x0A1,

        [FileID(735)]  // object_ru2
        Ru2 = 0x0A2,

        Empty_A3 = 0x0A3,

        [FileID(736)]  // object_gi_magicpot
        GiMagicpot = 0x0A4,

        [FileID(737)]  // object_gi_bomb_1
        GiBomb1 = 0x0A5,

        Empty_A6 = 0x0A6,

        [FileID(738)]  // object_ma2
        Ma2 = 0x0A7,

        [FileID(739)]  // object_gi_purse
        GiPurse = 0x0A8,

        Empty_A9 = 0x0A9,

        Empty_AA = 0x0AA,

        [FileID(740)]  // object_rr
        Rr = 0x0AB,

        Empty_AC = 0x0AC,

        Empty_AD = 0x0AD,

        Empty_AE = 0x0AE,

        [FileID(741)]  // object_gi_arrow
        GiArrow = 0x0AF,

        [FileID(742)]  // object_gi_bomb_2
        GiBomb2 = 0x0B0,

        Empty_B1 = 0x0B1,

        Empty_B2 = 0x0B2,

        [FileID(743)]  // object_gi_shield_2
        GiShield2 = 0x0B3,

        [FileID(744)]  // object_gi_hookshot
        GiHookshot = 0x0B4,

        [FileID(745)]  // object_gi_ocarina
        GiOcarina = 0x0B5,

        [FileID(746)]  // object_gi_milk
        GiMilk = 0x0B6,

        [FileID(747)]  // object_ma1
        Ma1 = 0x0B7,

        Empty_B8 = 0x0B8,

        Empty_B9 = 0x0B9,

        Empty_BA = 0x0BA,

        [FileID(748)]  // object_ny
        Ny = 0x0BB,

        [FileID(749)]  // object_fr
        Fr = 0x0BC,

        Empty_BD = 0x0BD,

        Empty_BE = 0x0BE,

        [FileID(750)]  // object_gi_bow
        GiBow = 0x0BF,

        [FileID(751)]  // object_gi_glasses
        GiGlasses = 0x0C0,

        [FileID(752)]  // object_gi_liquid
        GiLiquid = 0x0C1,

        [FileID(753)]  // object_ani
        Ani = 0x0C2,

        [FileID(754)]  // object_gi_shield_3
        GiShield3 = 0x0C3,

        Empty_C4 = 0x0C4,

        Empty_C5 = 0x0C5,

        [FileID(755)]  // object_gi_bean
        GiBean = 0x0C6,

        [FileID(756)]  // object_gi_fish
        GiFish = 0x0C7,

        Empty_C8 = 0x0C8,

        Empty_C9 = 0x0C9,

        Empty_CA = 0x0CA,

        [FileID(757)]  // object_gi_longsword
        GiLongsword = 0x0CB,

        Empty_CC = 0x0CC,

        Empty_CD = 0x0CD,

        Empty_CE = 0x0CE,

        Empty_CF = 0x0CF,

        [FileID(758)]  // object_zo
        Zo = 0x0D0,

        Empty_D1 = 0x0D1,

        [FileID(759)]  // object_umajump
        Umajump = 0x0D2,

        Empty_D3 = 0x0D3,

        Empty_D4 = 0x0D4,

        [FileID(760)]  // object_mastergolon
        Mastergolon = 0x0D5,

        [FileID(761)]  // object_masterzoora
        Masterzoora = 0x0D6,

        [FileID(762)]  // object_aob
        Aob = 0x0D7,

        [FileID(763)]  // object_ik
        Ik = 0x0D8,

        [FileID(764)]  // object_ahg
        Ahg = 0x0D9,

        [FileID(765)]  // object_cne
        Cne = 0x0DA,

        Empty_DB = 0x0DB,

        Empty_DC = 0x0DC,

        [FileID(1018)]  // object_an3
        An3 = 0x0DD,

        [FileID(766)]  // object_bji
        Bji = 0x0DE,

        [FileID(767)]  // object_bba
        Bba = 0x0DF,

        [FileID(1017)]  // object_an2
        An2 = 0x0E0,

        Empty_E1 = 0x0E1,

        [FileID(768)]  // object_an1
        An1 = 0x0E2,

        [FileID(769)]  // object_boj
        Boj = 0x0E3,

        [FileID(770)]  // object_fz
        Fz = 0x0E4,

        [FileID(771)]  // object_bob
        Bob = 0x0E5,

        [FileID(772)]  // object_ge1
        Ge1 = 0x0E6,

        [FileID(773)]  // object_yabusame_point
        YabusamePoint = 0x0E7,

        Empty_E8 = 0x0E8,

        Empty_E9 = 0x0E9,

        Empty_EA = 0x0EA,

        Empty_EB = 0x0EB,

        [FileID(774)]  // object_d_hsblock
        DHsblock = 0x0EC,

        [FileID(775)]  // object_d_lift
        DLift = 0x0ED,

        [FileID(776)]  // object_mamenoki
        Mamenoki = 0x0EE,

        [FileID(777)]  // object_goroiwa
        Goroiwa = 0x0EF,

        [FileID(778)]  // object_toryo
        Toryo = 0x0F0,

        [FileID(779)]  // object_daiku
        Daiku = 0x0F1,

        [FileID(780)]  // object_nwc
        Nwc = 0x0F2,

        [FileID(781)]  // object_gm
        Gm = 0x0F3,

        [FileID(782)]  // object_ms
        Ms = 0x0F4,

        [FileID(783)]  // object_hs
        Hs = 0x0F5,

        Empty_F6 = 0x0F6,

        [FileID(784)]  // object_lightswitch
        Lightswitch = 0x0F7,

        [FileID(785)]  // object_kusa
        Kusa = 0x0F8,

        [FileID(786)]  // object_tsubo
        Tsubo = 0x0F9,

        Empty_FA = 0x0FA,

        Empty_FB = 0x0FB,

        [FileID(787)]  // object_kanban
        Kanban = 0x0FC,

        [FileID(788)]  // object_owl
        Owl = 0x0FD,

        [FileID(789)]  // object_mk
        Mk = 0x0FE,

        [FileID(790)]  // object_fu
        Fu = 0x0FF,

        [FileID(791)]  // object_gi_ki_tan_mask
        GiKiTanMask = 0x100,

        Empty_101 = 0x101,

        [FileID(792)]  // object_gi_mask18
        GiMask18 = 0x102,

        [FileID(793)]  // object_gi_rabit_mask
        GiRabitMask = 0x103,

        [FileID(794)]  // object_gi_truth_mask
        GiTruthMask = 0x104,

        Empty_105 = 0x105,

        [FileID(795)]  // object_stream
        Stream = 0x106,

        [FileID(796)]  // object_mm
        Mm = 0x107,

        Empty_108 = 0x108,

        Empty_109 = 0x109,

        Empty_10A = 0x10A,

        Empty_10B = 0x10B,

        Empty_10C = 0x10C,

        Empty_10D = 0x10D,

        Empty_10E = 0x10E,

        [FileID(797)]  // object_js
        Js = 0x10F,

        [FileID(798)]  // object_cs
        Cs = 0x110,

        Empty_111 = 0x111,

        Empty_112 = 0x112,

        [FileID(799)]  // object_gi_soldout
        GiSoldout = 0x113,

        Empty_114 = 0x114,

        [FileID(800)]  // object_mag
        Mag = 0x115,

        Empty_116 = 0x116,

        Empty_117 = 0x117,

        Empty_118 = 0x118,

        [FileID(801)]  // object_gi_golonmask
        GiGolonmask = 0x119,

        [FileID(802)]  // object_gi_zoramask
        GiZoramask = 0x11A,

        Empty_11B = 0x11B,

        Empty_11C = 0x11C,

        [FileID(803)]  // object_ka
        Ka = 0x11D,

        Empty_11E = 0x11E,

        [FileID(804)]  // object_zg
        Zg = 0x11F,

        Empty_120 = 0x120,

        [FileID(805)]  // object_gi_m_arrow
        GiMArrow = 0x121,

        [FileID(806)]  // object_ds2
        Ds2 = 0x122,

        Empty_123 = 0x123,

        [FileID(807)]  // object_fish
        Fish = 0x124,

        [FileID(808)]  // object_gi_sutaru
        GiSutaru = 0x125,

        Empty_126 = 0x126,

        [FileID(809)]  // object_ssh
        Ssh = 0x127,

        [FileID(810)]  // object_bigslime
        Bigslime = 0x128,

        [FileID(811)]  // object_bg
        Bg = 0x129,

        [FileID(812)]  // object_bombiwa
        Bombiwa = 0x12A,

        [FileID(813)]  // object_hintnuts
        Hintnuts = 0x12B,

        [FileID(814)]  // object_rsn
        Rsn = 0x12C,

        Empty_12D = 0x12D,

        [FileID(815)]  // object_gla
        Gla = 0x12E,

        Empty_12F = 0x12F,

        [FileID(816)]  // object_geldb
        Geldb = 0x130,

        Empty_131 = 0x131,

        [FileID(817)]  // object_dog
        Dog = 0x132,

        [FileID(818)]  // object_kibako2
        Kibako2 = 0x133,

        [FileID(819)]  // object_dns
        Dns = 0x134,

        [FileID(820)]  // object_dnk
        Dnk = 0x135,

        Empty_136 = 0x136,

        [FileID(821)]  // object_gi_insect
        GiInsect = 0x137,

        Empty_138 = 0x138,

        [FileID(822)]  // object_gi_ghost
        GiGhost = 0x139,

        [FileID(823)]  // object_gi_soul
        GiSoul = 0x13A,

        Empty_13B = 0x13B,

        Empty_13C = 0x13C,

        Empty_13D = 0x13D,

        Empty_13E = 0x13E,

        [FileID(825)]  // object_gi_rupy
        GiRupy = 0x13F,

        [FileID(827)]  // object_mu
        Mu = 0x140,

        [FileID(828)]  // object_wf
        Wf = 0x141,

        [FileID(829)]  // object_skb
        Skb = 0x142,

        [FileID(830)]  // object_gs
        Gs = 0x143,

        [FileID(831)]  // object_ps
        Ps = 0x144,

        [FileID(832)]  // object_omoya_obj
        OmoyaObj = 0x145,

        [FileID(834)]  // object_cow
        Cow = 0x146,

        Empty_147 = 0x147,

        [FileID(835)]  // object_gi_sword_1
        GiSword1 = 0x148,

        Empty_149 = 0x149,

        Empty_14A = 0x14A,

        [FileID(836)]  // object_zl4
        Zl4 = 0x14B,

        [FileID(655)]  // object_link_goron
        LinkGoron = 0x14C,

        [FileID(656)]  // object_link_zora
        LinkZora = 0x14D,

        [FileID(837)]  // object_grasshopper
        Grasshopper = 0x14E,

        [FileID(838)]  // object_boyo
        Boyo = 0x14F,

        Empty_150 = 0x150,

        Empty_151 = 0x151,

        Empty_152 = 0x152,

        [FileID(839)]  // object_fwall
        Fwall = 0x153,

        [FileID(657)]  // object_link_nuts
        LinkNuts = 0x154,

        [FileID(840)]  // object_jso
        Jso = 0x155,

        [FileID(841)]  // object_knight
        Knight = 0x156,

        [FileID(842)]  // object_icicle
        Icicle = 0x157,

        [FileID(843)]  // object_spdweb
        Spdweb = 0x158,

        Empty_159 = 0x159,

        [FileID(844)]  // object_boss01
        Boss01 = 0x15A,

        [FileID(845)]  // object_boss02
        Boss02 = 0x15B,

        [FileID(846)]  // object_boss03
        Boss03 = 0x15C,

        [FileID(847)]  // object_boss04
        Boss04 = 0x15D,

        [FileID(848)]  // object_boss05
        Boss05 = 0x15E,

        Empty_15F = 0x15F,

        [FileID(849)]  // object_boss07
        Boss07 = 0x160,

        [FileID(850)]  // object_raf
        Raf = 0x161,

        [FileID(851)]  // object_funen
        Funen = 0x162,

        [FileID(852)]  // object_raillift
        Raillift = 0x163,

        [FileID(853)]  // object_numa_obj
        NumaObj = 0x164,

        [FileID(854)]  // object_flowerpot
        Flowerpot = 0x165,

        [FileID(855)]  // object_spinyroll
        Spinyroll = 0x166,

        [FileID(856)]  // object_ice_block
        IceBlock = 0x167,

        Empty_168 = 0x168,

        [FileID(857)]  // object_keikoku_demo
        KeikokuDemo = 0x169,

        [FileID(858)]  // object_slime
        Slime = 0x16A,

        [FileID(859)]  // object_pr
        Pr = 0x16B,

        [FileID(860)]  // object_f52_obj
        F52Obj = 0x16C,

        [FileID(861)]  // object_f53_obj
        F53Obj = 0x16D,

        Empty_16E = 0x16E,

        [FileID(862)]  // object_kibako
        Kibako = 0x16F,

        [FileID(863)]  // object_sek
        Sek = 0x170,

        [FileID(864)]  // object_gmo
        Gmo = 0x171,

        [FileID(865)]  // object_bat
        Bat = 0x172,

        [FileID(866)]  // object_sekihil
        Sekihil = 0x173,

        [FileID(867)]  // object_sekihig
        Sekihig = 0x174,

        [FileID(868)]  // object_sekihin
        Sekihin = 0x175,

        [FileID(869)]  // object_sekihiz
        Sekihiz = 0x176,

        Empty_177 = 0x177,

        [FileID(870)]  // object_wiz
        Wiz = 0x178,

        [FileID(871)]  // object_ladder
        Ladder = 0x179,

        [FileID(872)]  // object_mkk
        Mkk = 0x17A,

        Empty_17B = 0x17B,

        Empty_17C = 0x17C,

        Empty_17D = 0x17D,

        [FileID(873)]  // object_keikoku_obj
        KeikokuObj = 0x17E,

        [FileID(874)]  // object_sichitai_obj
        SichitaiObj = 0x17F,

        [FileID(875)]  // object_dekucity_ana_obj
        DekucityAnaObj = 0x180,

        [FileID(876)]  // object_rat
        Rat = 0x181,

        [FileID(877)]  // object_water_effect
        WaterEffect = 0x182,

        Empty_183 = 0x183,

        [FileID(878)]  // object_dblue_object
        DblueObject = 0x184,

        [FileID(879)]  // object_bal
        Bal = 0x185,

        [FileID(880)]  // object_warp_uzu
        WarpUzu = 0x186,

        [FileID(881)]  // object_driftice
        Driftice = 0x187,

        [FileID(882)]  // object_fall
        Fall = 0x188,

        [FileID(883)]  // object_hanareyama_obj
        HanareyamaObj = 0x189,

        [FileID(884)]  // object_crace_object
        CraceObject = 0x18A,

        [FileID(885)]  // object_dno
        Dno = 0x18B,

        [FileID(886)]  // object_obj_tokeidai
        ObjTokeidai = 0x18C,

        [FileID(887)]  // object_eg
        Eg = 0x18D,

        [FileID(888)]  // object_tru
        Tru = 0x18E,

        [FileID(889)]  // object_trt
        Trt = 0x18F,

        [FileID(890)]  // object_hakugin_obj
        HakuginObj = 0x190,

        [FileID(891)]  // object_horse_game_check
        HorseGameCheck = 0x191,

        [FileID(892)]  // object_stk
        Stk = 0x192,

        Empty_193 = 0x193,

        Empty_194 = 0x194,

        [FileID(893)]  // object_mnk
        Mnk = 0x195,

        [FileID(894)]  // object_gi_bottle_red
        GiBottleRed = 0x196,

        [FileID(895)]  // object_tokei_tobira
        TokeiTobira = 0x197,

        [FileID(896)]  // object_az
        Az = 0x198,

        [FileID(897)]  // object_twig
        Twig = 0x199,

        [FileID(898)]  // object_dekucity_obj
        DekucityObj = 0x19A,

        [FileID(899)]  // object_po_fusen
        PoFusen = 0x19B,

        [FileID(900)]  // object_racetsubo
        Racetsubo = 0x19C,

        [FileID(901)]  // object_ha
        Ha = 0x19D,

        [FileID(902)]  // object_bigokuta
        Bigokuta = 0x19E,

        [FileID(903)]  // object_open_obj
        OpenObj = 0x19F,

        [FileID(904)]  // object_fu_kaiten
        FuKaiten = 0x1A0,

        [FileID(905)]  // object_fu_mato
        FuMato = 0x1A1,

        [FileID(906)]  // object_mtoride
        Mtoride = 0x1A2,

        [FileID(907)]  // object_osn
        Osn = 0x1A3,

        [FileID(908)]  // object_tokei_step
        TokeiStep = 0x1A4,

        [FileID(909)]  // object_lotus
        Lotus = 0x1A5,

        [FileID(910)]  // object_tl
        Tl = 0x1A6,

        [FileID(911)]  // object_dkjail_obj
        DkjailObj = 0x1A7,

        [FileID(912)]  // object_visiblock
        Visiblock = 0x1A8,

        [FileID(913)]  // object_tsn
        Tsn = 0x1A9,

        [FileID(914)]  // object_ds2n
        Ds2n = 0x1AA,

        [FileID(915)]  // object_fsn
        Fsn = 0x1AB,

        [FileID(916)]  // object_shn
        Shn = 0x1AC,

        [FileID(917)]  // object_bigicicle
        Bigicicle = 0x1AD,

        [FileID(918)]  // object_gi_bottle_15
        GiBottle15 = 0x1AE,

        [FileID(919)]  // object_tk
        Tk = 0x1AF,

        [FileID(920)]  // object_market_obj
        MarketObj = 0x1B0,

        [FileID(921)]  // object_gi_reserve00
        GiReserve00 = 0x1B1,

        [FileID(922)]  // object_gi_reserve01
        GiReserve01 = 0x1B2,

        [FileID(923)]  // object_lightblock
        Lightblock = 0x1B3,

        [FileID(924)]  // object_takaraya_objects
        TakarayaObjects = 0x1B4,

        [FileID(925)]  // object_wdhand
        Wdhand = 0x1B5,

        [FileID(926)]  // object_sdn
        Sdn = 0x1B6,

        [FileID(927)]  // object_snowwd
        Snowwd = 0x1B7,

        [FileID(928)]  // object_giant
        Giant = 0x1B8,

        [FileID(929)]  // object_comb
        Comb = 0x1B9,

        [FileID(930)]  // object_hana
        Hana = 0x1BA,

        [FileID(931)]  // object_boss_hakugin
        BossHakugin = 0x1BB,

        [FileID(932)]  // object_meganeana_obj
        MeganeanaObj = 0x1BC,

        [FileID(933)]  // object_gi_nutsmask
        GiNutsmask = 0x1BD,

        [FileID(934)]  // object_stk2
        Stk2 = 0x1BE,

        [FileID(935)]  // object_spot11_obj
        Spot11Obj = 0x1BF,

        [FileID(936)]  // object_danpei_object
        DanpeiObject = 0x1C0,

        [FileID(937)]  // object_dhouse
        Dhouse = 0x1C1,

        [FileID(938)]  // object_hakaisi
        Hakaisi = 0x1C2,

        [FileID(939)]  // object_po
        Po = 0x1C3,

        [FileID(940)]  // object_snowman
        Snowman = 0x1C4,

        [FileID(941)]  // object_po_sisters
        PoSisters = 0x1C5,

        [FileID(942)]  // object_pp
        Pp = 0x1C6,

        [FileID(943)]  // object_goronswitch
        Goronswitch = 0x1C7,

        [FileID(944)]  // object_delf
        Delf = 0x1C8,

        [FileID(945)]  // object_botihasira
        Botihasira = 0x1C9,

        [FileID(946)]  // object_gi_bigbomb
        GiBigbomb = 0x1CA,

        [FileID(947)]  // object_pst
        Pst = 0x1CB,

        [FileID(948)]  // object_bsmask
        Bsmask = 0x1CC,

        [FileID(949)]  // object_spidertent
        Spidertent = 0x1CD,

        [FileID(950)]  // object_zoraegg
        Zoraegg = 0x1CE,

        [FileID(951)]  // object_kbt
        Kbt = 0x1CF,

        [FileID(952)]  // object_gg
        Gg = 0x1D0,

        [FileID(953)]  // object_maruta
        Maruta = 0x1D1,

        [FileID(954)]  // object_ghaka
        Ghaka = 0x1D2,

        [FileID(955)]  // object_oyu
        Oyu = 0x1D3,

        [FileID(956)]  // object_dnq
        Dnq = 0x1D4,

        [FileID(957)]  // object_dai
        Dai = 0x1D5,

        [FileID(958)]  // object_kgy
        Kgy = 0x1D6,

        [FileID(959)]  // object_fb
        Fb = 0x1D7,

        [FileID(960)]  // object_taisou
        Taisou = 0x1D8,

        [FileID(677)]  // object_mask_bu_san
        MaskBuSan = 0x1D9,

        [FileID(658)]  // object_mask_ki_tan
        MaskKiTan = 0x1DA,

        [FileID(659)]  // object_mask_rabit
        MaskRabit = 0x1DB,

        [FileID(660)]  // object_mask_skj
        MaskSkj = 0x1DC,

        [FileID(676)]  // object_mask_bakuretu
        MaskBakuretu = 0x1DD,

        [FileID(661)]  // object_mask_truth
        MaskTruth = 0x1DE,

        [FileID(961)]  // object_gk
        Gk = 0x1DF,

        [FileID(962)]  // object_haka_obj
        HakaObj = 0x1E0,

        [FileID(678)]  // object_mask_goron
        MaskGoron = 0x1E1,

        [FileID(679)]  // object_mask_zora
        MaskZora = 0x1E2,

        [FileID(680)]  // object_mask_nuts
        MaskNuts = 0x1E3,

        [FileID(681)]  // object_mask_boy
        MaskBoy = 0x1E4,

        [FileID(963)]  // object_dnt
        Dnt = 0x1E5,

        [FileID(964)]  // object_yukiyama
        Yukiyama = 0x1E6,

        [FileID(965)]  // object_icefloe
        Icefloe = 0x1E7,

        [FileID(966)]  // object_gi_gold_dust
        GiGoldDust = 0x1E8,

        [FileID(967)]  // object_gi_bottle_16
        GiBottle16 = 0x1E9,

        [FileID(968)]  // object_gi_bottle_22
        GiBottle22 = 0x1EA,

        [FileID(969)]  // object_bee
        Bee = 0x1EB,

        [FileID(970)]  // object_ot
        Ot = 0x1EC,

        [FileID(971)]  // object_utubo
        Utubo = 0x1ED,

        [FileID(972)]  // object_dora
        Dora = 0x1EE,

        [FileID(973)]  // object_gi_loach
        GiLoach = 0x1EF,

        [FileID(974)]  // object_gi_seahorse
        GiSeahorse = 0x1F0,

        [FileID(975)]  // object_bigpo
        Bigpo = 0x1F1,

        [FileID(976)]  // object_hariko
        Hariko = 0x1F2,

        [FileID(977)]  // object_dnj
        Dnj = 0x1F3,

        [FileID(978)]  // object_sinkai_kabe
        SinkaiKabe = 0x1F4,

        [FileID(979)]  // object_kin2_obj
        Kin2Obj = 0x1F5,

        [FileID(980)]  // object_ishi
        Ishi = 0x1F6,

        [FileID(981)]  // object_hakugin_demo
        HakuginDemo = 0x1F7,

        [FileID(982)]  // object_jg
        Jg = 0x1F8,

        [FileID(983)]  // object_gi_sword_2
        GiSword2 = 0x1F9,

        [FileID(984)]  // object_gi_sword_3
        GiSword3 = 0x1FA,

        [FileID(985)]  // object_gi_sword_4
        GiSword4 = 0x1FB,

        [FileID(986)]  // object_um
        Um = 0x1FC,

        [FileID(662)]  // object_mask_gibudo
        MaskGibudo = 0x1FD,

        [FileID(663)]  // object_mask_json
        MaskJson = 0x1FE,

        [FileID(664)]  // object_mask_kerfay
        MaskKerfay = 0x1FF,

        [FileID(665)]  // object_mask_bigelf
        MaskBigelf = 0x200,

        [FileID(987)]  // object_rb
        Rb = 0x201,

        [FileID(988)]  // object_mbar_obj
        MbarObj = 0x202,

        [FileID(989)]  // object_ikana_obj
        IkanaObj = 0x203,

        [FileID(990)]  // object_kz
        Kz = 0x204,

        [FileID(991)]  // object_tokei_turret
        TokeiTurret = 0x205,

        [FileID(992)]  // object_zog
        Zog = 0x206,

        [FileID(993)]  // object_rotlift
        Rotlift = 0x207,

        [FileID(994)]  // object_posthouse_obj
        PosthouseObj = 0x208,

        [FileID(995)]  // object_gi_mask09
        GiMask09 = 0x209,

        [FileID(996)]  // object_gi_mask14
        GiMask14 = 0x20A,

        [FileID(997)]  // object_gi_mask15
        GiMask15 = 0x20B,

        [FileID(998)]  // object_inibs_object
        InibsObject = 0x20C,

        [FileID(999)]  // object_tree
        Tree = 0x20D,

        [FileID(1000)]  // object_kaizoku_obj
        KaizokuObj = 0x20E,

        [FileID(1001)]  // object_gi_reserve_b_00
        GiReserveB00 = 0x20F,

        [FileID(1002)]  // object_gi_reserve_c_00
        GiReserveC00 = 0x210,

        [FileID(1003)]  // object_zob
        Zob = 0x211,

        [FileID(1004)]  // object_milkbar
        Milkbar = 0x212,

        [FileID(1005)]  // object_dmask
        Dmask = 0x213,

        [FileID(666)]  // object_mask_kyojin
        MaskKyojin = 0x214,

        [FileID(1006)]  // object_gi_reserve_c_01
        GiReserveC01 = 0x215,

        [FileID(1007)]  // object_zod
        Zod = 0x216,

        [FileID(1008)]  // object_kumo30
        Kumo30 = 0x217,

        [FileID(1009)]  // object_obj_yasi
        ObjYasi = 0x218,

        [FileID(667)]  // object_mask_romerny
        MaskRomerny = 0x219,

        [FileID(1010)]  // object_tanron1
        Tanron1 = 0x21A,

        [FileID(1011)]  // object_tanron2
        Tanron2 = 0x21B,

        [FileID(1012)]  // object_tanron3
        Tanron3 = 0x21C,

        [FileID(1013)]  // object_gi_magicmushroom
        GiMagicmushroom = 0x21D,

        [FileID(1014)]  // object_obj_chan
        ObjChan = 0x21E,

        [FileID(1015)]  // object_gi_mask10
        GiMask10 = 0x21F,

        [FileID(1016)]  // object_zos
        Zos = 0x220,

        [FileID(668)]  // object_mask_posthat
        MaskPosthat = 0x221,

        [FileID(1019)]  // object_f40_switch
        F40Switch = 0x222,

        [FileID(1020)]  // object_lodmoon
        Lodmoon = 0x223,

        [FileID(1021)]  // object_tro
        Tro = 0x224,

        [FileID(1022)]  // object_gi_mask12
        GiMask12 = 0x225,

        [FileID(1023)]  // object_gi_mask23
        GiMask23 = 0x226,

        [FileID(1024)]  // object_gi_bottle_21
        GiBottle21 = 0x227,

        [FileID(1025)]  // object_gi_camera
        GiCamera = 0x228,

        [FileID(1026)]  // object_kamejima
        Kamejima = 0x229,

        [FileID(1028)]  // object_harfgibud
        Harfgibud = 0x22A,

        [FileID(1029)]  // object_zov
        Zov = 0x22B,

        [FileID(1031)]  // object_hgdoor
        Hgdoor = 0x22C,

        Empty_22D = 0x22D,

        Empty_22E = 0x22E,

        Empty_22F = 0x22F,

        [FileID(1032)]  // object_dor01
        Dor01 = 0x230,

        [FileID(1033)]  // object_dor02
        Dor02 = 0x231,

        [FileID(1034)]  // object_dor03
        Dor03 = 0x232,

        [FileID(1035)]  // object_dor04
        Dor04 = 0x233,

        [FileID(1036)]  // object_last_obj
        LastObj = 0x234,

        [FileID(1037)]  // object_redead_obj
        RedeadObj = 0x235,

        [FileID(1038)]  // object_ikninside_obj
        IkninsideObj = 0x236,

        [FileID(1039)]  // object_iknv_obj
        IknvObj = 0x237,

        [FileID(1040)]  // object_pamera
        Pamera = 0x238,

        [FileID(1041)]  // object_hsstump
        Hsstump = 0x239,

        [FileID(1042)]  // object_zm
        Zm = 0x23A,

        [FileID(1055)]  // object_big_fwall
        BigFwall = 0x23B,

        [FileID(1045)]  // object_secom_obj
        SecomObj = 0x23C,

        [FileID(1056)]  // object_hunsui
        Hunsui = 0x23D,

        [FileID(1057)]  // object_uch
        Uch = 0x23E,

        [FileID(1058)]  // object_tanron4
        Tanron4 = 0x23F,

        [FileID(1059)]  // object_tanron5
        Tanron5 = 0x240,

        [FileID(1046)]  // object_dt
        Dt = 0x241,

        [FileID(1047)]  // object_gi_mask03
        GiMask03 = 0x242,

        [FileID(1048)]  // object_cha
        Cha = 0x243,

        [FileID(1049)]  // object_obj_dinner
        ObjDinner = 0x244,

        [FileID(1050)]  // object_gi_reserve_b_01
        GiReserveB01 = 0x245,

        [FileID(1051)]  // object_lastday
        Lastday = 0x246,

        [FileID(1052)]  // object_bai
        Bai = 0x247,

        [FileID(1060)]  // object_in2
        In2 = 0x248,

        [FileID(1053)]  // object_ikn_demo
        IknDemo = 0x249,

        [FileID(1061)]  // object_yb
        Yb = 0x24A,

        [FileID(1062)]  // object_rz
        Rz = 0x24B,

        [FileID(669)]  // object_mask_zacho
        MaskZacho = 0x24C,

        [FileID(1054)]  // object_gi_fieldmap
        GiFieldmap = 0x24D,

        [FileID(670)]  // object_mask_stone
        MaskStone = 0x24E,

        [FileID(1063)]  // object_bjt
        Bjt = 0x24F,

        [FileID(1064)]  // object_taru
        Taru = 0x250,

        [FileID(1065)]  // object_moonston
        Moonston = 0x251,

        [FileID(671)]  // object_mask_bree
        MaskBree = 0x252,

        [FileID(1066)]  // object_gi_schedule
        GiSchedule = 0x253,

        [FileID(1067)]  // object_gi_stonemask
        GiStonemask = 0x254,

        [FileID(1068)]  // object_zoraband
        Zoraband = 0x255,

        [FileID(1069)]  // object_kepn_koya
        KepnKoya = 0x256,

        [FileID(1070)]  // object_obj_usiyane
        ObjUsiyane = 0x257,

        [FileID(1071)]  // object_gi_mask05
        GiMask05 = 0x258,

        [FileID(1072)]  // object_gi_mask11
        GiMask11 = 0x259,

        [FileID(1073)]  // object_gi_mask20
        GiMask20 = 0x25A,

        [FileID(1074)]  // object_nnh
        Nnh = 0x25B,

        [FileID(672)]  // object_mask_gero
        MaskGero = 0x25C,

        [FileID(673)]  // object_mask_yofukasi
        MaskYofukasi = 0x25D,

        [FileID(674)]  // object_mask_meoto
        MaskMeoto = 0x25E,

        [FileID(675)]  // object_mask_dancer
        MaskDancer = 0x25F,

        [FileID(1075)]  // object_kzsaku
        Kzsaku = 0x260,

        [FileID(1076)]  // object_obj_milk_bin
        ObjMilkBin = 0x261,

        [FileID(1077)]  // object_random_obj
        RandomObj = 0x262,

        [FileID(1078)]  // object_kujiya
        Kujiya = 0x263,

        [FileID(1079)]  // object_kitan
        Kitan = 0x264,

        [FileID(1080)]  // object_gi_mask06
        GiMask06 = 0x265,

        [FileID(1081)]  // object_gi_mask16
        GiMask16 = 0x266,

        [FileID(1082)]  // object_astr_obj
        AstrObj = 0x267,

        [FileID(1083)]  // object_bsb
        Bsb = 0x268,

        [FileID(1084)]  // object_fall2
        Fall2 = 0x269,

        [FileID(1085)]  // object_sth
        Sth = 0x26A,

        [FileID(1086)]  // object_gi_mssa
        GiMssa = 0x26B,

        [FileID(1087)]  // object_smtower
        Smtower = 0x26C,

        [FileID(1088)]  // object_gi_mask21
        GiMask21 = 0x26D,

        [FileID(1089)]  // object_yado_obj
        YadoObj = 0x26E,

        [FileID(1090)]  // object_syoten
        Syoten = 0x26F,

        [FileID(1091)]  // object_moonend
        Moonend = 0x270,

        [FileID(1092)]  // object_ob
        Ob = 0x271,

        [FileID(1093)]  // object_gi_bottle_04
        GiBottle04 = 0x272,

        [FileID(1095)]  // object_obj_danpeilift
        ObjDanpeilift = 0x273,

        [FileID(1099)]  // object_wdor01
        Wdor01 = 0x274,

        [FileID(1100)]  // object_wdor02
        Wdor02 = 0x275,

        [FileID(1101)]  // object_wdor03
        Wdor03 = 0x276,

        [FileID(1104)]  // object_stk3
        Stk3 = 0x277,

        [FileID(1105)]  // object_kinsta1_obj
        Kinsta1Obj = 0x278,

        [FileID(1106)]  // object_kinsta2_obj
        Kinsta2Obj = 0x279,

        [FileID(1107)]  // object_bh
        Bh = 0x27A,

        [FileID(1102)]  // object_wdor04
        Wdor04 = 0x27B,

        [FileID(1103)]  // object_wdor05
        Wdor05 = 0x27C,

        [FileID(1108)]  // object_gi_mask17
        GiMask17 = 0x27D,

        [FileID(1109)]  // object_gi_mask22
        GiMask22 = 0x27E,

        [FileID(1110)]  // object_lbfshot
        Lbfshot = 0x27F,

        [FileID(1111)]  // object_fusen
        Fusen = 0x280,

        [FileID(1112)]  // object_ending_obj
        EndingObj = 0x281,

        [FileID(1113)]  // object_gi_mask13
        GiMask13 = 0x282,

        // this file was generated by walking the object table before MMFile list was updated,
        // and searching for the file based on vrom->file.Addr
        /*
        */
    }
}
