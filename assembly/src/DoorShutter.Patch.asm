.headersize G_DOOR_SHUTTER_DELTA

;==================================================================================================
; Get max Z distance for interaction
;==================================================================================================

; Replaces
;   LUI     AT, 0x4248
;   MTC1    AT, F18
.org 0x808A0EC0
    jal     DoorShutter_GetMaxZDistToOpen_Hook
    lw      a0, 0x0024 (sp)
