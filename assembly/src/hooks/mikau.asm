;==================================================================================================
; Mikau - Activate Beach Cutscene
;==================================================================================================

.headersize(G_EN_ZOG_VRAM - G_EN_ZOG_FILE)

; Check if beach cutscene should activate.
; Replaces:
;   addiu   v0, r0, 0x0001
;   andi    t2, t1, 0x0001
;   beqz    t2, 0x80B950CC
.org 0x80B95074
    jal     mikau_should_activate_beach_cutscene_hook
    lw      a1, 0x0054 (sp) ;; A1 = GlobalContext.
    beqz    t2, 0x80B950CC
