#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "macro.h"

const u16 baseGiIndex = 0x44F;

bool BombersNotebook_ShouldGrant(GlobalContext* ctxt, u8 notebookEntryIndex) {
    if (!gSaveContext.perm.inv.questStatus.bombersNotebook && notebookEntryIndex < 20) {
        return false;
    }

    if (MISC_CONFIG.internal.vanillaLayout) {
        u16* sBombersNotebookEventWeekEventFlags = (u16*)0x801C6B28;
        return !CHECK_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[notebookEntryIndex]);
    }

    u16* sBombersNotebookEventMessages = (u16*)0x801C6AB8;
    return sBombersNotebookEventMessages[notebookEntryIndex] != 0 && !MMR_GetGiFlag(baseGiIndex + notebookEntryIndex);
}

s8 BombersNotebook_Grant(GlobalContext* ctxt) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return 0;
    }

    MessageContext* msgCtx = &ctxt->msgCtx;

    u8 notebookEntryIndex = msgCtx->unk120B2[msgCtx->unk120B1];
    u16 giIndex = baseGiIndex + notebookEntryIndex;
    u16* sBombersNotebookEventMessages = (u16*)0x801C6AB8;
    u16 textId = sBombersNotebookEventMessages[notebookEntryIndex];
    if (textId != 0 && !MMR_GetGiFlag(giIndex)) {
        if (MMR_GetGiEntry(giIndex)->message == 0) { // if this entry is not randomized
            giIndex = MMR_GetNewGiIndex(ctxt, NULL, giIndex, true);
            GetItemEntry* entry = MMR_GetGiEntry(giIndex);
            *MMR_GetItemEntryContext = *entry;
            z2_GiveItem(ctxt, entry->item);
            if (gSaveContext.perm.inv.questStatus.bombersNotebook) {
                z2_Message_ContinueTextbox(ctxt, textId);
                z2_PlaySfx(0x4855); // NA_SE_SY_SCHEDULE_WRITE
                return 1;
            }
            return -1;
        }
        MMR_ProcessItem(ctxt, giIndex, true);
        z2_PlaySfx(0x4855); // NA_SE_SY_SCHEDULE_WRITE
        return 1;
    }
    return -1;
}

/*
2147 - Meeting the Bombers
2134 - Meeting Anju
2135 - Meeting Kafei
2136 - Meeting Curiosity Shop Man
2137 - Meeting Old Lady
2138 - Meeting Romani
2139 - Meeting Cremia
213A - Meeting the Mayor
213B - Meeting Madame Aroma
213C - Meeting Toto
213D - Meeting Gorman
213E - Meeting the postman
213F - Meeting the Rosa sisters
2140 - Meeting the Toilet Hand
2141 - Meeting Anju's grandmother
2142 - Meeting Kamaro
2143 - Meeting Grog
2144 - Meeting the Gorman Brothers
2145 - Meeting Shiro
2146 - Meeting Guru-Guru
2152 - Receiving Room Key
2153 - Secret Night Meeting
2154 - Promised to meet Kafei
0000 - Received Letter to Kafei
2156 - Deposit Letter to Kafei
2157 - Receiving the Pendant of Memories
2158 - Delivered Pendant
2159 - Escaped from Sakon's Hideout
215A - Promising Romani to help with the aliens
215B - Defending against the aliens
0000 - Received Milk Bottle
215D - Escorting Cremia
0000 - Received Romani's Mask
215F - Received Keaton Mask
2160 - Receiving priority mail
2161 - Delivering the priority mail to Madame Aroma
0000 - Secret Code
2163 - Received Bombers Notebook
2164 - Calming the mayor's meeting
2165 - Teaching Rosa sisters the dance
2166 - Giving toilet paper
2167 - Grandma short story?
2168 - Grandma long story?
2169 - Postman's Minigame
216A - Accepting the Kafei's Mask
216B - Buying the All Night's Mask
216C - Received the Bunny Hood
216D - Defeating the Gorman Brothers
216E - Milk Bar Performance
216F - Freeing the postman
2170 - Receiving reward from Anju and Kafei
2171 - Saving old lady
2172 - Healing Kamaro
2173 - Saving Shiro
2174 - Listening to Guru Guru
*/
