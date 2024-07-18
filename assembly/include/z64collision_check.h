#ifndef _Z64COLLISION_CHECK_H_
#define _Z64COLLISION_CHECK_H_

#include <PR/ultratypes.h>
#include <unk.h>

#include <z64math.h>

struct Actor;

typedef struct {
    /* 0x0 */ u32 unk0;
    /* 0x4 */ u8 unk4;
    /* 0x5 */ u8 unk5;
} ColBumpInit; // size = 0x8

typedef struct {
    /* 0x0 */ u8 unk0;
    /* 0x1 */ u8 unk1;
    /* 0x2 */ u8 unk2;
    /* 0x3 */ u8 unk3;
    /* 0x4 */ u8 unk4;
    /* 0x5 */ u8 type;
} ColCommonInit; // size = 0x6

typedef struct {
    /* 0x0 */ u32 dmgFlags;
    /* 0x4 */ u8 unk4;
    /* 0x5 */ u8 damage;
} ColTouch; // size = 0x8

typedef struct {
    /* 0x0 */ u32 unk0;
    /* 0x4 */ u8 unk4;
    /* 0x5 */ u8 unk5;
} ColTouchInit; // size = 0x8

typedef struct {
    /* 0x00 */ u8 unk0;
    /* 0x04 */ ColTouchInit unk4;
    /* 0x0C */ ColBumpInit unkC;
    /* 0x14 */ u8 unk14;
    /* 0x15 */ u8 unk15;
    /* 0x16 */ u8 unk16;
} ColBodyInfoInit; // size = 0x18

typedef struct {
    /* 0x0 */ u32 dmgFlags;
    /* 0x4 */ u8 effect;
    /* 0x5 */ u8 defense;
    /* 0x6 */ Vec3s hitPos;
} ColBump; // size = 0xC

typedef struct {
    /* 0x0 */ s16 radius;
    /* 0x2 */ s16 height;
    /* 0x4 */ s16 yOffset;
    /* 0x6 */ Vec3s loc;
} ColCylinderParams; // size = 0xC

typedef struct {
    /* 0x00 */ Vec3f pointA;
    /* 0x0C */ Vec3f pointB;
    /* 0x18 */ Vec3f pointC;
    /* 0x24 */ Vec3f pointD;
    /* 0x30 */ Vec3s unk30;
    /* 0x36 */ Vec3s unk36;
    /* 0x3C */ f32 unk3C;
} ColQuadParams; // size = 0x40

typedef struct {
    /* 0x00 */ Vec3f pointA;
    /* 0x0C */ Vec3f pointB;
    /* 0x18 */ Vec3f pointC;
    /* 0x24 */ Vec3f pointD;
} ColQuadParamsInit; // size = 0x30

typedef struct {
    /* 0x0 */ Vec3s loc;
    /* 0x6 */ s16 radius;
} ColSphereCollisionInfo; // size = 0x8

typedef struct {
    /* 0x00 */ Vec3s unk0;
    /* 0x06 */ s16 unk6;
    /* 0x08 */ ColSphereCollisionInfo colInfo;
    /* 0x10 */ f32 unk10;
    /* 0x14 */ u8 unk14;
    /* 0x15 */ UNK_TYPE1 pad15[0x3];
} ColSphereParams; // size = 0x18

typedef struct {
    /* 0x0 */ u8 unk0;
    /* 0x1 */ ColSphereCollisionInfo unk1;
    /* 0xA */ s16 unkA;
} ColSphereParamsInit; // size = 0xC

typedef struct {
    /* 0x00 */ Vec3f pointA;
    /* 0x0C */ Vec3f pointB;
    /* 0x18 */ Vec3f pointC;
    /* 0x24 */ Vec3f unitNormal;
    /* 0x30 */ f32 unk30;
} ColTriParams; // size = 0x34

typedef struct {
    /* 0x00 */ Vec3f unk0;
    /* 0x0C */ Vec3f unkC;
    /* 0x18 */ Vec3f unk18;
} ColTriParamsInit; // size = 0x24

typedef struct {
    /* 0x00 */ ColCommonInit base;
    /* 0x08 */ ColBodyInfoInit body;
    /* 0x20 */ ColCylinderParams info;
} ColCylinderInit; // size = 0x2C

