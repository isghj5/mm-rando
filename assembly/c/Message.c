#include <stdio.h>
#include <z64.h>
#include "MMR.h"

typedef struct String {
    char* value;
    u16 length;
} String;

typedef struct {
    String name;
    String description;
    String article; // I sell Bombchu. I sell a Recovery Heart. I sell the Song of Storms. I sell an Empty Bottle.
    String pronoun; // I'll buy it. I'll buy them.
    String amount; // 150 Rupees for it. What about for 100 Rupees? 150 Rupees for one. What about one for 100 Rupees?
    String verb; // Do you know what Bombchu are? Do you know what a Recovery Heart is?
} ItemInfo;

struct MessageExtensionState {
    bool isWrapping;
    s8 lastSpaceIndex;
    f32 lastSpaceCursorPosition;

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

    ItemInfo bombBagSmall;
    ItemInfo bombBagBig;
    ItemInfo bombBagBiggest;

    ItemInfo quiverSmall;
    ItemInfo quiverLarge;
    ItemInfo quiverLargest;

    s8 currentChar;
    char* currentReplacement;
    u16 currentReplacementLength;
};

const String articleIndefinite = {
    .value = "a ", // intentional trailing space.
    .length = 2,
};

const String articleIndefiniteVowel = {
    .value = "an ", // intentional trailing space.
    .length = 3,
};

const String articleDefinite = {
    .value = "the ", // intentional trailing space.
    .length = 4,
};

const String articleEmpty = {
    .value = "",
    .length = 0,
};

const String pronounSingular = {
    .value = "it",
    .length = 2,
};

const String amountSingular = {
    .value = " one", // intentional leading space.
    .length = 4,
};

const String amountDefinite = {
    .value = " it", // intentional leading space.
    .length = 3,
};

const String verbSingular = {
    .value = "is",
    .length = 2,
};

static struct MessageExtensionState gMessageExtensionState = {
    .isWrapping = false,
    .lastSpaceIndex = -1,
    .lastSpaceCursorPosition = 0,

