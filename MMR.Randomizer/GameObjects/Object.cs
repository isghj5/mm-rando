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

        [FileID(649)]
        Object_0 = 0x0,

        [FileID(650)]
        Object_1 = 0x1,

        [FileID(651)]
        Object_2 = 0x2,

        [FileID(1027)]
        Object_3 = 0x3,

        [FileID(683)]
        Object_4 = 0x4,

        [FileID(833)]
        Object_5 = 0x5,

        [FileID(1030)]
        Object_6 = 0x6,

        [FileID(685)]
        Object_7 = 0x7,

        [FileID(684)]
        Object_8 = 0x8,

        [FileID(687)]
        Object_9 = 0x9,

        [FileID(686)]
        Object_A = 0xA,

        [FileID(682)]
        Object_B = 0xB,

        [FileID(1043)]
        Object_C = 0xC,

        [FileID(693)]
        Object_D = 0xD,

        [FileID(688)]
        Object_E = 0xE,

        [FileID(653)]
        Object_F = 0xF,

        [FileID(654)]
        Object_10 = 0x10,

        [FileID(689)]
        Object_11 = 0x11,

        [FileID(1044)]
        Object_12 = 0x12,

        [FileID(690)]
        Object_13 = 0x13,

        [FileID(1094)]
        Object_14 = 0x14,

        [FileID(1097)]
        Object_15 = 0x15,

        [FileID(691)]
        Object_16 = 0x16,

        [FileID(1096)]
        Object_17 = 0x17,

        [FileID(692)]
        Object_18 = 0x18,

        [FileID(1098)]
        Object_19 = 0x19,

        Empty_1A = 0x1A,

        [FileID(694)]
        Object_1B = 0x1B,

        [FileID(695)]
        Object_1C = 0x1C,

        Empty_1D = 0x1D,

        Empty_1E = 0x1E,

        [FileID(696)]
        Object_1F = 0x1F,

        Empty_20 = 0x20,

        [FileID(697)]
        Object_21 = 0x21,

        Empty_22 = 0x22,

        Empty_23 = 0x23,

        Empty_24 = 0x24,

        Empty_25 = 0x25,

        Empty_26 = 0x26,

        Empty_27 = 0x27,

        Empty_28 = 0x28,

        [FileID(698)]
        Object_29 = 0x29,

        Empty_2A = 0x2A,

        Empty_2B = 0x2B,

        Empty_2C = 0x2C,

        Empty_2D = 0x2D,

        Empty_2E = 0x2E,

        [FileID(699)]
        Object_2F = 0x2F,

        [FileID(700)]
        Object_30 = 0x30,

        Empty_31 = 0x31,

        Empty_32 = 0x32,

        Empty_33 = 0x33,

        Empty_34 = 0x34,

        Empty_35 = 0x35,

        Empty_36 = 0x36,

        Empty_37 = 0x37,

        Empty_38 = 0x38,

        Empty_39 = 0x39,

        Empty_3A = 0x3A,

        Empty_3B = 0x3B,

        Empty_3C = 0x3C,

        [FileID(701)]
        Object_3D = 0x3D,

        [FileID(702)]
        Object_3E = 0x3E,

        [FileID(703)]
        Object_3F = 0x3F,

        Empty_40 = 0x40,

        Empty_41 = 0x41,

        Empty_42 = 0x42,

        Empty_43 = 0x43,

        Empty_44 = 0x44,

        Empty_45 = 0x45,

        Empty_46 = 0x46,

        Empty_47 = 0x47,

        Empty_48 = 0x48,

        Empty_49 = 0x49,

        Empty_4A = 0x4A,

        Empty_4B = 0x4B,

        Empty_4C = 0x4C,

        Empty_4D = 0x4D,

        Empty_4E = 0x4E,

        Empty_4F = 0x4F,

        [FileID(704)]
        Object_50 = 0x50,

        [FileID(705)]
        Object_51 = 0x51,

        Empty_52 = 0x52,

        Empty_53 = 0x53,

        Empty_54 = 0x54,

        Empty_55 = 0x55,

        Empty_56 = 0x56,

        Empty_57 = 0x57,

        Empty_58 = 0x58,

        Empty_59 = 0x59,

        Empty_5A = 0x5A,

        [FileID(824)]
        Object_5B = 0x5B,

        [FileID(826)]
        Object_5C = 0x5C,

        Empty_5D = 0x5D,

        [FileID(706)]
        Object_5E = 0x5E,

        Empty_5F = 0x5F,

        [FileID(707)]
        Object_60 = 0x60,

        Empty_61 = 0x61,

        Empty_62 = 0x62,

        [FileID(708)]
        Object_63 = 0x63,

        Empty_64 = 0x64,

        Empty_65 = 0x65,

        Empty_66 = 0x66,

        Empty_67 = 0x67,

        Empty_68 = 0x68,

        [FileID(709)]
        Object_69 = 0x69,

        Empty_6A = 0x6A,

        Empty_6B = 0x6B,

        Empty_6C = 0x6C,

        Empty_6D = 0x6D,

        Empty_6E = 0x6E,

        [FileID(710)]
        Object_6F = 0x6F,

        Empty_70 = 0x70,

        Empty_71 = 0x71,

        Empty_72 = 0x72,

        Empty_73 = 0x73,

        [FileID(711)]
        Object_74 = 0x74,

        [FileID(712)]
        Object_75 = 0x75,

        Empty_76 = 0x76,

        Empty_77 = 0x77,

        Empty_78 = 0x78,

        Empty_79 = 0x79,

        Empty_7A = 0x7A,

        Empty_7B = 0x7B,

        [FileID(713)]
        // 0xE4F0 size
        Epona = 0x7C,

        Empty_7D = 0x7D,

        Empty_7E = 0x7E,

        [FileID(714)]
        Object_7F = 0x7F,

        Empty_80 = 0x80,

        Empty_81 = 0x81,

        Empty_82 = 0x82,

        [FileID(715)]
        Object_83 = 0x83,

        Empty_84 = 0x84,

        [FileID(716)]
        Object_85 = 0x85,

        [FileID(717)]
        Object_86 = 0x86,

        [FileID(718)]
        Object_87 = 0x87,

        Empty_88 = 0x88,

        [FileID(719)]
        Object_89 = 0x89,

        Empty_8A = 0x8A,

        Empty_8B = 0x8B,

        Empty_8C = 0x8C,

        [FileID(720)]
        Object_8D = 0x8D,

        [FileID(721)]
        Object_8E = 0x8E,

        [FileID(722)]
        Object_8F = 0x8F,

        [FileID(723)]
        Object_90 = 0x90,

        [FileID(724)]
        Object_91 = 0x91,

        Empty_92 = 0x92,

        [FileID(725)]
        Object_93 = 0x93,

        Empty_94 = 0x94,

        [FileID(726)]
        Object_95 = 0x95,

        [FileID(727)]
        Object_96 = 0x96,

        [FileID(728)]
        Object_97 = 0x97,

        [FileID(729)]
        Object_98 = 0x98,

        Empty_99 = 0x99,

        Empty_9A = 0x9A,

        Empty_9B = 0x9B,

        [FileID(730)]
        Object_9C = 0x9C,

        [FileID(731)]
        Object_9D = 0x9D,

        [FileID(732)]
        Object_9E = 0x9E,

        [FileID(733)]
        Object_9F = 0x9F,

        [FileID(734)]
        Object_A0 = 0xA0,

        [FileID(735)]
        Object_A1 = 0xA1,

        Empty_A2 = 0xA2,

        [FileID(736)]
        Object_A3 = 0xA3,

        [FileID(737)]
        Object_A4 = 0xA4,

        Empty_A5 = 0xA5,

        [FileID(738)]
        Object_A6 = 0xA6,

        [FileID(739)]
        Object_A7 = 0xA7,

        Empty_A8 = 0xA8,

        Empty_A9 = 0xA9,

        [FileID(740)]
        Object_AA = 0xAA,

        Empty_AB = 0xAB,

        Empty_AC = 0xAC,

        Empty_AD = 0xAD,

        [FileID(741)]
        Object_AE = 0xAE,

        [FileID(742)]
        Object_AF = 0xAF,

        Empty_B0 = 0xB0,

        Empty_B1 = 0xB1,

        [FileID(743)]
        Object_B2 = 0xB2,

        [FileID(744)]
        Object_B3 = 0xB3,

        [FileID(745)]
        Object_B4 = 0xB4,

        [FileID(746)]
        Object_B5 = 0xB5,

        [FileID(747)]
        Object_B6 = 0xB6,

        Empty_B7 = 0xB7,

        Empty_B8 = 0xB8,

        Empty_B9 = 0xB9,

        [FileID(748)]
        Object_BA = 0xBA,

        [FileID(749)]
        Object_BB = 0xBB,

        Empty_BC = 0xBC,

        Empty_BD = 0xBD,

        [FileID(750)]
        Object_BE = 0xBE,

        [FileID(751)]
        Object_BF = 0xBF,

        [FileID(752)]
        Object_C0 = 0xC0,

        [FileID(753)]
        Object_C1 = 0xC1,

        [FileID(754)]
        Object_C2 = 0xC2,

        Empty_C3 = 0xC3,

        Empty_C4 = 0xC4,

        [FileID(755)]
        Object_C5 = 0xC5,

        [FileID(756)]
        Object_C6 = 0xC6,

        Empty_C7 = 0xC7,

        Empty_C8 = 0xC8,

        Empty_C9 = 0xC9,

        [FileID(757)]
        Object_CA = 0xCA,

        Empty_CB = 0xCB,

        Empty_CC = 0xCC,

        Empty_CD = 0xCD,

        Empty_CE = 0xCE,

        [FileID(758)]
        Object_CF = 0xCF,

        Empty_D0 = 0xD0,

        [FileID(759)]
        Object_D1 = 0xD1,

        Empty_D2 = 0xD2,

        Empty_D3 = 0xD3,

        [FileID(760)]
        Object_D4 = 0xD4,

        [FileID(761)]
        Object_D5 = 0xD5,

        [FileID(762)]
        Object_D6 = 0xD6,

        [FileID(763)]
        Object_D7 = 0xD7,

        [FileID(764)]
        Object_D8 = 0xD8,

        [FileID(765)]
        Object_D9 = 0xD9,

        Empty_DA = 0xDA,

        Empty_DB = 0xDB,

        [FileID(1018)]
        Object_DC = 0xDC,

        [FileID(766)]
        Object_DD = 0xDD,

        [FileID(767)]
        Object_DE = 0xDE,

        [FileID(1017)]
        Object_DF = 0xDF,

        Empty_E0 = 0xE0,

        [FileID(768)]
        Object_E1 = 0xE1,

        [FileID(769)]
        Object_E2 = 0xE2,

        [FileID(770)]
        Object_E3 = 0xE3,

        [FileID(771)]
        Object_E4 = 0xE4,

        [FileID(772)]
        Object_E5 = 0xE5,

        [FileID(773)]
        Object_E6 = 0xE6,

        Empty_E7 = 0xE7,

        Empty_E8 = 0xE8,

        Empty_E9 = 0xE9,

        Empty_EA = 0xEA,

        [FileID(774)]
        Object_EB = 0xEB,

        [FileID(775)]
        Object_EC = 0xEC,

        [FileID(776)]
        Object_ED = 0xED,

        [FileID(777)]
        Object_EE = 0xEE,

        [FileID(778)]
        Object_EF = 0xEF,

        [FileID(779)]
        Object_F0 = 0xF0,

        [FileID(780)]
        Object_F1 = 0xF1,

        [FileID(781)]
        Object_F2 = 0xF2,

        [FileID(782)]
        Object_F3 = 0xF3,

        [FileID(783)]
        Object_F4 = 0xF4,

        Empty_F5 = 0xF5,

        [FileID(784)]
        Object_F6 = 0xF6,

        [FileID(785)]
        Object_F7 = 0xF7,

        [FileID(786)]
        Object_F8 = 0xF8,

        Empty_F9 = 0xF9,

        Empty_FA = 0xFA,

        [FileID(787)]
        Object_FB = 0xFB,

        [FileID(788)]
        Object_FC = 0xFC,

        [FileID(789)]
        Object_FD = 0xFD,

        [FileID(790)]
        Object_FE = 0xFE,

        [FileID(791)]
        Object_FF = 0xFF,

        Empty_100 = 0x100,

        [FileID(792)]
        Object_101 = 0x101,

        [FileID(793)]
        Object_102 = 0x102,

        [FileID(794)]
        Object_103 = 0x103,

        Empty_104 = 0x104,

        [FileID(795)]
        Object_105 = 0x105,

        [FileID(796)]
        Object_106 = 0x106,

        Empty_107 = 0x107,

        Empty_108 = 0x108,

        Empty_109 = 0x109,

        Empty_10A = 0x10A,

        Empty_10B = 0x10B,

        Empty_10C = 0x10C,

        Empty_10D = 0x10D,

        [FileID(797)]
        Object_10E = 0x10E,

        [FileID(798)]
        Object_10F = 0x10F,

        Empty_110 = 0x110,

        Empty_111 = 0x111,

        [FileID(799)]
        Object_112 = 0x112,

        Empty_113 = 0x113,

        [FileID(800)]
        Object_114 = 0x114,

        Empty_115 = 0x115,

        Empty_116 = 0x116,

        Empty_117 = 0x117,

        [FileID(801)]
        Object_118 = 0x118,

        [FileID(802)]
        Object_119 = 0x119,

        Empty_11A = 0x11A,

        Empty_11B = 0x11B,

        [FileID(803)]
        Object_11C = 0x11C,

        Empty_11D = 0x11D,

        [FileID(804)]
        Object_11E = 0x11E,

        Empty_11F = 0x11F,

        [FileID(805)]
        Object_120 = 0x120,

        [FileID(806)]
        Object_121 = 0x121,

        Empty_122 = 0x122,

        [FileID(807)]
        Object_123 = 0x123,

        [FileID(808)]
        Object_124 = 0x124,

        Empty_125 = 0x125,

        [FileID(809)]
        Object_126 = 0x126,

        [FileID(810)]
        Object_127 = 0x127,

        [FileID(811)]
        Object_128 = 0x128,

        [FileID(812)]
        Object_129 = 0x129,

        [FileID(813)]
        Object_12A = 0x12A,

        [FileID(814)]
        Object_12B = 0x12B,

        Empty_12C = 0x12C,

        [FileID(815)]
        Object_12D = 0x12D,

        Empty_12E = 0x12E,

        [FileID(816)]
        Object_12F = 0x12F,

        Empty_130 = 0x130,

        [FileID(817)]
        Object_131 = 0x131,

        [FileID(818)]
        Object_132 = 0x132,

        [FileID(819)]
        Object_133 = 0x133,

        [FileID(820)]
        Object_134 = 0x134,

        Empty_135 = 0x135,

        [FileID(821)]
        Object_136 = 0x136,

        Empty_137 = 0x137,

        [FileID(822)]
        Object_138 = 0x138,

        [FileID(823)]
        Object_139 = 0x139,

        Empty_13A = 0x13A,

        Empty_13B = 0x13B,

        Empty_13C = 0x13C,

        Empty_13D = 0x13D,

        [FileID(825)]
        Object_13E = 0x13E,

        [FileID(827)]
        Object_13F = 0x13F,

        [FileID(828)]
        Object_140 = 0x140,

        [FileID(829)]
        Object_141 = 0x141,

        [FileID(830)]
        Object_142 = 0x142,

        [FileID(831)]
        Object_143 = 0x143,

        [FileID(832)]
        Object_144 = 0x144,

        [FileID(834)]
        Object_145 = 0x145,

        Empty_146 = 0x146,

        [FileID(835)]
        Object_147 = 0x147,

        Empty_148 = 0x148,

        Empty_149 = 0x149,

        [FileID(836)]
        Object_14A = 0x14A,

        [FileID(655)]
        Object_14B = 0x14B,

        [FileID(656)]
        Object_14C = 0x14C,

        [FileID(837)]
        Object_14D = 0x14D,

        [FileID(838)]
        Object_14E = 0x14E,

        Empty_14F = 0x14F,

        Empty_150 = 0x150,

        Empty_151 = 0x151,

        [FileID(839)]
        Object_152 = 0x152,

        [FileID(657)]
        Object_153 = 0x153,

        [FileID(840)]
        Object_154 = 0x154,

        [FileID(841)]
        Object_155 = 0x155,

        [FileID(842)]
        Object_156 = 0x156,

        [FileID(843)]
        Object_157 = 0x157,

        Empty_158 = 0x158,

        [FileID(844)]
        Object_159 = 0x159,

        [FileID(845)]
        Object_15A = 0x15A,

        [FileID(846)]
        Object_15B = 0x15B,

        [FileID(847)]
        Object_15C = 0x15C,

        [FileID(848)]
        Object_15D = 0x15D,

        [FileID(849)]
        Object_15E = 0x15E,

        [FileID(849)]
        Object_15F = 0x15F,

        [FileID(850)]
        Object_160 = 0x160,

        [FileID(851)]
        Object_161 = 0x161,

        [FileID(852)]
        Object_162 = 0x162,

        [FileID(853)]
        Object_163 = 0x163,

        [FileID(854)]
        Object_164 = 0x164,

        [FileID(855)]
        Object_165 = 0x165,

        [FileID(856)]
        Object_166 = 0x166,

        Empty_167 = 0x167,

        [FileID(857)]
        Object_168 = 0x168,

        [FileID(858)]
        Object_169 = 0x169,

        [FileID(859)]
        Object_16A = 0x16A,

        [FileID(860)]
        Object_16B = 0x16B,

        [FileID(861)]
        Object_16C = 0x16C,

        Empty_16D = 0x16D,

        [FileID(862)]
        Object_16E = 0x16E,

        [FileID(863)]
        Object_16F = 0x16F,

        [FileID(864)]
        Object_170 = 0x170,

        [FileID(865)]
        Object_171 = 0x171,

        [FileID(866)]
        Object_172 = 0x172,

        [FileID(867)]
        Object_173 = 0x173,

        [FileID(868)]
        Object_174 = 0x174,

        [FileID(869)]
        Object_175 = 0x175,

        Empty_176 = 0x176,

        [FileID(870)]
        Object_177 = 0x177,

        [FileID(871)]
        Object_178 = 0x178,

        [FileID(872)]
        Object_179 = 0x179,

        Empty_17A = 0x17A,

        Empty_17B = 0x17B,

        Empty_17C = 0x17C,

        [FileID(873)]
        Object_17D = 0x17D,

        [FileID(874)]
        Object_17E = 0x17E,

        [FileID(875)]
        Object_17F = 0x17F,

        [FileID(876)]
        Object_180 = 0x180,

        [FileID(877)]
        Object_181 = 0x181,

        Empty_182 = 0x182,

        [FileID(878)]
        Object_183 = 0x183,

        [FileID(879)]
        Object_184 = 0x184,

        [FileID(880)]
        Object_185 = 0x185,

        [FileID(881)]
        Object_186 = 0x186,

        [FileID(882)]
        Object_187 = 0x187,

        [FileID(883)]
        Object_188 = 0x188,

        [FileID(884)]
        Object_189 = 0x189,

        [FileID(885)]
        Object_18A = 0x18A,

        [FileID(886)]
        Object_18B = 0x18B,

        [FileID(887)]
        Object_18C = 0x18C,

        [FileID(888)]
        Object_18D = 0x18D,

        [FileID(889)]
        Object_18E = 0x18E,

        [FileID(890)]
        Object_18F = 0x18F,

        [FileID(891)]
        Object_190 = 0x190,

        [FileID(892)]
        Object_191 = 0x191,

        Empty_192 = 0x192,

        Empty_193 = 0x193,

        [FileID(893)]
        Object_194 = 0x194,

        [FileID(894)]
        Object_195 = 0x195,

        [FileID(895)]
        Object_196 = 0x196,

        [FileID(896)]
        Object_197 = 0x197,

        [FileID(897)]
        Object_198 = 0x198,

        [FileID(898)]
        Object_199 = 0x199,

        [FileID(899)]
        Object_19A = 0x19A,

        [FileID(900)]
        Object_19B = 0x19B,

        [FileID(901)]
        Object_19C = 0x19C,

        [FileID(902)]
        Object_19D = 0x19D,

        [FileID(903)]
        Object_19E = 0x19E,

        [FileID(904)]
        Object_19F = 0x19F,

        [FileID(905)]
        Object_1A0 = 0x1A0,

        [FileID(906)]
        Object_1A1 = 0x1A1,

        [FileID(907)]
        Object_1A2 = 0x1A2,

        [FileID(908)]
        Object_1A3 = 0x1A3,

        [FileID(909)]
        Object_1A4 = 0x1A4,

        [FileID(910)]
        Object_1A5 = 0x1A5,

        [FileID(911)]
        Object_1A6 = 0x1A6,

        [FileID(912)]
        Object_1A7 = 0x1A7,

        [FileID(913)]
        Object_1A8 = 0x1A8,

        [FileID(914)]
        Object_1A9 = 0x1A9,

        [FileID(915)]
        Object_1AA = 0x1AA,

        [FileID(916)]
        Object_1AB = 0x1AB,

        [FileID(917)]
        Object_1AC = 0x1AC,

        [FileID(918)]
        Object_1AD = 0x1AD,

        [FileID(919)]
        Object_1AE = 0x1AE,

        [FileID(920)]
        Object_1AF = 0x1AF,

        [FileID(921)]
        Object_1B0 = 0x1B0,

        [FileID(922)]
        Object_1B1 = 0x1B1,

        [FileID(923)]
        Object_1B2 = 0x1B2,

        [FileID(924)]
        Object_1B3 = 0x1B3,

        [FileID(925)]
        Object_1B4 = 0x1B4,

        [FileID(926)]
        Object_1B5 = 0x1B5,

        [FileID(927)]
        Object_1B6 = 0x1B6,

        [FileID(928)]
        Object_1B7 = 0x1B7,

        [FileID(929)]
        Object_1B8 = 0x1B8,

        [FileID(930)]
        Object_1B9 = 0x1B9,

        [FileID(931)]
        Object_1BA = 0x1BA,

        [FileID(932)]
        Object_1BB = 0x1BB,

        [FileID(933)]
        Object_1BC = 0x1BC,

        [FileID(934)]
        Object_1BD = 0x1BD,

        [FileID(935)]
        Object_1BE = 0x1BE,

        [FileID(936)]
        Object_1BF = 0x1BF,

        [FileID(937)]
        Object_1C0 = 0x1C0,

        [FileID(938)]
        Object_1C1 = 0x1C1,

        [FileID(939)]
        Object_1C2 = 0x1C2,

        [FileID(940)]
        Object_1C3 = 0x1C3,

        [FileID(941)]
        Object_1C4 = 0x1C4,

        [FileID(942)]
        Object_1C5 = 0x1C5,

        [FileID(943)]
        Object_1C6 = 0x1C6,

        [FileID(944)]
        Object_1C7 = 0x1C7,

        [FileID(945)]
        Object_1C8 = 0x1C8,

        [FileID(946)]
        Object_1C9 = 0x1C9,

        [FileID(947)]
        Object_1CA = 0x1CA,

        [FileID(948)]
        Object_1CB = 0x1CB,

        [FileID(949)]
        Object_1CC = 0x1CC,

        [FileID(950)]
        Object_1CD = 0x1CD,

        [FileID(951)]
        Object_1CE = 0x1CE,

        [FileID(952)]
        Object_1CF = 0x1CF,

        [FileID(953)]
        Object_1D0 = 0x1D0,

        [FileID(954)]
        Object_1D1 = 0x1D1,

        [FileID(955)]
        Object_1D2 = 0x1D2,

        [FileID(956)]
        Object_1D3 = 0x1D3,

        [FileID(957)]
        Object_1D4 = 0x1D4,

        [FileID(958)]
        Object_1D5 = 0x1D5,

        [FileID(959)]
        Object_1D6 = 0x1D6,

        [FileID(960)]
        Object_1D7 = 0x1D7,

        [FileID(677)]
        Object_1D8 = 0x1D8,

        [FileID(658)]
        Object_1D9 = 0x1D9,

        [FileID(659)]
        Object_1DA = 0x1DA,

        [FileID(660)]
        Object_1DB = 0x1DB,

        [FileID(676)]
        Object_1DC = 0x1DC,

        [FileID(661)]
        Object_1DD = 0x1DD,

        [FileID(961)]
        Object_1DE = 0x1DE,

        [FileID(962)]
        Object_1DF = 0x1DF,

        [FileID(678)]
        Object_1E0 = 0x1E0,

        [FileID(679)]
        Object_1E1 = 0x1E1,

        [FileID(680)]
        Object_1E2 = 0x1E2,

        [FileID(681)]
        Object_1E3 = 0x1E3,

        [FileID(963)]
        Object_1E4 = 0x1E4,

        [FileID(964)]
        Object_1E5 = 0x1E5,

        [FileID(965)]
        Object_1E6 = 0x1E6,

        [FileID(966)]
        Object_1E7 = 0x1E7,

        [FileID(967)]
        Object_1E8 = 0x1E8,

        [FileID(968)]
        Object_1E9 = 0x1E9,

        [FileID(969)]
        Object_1EA = 0x1EA,

        [FileID(970)]
        Object_1EB = 0x1EB,

        [FileID(971)]
        Object_1EC = 0x1EC,

        [FileID(972)]
        Object_1ED = 0x1ED,

        [FileID(973)]
        Object_1EE = 0x1EE,

        [FileID(974)]
        Object_1EF = 0x1EF,

        [FileID(975)]
        Object_1F0 = 0x1F0,

        [FileID(976)]
        Object_1F1 = 0x1F1,

        [FileID(977)]
        Object_1F2 = 0x1F2,

        [FileID(978)]
        Object_1F3 = 0x1F3,

        [FileID(979)]
        Object_1F4 = 0x1F4,

        [FileID(980)]
        Object_1F5 = 0x1F5,

        [FileID(981)]
        Object_1F6 = 0x1F6,

        [FileID(982)]
        Object_1F7 = 0x1F7,

        [FileID(983)]
        Object_1F8 = 0x1F8,

        [FileID(984)]
        Object_1F9 = 0x1F9,

        [FileID(985)]
        Object_1FA = 0x1FA,

        [FileID(986)]
        Object_1FB = 0x1FB,

        [FileID(662)]
        Object_1FC = 0x1FC,

        [FileID(663)]
        Object_1FD = 0x1FD,

        [FileID(664)]
        Object_1FE = 0x1FE,

        [FileID(665)]
        Object_1FF = 0x1FF,

        [FileID(987)]
        Object_200 = 0x200,

        [FileID(988)]
        Object_201 = 0x201,

        [FileID(989)]
        Object_202 = 0x202,

        [FileID(990)]
        Object_203 = 0x203,

        [FileID(991)]
        Object_204 = 0x204,

        [FileID(992)]
        Object_205 = 0x205,

        [FileID(993)]
        Object_206 = 0x206,

        [FileID(994)]
        Object_207 = 0x207,

        [FileID(995)]
        Object_208 = 0x208,

        [FileID(996)]
        Object_209 = 0x209,

        [FileID(997)]
        Object_20A = 0x20A,

        [FileID(998)]
        Object_20B = 0x20B,

        [FileID(999)]
        Object_20C = 0x20C,

        [FileID(1000)]
        Object_20D = 0x20D,

        [FileID(1001)]
        Object_20E = 0x20E,

        [FileID(1002)]
        Object_20F = 0x20F,

        [FileID(1003)]
        Object_210 = 0x210,

        [FileID(1004)]
        Object_211 = 0x211,

        [FileID(1005)]
        Object_212 = 0x212,

        [FileID(666)]
        Object_213 = 0x213,

        [FileID(1006)]
        Object_214 = 0x214,

        [FileID(1007)]
        Object_215 = 0x215,

        [FileID(1008)]
        Object_216 = 0x216,

        [FileID(1009)]
        Object_217 = 0x217,

        [FileID(667)]
        Object_218 = 0x218,

        [FileID(1010)]
        Object_219 = 0x219,

        [FileID(1011)]
        Object_21A = 0x21A,

        [FileID(1012)]
        Object_21B = 0x21B,

        [FileID(1013)]
        Object_21C = 0x21C,

        [FileID(1014)]
        Object_21D = 0x21D,

        [FileID(1015)]
        Object_21E = 0x21E,

        [FileID(1016)]
        Object_21F = 0x21F,

        [FileID(668)]
        Object_220 = 0x220,

        [FileID(1019)]
        Object_221 = 0x221,

        [FileID(1020)]
        Object_222 = 0x222,

        [FileID(1021)]
        Object_223 = 0x223,

        [FileID(1022)]
        Object_224 = 0x224,

        [FileID(1023)]
        Object_225 = 0x225,

        [FileID(1024)]
        Object_226 = 0x226,

        [FileID(1025)]
        Object_227 = 0x227,

        [FileID(1026)]
        Object_228 = 0x228,

        [FileID(1028)]
        Object_229 = 0x229,

        [FileID(1029)]
        Object_22A = 0x22A,

        [FileID(1031)]
        Object_22B = 0x22B,

        Empty_22C = 0x22C,

        Empty_22D = 0x22D,

        Empty_22E = 0x22E,

        [FileID(1032)]
        Object_22F = 0x22F,

        [FileID(1033)]
        Object_230 = 0x230,

        [FileID(1034)]
        Object_231 = 0x231,

        [FileID(1035)]
        Object_232 = 0x232,

        [FileID(1036)]
        Object_233 = 0x233,

        [FileID(1037)]
        Object_234 = 0x234,

        [FileID(1038)]
        Object_235 = 0x235,

        [FileID(1039)]
        Object_236 = 0x236,

        [FileID(1040)]
        Object_237 = 0x237,

        [FileID(1041)]
        Object_238 = 0x238,

        [FileID(1042)]
        Object_239 = 0x239,

        [FileID(1055)]
        Object_23A = 0x23A,

        [FileID(1045)]
        Object_23B = 0x23B,

        [FileID(1056)]
        Object_23C = 0x23C,

        [FileID(1057)]
        Object_23D = 0x23D,

        [FileID(1058)]
        Object_23E = 0x23E,

        [FileID(1059)]
        Object_23F = 0x23F,

        [FileID(1046)]
        Object_240 = 0x240,

        [FileID(1047)]
        Object_241 = 0x241,

        [FileID(1048)]
        Object_242 = 0x242,

        [FileID(1049)]
        Object_243 = 0x243,

        [FileID(1050)]
        Object_244 = 0x244,

        [FileID(1051)]
        Object_245 = 0x245,

        [FileID(1052)]
        Object_246 = 0x246,

        [FileID(1060)]
        Object_247 = 0x247,

        [FileID(1053)]
        Object_248 = 0x248,

        [FileID(1061)]
        Object_249 = 0x249,

        [FileID(1062)]
        Object_24A = 0x24A,

        [FileID(669)]
        Object_24B = 0x24B,

        [FileID(1054)]
        Object_24C = 0x24C,

        [FileID(670)]
        Object_24D = 0x24D,

        [FileID(1063)]
        Object_24E = 0x24E,

        [FileID(1064)]
        Object_24F = 0x24F,

        [FileID(1065)]
        Object_250 = 0x250,

        [FileID(671)]
        Object_251 = 0x251,

        [FileID(1066)]
        Object_252 = 0x252,

        [FileID(1067)]
        Object_253 = 0x253,

        [FileID(1068)]
        Object_254 = 0x254,

        [FileID(1069)]
        Object_255 = 0x255,

        [FileID(1070)]
        Object_256 = 0x256,

        [FileID(1071)]
        Object_257 = 0x257,

        [FileID(1072)]
        Object_258 = 0x258,

        [FileID(1073)]
        Object_259 = 0x259,

        [FileID(1074)]
        Object_25A = 0x25A,

        [FileID(672)]
        Object_25B = 0x25B,

        [FileID(673)]
        Object_25C = 0x25C,

        [FileID(674)]
        Object_25D = 0x25D,

        [FileID(675)]
        Object_25E = 0x25E,

        [FileID(1075)]
        Object_25F = 0x25F,

        [FileID(1076)]
        Object_260 = 0x260,

        [FileID(1077)]
        Object_261 = 0x261,

        [FileID(1078)]
        Object_262 = 0x262,

        [FileID(1079)]
        Object_263 = 0x263,

        [FileID(1080)]
        Object_264 = 0x264,

        [FileID(1081)]
        Object_265 = 0x265,

        [FileID(1082)]
        Object_266 = 0x266,

        [FileID(1083)]
        Object_267 = 0x267,

        [FileID(1084)]
        Object_268 = 0x268,

        [FileID(1085)]
        Object_269 = 0x269,

        [FileID(1086)]
        Object_26A = 0x26A,

        [FileID(1087)]
        Object_26B = 0x26B,

        [FileID(1088)]
        Object_26C = 0x26C,

        [FileID(1089)]
        Object_26D = 0x26D,

        [FileID(1090)]
        Object_26E = 0x26E,

        [FileID(1091)]
        Object_26F = 0x26F,

        [FileID(1092)]
        Object_270 = 0x270,

        [FileID(1093)]
        Object_271 = 0x271,

        [FileID(1095)]
        Object_272 = 0x272,

        [FileID(1099)]
        Object_273 = 0x273,

        [FileID(1100)]
        Object_274 = 0x274,

        [FileID(1101)]
        Object_275 = 0x275,

        [FileID(1104)]
        Object_276 = 0x276,

        [FileID(1105)]
        Object_277 = 0x277,

        [FileID(1106)]
        Object_278 = 0x278,

        [FileID(1107)]
        Object_279 = 0x279,

        [FileID(1102)]
        Object_27A = 0x27A,

        [FileID(1103)]
        Object_27B = 0x27B,

        [FileID(1108)]
        Object_27C = 0x27C,

        [FileID(1109)]
        Object_27D = 0x27D,

        [FileID(1110)]
        Object_27E = 0x27E,

        [FileID(1111)]
        Object_27F = 0x27F,

        [FileID(1112)]
        Object_280 = 0x280,

        [FileID(1113)]
        Object_281 = 0x281,

        Empty_282 = 0x282,

        // this file was generated by walking the object table before MMFile list was updated,
        // and searching for the file based on vrom->file.Addr
        /*
                        int objectListFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ObjectList);
                RomUtils.CheckCompressed(objectListFID);
                var codefile = RomData.MMFileList[objectListFID].Data;
                int objectListOffset = Constants.Addresses.ObjectList - RomData.MMFileList[objectListFID].Addr;

                
                for (int i = 0; i < 0x290; i++)
                {
                    var offset = objectListOffset + (0x8 * i);
                    var vROMstart = ReadWriteUtils.Arr_ReadU32(codefile, offset);
                    var fID = -1;
                    var name = $"Empty_{i.ToString("X")}";
                    if (vROMstart > 0)
                    {
                        //var fileLookup = RomData.MMFileList.Find(u => u.Addr == vROMstart);
                        var fileLookup = RomData.MMFileList.FindIndex(u => u.Addr == vROMstart);
                        if (fileLookup > 0)
                        {
                            name = $"Object_{i.ToString("X")}";
                            Debug.WriteLine($"  [FileID({fileLookup})]");
                        }
                    }
                    Debug.WriteLine($"  {name} = 0x{i.ToString("X")},\n");
                }
        */
    }
}