typedef struct {
    /* 0x00 */ ColCommonInit base;
    /* 0x08 */ ColBodyInfoInit body;
    /* 0x20 */ ColQuadParamsInit params;
} ColQuadInit; // size = 0x50

typedef struct {
    /* 0x00 */ ColBodyInfoInit body;
    /* 0x18 */ ColSphereParamsInit params;
} ColSphereGroupElementInit; // size = 0x24

typedef struct {
    /* 0x0 */ ColCommonInit base;
    /* 0x6 */ UNK_TYPE1 pad6[0x2];
    /* 0x8 */ u32 count;
    /* 0xC */ ColSphereGroupElementInit* init;
} ColSphereGroupInit; // size = 0x10

typedef struct {
    /* 0x00 */ ColCommonInit base;
    /* 0x08 */ ColBodyInfoInit body;
    /* 0x20 */ ColSphereParamsInit info;
} ColSphereInit; // size = 0x2C

typedef struct {
    /* 0x00 */ ColBodyInfoInit body;
    /* 0x18 */ ColTriParamsInit params;
} ColTriInit; // size = 0x3C

typedef struct {
    /* 0x0 */ ColCommonInit base;
    /* 0x8 */ u32 count;
    /* 0xC */ ColTriInit* elemInit;
} ColTriGroupInit; // size = 0x10

typedef struct {
    /* 0x00 */ struct Actor* actor;
    /* 0x04 */ struct Actor* collisionAT;
    /* 0x08 */ struct Actor* collisionAC;
    /* 0x0C */ struct Actor* collisionOT;
    /* 0x10 */ u8 flagsAT;
    /* 0x11 */ u8 flagsAC; // bit 1 - collision occured?
    /* 0x12 */ u8 ocFlags1;
    /* 0x13 */ u8 ocFlags2;
    /* 0x14 */ u8 colType;
    /* 0x15 */ u8 type;
    /* 0x16 */ UNK_TYPE1 pad16[0x2];
} ColCommon; // size = 0x18

typedef struct {
    /* 0x000 */ s16 ATgroupLength;
    /* 0x002 */ u16 flags; // bit 0: collision bodies can't be added or removed, only swapped out
    /* 0x004 */ ColCommon* ATgroup[50];
    /* 0x0CC */ s32 ACgroupLength;
    /* 0x0D0 */ ColCommon* ACgroup[60];
    /* 0x1C0 */ s32 OTgroupLength;
    /* 0x1C4 */ ColCommon* OTgroup[50];
    /* 0x28C */ s32 group4Length;
    /* 0x290 */ ColCommon* group4[3];
} CollisionCheckContext; // size = 0x29C

typedef struct ColBodyInfo_t {
    /* 0x00 */ ColTouch toucher;
    /* 0x08 */ ColBump bumper;
    /* 0x14 */ u8 elemType;
    /* 0x15 */ u8 toucherFlags; // bit 0: can be toucher in AT-AC collision
    /* 0x16 */ u8 bumperFlags; // bit 0: can be bumper in AT-AC collision
    /* 0x17 */ u8 ocElemFlags;
    /* 0x18 */ ColCommon* atHit;
    /* 0x1C */ ColCommon* acHit;
    /* 0x20 */ struct ColBodyInfo_t* atHitInfo;
    /* 0x24 */ struct ColBodyInfo_t* acHitInfo;
} ColBodyInfo; // size = 0x28

typedef struct {
    /* 0x00 */ ColBodyInfo body;
    /* 0x28 */ ColSphereParams params;
} ColSphereGroupElement; // size = 0x40

typedef struct {
    /* 0x00 */ ColBodyInfo body;
    /* 0x28 */ ColTriParams params;
} ColTri; // size = 0x5C

typedef struct {
    /* 0x00 */ ColCommon base;
    /* 0x18 */ ColBodyInfo body;
    /* 0x40 */ ColCylinderParams params;
} ColCylinder; // size = 0x4C

typedef struct {
    /* 0x00 */ ColCommon base;
    /* 0x18 */ ColBodyInfo body;
    /* 0x40 */ ColQuadParams params;
} ColQuad; // size = 0x80

typedef struct {
    /* 0x00 */ ColCommon base;
    /* 0x18 */ ColBodyInfo body;
    /* 0x40 */ ColSphereParams params;
} ColSphere; // size = 0x58

