;==================================================================================================
; Mikau - Activate Beach Cutscene
;==================================================================================================

.headersize G_EN_ZOG_DELTA

; Check if beach cutscene should activate.
; Replaces:
;   addiu   v0, r0, 0x0001
;   andi    t2, t1, 0x0001
;   beqz    t2, 0x80B950CC
.org 0x80B95074
    jal     Mikau_ShouldActivateBeachCutscene_Hook
    lw      a1, 0x0054 (sp) ;; A1 = GlobalContext.
    beqz    t2, 0x80B950CC
