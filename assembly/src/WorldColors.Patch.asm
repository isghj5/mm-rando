;==================================================================================================
; Blue Bubble color
;==================================================================================================

.headersize G_EN_BB_DELTA

; Replaces:
;   lui     t8, 0xFB00
;   ori     t9, r0, 0xFF00
;   addiu   t7, v0, 0x0008
;   sw      t7, 0x02C0 (s0)
;   sw      t9, 0x0004 (v0)
;   sw      t8, 0x0000 (v0)
.org 0x808C361C ; Offset: 0x18DC‬
.area 0x18
    lui     t8, 0xFB00
    addiu   t7, v0, 0x0008
    sw      t7, 0x02C0 (s0)
    jal     WorldColors_GetBlueBubbleColor_Hook
    sw      t8, 0x0000 (v0)
    sw      t9, 0x0004 (v0)
.endarea

;==================================================================================================
; Goron Punching Energy Color
;==================================================================================================

.headersize G_CODE_DELTA

@GoronPunchEnergyColor equ (WORLD_COLOR_CONFIG + 0x8)

; Goron punch energy color (SetEnvColor).
; Replaces:
;   andi    t3, t0, 0x00FF
;   lui     at, 0xFF00
.org 0x801274E4
    lui     at, hi(@GoronPunchEnergyColor)
    lw      at, lo(@GoronPunchEnergyColor) (at)

; Replaces:
;   or      t4, t3, at
.org 0x801274F4
    or      t4, t0, at

;==================================================================================================
; Goron Rolling Interior Energy Color
;==================================================================================================

.headersize G_PLAYER_ACTOR_DELTA

@GoronInnerEnergyColor equ (WORLD_COLOR_CONFIG + 0xC)

; Goron inner energy color (SetEnvColor).
; Replaces:
;   lui     at, 0x9B00 ;; AT = #9B0000 (color value).
;   addiu   t3, v0, 0x0008
;   sw      t3, 0x02C0 (a1)
.org 0x80846BE4 ; Offset: 0x19154
    lui     at, hi(@GoronInnerEnergyColor)
    lw      at, lo(@GoronInnerEnergyColor) (at)
    nop

; Replaces:
;   lw      v0, 0x02C0 (a1)
.org 0x80846C08 ; Offset: 0x19178
    addiu   v0, v0, 0x0008

;==================================================================================================
; Sword Spin Charge Energy Color
;==================================================================================================

.headersize G_EN_M_THUNDER_DELTA

@SwordChargeBluEnvColor equ (WORLD_COLOR_CONFIG + 0x10)
@SwordChargeBluPriColor equ (WORLD_COLOR_CONFIG + 0x14)
@SwordChargeRedEnvColor equ (WORLD_COLOR_CONFIG + 0x18)
@SwordChargeRedPriColor equ (WORLD_COLOR_CONFIG + 0x1C)

; Charge blue prim color.
; Replaces:
;   lui     at, 0xAAFF
;   ori     at, at, 0xFF00
.org 0x808B6F90 ; Offset: 0x1BD0
    lui     at, hi(@SwordChargeBluPriColor)
    lw      at, lo(@SwordChargeBluPriColor) (at)

; Charge blue env color (part 1).
; Replaces:
;   lui     t9, 0x0064
.org 0x808B6F68 ; Offset: 0x1BA8
    lui     t9, hi(@SwordChargeBluEnvColor)

; Charge blue env color (part 2).
; Replaces:
;   ori     t9, t9, 0xFF80
.org 0x808B6FAC ; Offset: 0x1BEC
    lw      t9, lo(@SwordChargeBluEnvColor) (t9)

; Charge red color (part 1).
; Replaces:
;   lui     t8, 0xFF64
.org 0x808B6EE0 ; Offset: 0x1B20
    lui     t8, hi(@SwordChargeRedEnvColor)

; Charge red color (part 2).
; Replaces:
;   addiu   t9, v0, 0x0008
;   sw      t9, 0x02C0 (t0)
;   sw      t5, 0x0000 (v0)
;   lw      t6, 0x00B8 (sp)
;   addiu   at, r0, 0xAA00
.org 0x808B6EF8 ; Offset: 0x1B38
    lui     at, hi(@SwordChargeRedPriColor)
    lw      at, lo(@SwordChargeRedPriColor) (at)
    sw      t5, 0x0000 (v0)
    lw      t6, 0x00B8 (sp)
    lw      t8, lo(@SwordChargeRedEnvColor) (t8)

; Replaces:
;   lw      v0, 0x02C0 (t0)
.org 0x808B6F18 ; Offset: 0x1B58
    addiu   v0, v0, 0x0008

;==================================================================================================
; Sword Spin Charge Sparks Color
;==================================================================================================

.headersize G_EFF_DUST_DELTA

@SwordChargeSparksBluColor equ (WORLD_COLOR_CONFIG + 0x20)
@SwordChargeSparksRedColor equ (WORLD_COLOR_CONFIG + 0x24)

