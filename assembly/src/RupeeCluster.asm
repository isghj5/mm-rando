RupeeCluster_SpawnRupee_Hook:
    addiu   sp, sp, -0x20
    sw      ra, 0x0010 (sp)

    jal     0x800A7AD4
    sw      a0, 0x0014 (sp)

    lw      a0, 0x0014 (sp)
    or      a1, s2, r0 ; should this be loaded from 0x0040 (sp) instead?
    or      a2, v0, r0
    jal     RupeeCluster_SpawnRupee
    or      a3, s0, r0

    lw      ra, 0x0010 (sp)
    jr      ra
    addiu   sp, sp, 0x20
