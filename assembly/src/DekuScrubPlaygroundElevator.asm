DekuScrubPlaygroundElevator_SpawnRupee_Hook:
    sw      r0, 0x001C (sp)
    sw      r0, 0x0018 (sp)
    j       0x800BAC60
    swc1    f16, 0x0014 (sp)

DekuScrubPlaygroundElevator_AfterSpawnRupee_Hook:
    or      a0, s1, r0
    or      a1, s0, r0
    j       DekuScrubPlaygroundElevator_AfterSpawnRupee
    or      a2, v0, r0
