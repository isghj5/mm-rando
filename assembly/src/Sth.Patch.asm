;==================================================================================================
; Ocean Spider House reward NPC
;==================================================================================================

.headersize G_EN_STH_DELTA

; Replaces:
;   JAL     0x80151938 ; Message_ContinueTextbox
.org 0x80B67660
    jal     Sth_StartRewardTextbox

; Replaces:
;   lh         a0,0xa4(a1)
;   jal        Inventory_GetSkullTokenCount
;   sw         a2,0x18(sp)
;   slti       at,v0,0x1e
;   bne        at,zero,0x80b67e68
.org 0x80B67E2C
    sw      a2, 0x0018 (sp)
    jal     Sth_ShouldSpawn
    or      a0, a1, r0
    beqz    v0, 0x80B67E68
    nop

; Replaces:
;   lw         t4,0x44(sp)
;   jal        Inventory_GetSkullTokenCount
;   lh         a0,0xa4(t4)
;   slti       at,v0,0x1e
;   beq        at,zero,0x80b68094
.org 0x80B68060
    lw      a0, 0x0044 (sp)
    jal     Sth_ShouldSpawn
    nop
    bnez    v0, 0x80B68094
    nop
