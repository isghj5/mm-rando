;==================================================================================================
; Add dmadata File Entry
;==================================================================================================

.headersize G_CODE_DELTA

.orga (G_DMA_TABLE + (0x10 * 1544))
.area 0x70, 0
    .word G_PAYLOAD_VROM, G_PAYLOAD_VROM + G_PAYLOAD_SIZE, G_PAYLOAD_VROM, 0
.align 16
    .word G_KALEIDOPAYLOAD_VROM, G_KALEIDOPAYLOAD_VROM + G_KALEIDO_FREESIZE, G_KALEIDOPAYLOAD_VROM, 0
.endarea

;==================================================================================================
; Load new code from ROM
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   sw      s0, 0x0018 (sp)
;   lui     s0, 0x801C
;   addiu   s0, s0, 0xD910
;   sw      ra, 0x002C (sp)
;   sw      s4, 0x0028 (sp)
;   sw      s3, 0x0024 (sp)
;   sw      s2, 0x0020 (sp)
;   sw      s1, 0x001C (sp)
;   sw      a0, 0x0340 (sp)
;   lui     a0, 0x0004
;   or      s2, s0, r0
.org 0x801748A4
.area 0x2C, 0
    sw      ra, 0x002C (sp)
    sw      a0, 0x0340 (sp)

    ; Load first code file from ROM
    lui     a0, hi(G_PAYLOAD_ADDR)
    addiu   a0, lo(G_PAYLOAD_ADDR)
    lui     a1, hi(G_PAYLOAD_VROM)
    addiu   a1, lo(G_PAYLOAD_VROM)
    lui     a2, hi(PAYLOAD_END - PAYLOAD_START)
    jal     0x80080C90
    addiu   a2, lo(PAYLOAD_END - PAYLOAD_START)

    jal     init
    sw      s0, 0x0018 (sp)
.endarea

;==================================================================================================
; Set the size of the main heap
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lui     t8, 0x8078
;   addiu   v1, v1, 0x1528
.org 0x80174C4C
    lui     t8, hi(G_PAYLOAD_ADDR)
    addiu   t8, lo(G_PAYLOAD_ADDR)

; Replaces:
;   sw      a1, 0x0000 (v1)
.org 0x80174C5C
    sw      a1, 0x1528 (v1)
