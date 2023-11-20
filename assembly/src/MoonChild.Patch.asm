;==================================================================================================
;  Moon Child alter conversation for victory modes
;==================================================================================================

.headersize G_EN_JS_DELTA

; Replaces:
;   BEQL    V0, AT, .+0xC0
.org 0x8096A498
    beql    v0, at, .+0x10

; Replaces:
;   B       .+0x248
;   LW      RA, 0x0014 (SP)
.org 0x8096A4A0
    b       .+0xB8
    lbu     v0, 0x6929 (v1)

; Replaces:
;   ADDIU   A1, R0, 0x21FE
.org 0x8096A4AC
    lhu     a1, 0x680C (v1)

; Replaces:
;   ADDIU   A1, R0, 0x21FD
.org 0x8096A4BC
    addiu   a1, r0, 0x2204

; Replaces:
;   JAL     0x80151938 ; Message_ContinueTextbox
.org 0x8096A4C8
    jal     MoonChild_HandleWillYouPlayYes

; Replaces:
;   ADDIU   A1, R0, 0x2204
.org 0x8096A568
    addiu   a1, r0, 0x221F

; Replaces:
;   ADDIU   A1, R0, 0x2205
.org 0x8096A558
    or      a1, a3, r0

; Replaces:
;   JAL     0x80151938 ; Message_ContinueTextbox
;   OR      A0, A3, R0
;   LUI     A1, 0x0601
;   ADDIU   A1, A1, 0x6F58
;   LW      A0, 0x0020 (SP)
;   JAL     0x80137488 ; Animation_MorphToPlayOnce
;   LUI     A2, 0xC0A0
;   LW      V0, 0x0028 (SP)
;   LHU     T2, 0x02B8 (V0)
;   ORI     T3, T2, 0x0010
;   B       .+0x148
;   SH      T3, 0x02B8 (V0)
.org 0x8096A574
    jal     MoonChild_BeginTimeReset
    lw      a0, 0x0028 (sp)
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    b       .+0x148
    nop

; Replaces:
;   SW      R0, 0x0010 (SP)
;   LW      A0, 0x002C (SP)
;   JAL     0x80133F28 ; SkelAnime_DrawFlexOpa
;   SW      V0, 0x0018 (SP)
;   LW      RA, 0x0024 (SP)
;   ADDIU   SP, SP, 0x28
;   JR      RA
;   NOP
;   NOP
.org 0x8096AB58
    lui     t7, hi(MoonChild_OverrideLimbDraw)
    addiu   t7, t7, lo(MoonChild_OverrideLimbDraw)
    sw      t7, 0x0010 (sp)
    lw      A0, 0x002C (sp)
    jal     0x80133F28 ; SkelAnime_DrawFlexOpa
    sw      v0, 0x0018 (sp)
    lw      ra, 0x0024 (sp)
    addiu   sp, sp, 0x28
    jr      ra

; Replaces:
;   ADDIU   A1, R0, 0x2208
;   JAL     0x801518B0 ; Message_StartTextbox
;   OR      A2, S0, R0
.org 0x8096A258
    jal     MoonChild_StartTextboxAfterGiveItem
    or      a1, s0, r0
    beqz    v0, .+0x4C
