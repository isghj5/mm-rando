#include <stdio.h>
#include <z64.h>
#include "MMR.h"
#include "Pictobox.h"

typedef struct {
    const char* name;
    const char* description;
    const char* article; // I sell Bombchu. I sell a Recovery Heart. I sell the Song of Storms. I sell an Empty Bottle.
    const char* pronoun; // I'll buy it. I'll buy them.
    const char* amount; // 150 Rupees for it. What about for 100 Rupees? 150 Rupees for one. What about one for 100 Rupees?
    const char* verb; // Do you know what Bombchu are? Do you know what a Recovery Heart is?
} ItemInfo;

struct MessageExtensionState {
    bool isWrapping;
    s8 lastSpaceIndex;
    f32 lastSpaceCursorPosition;

    ItemInfo greenRupee;
    ItemInfo recoveryHeart;
    ItemInfo redPotion;
    ItemInfo chateauRomani;
    ItemInfo milk;
    ItemInfo goldDust;

    ItemInfo swordKokiri;
    ItemInfo swordRazor;
    ItemInfo swordGilded;

    ItemInfo magicSmall;
    ItemInfo magicLarge;

    ItemInfo walletAdult;
    ItemInfo walletGiant;
    ItemInfo walletRoyal;

    ItemInfo bombBagSmall;
    ItemInfo bombBagBig;
    ItemInfo bombBagBiggest;

    ItemInfo quiverSmall;
    ItemInfo quiverLarge;
    ItemInfo quiverLargest;

    ItemInfo lullaby;
    ItemInfo lullabyIntro;

    s8 currentChar;
    char* currentReplacement;
    u16 currentReplacementLength;
    char tempStrayFairyCount[4];
};

const static char articleIndefinite[] = "a "; // intentional trailing space.

const static char articleIndefiniteVowel[] = "an "; // intentional trailing space.

const static char articleDefinite[] = "the "; // intentional trailing space.

const static char articleEmpty[] = "";

const static char pronounSingular[] = "it";

const static char amountSingular[] = " one"; // intentional leading space.

const static char amountDefinite[] = " it"; // intentional leading space.

const static char verbSingular[] = "is";

static struct MessageExtensionState gMessageExtensionState = {
    .isWrapping = false,
    .lastSpaceIndex = -1,
    .lastSpaceCursorPosition = 0,

