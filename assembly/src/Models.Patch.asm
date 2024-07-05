;==================================================================================================
; Freestanding Models (Heart Piece)
;==================================================================================================

.headersize G_CODE_DELTA

; Heart Piece draw function call.
; Replaces:
;   jal     0x800A75B8
.org 0x800A7188
    jal     Models_DrawHeartPiece

; Bio Baba Heart Piece rotation fix.
; Replaces:
;   bnez    at, 0x800A68F0
;   addiu   at, r0, 0x0017
;   beq     v0, at, 0x800A68F0
.org 0x800A68B4
    jal     Models_BioBabaHeartPieceRotationFix_Hook
    nop
    bnez    at, 0x800A68F0

;==================================================================================================
; Freestanding Models (Item00 Not Heart Piece)
;==================================================================================================

.headersize G_CODE_DELTA

; Remove original "disappear flicker" handling. Now handled in Models_DrawItem00.
; Replaces:
;   lh      t6, 0x014E (a2)
;   lh      t7, 0x0150 (a2)
;   and     t8, t6, t7
;   bnezl   t8, 0x800A72A0
;   lw      ra, 0x0014 (sp)
.org 0x800A7138
    nop
    nop
    nop
    nop
    nop

; Item draw function call.
; Replaces:
;   lui     at, 0x801E
;   addu    at, at, t9
;   lw      t9, 0xBFF4 (at)
.org 0x800A715C
    jal     Models_DrawItem00_Hook
    nop
    nop

; Item set scale in constructor
; Replaces:
;   lui     at, 0x801E
;   addu    at, at, t7
;   lw      t7, 0xBDF4 (at)
.org 0x800A5E6C
    jal     Models_Item00_SetActorSize_Hook
    or      a1, s0, r0
    nop

;==================================================================================================
; Freestanding Models (Skulltula Token)
;==================================================================================================

.headersize G_EN_SI_DELTA

; Skulltula Token draw function.
; Replaces:
;   addiu   sp, sp, -0x18
;   sw      ra, 0x0014 (sp)
.org 0x8098CD0C
    j       Models_DrawSkulltulaToken
    nop

;==================================================================================================
; Freestanding Models (Stray Fairy)
;==================================================================================================

.headersize G_EN_ELFORG_DELTA

; Stray Fairy main function.
; Replaces:
;   sw      a0, 0x0018 (sp)
;   lw      t9, 0x022C (a0)
.org 0x80ACD7A0
    jal     Models_BeforeStrayFairyMain_Hook
    sw      a0, 0x0018 (sp)

; Stray Fairy draw function.
; Replaces:
;   addiu   sp, sp, -0x40
;   sw      s0, 0x0028 (sp)
;   or      s0, a1, r0
;   sw      ra, 0x002C (sp)
.org 0x80ACD8C0
    addiu   sp, sp, -0x40
    sw      ra, 0x002C (sp)
    jal     Models_DrawStrayFairy_Hook
    sw      s0, 0x0028 (sp)

;==================================================================================================
; Freestanding Models (Heart Container)
;==================================================================================================

.headersize G_ITEM_B_HEART_DELTA

; Heart Container draw function.
; Replaces:
;   or      a3, r0, r0
;   lw      a2, 0x0000 (a1)
.org 0x808BCFCC
    jal     Models_DrawHeartContainer_Hook
    nop

;==================================================================================================
; Freestanding Models (Boss Remains)
;==================================================================================================

.headersize G_DM_HINA_DELTA

; Overwrite rotation calculation
; Replaces:
;   LUI     A1, 0x0002
;   ADDU    A1, A1, S1
;   LW      A1, 0x8840 (A1)
;   OR      A0, R0, R0
;   OR      A2, R0, R0
;   ADDU    AT, A1, R0
;   SLL     A1, A1, 5
;   SUBU    A1, A1, AT
;   SLL     A1, A1, 2
;   ADDU    A1, A1, AT
;   SLL     A1, A1, 3
;   SLL     A1, A1, 16
;   SRA     A1, A1, 16
.org 0x80A1FCA8
.area 0x34
    or      a0, s0, r0
    jal     Models_GetBossRemainRotation
    or      a1, s1, r0
    or      a0, r0, r0
    or      a1, v0, r0
    or      a2, r0, r0
    nop
    nop
    nop
    nop
    nop
    nop
    nop
.endarea

; Overwrite draw function call for Odolwa's Remains.
; Replaces:
;   jal     0x800EE320
.org 0x80A1FD54
    jal     Models_DrawBossRemains_Hook

