extended_objects_lookup_gi_hook:
    bge     t8, G_NORMAL_OBJ_COUNT, @@extended_item
    nop
@@normal_item:
    lui     t0, hi(G_NORMAL_OBJ_TABLE)
    jr      ra
    addiu   t0, lo(G_NORMAL_OBJ_TABLE)
@@extended_item:
    la      t0, EXT_OBJECTS
    jr      ra
    addiu   t8, t8, -(G_NORMAL_OBJ_COUNT - 1)

extended_objects_lookup_load_hook:
    bge     a2, G_NORMAL_OBJ_COUNT, @@extended_item
    nop
@@normal_item:
    lui     v0, hi(G_NORMAL_OBJ_TABLE)
    jr      ra
    addiu   v0, lo(G_NORMAL_OBJ_TABLE)
@@extended_item:
    la      v0, EXT_OBJECTS
    jr      ra
    addiu   a2, a2, -(G_NORMAL_OBJ_COUNT - 1)

extended_objects_lookup_unknown_hook:
    bge     s2, G_NORMAL_OBJ_COUNT, @@extended_item
    addiu   a0, s0, 0x0028 ; Displaced code.
@@normal_item:
    lui     s5, hi(G_NORMAL_OBJ_TABLE)
    jr      ra
    addiu   s5, lo(G_NORMAL_OBJ_TABLE)
@@extended_item:
    lui     s5, hi(EXT_OBJECTS)
    bnez    t6, @@return
    addiu   s5, lo(EXT_OBJECTS)
@@update_index:
    addiu   s2, s2, -(G_NORMAL_OBJ_COUNT - 1)
@@return:
    jr      ra
    nop

extended_objects_lookup_unpause_hook:
    bge     v1, G_NORMAL_OBJ_COUNT, @@extended_item
    addu    t8, t8, a3 ; Displaced code
@@normal_item:
    lui     s2, hi(G_NORMAL_OBJ_TABLE)
    jr      ra
    addiu   s2, lo(G_NORMAL_OBJ_TABLE)
@@extended_item:
    la      s2, EXT_OBJECTS
    jr      ra
    addiu   v1, v1, -(G_NORMAL_OBJ_COUNT - 1)

extended_objects_lookup_shop_hook:
    ; Displaced code, and reuse existing hook as index is in A2.
    sll     t6, t6, 2
    j       extended_objects_lookup_load_hook
    addu    v1, a0, t6