; Red sparks env color.
; Replaces:
;   lui     t5, 0xFF00
;   addiu   t3, v0, 0x0008
;   sw      t3, 0x02C0 (s1)
.org 0x80919ACC ; Offset: 0xF8C
    lui     t5, hi(@SwordChargeSparksRedColor)
    lw      t5, lo(@SwordChargeSparksRedColor) (t5)
    nop

; Replaces:
;   lw      v0, 0x02C0 (s1)
.org 0x80919AE4 ; Offset: 0xFA4
    addiu   v0, v0, 0x0008

; Blue sparks env color.
; Replaces:
;   ori     t8, r0, 0xFF00
;   addiu   t6, v0, 0x0008
;   sw      t6, 0x02C0 (s1)
.org 0x80919AF0 ; Offset: 0xFB0
    lui     t8, hi(@SwordChargeSparksBluColor)
    lw      t8, lo(@SwordChargeSparksBluColor) (t8)
    nop

; Replaces:
;   lw      v0, 0x02C0 (s1)
.org 0x80919B04 ; Offset: 0xFC4
    addiu   v0, v0, 0x0008

;==================================================================================================
; Sword Spin Attack Energy Color
;==================================================================================================

.headersize G_EN_M_THUNDER_DELTA

@SwordSlashBluPriColor equ (WORLD_COLOR_CONFIG + 0x28)
@SwordSlashRedPriColor equ (WORLD_COLOR_CONFIG + 0x2C)

; Red prim color (part 1).
; Replaces:
;   addiu   at, r0, 0xAA00
;   ctc1    t5, fcsr
;   or      t9, t7, at
;   sw      t9, 0x0004 (v1)
.org 0x808B6A68 ; Offset: 0x16A8
    lui     at, hi(@SwordSlashRedPriColor)
    lw      at, lo(@SwordSlashRedPriColor) (at)
    or      t9, t7, at
    ctc1    t5, fcsr

; Red prim color (part 2).
; Replaces:
;   sw      t5, 0x02C0 (t0)
.org 0x808B6A88 ; Offset: 0x16C8
    sw      t9, 0x0004 (v1)

; Red prim color (part 3).
; Replaces:
;   lw      v0, 0x02C0 (t0)
.org 0x808B6A94 ; Offset: 0x16D4
    or      v0, t5, r0

; Blue prim color.
; Replaces:
;   lui     at, 0xAAFF
;   ori     at, at, 0xFF00
.org 0x808B6B64 ; Offset: 0x17A4
    lui     at, hi(@SwordSlashBluPriColor)
    lw      at, lo(@SwordSlashBluPriColor) (at)

;==================================================================================================
; Fierce Deity Sword Beam Energy Color
;==================================================================================================

.headersize G_EN_M_THUNDER_DELTA

@SwordBeamEnvColor equ (WORLD_COLOR_CONFIG + 0x30)
@SwordBeamPriColor equ (WORLD_COLOR_CONFIG + 0x34)

; Prim color.
; Replaces:
;   lui     at, 0xAAFF
;   ori     at, at, 0xFF00
.org 0x808B6C64 ; Offset: 0x18A4
    lui     at, hi(@SwordBeamPriColor)
    lw      at, lo(@SwordBeamPriColor) (at)

; Env color (part 1).
; Replaces:
;   lui     t6, 0x0064
.org 0x808B6BE8 ; Offset: 0x1828
    lui     t6, hi(@SwordBeamEnvColor)

; Env color (part 2).
; Replaces:
;   ori     t6, t6, 0xFF80
.org 0x808B6C84 ; Offset: 0x18C4
    lw      t6, lo(@SwordBeamEnvColor) (t6)

;==================================================================================================
; Fierce Deity Sword Beam Damage Color
;==================================================================================================

.headersize G_CODE_DELTA

@SwordBeamDamageEnvColor equ (WORLD_COLOR_CONFIG + 0x38)

; Replaces:
;   lh      t7, 0x0C3E (a2)
;   lbu     t3, 0x0C3B (a2)
;   lh      t5, 0x0C3C (a2)
;   addiu   t6, t7, 0x00FF
;   andi    t2, t6, 0x00FF
;   sll     t8, t2, 8
;   sll     t9, t3, 24
;   addiu   t2, t5, 0x00FF
;   andi    t4, t2, 0x00FF
;   sll     t3, t4, 16
;   or      t7, t8, t9
;   or      t8, t7, t3
.org 0x800BF1B4
.area 0x30
    lui     t8, hi(@SwordBeamDamageEnvColor)
    lw      t8, lo(@SwordBeamDamageEnvColor) (t8)
    addiu   at, r0, 0xFF00 ;; AT = 0xFFFFFF00 (bitmask)
    and     t8, t8, at
    nop
    nop
    nop
    nop
    nop
    nop
    nop
    nop
.endarea
