Chest_UpdateGiIndexWhileOpening_Hook:
    lw      a1, 0x0084 (sp)        ;; A1 = GlobalContext
    j       Chest_GetNewGiIndex    ;; Call function to update flags
    ori     a2, r0, 0x0001         ;; grant = true (update flags)
