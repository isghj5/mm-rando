.include "BuildPatch.asm"

.orga 0xC40DF8
    lw      v0, 0x0018 (sp)
    addiu   t6, r0, 0x54C0

.orga 0xC40E08
    lui     at, 0x0002
    addu    at, at, v0

.orga 0xC40E14
    sh      t6, 0x887A (at)

.orga 0xC40E1C
    addiu   t6, r0, 0x0014
    sb      t6, 0x8875 (at)
    sb      t4, 0x887f (at)

.close