typedef struct {
    /* 0x00 */ ColCommon base;
    /* 0x18 */ u32 count;
    /* 0x1C */ ColSphereGroupElement* spheres;
} ColSphereGroup; // size = 0x20

typedef struct {
    /* 0x00 */ ColCommon base;
    /* 0x18 */ u32 count;
    /* 0x1C */ ColTri* tris;
} ColTriGroup; // size = 0x20

#define SAC_ON (1 << 0) // CollisionContext SAC Flag

#define AT_NONE 0 // No flags set. Cannot have AT collisions when set as AT
#define AT_ON (1 << 0) // Can have AT collisions when set as AT
#define AT_HIT (1 << 1) // Had an AT collision
#define AT_BOUNCED (1 << 2) // Had an AT collision with an AC_HARD collider
#define AT_TYPE_PLAYER (1 << 3) // Has player-aligned damage
#define AT_TYPE_ENEMY (1 << 4) // Has enemy-aligned damage
#define AT_TYPE_OTHER (1 << 5) // Has non-aligned damage
#define AT_SELF (1 << 6) // Can have AT collisions with colliders attached to the same actor
#define AT_TYPE_ALL (AT_TYPE_PLAYER | AT_TYPE_ENEMY | AT_TYPE_OTHER) // Has all three damage alignments

#define AC_NONE 0 // No flags set. Cannot have AC collisions when set as AC
#define AC_ON (1 << 0) // Can have AC collisions when set as AC
#define AC_HIT (1 << 1) // Had an AC collision
#define AC_HARD (1 << 2) // Causes AT colliders to bounce off it
#define AC_TYPE_PLAYER AT_TYPE_PLAYER // Takes player-aligned damage
#define AC_TYPE_ENEMY AT_TYPE_ENEMY // Takes enemy-aligned damage
#define AC_TYPE_OTHER AT_TYPE_OTHER // Takes non-aligned damage
#define AC_NO_DAMAGE (1 << 6) // Collider does not take damage
#define AC_BOUNCED (1 << 7) // Caused an AT collider to bounce off it
#define AC_TYPE_ALL (AC_TYPE_PLAYER | AC_TYPE_ENEMY | AC_TYPE_OTHER) // Takes damage from all three alignments

#define OC1_NONE 0 // No flags set. Cannot have OC collisions when set as OC
#define OC1_ON (1 << 0) // Can have OC collisions when set as OC
#define OC1_HIT (1 << 1) // Had an OC collision
#define OC1_NO_PUSH (1 << 2) // Does not push other colliders away during OC collisions
#define OC1_TYPE_PLAYER (1 << 3) // Can have OC collisions with OC type player
#define OC1_TYPE_1 (1 << 4) // Can have OC collisions with OC type 1
#define OC1_TYPE_2 (1 << 5) // Can have OC collisions with OC type 2
#define OC1_TYPE_ALL (OC1_TYPE_PLAYER | OC1_TYPE_1 | OC1_TYPE_2) // Can have collisions with all three OC types

#define OC2_NONE 0 // No flags set. Has no OC type
#define OC2_HIT_PLAYER (1 << 0) // Had an OC collision with OC type player
#define OC2_UNK1 (1 << 1) // Prevents OC collisions with OC2_UNK2. Some horses and toki_sword have it.
#define OC2_UNK2 (1 << 2) // Prevents OC collisions with OC2_UNK1. Nothing has it.
#define OC2_TYPE_PLAYER OC1_TYPE_PLAYER // Has OC type player
#define OC2_TYPE_1 OC1_TYPE_1 // Has OC type 1
#define OC2_TYPE_2 OC1_TYPE_2 // Has OC type 2
#define OC2_FIRST_ONLY (1 << 6) // Skips AC checks on elements after the first collision. Only used by Ganon

