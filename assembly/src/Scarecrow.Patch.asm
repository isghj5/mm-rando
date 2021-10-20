;==================================================================================================
; Free Scarecrow's Song
;==================================================================================================

.headersize G_EN_KAKASI_DELTA

; Preserve RA for returning, and check for Scarecrow's Song flag or Free Scarecrow bit.
; Replaces:
;   lui     t6, 0x801F
;   lbu     t6, 0x05B7 (t6)
;   andi    t7, t6, 0x0008
;   beqz    t7, 0x80971854
;   nop
;   lwc1    f4, 0x0098 (a0)
.org 0x809717D0 ; Offset: 0x21F0
    addiu   sp, sp, -0x14
    sw      ra, 0x0010 (sp)
    jal     Scarecrow_CheckSongFlag_Hook
    nop
    beqz    v0, 0x80971854
    lw      ra, 0x0010 (sp)

; Check if Scarecrow actor should "activate" and update its function pointer.
; Replaces:
;   lhu     t0, 0x6932 (t0)
;   addiu   at, r0, 0x000D
;   bne     t0, at, 0x80971854
;   nop
.org 0x80971820 ; Offset: 0x2240
    jal     Scarecrow_ShouldActivate_Hook
    lhu     t0, 0x6932 (t0)
    beqz    v0, 0x80971854
    lw      ra, 0x0010 (sp)

; Replaces:
;   nop
.org 0x80971858
    addiu   sp, sp, 0x14

; Check if Scarecrow actor should spawn.
; Replaces:
;   lh      a0, 0x01AE (v1)
;   jal     0x800F1BE4 ; Calls ActorCutscene_GetCanPlayNext
;   sw      v1, 0x0020 (sp)
.org 0x809718CC ; Offset: 0x22EC
    lw      a1, 0x002C (sp)
    jal     Scarecrow_ShouldSpawn_Hook
    sw      v1, 0x0020 (sp)
