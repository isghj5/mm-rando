.headersize G_OBJ_TAKARAYA_WALL_DELTA

; Replaces:
;    lw      ra, 0x004C (sp)
;    ldc1    f20, 0x0018 (sp)
;    ldc1    f22, 0x0020 (sp)

.org 0x80ADA248 ; offset 0x1008
    jal     ChestGame_DrawMap_Hook
    ldc1    f20, 0x0018 (sp)
    lw      ra, 0x004C (sp)

; Replaces:
;    sw      t2, 0x0144 (s2)
;    lw      ra, 0x005C (sp)
;    ldc1    f20, 0x0040 (sp)

.org 0x80AD9A44 ; offset 0x804
    jal     ChestGame_ResetMap_Hook
    ldc1    f20, 0x0040 (sp)
    lw      ra, 0x005C (sp)
