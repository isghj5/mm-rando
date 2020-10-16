;chest_write_gi_index_hook:
;    lw      a1, 0x005C (sp)

;    addiu   sp, sp, -0x18
;    sw      a1, 0x0010 (sp)
;    sw      ra, 0x0014 (sp)

;    jal     chest_write_gi_index
;    or      a0, s1, r0

;    ; Displaced code
;    lw      a0, 0x0010 (sp)

;    lw      ra, 0x0014 (sp)
;    jr      ra
;    addiu   sp, sp, 0x18

chest_update_gi_index_while_opening_hook:
    lw      a1, 0x0084 (sp)        ;; A1 = GlobalContext
    j       chest_update_gi_index  ;; Call function to update flags
    ori     a2, r0, 0x0001         ;; grant = true (update flags)