    .recoveryHeart = {
        .name = {
            .value = "Recovery Heart",
            .length = 14,
        },
        .description = {
            .value = "Replenishes a small amount of your life energy.",
            .length = 47,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .redPotion = {
        .name = {
            .value = "Red Potion",
            .length = 10,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .chateauRomani = {
        .name = {
            .value = "Chateau Romani",
            .length = 14,
        },
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .milk = {
        .name = {
            .value = "Milk",
            .length = 4,
        },
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .goldDust = {
        .name = {
            .value = "Gold Dust",
            .length = 9,
        },
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .swordKokiri = {
        .name = {
            .value = "Kokiri Sword",
            .length = 12,
        },
        .description = {
            .value = "A sword created by forest folk.",
            .length = 31,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .swordRazor = {
        .name = {
            .value = "Razor Sword",
            .length = 11,
        },
        .description = {
            .value = "A sharp sword forged at the smithy.",
            .length = 35,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .swordGilded = {
        .name = {
            .value = "Gilded Sword",
            .length = 14,
        },
        .description = {
            .value = "A very sharp sword forged from gold dust.",
            .length = 41,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .magicSmall = {
        .name = {
            .value = "Magic Power",
            .length = 11,
        },
        .description = {
            .value = "Grants the ability to use magic.",
            .length = 32,
        },
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },
    .magicLarge = {
        .name = {
            .value = "Extended Magic Power",
            .length = 20,
        },
        .description = {
            .value = "Grants the ability to use lots of magic.",
            .length = 40,
        },
        .article = articleEmpty,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .walletAdult = {
        .name = {
            .value = "Adult Wallet",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 200 rupees.",
            .length = 44,
        },
        .article = articleIndefiniteVowel,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .walletGiant = {
        .name = {
            .value = "Giant Wallet",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 500 rupees.",
            .length = 44,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },

    .bombBagSmall = {
        .name = {
            .value = "Bomb Bag",
            .length = 8,
        },
        .description = {
            .value = "This can hold up to a maximum of 20 bombs.",
            .length = 42,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .bombBagBig = {
        .name = {
            .value = "Big Bomb Bag",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 30 bombs.",
            .length = 42,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .bombBagBiggest = {
        .name = {
            .value = "Biggest Bomb Bag",
            .length = 16,
        },
        .description = {
            .value = "This can hold up to a maximum of 40 bombs.",
            .length = 42,
        },
        .article = articleDefinite,
        .pronoun = pronounSingular,
        .amount = amountDefinite,
        .verb = verbSingular,
    },

    .quiverSmall = {
        .name = {
            .value = "Hero's Bow",
            .length = 10,
        },
        .description = {
            .value = "Use it to shoot arrows.",
            .length = 23,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .quiverLarge = {
        .name = {
            .value = "Large Quiver",
            .length = 12,
        },
        .description = {
            .value = "This can hold up to a maximum of 40 arrows.",
            .length = 43,
        },
        .article = articleIndefinite,
        .pronoun = pronounSingular,
        .amount = amountSingular,
        .verb = verbSingular,
    },
    .quiverLargest = {
        .name = {
            .value = "Largest Quiver",
            .length = 14,
        },
        .description = {
            .value = "This can hold up to a maximum of 50 arrows.",
            .length = 47,
        },
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
                    if (newGiIndex == 0x0A) {
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
                    } else {
                        itemSet = false;
                    }

                    if (itemSet) {
                        gMessageExtensionState.currentChar = 0;
                        if (currentCharacter == 0x03) {
                            gMessageExtensionState.currentReplacement = item.name.value;
                            gMessageExtensionState.currentReplacementLength = item.name.length;
                        } else if (currentCharacter == 0x04) {
                            gMessageExtensionState.currentReplacement = item.description.value;
                            gMessageExtensionState.currentReplacementLength = item.description.length;
                        } else if (currentCharacter == 0x05) {
                            gMessageExtensionState.currentReplacement = item.article.value;
                            gMessageExtensionState.currentReplacementLength = item.article.length;
                        } else if (currentCharacter == 0x06) {
                            gMessageExtensionState.currentReplacement = item.pronoun.value;
                            gMessageExtensionState.currentReplacementLength = item.pronoun.length;
                        } else if (currentCharacter == 0x07) {
                            gMessageExtensionState.currentReplacement = item.amount.value;
                            gMessageExtensionState.currentReplacementLength = item.amount.length;
                        } else if (currentCharacter == 0x08) {
                            gMessageExtensionState.currentReplacement = item.verb.value;
                            gMessageExtensionState.currentReplacementLength = item.verb.length;
                        } else {
                            // error?
                        }
                    }
                }
                if (gMessageExtensionState.currentChar == -1) {
                    args->outputIndex--;
                    ctxt->msgCtx.currentMessageCharIndex = index;
                    return -1;
                }
            }
            if (gMessageExtensionState.currentChar < gMessageExtensionState.currentReplacementLength) {
                ctxt->msgCtx.currentMessageCharIndex--;
                currentCharacter = gMessageExtensionState.currentReplacement[gMessageExtensionState.currentChar++];

                CheckTextWrapping(ctxt, args, currentCharacter);

                ctxt->msgCtx.currentMessageDisplayed[args->outputIndex] = currentCharacter;
                return currentCharacter;
            }
            gMessageExtensionState.currentChar = -1;
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
        } else if (currentCharacter == 0x11) { // begin auto text wrapping
            gMessageExtensionState.isWrapping = true;
        } else if (currentCharacter == 0x12) { // end auto text wrapping
            gMessageExtensionState.isWrapping = false;
            gMessageExtensionState.lastSpaceIndex = -1;
            gMessageExtensionState.lastSpaceCursorPosition = 0;
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
