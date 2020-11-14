#include <stdio.h>
#include "z2.h"
#include "mmr.h"

typedef struct string {
    char *value;
    u16 length;
} string;

typedef struct {
    string name;
    string description;
    string article; // I sell Bombchu. I sell a Recovery Heart. I sell the Song of Storms. I sell an Empty Bottle.
    string pronoun; // I'll buy it. I'll buy them.
    string amount; // 150 Rupees for it. What about for 100 Rupees? 150 Rupees for one. What about one for 100 Rupees?
    string verb; // Do you know what Bombchu are? Do you know what a Recovery Heart is?
} item_info_t;

struct message_extension_state {
    bool is_wrapping;
    s8 last_space_index;
    f32 last_space_cursor_position;

    item_info_t recovery_heart;
    item_info_t red_potion;
    item_info_t chateau_romani;
    item_info_t milk;
    item_info_t gold_dust;

    item_info_t sword_kokiri;
    item_info_t sword_razor;
    item_info_t sword_gilded;

    item_info_t magic_small;
    item_info_t magic_large;

    item_info_t wallet_adult;
    item_info_t wallet_giant;

    item_info_t bomb_bag_small;
    item_info_t bomb_bag_big;
    item_info_t bomb_bag_biggest;

    item_info_t quiver_small;
    item_info_t quiver_large;
    item_info_t quiver_largest;

    s8 current_char;
    char *current_replacement;
    u16 current_replacement_length;
};

const string article_indefinite = {
    .value = "a ", // intentional trailing space.
    .length = 2,
};

const string article_indefinite_vowel = {
    .value = "an ", // intentional trailing space.
    .length = 3,
};

const string article_definite = {
    .value = "the ", // intentional trailing space.
    .length = 4,
};

const string article_empty = {
    .value = "",
    .length = 0,
};

const string pronoun_singular = {
    .value = "it",
    .length = 2,
};

const string amount_singular = {
    .value = " one", // intentional leading space.
    .length = 4,
};

const string amount_definite = {
    .value = " it", // intentional leading space.
    .length = 3,
};

const string verb_singular = {
    .value = "is",
    .length = 2,
};

static struct message_extension_state g_message_extension_state = {
    .is_wrapping = false,
    .last_space_index = -1,
    .last_space_cursor_position = 0,

