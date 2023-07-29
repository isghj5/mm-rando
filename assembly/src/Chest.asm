Chest_UpdateGiIndexWhileOpening_Hook:
    lw      a1, 0x0084 (sp)        ;; A1 = GlobalContext
    j       Chest_GetNewGiIndex    ;; Call function to update flags
    or      a2, r0, r0             ;; grant = false (do not update flags)
