#include <stdbool.h>
#include <z64.h>

#define MESSAGE_TABLE_EXT_COUNT 0x40

struct MsgTableEntry {
    u16 textId;
    u16 padding;
    u32 segaddr;
};

// Extended message table.
struct MsgTableEntry EXT_MSG_TABLE[MESSAGE_TABLE_EXT_COUNT] = { 0 };

// Extended message table count.
u32 EXT_MSG_TABLE_COUNT = MESSAGE_TABLE_EXT_COUNT;

// File index of extended message data.
u32 EXT_MSG_DATA_FILE = 0;

// Whether or not the current message is extended and should use the extended message data file.
static bool gCurrentExtended = false;

/**
 * Helper function to check if current message is in extended table, and resets to false.
 **/
static bool PopCurrentExtended(void) {
    bool result = gCurrentExtended;
    gCurrentExtended = false;
    return result;
}

/**
 * Hook function used to perform a message table extended lookup if text Id not found in the
 * primary message table.
 **/
bool MessageTable_LookupExtended(GlobalContext* ctxt, u16 textId) {
    for (int i = 0; i < MESSAGE_TABLE_EXT_COUNT; i++) {
        u16 curId = EXT_MSG_TABLE[i].textId;
        // For this table, use 0 as terminator in addition to 0xFFFF.
        if (curId == 0 || curId == 0xFFFF) {
            break;
        } else if (curId == textId) {
            u32 offset = EXT_MSG_TABLE[i].segaddr - EXT_MSG_TABLE[0].segaddr;
            u32 length = EXT_MSG_TABLE[i+1].segaddr - EXT_MSG_TABLE[i].segaddr;
            ctxt->msgCtx.messageDataOffset = offset;
            ctxt->msgCtx.messageDataLength = length;
            // Mark current message as extended.
            gCurrentExtended = true;
            return true;
        }
    }
    return false;
}

/**
 * Hook function used to get the VROM address of the relevant message data file.
 **/
u32 MessageTable_GetDataFileVrom(void) {
    if (PopCurrentExtended()) {
        // Return extended data file VROM address.
        return dmadata[EXT_MSG_DATA_FILE].vromStart;
    } else {
        // Return original VROM address.
        return 0xAD1000;
    }
}

/**
 * Hook function used to check if a message is a "primary message" by Id. This determines which
 * message data file should be used (0xAD1000 or 0xB3B000), among other things.
 **/
bool MessageTable_IsPrimaryMessage(GlobalContext* ctxt, u32 textId) {
    // Credits message table seems to use Ids: [0x4E20, 0x4E4C]
    return (textId < 0x4E20) || (0x4E4C < textId);
}
