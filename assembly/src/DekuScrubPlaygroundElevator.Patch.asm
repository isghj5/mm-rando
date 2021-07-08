;==================================================================================================
; Spawn Rupee
;==================================================================================================

.headersize G_OBJ_LUPYGAMELIFT_DELTA

; Replaces:
;   sw      r0, 0x001C (sp)
;   sw      r0, 0x0018 (sp)
;   jal     0x800BAC60
;   swc1    f16, 0x0014 (sp)
.org 0x80AF0330
    jal     DekuScrubPlaygroundElevator_SpawnRupee_Hook
    nop
    jal     DekuScrubPlaygroundElevator_AfterSpawnRupee_Hook
    nop
