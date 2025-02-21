PauseMenu_SetupUpdate_HasPressedStart_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0014 (sp)
    sw      v1, 0x0018 (sp)
    sw      a2, 0x0028 (sp)

    jal     PauseMenu_SetupUpdate_HasPressedStart
    sw      a3, 0x002C (sp)

    lw      a3, 0x002C (sp)
    lw      a2, 0x0028 (sp)
    lw      v1, 0x0018 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x20

PauseMenu_DMAKaleidoPayload_Hook:
    addiu   sp, sp, -0x18
	sw      ra, 0x0014(sp)
	jal     0x80163758
	nop

	lui     a0, hi(G_KALEIDOPAYLOAD_ADDR)
	lui     a1, hi(G_KALEIDOPAYLOAD_VROM)
	lui     a2, hi(KALEIDOPAYLOAD_END - KALEIDOPAYLOAD_START)
	addiu   a0, lo(G_KALEIDOPAYLOAD_ADDR)
	addiu   a1, lo(G_KALEIDOPAYLOAD_VROM)
	jal     0x80080C90
	addiu   a2, lo(KALEIDOPAYLOAD_END - KALEIDOPAYLOAD_START)

	; Run invalIcache function
	lui     a0, hi(G_KALEIDOPAYLOAD_ADDR)
	lui     a1, hi(KALEIDOPAYLOAD_END - KALEIDOPAYLOAD_START)
	addiu   a0, lo(G_KALEIDOPAYLOAD_ADDR)
	jal     0x8008F270
	addiu   a1, lo(KALEIDOPAYLOAD_END - KALEIDOPAYLOAD_START)

	lw      ra, 0x0014(sp)
	jr      ra
	addiu   sp, sp, 0x18
