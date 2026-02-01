.headersize G_EN_WARP_TAG_DELTA

; Replaces:
;   jal     z2_Scene_SetExitFade
.org 0x809C0EC4
    jal     WarpTag_HandleExit