; Overwrite draw function call for Goht's Remains.
; Replaces:
;   jal     0x800EE320
.org 0x80A1FD64
    jal     Models_DrawBossRemains_Hook

; Overwrite draw function call for Gyorg's Remains.
; Replaces:
;   jal     0x800EE320
.org 0x80A1FD74
    jal     Models_DrawBossRemains_Hook

; Overwrite draw function call for Twinmold's Remains.
; Replaces:
;   jal     0x800EE320
.org 0x80A1FD84
    jal     Models_DrawBossRemains_Hook

.headersize G_CODE_DELTA

; Replace behaviour of Boss Remains' Get-Item function always writing DList instruction to set
; object segment address.
; Replaces:
;   sw      t6, 0x02B0 (s0)
;   ori     t7, t7, 0x0018
;   addu    t1, s1, t0
;   lui     t2, 0x0001
;   addu    t2, t2, t1
;   sw      t7, 0x0000 (v1)
;   lw      t2, 0x7D98 (t2)
;   sw      t2, 0x0004 (v1)
.org 0x800EFD94
.area 0x20
    or      a0, s1, r0
    jal     Models_WriteBossRemainsObjectSegment
    lw      a1, 0x003C (sp)
    nop
    nop
    nop
    nop
    nop
.endarea

;==================================================================================================
; Freestanding Models (Moon's Tear)
;==================================================================================================

.headersize G_OBJ_MOON_STONE_DELTA

; Before Moon's Tear main function.
; Replaces:
;   lw      v0, 0x1CCC (a1)
;   lui     at, 0x1000
;   ori     at, at, 0x0282
.org 0x80C068D8
    jal     Models_BeforeMoonsTearMain_Hook
    nop
    lw      v0, 0x1CCC (a1)

; Moon's Tear draw function.
; Replaces:
;   sw      s1, 0x0018 (sp)
;   or      s1, a1, r0
;   sw      ra, 0x001C (sp)
.org 0x80C06914
    sw      ra, 0x001C (sp)
    jal     Models_DrawMoonsTear_Hook
    sw      s1, 0x0018 (sp)

;==================================================================================================
; Freestanding Models (Lab Fish Heart Piece)
;==================================================================================================

.headersize G_EN_COL_MAN_DELTA

; Lab Fish Heart Piece draw function.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   sw      a0, 0x0030 (sp)
;   sw      a1, 0x0034 (sp)
.org 0x80AFE41C
    sw      s0, 0x0018 (sp)
    jal     Models_DrawLabFishHeartPiece_Hook
    sw      a0, 0x0030 (sp)

;==================================================================================================
; Freestanding Models (Seahorse)
;==================================================================================================

.headersize G_EN_OT_DELTA

; Before Seahorse main function.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x001C (sp)
.org 0x80B5DAF0
    sw      ra, 0x001C (sp)
    jal     Models_BeforeSeahorseMain_Hook
    sw      s0, 0x0018 (sp)

; Seahorse draw function.
; Replaces:
;   sw      s0, 0x0028 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x002C (sp)
;   sw      a1, 0x0054 (sp)
.org 0x80B5DD24
    sw      ra, 0x002C (sp)
    sw      s0, 0x0028 (sp)
    jal     Models_DrawSeahorse_Hook
    or      s0, a0, r0

;==================================================================================================
; Freestanding Models (Shops)
;==================================================================================================

.headersize G_EN_GIRLA_DELTA

; Overwrite draw function call for shop inventory.
; Replaces:
;   jal     0x800EE320
.org 0x80864A14
    jal     Models_DrawShopInventory_Hook

;==================================================================================================
; Model Rotation (En_Item00)
;==================================================================================================

.headersize G_CODE_DELTA

; Allow rotating backwards for En_Item00 (Heart Piece).
; Replaces:
;   lh      t7, 0x00BE (s0)
;   lh      v1, 0x001C (s0)
;   addiu   t8, t7, 0x03C0
;   b       0x800A6550
;   sh      t8, 0x00BE (s0)
.org 0x800A6454
    jal     Models_RotateEnItem00
    nop
    lh      v1, 0x001C (s0)
    b       0x800A6550
    nop

; Allow rotating backwards for bouncing En_Item00.
; Replaces:
;   lh      t7, 0x00BE (s0)
;   addiu   t8, t7, 0x03C0
;   sh      t8, 0x00BE (s0)
.org 0x800A6674
    jal     Models_RotateEnItem00
    nop
    nop

; Allow items that normally don't rotate to rotate.
; Replaces:
;   LH      V1, 0x001C (S0)
;   SLTI    AT, V1, 0x0003
;   BNEZ    AT, 0x800A6454
;   ADDIU   AT, R0, 0x0003
;   BNEL    V1, AT, 0x800A6444
;   ADDIU   AT, R0, 0x0006
;   LH      T6, 0x0152 (S0)
;   BLTZ    T6, 0x800A6454
;   ADDIU   AT, R0, 0x0006
;   BEQ     V1, AT, 0x800A6454
;   ADDIU   AT, R0, 0x0007
;   BNEL    V1, AT, 0x800A646C
;   SLTI    AT, V1, 0x0016
.org 0x800A6420
.area 0x34
    jal     Models_ShouldEnItem00Rotate
    nop
    beqz    v0, 0x800A6468
    lh      v1, 0x001C (s0)
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
.endarea

;==================================================================================================
; Model Rotation (Skulltula Token)
;==================================================================================================

.headersize G_EN_SI_DELTA

; Allows rotating backwards for Skulltula Tokens.
; Replaces:
;   addiu   t2, t1, 0x038E
;   sh      t2, 0x00BE (a0)
.org 0x8098CBC4
    jal     Models_RotateSkulltulaToken
    nop

;==================================================================================================
; Model Rotation (Heart Container)
;==================================================================================================

.headersize G_ITEM_B_HEART_DELTA

; Allows rotating backwards for Heart Containers.
; Replaces:
;   lh      t6, 0x00BE (s0)
;   lui     a1, 0x3ECC
;   lui     a2, 0x3DCC
;   lui     a3, 0x3C23
;   addiu   t7, t6, 0x0400
;   sh      t7, 0x00BE (s0)
.org 0x808BCF68
.area 0x1C
    jal     Models_RotateHeartContainer
    nop
    nop
    lui     a1, 0x3ECC
    lui     a2, 0x3DCC
    lui     a3, 0x3C23
.endarea

;==================================================================================================
; Model Rotation (Lab Fish Heart Piece)
;==================================================================================================

.headersize G_EN_COL_MAN_DELTA

; Allows rotating backwards for Lab Fish Heart Piece.
; Replaces:
;   bnezl   t1, 0x80AFDE78
;   sw      a0, 0x0020 (sp)
;   lh      t2, 0x00BE (a0)
;   addiu   t3, t2, 0x03E8
;   sh      t3, 0x00BE (a0)
;   sw      a0, 0x0020 (sp)
.org 0x80AFDE60
.area 0x1C
    bnez    t1, 0x80AFDE78
    sw      a0, 0x0020 (sp)
    jal     Models_RotateLabFishHeartPiece
    sw      a1, 0x0024 (sp)
    lw      a0, 0x0020 (sp)
    lw      a1, 0x0024 (sp)
.endarea

;==================================================================================================
; Freestanding Models (Scopecoin)
;==================================================================================================

.headersize G_EN_SCOPECOIN_DELTA

; Scopecoin draw function.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   sw      a0, 0x0038 (sp)
;   sw      a1, 0x003C (sp)
;   lw      t6, 0x003C (sp)
.org 0x80BFD184
    jal     Models_DrawScopecoin_Hook
    nop
    bnez    v0, 0x80BFD240
    nop

;==================================================================================================
; Model Rotation (Scopecoin)
;==================================================================================================

.headersize G_EN_SCOPECOIN_DELTA

; Replaces:
;   SW      A1, 0x0004 (SP)
;   LH      T6, 0x00BE (A0)
.org 0x80BFCFA0
    j       Models_RotateScopecoin
    nop

;==================================================================================================
; Freestanding Models (ScRuppe)
;==================================================================================================

.headersize G_EN_SC_RUPPE_DELTA

; ScRuppe draw function.
; Replaces:
;   sw      s0, 0x0018 (sp)
;   sw      a0, 0x0038 (sp)
;   sw      a1, 0x003C (sp)
;   lw      t6, 0x003C (sp)
.org 0x80BD6D20
    jal     Models_DrawScRuppe_Hook
    nop
    bnez    v0, 0x80BD6DDC
    nop

;==================================================================================================
; Model Rotation (ScRuppe)
;==================================================================================================

.headersize G_EN_SC_RUPPE_DELTA

; Replaces:
;   OR      A0, A2, R0
;   ADDIU   T2, T1, 0x01F4
;   JAL     0x800B6A88
;   SH      T2, 0x00BE (A2)
.org 0x80BD6AF8
    jal     0x800B6A88
    or      a0, a2, r0
    jal     Models_RotateScRuppe_Hook
    nop

;==================================================================================================
; Freestanding Models (Deku Playground Rupee)
;==================================================================================================

.headersize G_EN_GAMELUPY_DELTA

; Replaces:
;   sw      s0, 0x0018 (sp)
;   sw      a0, 0x0038 (sp)
;   sw      a1, 0x003C (sp)
;   lw      t6, 0x003C (sp)
.org 0x80AF6C00
    jal     Models_DrawDekuScrubPlaygroundRupee_Hook
    nop
    bnez    v0, 0x80AF6CBC
    nop

;==================================================================================================
; Model Rotation (Deku Playground Rupee)
;==================================================================================================

.headersize G_EN_GAMELUPY_DELTA

; Replaces:
;   ADDIU   T2, T1, 0x01F4
;   SH      T2, 0x00BE (A1)
.org 0x80AF6A24
    jal     Models_RotateDekuScrubPlaygroundRupee
    lw      a1, 0x001C (sp)

;==================================================================================================
; Freestanding Models (Masks)
;==================================================================================================

.headersize G_DM_CHAR05_DELTA

; Replaces:
;   jal     0x80133B3C
.org 0x80AADA5C
    jal     Models_DrawGoronMask

; Replaces:
;   jal     0x80133B3C
.org 0x80AADB18
    jal     Models_DrawZoraMask

; Replaces:
;   jal     0x80133F28
.org 0x80AADBCC
    jal     Models_DrawGibdoMask


;==================================================================================================
; Freestanding Models (Hero's Shield)
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   jal     0x800EE320
.org 0x800A726C
    jal     Models_DrawItem00Shield

.org 0x800A6088
    .dw 0x00000000

;==================================================================================================
; Freestanding Models (Ocarina)
;==================================================================================================

.headersize G_DM_CHAR02_DELTA

; Replaces:
;   jal     0x801343C0
.org 0x80AAB370
    jal     Models_DrawOcarina

.headersize G_DM_STK_DELTA
; Replaces:
;   lw      v0, 0x02B0 (a1)
;   lui     t9, 0x0601
;   addiu   t9, t9, 0xCAD0
;   addiu   t4, v0, 0x0008
;   sw      t4, 0x02B0 (a1)
;   sw      t9, 0x0004 (v0)
;   sw      t2, 0x0000 (v0)
.org 0x80AA32B4
    lw      a0, 0x00A0 (sp)
    or      a1, s0, r0
    jal     Models_DrawOcarinaLimb
    addiu   a2, r0, 0x004C
    lw      a1, 0x0030 (sp)
    nop
    nop

; Replaces:
;   ADDIU   AT, R0, 0x0003
;   BEQ     V1, AT, 0x80AA30B0
;   LUI     T2, 0xDE00
;   ADDIU   AT, R0, 0x0005
;   BEQ     V1, AT, 0x80AA3114
;   LUI     A2, 0x4457
;   B       0x80AA3398
;   LW      RA, 0x0024 (SP)
.org 0x80AA3010
    addiu   at, r0, 0x0005
    beq     v1, at, 0x80AA3114
    lui     a2, 0x4457
    addiu   at, r0, 0x0003
    beq     v1, at, 0x80AA30B8
    nop
    b       0x80AA30B0
    addiu   at, r0, 0x0004

; Replaces:
;   gSPDisplayList(POLY_OPA_DISP++, gSkullKidOcarinaHoldingRightHandDL);
;
;   if ((play->sceneId == SCENE_LOST_WOODS) && (gSaveContext.sceneLayer == 1)) {
;       gSPDisplayList(POLY_OPA_DISP++, gSkullKidOcarinaOfTimeDL);
;   }
.org 0x80AA30B0
.area 0x64, 0
    bne     v1, at, 0x80AA3398
    lw      ra, 0x0024 (sp)
    lw      a0, 0x00A0 (sp)
    lw      a1, 0x00B0 (sp)
    jal     Models_DrawOcarinaLimb
    addiu   a2, r0, 0x044C
    lw      t0, 0x005C (sp)
    lw      v0, 0x02B0 (t0)
    lui     t7, 0x0601
    addiu   t7, t7, 0x9DA0
    addiu   t5, v0, 0x0008
    sw      t5, 0x2B0 (t0)
    sw      t7, 0x0004 (v0)
    lui     t2, 0xDE00
    sw      t2, 0x0000 (v0)
    b       0x80AA3398
    lw      ra, 0x0024 (sp)
.endarea

;==================================================================================================
; Freestanding Models (Mountain Smithy)
;==================================================================================================

.headersize G_EN_KGY_DELTA
; Replaces:
;    lw     t6, 0x002C (sp)
;    lw     a0, 0x0000 (t6)
;    jal    0x8012C28C
;    sw     a0, 0x001C (sp)

.org 0x80B43084
    lw      t6, 0x0000 (a1)
    jal     Models_DrawSmithyItem
    sw      t6, 0x001C (sp)
    bnez    v0, 0x80B431C0

;==================================================================================================
; Freestanding Models (Keaton Mask)
;==================================================================================================

.headersize G_EN_TEST3_DELTA

; After limb functions
; Replaces:
;   lb      t4, 0x0d5c (s0)
;   lw      t5, 0x0060 (sp)
.org 0x80A414DC
    jal     Models_DrawKeatonMask_Hook
    nop

;==================================================================================================
; Freestanding Models (Pendant of Memories)
;==================================================================================================

.headersize G_EN_TEST3_DELTA

; Replaces:
;   lui     t9, 0xDE00
;   lw      a0, 0x0000 (t7)
;   lw      v1, 0x02B0 (t7)
;   lui     t1, 0x0601
;   addiu   t1, t1, 0xCB60
;   addiu   t8, v1, 0x0008
;   sw      t8, 0x02B0 (a0)
;   sw      t1, 0x0004 (v1)
;   sw      t9, 0x0000 (v1)
.org 0x80A41254 ;offset 0x2A74
    or      a0, s1, r0
    or      a1, s0, r0
    jal     Models_DrawPendantOfMemories
    nop
    nop
    nop
    nop
    nop
    nop

; Replaces:
;   jal     0x8012697C ;z2_Player_DrawGetItem
.org 0x80A41510 ; offset 0x2D30
    jal     Models_DrawPendantInHand

;==================================================================================================
; Freestanding Models (Don Gero's Mask)
;==================================================================================================

.headersize G_EN_GEG_DELTA

; Replaces:
;   lw      t7, 0x0048 (sp)
;   jal     0x8012C28C
;   lw      a0, 0x0000 (t7)
;   lw      t8, 0x0048 (sp)
;   lui     t0, 0xDE00
;   lw      a0, 0x0000 (t8)
;   lw      v1, 0x02B0 (a0)
;   lui     t1, 0x0600
;   addiu   t1, t1, 0x4DB0
;   addiu   t9, v1, 0x0008
;   sw      t9, 0x02B0 (a0)
;   sw      t1, 0x0004 (v1)
;   sw      t0, 0x0000 (v1)
.org 0x80BB38F4
    lw      a0, 0x0048 (sp)
    jal     Models_DrawDonGeroMask
    or      a1, s0, r0
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop

;==================================================================================================
; Freestanding Models (Postman's Hat)
;==================================================================================================

.headersize G_EN_PM_DELTA

; Replaces:
;   lw      v0, 0x0000 (a1)
;   lui     t5, 0x0601
;   addiu   t5, t5, 0x85C8
;   addiu   t3, v0, 0x0008
;   sw      t3, 0x0000 (a1)
;   sw      t5, 0x0004 (v0)
;   sw      t4, 0x0000 (v0)
.org 0x80AF8920
    addiu   a1, a1, 0xFFF8
    lui     a2, 0x803E
    jal     Models_DrawPostmanHat
    ori     a2, a2, 0x6B20
    nop
    nop
    nop

;==================================================================================================
; Freestanding Models (Mask of Truth)
;==================================================================================================

.headersize G_EN_SSH_DELTA ;; Cursed Skulltula Guy

; Set up a matrix for an end of draw function getItem
; Replaces:
;   lw      t6, 0x0028 (sp)
;   lhu     t7, 0x05C2 (t6)
;   lui     t0, 0xDE00
;   andi    t8, t7, 0x0020
;   beqzl   t8, 0x80975F20
.org 0x80975EE0
    or      a1, s0, r0
    jal     Models_SetEnSshMatrix
    nop
    lui     t0, 0xDE00
    beqzl   v0, 0x80975F1C

; Draw a getItem
; Replaces
;   lw      ra, 0x0024 (sp)
;   lw      s0, 0x0020 (sp)
;   addiu   sp, sp, 0x0038
;   jr      ra
;   nop
.org 0x80975FFC
    jal     Models_DrawEnSshMaskOfTruth_Hook
    or      a1, s0, r0
    lw      ra, 0x0024 (sp)
    jr      ra
    addiu   sp, sp, 0x0038

.headersize G_EN_STH_DELTA ;;Healed Skulltula Guy

; Replaces:
;   lhu     t3, 0x029C (v0)
;   lui     at, 0x0001
;   lw      t0, 0x0000 (v1)
;   andi    t4, t3, 0x0001
;   beqz    t4, 0x80B6848C
;   ori     at, at, 0x7D88
;   lbu     a1, 0x029F (v0)
;   sw      t0, 0x0020 (sp)
;   jal     0x8012F668
;   addu    a0, v1, at
;   beqz    v0, 0x80B6848C
;   lw      t0, 0x0020 (sp)
.org 0x80B68388
    or      a0, s1, r0
    jal     Models_DrawEnSthMaskOfTruth
    or      a1, s0, r0
    lui     at, 0x0001
    beqz    v0, 0x80B6848C
    ori     at, at, 0x7D88
    lbu     a1, 0x029F (s0)
    lw      t0, 0x0000 (s1)

.org 0x80B683AC
    addu    a0, s1, at
    beqz    v0, 0x80B6848C
    sw      t0, 0x0020 (sp)

;==================================================================================================
; Freestanding Models (Garos Mask)
;==================================================================================================

.headersize G_EN_IN_DELTA

; Toggle head mesh
; Replaces:
;   lui     t6, 0x0602
;   addiu   t6, t6, 0xC528
;   sw      t6, 0x0000 (a2)
.org 0x808F654C
    lui     t6, 0x0602
    jal     Models_SetEnInHead
    or      a0, a2, r0

; Set a matrix
; Replaces:
;   lw      s0, 0x0070 (sp)
;   or      a0, a3, r0
;   sw      a2, 0x0064 (sp)
;   addiu   a1, s0, 0x04B4
.org 0x808F6868
    jal     Models_SetEnInMatrix_Hook
    addiu   a0, s0, 0x03D0

; Replaces
;   lw      ra, 0x0024 (sp)
;   addiu   sp, sp, 0x30
;   jr      ra
;   nop
;   nop
;   nop
;   nop
.org 0x808F6A24
    or      a0, s1, r0
    jal     Models_DrawGaroMask
    or      a1, s0, r0
    lw      ra, 0x0024 (sp)
    addiu   sp, sp, 0x30
    jr      ra
    nop

;==================================================================================================
; Freestanding Models (Fairy)
;==================================================================================================

.headersize G_EN_ELF_DELTA

; + 0x4861B0

; Replaces:
;   JAL     0x8012C94C
;   SW      A0, 0x0058 (SP)
;   LHU     A0, 0x025A (S0)
.org 0x8040A4E8 + 0x4861B0
    jal     Models_DrawFairy_Hook
    sw      a0, 0x0058 (sp)
    bnez    v0, 0x8040A70C + 0x4861B0

;==================================================================================================
; Freestanding Models (Bomb Shop Keeper Bomb)
;==================================================================================================

.headersize G_EN_SOB1_DELTA

; Replaces:
;   LW      A0, 0x02B0 (V0)
;   LUI     T8, 0x0600
;   ADDIU   T8, T8, 0x0970
;   ADDIU   T6, A0, 0x0008
;   SW      T6, 0x02B0 (V0)
;   LUI     T7, 0xDE00
;   SW      T7, 0x0000 (A0)
;   SW      T8, 0x0004 (A0)
.org 0x80A1031C
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)
    jal     Models_SetBombShopkeeperHand
    nop
    nop
    nop
    lw      ra, 0x0014 (sp)
    addiu   sp, sp, 0x18

; Replaces:
;   LW      RA, 0x0044 (SP)
;   LW      S0, 0x0038 (SP)
;   LW      S1, 0x003C (SP)
;   LW      S2, 0x0040 (SP)
;   JR      RA
;   ADDIU   SP, SP, 0x68
;   NOP
;   NOP
;   NOP
.org 0x80A1083C
    lw      a0, 0x0038 (sp)
    jal     Models_BombShopkeeperDrawBomb
    lw      a1, 0x003C (sp)
    lw      ra, 0x0044 (sp)
    lw      s0, 0x0038 (sp)
    lw      s1, 0x003C (sp)
    lw      s2, 0x0040 (sp)
    jr      ra
    addiu   sp, sp, 0x68
