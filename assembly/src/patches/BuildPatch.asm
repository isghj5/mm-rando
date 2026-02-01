.n64
.relativeinclude on

;; Force armips version 0.11 or later for fix to MIPS LO/HI ELF symbol relocation.
.if (version() < 110)
.notice version()
.error "Detected armips build is too old. Please install https://github.com/Kingcom/armips version 0.11 or later."
.endif

.create "../../roms/smallpatch.z64", 0
.incbin "../../roms/patched.z64"

;==================================================================================================
; Constants
;==================================================================================================

.include "../../build/symbols.asm"
