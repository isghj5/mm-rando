;==================================================================================================
; Induce a Crash on Pause Menu
;==================================================================================================

.headersize(G_KALEIDO_SCOPE_VRAM - G_KALEIDO_SCOPE_FILE)

; This is used to induce a crash when using the pause menu. It is primarily intended for debugging
; on console to get a (working) crash debugger.
; Replaces:
;   lw      s1, 0x0018 (sp)
.org 0x808286CC ; In RDRAM: 0x8075D54C
    sw      s1, 0x0000 (r0)
