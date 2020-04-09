; Payload configuration constants
.definelabel G_PAYLOAD_VROM,  0x03800000
.definelabel G_PAYLOAD_SIZE,  0x30000
.definelabel G_PAYLOAD_END,   0x80780000
.definelabel G_PAYLOAD_ADDR,  (G_PAYLOAD_END - G_PAYLOAD_SIZE)

; Subset of payload reserved for "C heap"
.definelabel G_C_HEAP_SIZE,   0x28000
.definelabel G_C_HEAP,        (G_PAYLOAD_END - G_C_HEAP_SIZE)

; Normal & extended objects constants
.definelabel G_NORMAL_OBJ_COUNT, 0x283
.definelabel G_NORMAL_OBJ_TABLE, 0x801C2740

; Whether or not MMR mods files and relevant data should be included in the build.
; If building to test using the vanilla-patched ROM, this value should be 1.
; If building to include the resulting files in MMR, this value should be 0.
.definelabel MMR_MODS, 0
