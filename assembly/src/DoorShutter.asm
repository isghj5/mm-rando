DoorShutter_GetMaxZDistToOpen_Hook:
    addiu   sp, sp, -0x18
    sw      ra, 0x0014 (sp)

    jal     DoorShutter_GetMaxZDistToOpen
    swc1    f0, 0x0010 (sp)

    mov.s   f18, f0
    lwc1    f0, 0x0010 (sp)
    lw      ra, 0x0014 (sp)
    jr      ra
    addiu   sp, sp, 0x18
