#ifndef DPAD_H
#define DPAD_H

#include <stdbool.h>
#include "z2.h"

// Magic number for dpad_config: "DPAD"
#define DPAD_CONFIG_MAGIC 0x44504144

// Most of these states get checked by the "UseItem" function itself.
// We do need to handle some of them though.
#define DPAD_ACTION_STATE1 (       \
    Z2_ACTION_STATE1_HOLD        | \
    Z2_ACTION_STATE1_MOVE_SCENE    \
)

typedef enum {
    DPAD_STATE_TYPE_DISABLED, // 0
    DPAD_STATE_TYPE_ENABLED,  // 1, enabled with DPAD_CONFIG values
    DPAD_STATE_TYPE_DEFAULTS, // 2, enabled with DPAD_DEFAULT values
} dpad_state_type_t;

typedef enum {
    DPAD_DISPLAY_NONE,
    DPAD_DISPLAY_LEFT,
    DPAD_DISPLAY_RIGHT,
} dpad_display_t;

struct dpad_items {
    union {
        u8 values[4];                   /* 0x0000 */
        struct {
            u8 du;                      /* 0x0000, up    */
            u8 dr;                      /* 0x0001, right */
            u8 dd;                      /* 0x0002, down  */
            u8 dl;                      /* 0x0003, left  */
        };
    };
};

struct dpad_config {
    u32 magic;                          /* 0x0000 */
    u32 version;                        /* 0x0004 */
    union {
        struct dpad_items items[4];     /* 0x0008 */
        struct {
            struct dpad_items primary;  /* 0x0008 */
            struct dpad_items alts[3];  /* 0x000C */
        };
    };
    u8 state;                           /* 0x0018 */
    u8 display;                         /* 0x0019 */
    u8 reserved[2];                     /* 0x001A */
};                                      /* 0x001C */

void dpad_before_player_actor_update(z2_link_t *link, z2_game_t *game);
void dpad_clear_item_textures(void);
void dpad_init(void);

#endif