    .recovery_heart = {
        .name = {
            .value = "Recovery Heart",
            .length = 14,
        },
        .description = {
            .value = "Replenishes a small amount of your life energy.",
            .length = 47,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },

    .red_potion = {
        .name = {
            .value = "Red Potion",
            .length = 10,
        },
        // .description = {

        // },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },

    .chateau_romani = {
        .name = {
            .value = "Chateau Romani",
            .length = 14,
        },
        // .description = {

        // },
        .article = article_empty,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .milk = {
        .name = {
            .value = "Milk",
            .length = 4,
        },
        // .description = {

        // },
        .article = article_empty,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .gold_dust = {
        .name = {
            .value = "Gold Dust",
            .length = 9,
        },
        // .description = {

        // },
        .article = article_empty,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .sword_kokiri = {
        .name = {
            .value = "Kokiri Sword",
            .length = 12,
        },
        .description = {
            .value = "A sword created by forest folk.",
            .length = 31,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .sword_razor = {
        .name = {
            .value = "Razor Sword",
            .length = 11,
        },
        .description = {
            .value = "A sharp sword forged at the smithy.",
            .length = 35,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .sword_gilded = {
        .name = {
            .value = "Gilded Sword",
            .length = 14,
        },
        .description = {
            .value = "A very sharp sword forged from gold dust.",
            .length = 41,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },

    .magic_small = {
        .name = {
            .value = "Magic Power",
            .length = 11,
        },
        .description = {
            .value = "Grants the ability to use magic.",
            .length = 32,
        },
        .article = article_empty,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },
    .magic_large = {
        .name = {
            .value = "Extended Magic Power",
            .length = 20,
        },
        .description = {
            .value = "Grants the ability to use lots of magic.",
            .length = 40,
        },
        .article = article_empty,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .wallet_adult = {
        .name = {
            .value = "Adult Wallet",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 200 rupees.",
            .length = 44,
        },
        .article = article_indefinite_vowel,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .wallet_giant = {
        .name = {
            .value = "Giant Wallet",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 500 rupees.",
            .length = 44,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },

    .bomb_bag_small = {
        .name = {
            .value = "Bomb Bag",
            .length = 8,
        },
        .description = {
            .value = "This can hold up to a maximum of 20 bombs.",
            .length = 42,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .bomb_bag_big = {
        .name = {
            .value = "Big Bomb Bag",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 30 bombs.",
            .length = 42,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .bomb_bag_biggest = {
        .name = {
            .value = "Biggest Bomb Bag",
            .length = 16,
        },
        .description = {
            .value = "This can hold up to a maximum of 40 bombs.",
            .length = 42,
        },
        .article = article_definite,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .quiver_small = {
        .name = {
            .value = "Hero's Bow",
            .length = 10,
        },
        .description = {
            .value = "Use it to shoot arrows.",
            .length = 23,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .quiver_large = {
        .name = {
            .value = "Large Quiver",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 40 arrows.",
            .length = 43,
        },
        .article = article_indefinite,
        .pronoun = pronoun_singular,
        .amount = amount_singular,
        .verb = verb_singular,
    },
    .quiver_largest = {
        .name = {
            .value = "Largest Quiver",
            .length = 14,
        },
        .description = {
            .value = "This can hold up to a maximum of 50 arrows.",
            .length = 47,
        },
        .article = article_definite,
        .pronoun = pronoun_singular,
        .amount = amount_definite,
        .verb = verb_singular,
    },

    .current_char = -1,
    .current_replacement_length = 0,
};

typedef struct message_character_process_variables_s {
    u8               unk_0x00[0x0020];               /* 0x0000 */
    f64              f20;                            /* 0x0020 */
    f64              f22;                            /* 0x0028 */
    f64              f24;                            /* 0x0030 */
    f64              f26;                            /* 0x0038 */
    u32              s0;                             /* 0x0040 */
    u32              s1;                             /* 0x0044 */
    u32              s2;                             /* 0x0048 */
    u32              s3;                             /* 0x004C */
    u32              s4;                             /* 0x0050 */ // global context
    u32              s5;                             /* 0x0054 */
    u32              s6;                             /* 0x0058 */
    u32              s7;                             /* 0x005C */
    u32              fp;                             /* 0x0060 */
    u32              return_address;                 /* 0x0064 */
    u8               unk_0x68[0x0008];               /* 0x0068 */
    u32              msgbox_ctxt;                    /* 0x0070 */
    u8               unk_0x74[0x0030];               /* 0x0074 */
    f32              cursor_position;                /* 0x00A4 */
    u8               unk_0xA8[0x0014];               /* 0x00A8 */
    u32              unk_0xBC;                       /* 0x00BC */
    u8               unk_0xC0[0x0006];               /* 0x00C0 */
    u8               number_of_new_lines;            /* 0x00C6 */
    u8               unk_0xC7[0x0007];               /* 0x00C7 */
    u16              unk_0xCE;                       /* 0x00CE */
    s16              number_of_new_lines_2;          /* 0x00D0 */
    u8               unk_0xD2[0x0008];               /* 0x00D2 */
    s16              output_index;                   /* 0x00DA */
    u32              link_actor;                     /* 0x00DC */
    u32              s3_2;                           /* 0x00E0 */
} message_character_process_variables_t;

void check_text_wrapping(z2_game_t *game, message_character_process_variables_t *args, u8 current_character) {
    if (g_message_extension_state.is_wrapping) {
        if (current_character == 0x20) {
            // set last_space_index
            g_message_extension_state.last_space_index = args->output_index;
            // set last_space_cursor_position
            g_message_extension_state.last_space_cursor_position = args->cursor_position;
        } else {
            // if cursor_position > 200 // just a guess at line length
            if (args->cursor_position > 200 && g_message_extension_state.last_space_index >= 0) {
                // replace character at last_space_index with 0x11
                game->msgbox_ctxt.cur_msg_displayed[g_message_extension_state.last_space_index] = 0x11;
                // add one to number_of_new_lines
                args->number_of_new_lines_2++;
                // subtract last_space_cursor_position from cursor_position
                args->cursor_position -= g_message_extension_state.last_space_cursor_position;
                g_message_extension_state.last_space_index = -1;
                g_message_extension_state.last_space_cursor_position = 0;
                // TODO subtract the width of a space from cursor_position
            }
        }
    }
}

/**
 * TODO
 **/
u8 before_message_character_process(z2_game_t *game, message_character_process_variables_t *args) {
    u16 index = game->msgbox_ctxt.cur_msg_char_index;
    u8 current_character = game->msgbox_ctxt.cur_msg_raw[index];
    if (current_character == 0x09) {
        index++;
        current_character = game->msgbox_ctxt.cur_msg_raw[index];
        if (current_character == 0x03 || current_character == 0x04 || current_character == 0x05 || current_character == 0x06 || current_character == 0x07 || current_character == 0x08) {
            if (g_message_extension_state.current_char == -1) {
                index++;
                u32 gi_index = game->msgbox_ctxt.cur_msg_raw[index] << 8;
                index++;
                gi_index |= game->msgbox_ctxt.cur_msg_raw[index];
                u32 new_gi_index = mmr_GetNewGiIndex(game, 0, gi_index, false);
                if (new_gi_index != gi_index) {
                    item_info_t item;
                    bool item_set = true;
                    if (new_gi_index == 0x0A) {
                        item = g_message_extension_state.recovery_heart;
                    } else if (new_gi_index == 0x5B) {
                        item = g_message_extension_state.red_potion;
                    } else if (new_gi_index == 0x91) {
                        item = g_message_extension_state.chateau_romani;
                    } else if (new_gi_index == 0x92) {
                        item = g_message_extension_state.milk;
                    } else if (new_gi_index == 0x93) {
                        item = g_message_extension_state.gold_dust;
                    } else if (new_gi_index == MMR_CONFIG.location_sword_kokiri) {
                        item = g_message_extension_state.sword_kokiri;
                    } else if (new_gi_index == MMR_CONFIG.location_sword_razor) {
                        item = g_message_extension_state.sword_razor;
                    } else if (new_gi_index == MMR_CONFIG.location_sword_gilded) {
                        item = g_message_extension_state.sword_gilded;
                    } else if (new_gi_index == MMR_CONFIG.location_magic_small) {
                        item = g_message_extension_state.magic_small;
                    } else if (new_gi_index == MMR_CONFIG.location_magic_large) {
                        item = g_message_extension_state.magic_large;
                    } else if (new_gi_index == MMR_CONFIG.location_wallet_adult) {
                        item = g_message_extension_state.wallet_adult;
                    } else if (new_gi_index == MMR_CONFIG.location_wallet_giant) {
                        item = g_message_extension_state.wallet_giant;
                    } else if (new_gi_index == MMR_CONFIG.location_bomb_bag_small) {
                        item = g_message_extension_state.bomb_bag_small;
                    } else if (new_gi_index == MMR_CONFIG.location_bomb_bag_big) {
                        item = g_message_extension_state.bomb_bag_big;
                    } else if (new_gi_index == MMR_CONFIG.location_bomb_bag_biggest) {
                        item = g_message_extension_state.bomb_bag_biggest;
                    } else if (new_gi_index == MMR_CONFIG.location_quiver_small) {
                        item = g_message_extension_state.quiver_small;
                    } else if (new_gi_index == MMR_CONFIG.location_quiver_large) {
                        item = g_message_extension_state.quiver_large;
                    } else if (new_gi_index == MMR_CONFIG.location_quiver_largest) {
                        item = g_message_extension_state.quiver_largest;
                    } else {
                        item_set = false;
                    }

                    if (item_set) {
                        g_message_extension_state.current_char = 0;
                        if (current_character == 0x03) {
                            g_message_extension_state.current_replacement = item.name.value;
                            g_message_extension_state.current_replacement_length = item.name.length;
                        } else if (current_character == 0x04) {
                            g_message_extension_state.current_replacement = item.description.value;
                            g_message_extension_state.current_replacement_length = item.description.length;
                        } else if (current_character == 0x05) {
                            g_message_extension_state.current_replacement = item.article.value;
                            g_message_extension_state.current_replacement_length = item.article.length;
                        } else if (current_character == 0x06) {
                            g_message_extension_state.current_replacement = item.pronoun.value;
                            g_message_extension_state.current_replacement_length = item.pronoun.length;
                        } else if (current_character == 0x07) {
                            g_message_extension_state.current_replacement = item.amount.value;
                            g_message_extension_state.current_replacement_length = item.amount.length;
                        } else if (current_character == 0x08) {
                            g_message_extension_state.current_replacement = item.verb.value;
                            g_message_extension_state.current_replacement_length = item.verb.length;
                        } else {
                            // error?
                        }
                    }
                }
                if (g_message_extension_state.current_char == -1) {
                    args->output_index--;
                    game->msgbox_ctxt.cur_msg_char_index = index;
                    return -1;
                }
            }
            if (g_message_extension_state.current_char < g_message_extension_state.current_replacement_length) {
                game->msgbox_ctxt.cur_msg_char_index--;
                current_character = g_message_extension_state.current_replacement[g_message_extension_state.current_char++];

                check_text_wrapping(game, args, current_character);

                game->msgbox_ctxt.cur_msg_displayed[args->output_index] = current_character;
                return current_character;
            }
            g_message_extension_state.current_char = -1;
            current_character = 0x01;
        }

        if (current_character == 0x01) {
            // check gi-index and skip until end command if item has been received before
            index++;
            u32 gi_index = game->msgbox_ctxt.cur_msg_raw[index] << 8;
            index++;
            gi_index |= game->msgbox_ctxt.cur_msg_raw[index];
            u32 new_gi_index = mmr_GetNewGiIndex(game, 0, gi_index, false);
            if (gi_index != new_gi_index) {
                do {
                    index++;
                    current_character = game->msgbox_ctxt.cur_msg_raw[index];
                } while (current_character != 0x09 || game->msgbox_ctxt.cur_msg_raw[index+1] != 0x02);
                index++;
            }
        } else if (current_character == 0x02) {
            // end command
            // does nothing by itself
        } else if (current_character == 0x11) { // begin auto text wrapping
            g_message_extension_state.is_wrapping = true;
        } else if (current_character == 0x12) { // end auto text wrapping
            g_message_extension_state.is_wrapping = false;
            g_message_extension_state.last_space_index = -1;
            g_message_extension_state.last_space_cursor_position = 0;
        } else {
            index--;
        }
        args->output_index--;
        game->msgbox_ctxt.cur_msg_char_index = index;
        return -1;
    }
    
    check_text_wrapping(game, args, current_character);

    game->msgbox_ctxt.cur_msg_displayed[args->output_index] = current_character;
    return current_character;
}
