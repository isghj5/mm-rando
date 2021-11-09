.headersize G_CODE_DELTA

; Replaces:
;   JAL     0x8012BF78
;   LW      A0, 0x02A0 (S3)
;   SW      V0, 0x02A0 (S3)
.org 0x800B545C
    or      a0, fp, r0
    jal     TargetHealth_Draw_Hook
    addiu   a1, sp, 0x00B0