    .greenRupee = {
        .name = "Green Rupee",
        .description = "This is worth 1 rupee.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .recoveryHeart = {
        .name = "Recovery Heart",
        .description = "Replenishes a small amount of your life energy.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .redPotion = {
        .name = "Red Potion",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .chateauRomani = {
        .name = "Chateau Romani",
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .milk = {
        .name = "Milk",
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .goldDust = {
        .name = "Gold Dust",
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .swordKokiri = {
        .name = "Kokiri Sword",
        .description = "A sword created by forest folk.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .swordRazor = {
        .name = "Razor Sword",
        .description = "A sharp sword forged at the smithy.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .swordGilded = {
        .name = "Gilded Sword",
        .description = "A very sharp sword forged from gold dust.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .magicSmall = {
        .name = "Magic Power",
        .description = "Grants the ability to use magic.",
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },
    .magicLarge = {
        .name = "Extended Magic Power",
        .description = "Grants the ability to use lots of magic.",
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .walletAdult = {
        .name = "Adult Wallet",
        .description = "This can hold up to a maximum of 200 rupees.",
        .article = articleIndefiniteVowel,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .walletGiant = {
        .name = "Giant Wallet",
        .description = "This can hold up to a maximum of 500 rupees.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .walletRoyal = {
        .name = "Royal Wallet",
        .description = "This can hold up to a maximum of 999 rupees.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .bombBagSmall = {
        .name = "Bomb Bag",
        .description = "This can hold up to a maximum of 20 bombs.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .bombBagBig = {
        .name = "Big Bomb Bag",
        .description = "This can hold up to a maximum of 30 bombs.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .bombBagBiggest = {
        .name = "Biggest Bomb Bag",
        .description = "This can hold up to a maximum of 40 bombs.",
        .article = articleDefinite,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .quiverSmall = {
        .name = "Hero's Bow",
        .description = "Use it to shoot arrows.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .quiverLarge = {
        .name = "Large Quiver",
        .description = "This can hold up to a maximum of 40 arrows.",
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .quiverLargest = {
        .name = "Largest Quiver",
        .description = "This can hold up to a maximum of 50 arrows.",
        .article = articleDefinite,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .lullaby = {
        .name = "Goron Lullaby",
        .description = "This melody blankets listeners in calm while making eyelids grow heavy.",
        .article = articleDefinite,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },
    .lullabyIntro = {
        .name = "Goron Lullaby Intro",
        .description = "The soothing melody of a thoughtful father.",
        .article = articleDefinite,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .currentChar = -1,
    .currentReplacementLength = 0,
};

// Slice of hooked function's stack frame, any pointers may not be valid.
typedef struct {
    /* 0x00 */ UNK_TYPE1 pad0[0x20];
    /* 0x20 */ f64 unk20;
    /* 0x28 */ f64 unk28;
    /* 0x30 */ f64 unk30;
    /* 0x38 */ f64 unk38;
    /* 0x40 */ u32 s0;
    /* 0x44 */ u32 s1;
    /* 0x48 */ u32 s2;
    /* 0x4C */ u32 s3;
    /* 0x50 */ GlobalContext* s4;
    /* 0x54 */ u32 s5;
    /* 0x58 */ u32 s6;
    /* 0x5C */ u32 s7;
    /* 0x60 */ u32 fp;
    /* 0x64 */ void* returnAddress;
    /* 0x68 */ UNK_TYPE1 pad68[0x8];
    /* 0x70 */ MessageContext* msgCtx;
    /* 0x74 */ UNK_TYPE1 pad74[0x30];
    /* 0xA4 */ f32 cursorPosition;
    /* 0xA8 */ UNK_TYPE1 padA8[0x14];
    /* 0xBC */ u32 unkBC;
    /* 0xC0 */ UNK_TYPE1 padC0[0x6];
    /* 0xC6 */ u8 numberOfNewLines;
    /* 0xC7 */ UNK_TYPE1 padC7[0x7];
    /* 0xCE */ u16 unkCE;
    /* 0xD0 */ s16 numberOfNewLines2;
    /* 0xD2 */ UNK_TYPE1 padD2[0x8];
    /* 0xDA */ s16 outputIndex;
    /* 0xDC */ ActorPlayer* player;
    /* 0xE0 */ u32 unkE0;
    /* 0xE4 */ UNK_TYPE1 unkE4[0x4];
} MessageCharacterProcessVariables; // size = 0xE8

static void CheckTextWrapping(GlobalContext* ctxt, MessageCharacterProcessVariables* args, u8 currentCharacter) {
    if (gMessageExtensionState.isWrapping) {
        if (currentCharacter == 0x20) {
            // set lastSpaceIndex
            gMessageExtensionState.lastSpaceIndex = args->outputIndex;
            // set lastSpaceCursorPosition
            gMessageExtensionState.lastSpaceCursorPosition = args->cursorPosition;
        } else {
            // if cursorPosition > 200 // just a guess at line length
            if (args->cursorPosition > 200 && gMessageExtensionState.lastSpaceIndex >= 0) {
                // replace character at lastSpaceIndex with 0x11
                ctxt->msgCtx.currentMessageDisplayed[gMessageExtensionState.lastSpaceIndex] = 0x11;
                // add one to numberOfNewLines
                args->numberOfNewLines2++;
                // subtract lastSpaceCursorPosition from cursorPosition
                args->cursorPosition -= gMessageExtensionState.lastSpaceCursorPosition;
                gMessageExtensionState.lastSpaceIndex = -1;
                gMessageExtensionState.lastSpaceCursorPosition = 0;
                // TODO subtract the width of a space from cursorPosition
            }
        }
    }
}

static u8 ProcessCurrentReplacement(GlobalContext* ctxt, MessageCharacterProcessVariables* args) {
    if (gMessageExtensionState.currentChar >= 0 && gMessageExtensionState.currentChar < gMessageExtensionState.currentReplacementLength) {
        ctxt->msgCtx.currentMessageCharIndex--;
        u8 currentCharacter = gMessageExtensionState.currentReplacement[gMessageExtensionState.currentChar++];

        CheckTextWrapping(ctxt, args, currentCharacter);

        ctxt->msgCtx.currentMessageDisplayed[args->outputIndex] = currentCharacter;
        return currentCharacter;
    }
    gMessageExtensionState.currentChar = -1;
    return 0xFF;
}

/**
 * TODO
 **/
u8 Message_BeforeCharacterProcess(GlobalContext* ctxt, MessageCharacterProcessVariables* args) {
    u16 index = ctxt->msgCtx.currentMessageCharIndex;
    u8 currentCharacter = ctxt->msgCtx.currentMessageRaw[index];
    if (currentCharacter == 0x09) {
        index++;
        currentCharacter = ctxt->msgCtx.currentMessageRaw[index];
        if (currentCharacter == 0x03 || currentCharacter == 0x04 || currentCharacter == 0x05 || currentCharacter == 0x06 || currentCharacter == 0x07 || currentCharacter == 0x08) {
            if (gMessageExtensionState.currentChar == -1) {
                index++;
                u32 giIndex = ctxt->msgCtx.currentMessageRaw[index] << 8;
                index++;
                giIndex |= ctxt->msgCtx.currentMessageRaw[index];
                u32 newGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
                if (newGiIndex != giIndex) {
                    ItemInfo item;
                    bool itemSet = true;
                    if (newGiIndex == 0x01) {
                        item = gMessageExtensionState.greenRupee;
                    } else if (newGiIndex == 0x0A) {
                        item = gMessageExtensionState.recoveryHeart;
                    } else if (newGiIndex == 0x5B) {
                        item = gMessageExtensionState.redPotion;
                    } else if (newGiIndex == 0x91) {
                        item = gMessageExtensionState.chateauRomani;
                    } else if (newGiIndex == 0x92) {
                        item = gMessageExtensionState.milk;
                    } else if (newGiIndex == 0x93) {
                        item = gMessageExtensionState.goldDust;
                    } else if (newGiIndex == MMR_CONFIG.locations.swordKokiri) {
                        item = gMessageExtensionState.swordKokiri;
                    } else if (newGiIndex == MMR_CONFIG.locations.swordRazor) {
                        item = gMessageExtensionState.swordRazor;
                    } else if (newGiIndex == MMR_CONFIG.locations.swordGilded) {
                        item = gMessageExtensionState.swordGilded;
                    } else if (newGiIndex == MMR_CONFIG.locations.magicSmall) {
                        item = gMessageExtensionState.magicSmall;
                    } else if (newGiIndex == MMR_CONFIG.locations.magicLarge) {
                        item = gMessageExtensionState.magicLarge;
                    } else if (newGiIndex == MMR_CONFIG.locations.walletAdult) {
                        item = gMessageExtensionState.walletAdult;
                    } else if (newGiIndex == MMR_CONFIG.locations.walletGiant) {
                        item = gMessageExtensionState.walletGiant;
                    } else if (newGiIndex == MMR_CONFIG.locations.walletRoyal) {
                        item = gMessageExtensionState.walletRoyal;
                    } else if (newGiIndex == MMR_CONFIG.locations.bombBagSmall) {
                        item = gMessageExtensionState.bombBagSmall;
                    } else if (newGiIndex == MMR_CONFIG.locations.bombBagBig) {
                        item = gMessageExtensionState.bombBagBig;
                    } else if (newGiIndex == MMR_CONFIG.locations.bombBagBiggest) {
                        item = gMessageExtensionState.bombBagBiggest;
                    } else if (newGiIndex == MMR_CONFIG.locations.quiverSmall) {
                        item = gMessageExtensionState.quiverSmall;
                    } else if (newGiIndex == MMR_CONFIG.locations.quiverLarge) {
                        item = gMessageExtensionState.quiverLarge;
                    } else if (newGiIndex == MMR_CONFIG.locations.quiverLargest) {
                        item = gMessageExtensionState.quiverLargest;
                    } else if (newGiIndex == MMR_CONFIG.locations.lullaby) {
                        item = gMessageExtensionState.lullaby;
                    } else if (newGiIndex == MMR_CONFIG.locations.lullabyIntro) {
                        item = gMessageExtensionState.lullabyIntro;
                    } else {
                        itemSet = false;
                    }

                    if (itemSet) {
                        gMessageExtensionState.currentChar = 0;
                        const char* text;
                        if (currentCharacter == 0x03) {
                            text = item.name;
                        } else if (currentCharacter == 0x04) {
                            text = item.description;
                        } else if (currentCharacter == 0x05) {
                            text = item.article;
                        } else if (currentCharacter == 0x06) {
                            text = item.pronoun;
                        } else if (currentCharacter == 0x07) {
                            text = item.amount;
                        } else if (currentCharacter == 0x08) {
                            text = item.verb;
                        } else {
                            text = "";
                            // error?
                        }
                        gMessageExtensionState.currentReplacement = (char*)text;
                        gMessageExtensionState.currentReplacementLength = z2_strlen(text);
                    }
                }
                if (gMessageExtensionState.currentChar == -1) {
                    args->outputIndex--;
                    ctxt->msgCtx.currentMessageCharIndex = index;
                    return -1;
                }
            }
            currentCharacter = ProcessCurrentReplacement(ctxt, args);
            if (currentCharacter != 0xFF) {
                return currentCharacter;
            }
            currentCharacter = 0x01;
        }

        if (currentCharacter == 0x01) {
            // check gi-index and skip until end command if item has been received before
            index++;
            u32 giIndex = ctxt->msgCtx.currentMessageRaw[index] << 8;
            index++;
            giIndex |= ctxt->msgCtx.currentMessageRaw[index];
            u32 newGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
            if (giIndex != newGiIndex) {
                do {
                    index++;
                    currentCharacter = ctxt->msgCtx.currentMessageRaw[index];
                } while (currentCharacter != 0x09 || ctxt->msgCtx.currentMessageRaw[index+1] != 0x02);
                index++;
            }
        } else if (currentCharacter == 0x02) {
            // end command
            // does nothing by itself
        } else if (currentCharacter == 0x09) {
            if (gMessageExtensionState.currentChar == -1) {
                u16 giIndex = 0xFFFF;
                u8 count = 0;
                do {
                    index++;
                    giIndex = ctxt->msgCtx.currentMessageRaw[index] << 8;
                    index++;
                    giIndex |= ctxt->msgCtx.currentMessageRaw[index];
                    if (giIndex != 0xFFFF && !MMR_GetGiFlag(giIndex)) {
                        count++;
                    }
                } while (giIndex != 0xFFFF);

                if (count > 0) {
                    s16 digits[4];
                    digits[0] = digits[1] = 0;
                    digits[2] = count;

                    while (digits[2] >= 100) {
                        digits[0]++;
                        digits[2] -= 100;
                    }
                    while (digits[2] >= 10) {
                        digits[1]++;
                        digits[2] -= 10;
                    }

                    u8 fairyCharIndex = 0;
                    gMessageExtensionState.tempStrayFairyCount[fairyCharIndex++] = 1; // Red color

                    bool loadChar = false;
                    for (u8 i = 0; i < 3; i++) {
                        if ((i == 2) || (digits[i] != 0)) {
                            loadChar = true;
                        }
                        if (loadChar) {
                            gMessageExtensionState.tempStrayFairyCount[fairyCharIndex++] = '0' + digits[i];
                        }
                    }

                    gMessageExtensionState.currentReplacement = gMessageExtensionState.tempStrayFairyCount;
                    gMessageExtensionState.currentReplacementLength = fairyCharIndex;
                    gMessageExtensionState.currentChar = 0;
                } else {
                    // TODO maybe clean this up, it's kind of a copy of command 0x01
                    do {
                        index++;
                        currentCharacter = ctxt->msgCtx.currentMessageRaw[index];
                    } while (currentCharacter != 0x09 || ctxt->msgCtx.currentMessageRaw[index+1] != 0x02);
                    index++;
                    args->outputIndex--;
                    ctxt->msgCtx.currentMessageCharIndex = index;
                    return -1;
                }
            }
            currentCharacter = ProcessCurrentReplacement(ctxt, args);
            if (currentCharacter != 0xFF) {
                return currentCharacter;
            } else {
                u16 giIndex = 0xFFFF;
                do {
                    index++;
                    giIndex = ctxt->msgCtx.currentMessageRaw[index] << 8;
                    index++;
                    giIndex |= ctxt->msgCtx.currentMessageRaw[index];
                } while (giIndex != 0xFFFF);
            }
        } else if (currentCharacter == 0x11) { // begin auto text wrapping
            gMessageExtensionState.isWrapping = true;
        } else if (currentCharacter == 0x12) { // end auto text wrapping
            gMessageExtensionState.isWrapping = false;
            gMessageExtensionState.lastSpaceIndex = -1;
            gMessageExtensionState.lastSpaceCursorPosition = 0;
        } else if (currentCharacter == 0x13) {
            // Write the text for the current Pictobox picture subject.
            // TODO: This implementation is mostly a copy-paste of the code used to write grammar text, maybe clean this up later.
            if (gMessageExtensionState.currentChar == -1) {
                const char* text = Pictobox_CurrentText();
                gMessageExtensionState.currentReplacement = (char*)text;
                gMessageExtensionState.currentReplacementLength = z2_strlen(text);
                gMessageExtensionState.currentChar = 0;
            }
            currentCharacter = ProcessCurrentReplacement(ctxt, args);
            if (currentCharacter != 0xFF) {
                return currentCharacter;
            }
        } else {
            index--;
        }
        args->outputIndex--;
        ctxt->msgCtx.currentMessageCharIndex = index;
        return -1;
    }

    CheckTextWrapping(ctxt, args, currentCharacter);

    ctxt->msgCtx.currentMessageDisplayed[args->outputIndex] = currentCharacter;
    return currentCharacter;
}

u16 Message_GetStrayFairyIconColorIndex(MessageContext* msgCtx) {
    u16 id = msgCtx->currentMessageId;
    if (id >= 0x74 && id <= 0x77) {
        return id - 0x74;
    }
    if (id >= 0x584 && id <= 0x58D) {
        return (id - 0x584) / 3;
    }
    return gSaveContext.dungeonIndex;
}
