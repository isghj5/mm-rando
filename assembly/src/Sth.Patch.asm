;==================================================================================================
; Ocean Spider House reward NPC
;==================================================================================================

.headersize G_EN_STH_DELTA

; Replaces:
;   JAL     0x80151938 ; Message_ContinueTextbox
.org 0x80B67660
    jal     Sth_StartRewardTextbox
