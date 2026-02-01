.include "BuildPatch.asm"

; Define health reducing function
.orga 0xC5CF3C
    addiu   ra, ra, 0x0014  ; return to where the branch instruction would have lead
    andi    t2, t3, 0x007F  ; use tatl timer from caller as timer for doom mode
    lhu     t6, 0x3F22 (v1) ; load interface state
    addiu   t5, r0, 0x0032
    bne     t5, t6, .+0x20  ; if interface state is not 0x32, game is not active, return
    lh      t0, 0x0036 (v1) ; load current health
    bnez    t2, .+0x18      ; reduce health every 0x7F frames
    lh      t1, 0x0034 (v1) ; load max health
    srl     t2, t1, 4       ; divide by 16
    subu    t0, t0, t2      ; subtract from current health
    bltzl   t0, .+0x8       ; if result is less than 0
    or      t0, r0, r0      ; set result to 0
    jr      ra
    sh      t0, 0x0036 (v1) ; store to current health

; Call above function on Tatl timer
; Replaces
;   b       .+0x1C
;   sh      t3, 0x003E (v1)
.orga 0xD0A574
    jal     0x801C69FC
    sh      t3, 0x003E (v1)

; Reset health to max on death
; Replaces:
;   addiu   t5, r0, 0x0030
.orga 0xC40E18
    lh      t5, 0x0034 (v1)

.close