#define TOUCH_NONE 0 // No flags set. Cannot have AT collisions
#define TOUCH_ON (1 << 0) // Can have AT collisions
#define TOUCH_HIT (1 << 1) // Had an AT collision
#define TOUCH_NEAREST (1 << 2) // If a Quad, only collides with the closest bumper
#define TOUCH_SFX_NORMAL (0 << 3) // Hit sound effect based on AC collider's type
#define TOUCH_SFX_HARD (1 << 3) // Always uses hard deflection sound
#define TOUCH_SFX_WOOD (2 << 3) // Always uses wood deflection sound
#define TOUCH_SFX_NONE (3 << 3) // No hit sound effect
#define TOUCH_AT_HITMARK (1 << 5) // Draw hitmarks for every AT collision
#define TOUCH_DREW_HITMARK (1 << 6) // Already drew hitmark for this frame
#define TOUCH_UNK7 (1 << 7) // Unknown purpose. Used by some enemy quads

#define BUMP_NONE 0 // No flags set. Cannot have AC collisions
#define BUMP_ON (1 << 0) // Can have AC collisions
#define BUMP_HIT (1 << 1) // Had an AC collision
#define BUMP_HOOKABLE (1 << 2) // Can be hooked if actor has hookability flags set.
#define BUMP_NO_AT_INFO (1 << 3) // Does not give its info to the AT collider that hit it.
#define BUMP_NO_DAMAGE (1 << 4) // Does not take damage.
#define BUMP_NO_SWORD_SFX (1 << 5) // Does not have a sound when hit by player-attached AT colliders.
#define BUMP_NO_HITMARK (1 << 6) // Skips hit effects.
#define BUMP_DRAW_HITMARK (1 << 7) // Draw hitmark for AC collision this frame.

#define OCELEM_NONE 0 // No flags set. Cannot have OC collisions
#define OCELEM_ON (1 << 0) // Can have OC collisions
#define OCELEM_HIT (1 << 1) // Had an OC collision
#define OCELEM_UNK2 (1 << 2) // Unknown purpose.
#define OCELEM_UNK3 (1 << 3) // Unknown purpose. Used by Dead Hand element 0 and Dodongo element 5

#define OCLINE_NONE 0 // Did not have an OcLine collision
#define OCLINE_HIT (1 << 0) // Had an OcLine collision

#define DMG_ENTRY(damage, effect) ((damage) | ((effect) << 4))

// Don't use combinations of these flags in code until we figure out how we want to format them.
// It's okay to use these flags if the code is only checking a single flag, though.
#define DMG_DEKU_NUT       (1 << 0x00)
#define DMG_DEKU_STICK     (1 << 0x01)
#define DMG_HORSE_TRAMPLE  (1 << 0x02)
#define DMG_EXPLOSIVES     (1 << 0x03)
#define DMG_ZORA_BOOMERANG (1 << 0x04)
#define DMG_NORMAL_ARROW   (1 << 0x05)
#define DMG_UNK_0x06       (1 << 0x06)
#define DMG_HOOKSHOT       (1 << 0x07)
#define DMG_GORON_PUNCH    (1 << 0x08)
#define DMG_SWORD          (1 << 0x09)
#define DMG_GORON_POUND    (1 << 0x0A)
#define DMG_FIRE_ARROW     (1 << 0x0B)
#define DMG_ICE_ARROW      (1 << 0x0C)
#define DMG_LIGHT_ARROW    (1 << 0x0D)
#define DMG_GORON_SPIKES   (1 << 0x0E)
#define DMG_DEKU_SPIN      (1 << 0x0F)
#define DMG_DEKU_BUBBLE    (1 << 0x10)
#define DMG_DEKU_LAUNCH    (1 << 0x11)
#define DMG_UNK_0x12       (1 << 0x12)
#define DMG_ZORA_BARRIER   (1 << 0x13)
#define DMG_NORMAL_SHIELD  (1 << 0x14)
#define DMG_LIGHT_RAY      (1 << 0x15)
#define DMG_THROWN_OBJECT  (1 << 0x16)
#define DMG_ZORA_PUNCH     (1 << 0x17)
#define DMG_SPIN_ATTACK    (1 << 0x18)
#define DMG_SWORD_BEAM     (1 << 0x19)
#define DMG_NORMAL_ROLL    (1 << 0x1A)
#define DMG_UNK_0x1B       (1 << 0x1B)
#define DMG_UNK_0x1C       (1 << 0x1C)
#define DMG_UNBLOCKABLE    (1 << 0x1D)
#define DMG_UNK_0x1E       (1 << 0x1E)
#define DMG_POWDER_KEG     (1 << 0x1F)

#endif
