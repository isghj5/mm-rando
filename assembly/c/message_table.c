#include <stdbool.h>
#include "z2.h"

#define MESSAGE_TABLE_EXT_COUNT 0x40

typedef struct z2_msg_table_entry_s {
    u16 text_id;
    u16 padding;
    u32 segaddr;
} z2_msg_table_entry_t;

// Extended message table.
z2_msg_table_entry_t EXT_MSG_TABLE[MESSAGE_TABLE_EXT_COUNT] = { 0 };

// Extended message table count.
u32 EXT_MSG_TABLE_COUNT = MESSAGE_TABLE_EXT_COUNT;

// File index of extended message data.
u32 EXT_MSG_DATA_FILE = 0;

// Whether or not the current message is extended and should use the extended message data file.
static bool g_current_extended = false;

/**
 * Hook function used to perform a message table extended lookup if text Id not found in the
 * primary message table.
 **/
bool message_table_lookup_extended(z2_game_t *game, u16 text_id) {
    for (int i = 0; i < MESSAGE_TABLE_EXT_COUNT; i++) {
        u16 cur_id = EXT_MSG_TABLE[i].text_id;
        // For this table, use 0 as terminator in addition to 0xFFFF.
        if (cur_id == 0 || cur_id == 0xFFFF) {
            break;
        } else if (cur_id == text_id) {
            u32 offset = EXT_MSG_TABLE[i].segaddr - EXT_MSG_TABLE[0].segaddr;
            u32 length = EXT_MSG_TABLE[i+1].segaddr - EXT_MSG_TABLE[i].segaddr;
            game->msgbox_ctxt.msg_data_offset = offset;
            game->msgbox_ctxt.msg_data_length = length;
            // Mark current message as extended.
            g_current_extended = true;
            return true;
        }
    }

    return false;
}

/**
 * Helper function to check if current message is in extended table, and resets to false.
 **/
bool message_table_pop_current_extended(void) {
    bool result = g_current_extended;
    g_current_extended = false;
    return result;
}

/**
 * Hook function used to get the VROM address of the relevant message data file.
 **/
u32 message_table_get_data_file_vrom(void) {
    if (message_table_pop_current_extended()) {
        // Return extended data file VROM address.
        return z2_file_table[EXT_MSG_DATA_FILE].vrom_start;
    } else {
        // Return original VROM address.
        return 0xAD1000;
    }
}

/**
 * Hook function used to check if a message is a "primary message" by Id. This determines which
 * message data file should be used (0xAD1000 or 0xB3B000), among other things.
 **/
bool message_table_is_primary_message(z2_game_t *game, u32 text_id) {
    // Credits message table seems to use Ids: [0x4E20, 0x4E4C]
    return (text_id < 0x4E20) || (0x4E4C < text_id);
}
