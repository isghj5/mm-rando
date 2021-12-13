#ifndef _ENUMS_H_
#define _ENUMS_H_

enum ButtonsStateValue {
    BUTTONS_STATE_BLACK_SCREEN,
    BUTTONS_STATE_TRANSITION,
    BUTTONS_STATE_TRANSITION_2,          // Postman's timing game.
    BUTTONS_STATE_DIALOGUE = 5,
    BUTTONS_STATE_6 = 6,                 // Used when on Epona sometimes, for example holding B (bow) while pressing A ("Return").
    BUTTONS_STATE_PAUSE = 7,
    BUTTONS_STATE_ARCHERY_GAME = 8,
    BUTTONS_STATE_MINIGAME = 0xC,
    BUTTONS_STATE_SHOP = 0xF,
    BUTTONS_STATE_ITEM_PROMPT = 0x10,
    BUTTONS_STATE_BOAT_ARCHERY = 0x11,   // Boat cruise archery game.
    BUTTONS_STATE_HONEY_DARLING = 0x14,  // Honey & Darling minigame.
    BUTTONS_STATE_PICTOBOX = 0x15,
    BUTTONS_STATE_SWORDSMAN_GAME = 0x16,
    BUTTONS_STATE_NORMAL = 0x32,
};

enum ButtonTextID {
    BUTTON_TEXT_ATTACK,
    BUTTON_TEXT_CHECK,
    BUTTON_TEXT_ENTER,
    BUTTON_TEXT_RETURN,
    BUTTON_TEXT_OPEN,
    BUTTON_TEXT_JUMP,
    BUTTON_TEXT_DECIDE,
    BUTTON_TEXT_DIVE,
    BUTTON_TEXT_FASTER,
    BUTTON_TEXT_THROW,
    BUTTON_TEXT_BLANK,
    BUTTON_TEXT_CLIMB,
    BUTTON_TEXT_DROP,
    BUTTON_TEXT_DOWN,
    BUTTON_TEXT_QUIT,
    BUTTON_TEXT_SPEAK,
    BUTTON_TEXT_NEXT,
    BUTTON_TEXT_GRAB,
    BUTTON_TEXT_STOP,
    BUTTON_TEXT_PUT_AWAY,
    BUTTON_TEXT_J_SOW,
    BUTTON_TEXT_INFO,
    BUTTON_TEXT_WARP,
    BUTTON_TEXT_SNAP,
    BUTTON_TEXT_EXPLODE,
    BUTTON_TEXT_DANCE,
    BUTTON_TEXT_MARCH,
    BUTTON_TEXT_1,
    BUTTON_TEXT_2,
    BUTTON_TEXT_3,
    BUTTON_TEXT_4,
    BUTTON_TEXT_5,
    BUTTON_TEXT_6,
    BUTTON_TEXT_7,
    BUTTON_TEXT_8,
    BUTTON_TEXT_CURL,
    BUTTON_TEXT_SURFACE,
    BUTTON_TEXT_SWIM,
    BUTTON_TEXT_PUNCH,
    BUTTON_TEXT_POUND,
    BUTTON_TEXT_J_HOOK,
    BUTTON_TEXT_SHOOT,
};

enum CameraState {
    CAMERA_STATE_NONE,
    CAMERA_STATE_NORMAL0,
    CAMERA_STATE_NORMAL3,
    CAMERA_STATE_CIRCLE5,
    CAMERA_STATE_HORSE0,
    CAMERA_STATE_ZORA0,
    CAMERA_STATE_PREREND0,
    CAMERA_STATE_PREREND1,
    CAMERA_STATE_DOORC,
    CAMERA_STATE_DEMO0,
    CAMERA_STATE_FREE0,
    CAMERA_STATE_FUKAN0,
    CAMERA_STATE_NORMAL1,
    CAMERA_STATE_NANAME,
    CAMERA_STATE_CIRCLE0,
    CAMERA_STATE_FIXED0,
    CAMERA_STATE_SPIRAL,
    CAMERA_STATE_DUNGEON0,
    CAMERA_STATE_ITEM0,
    CAMERA_STATE_ITEM1,
    CAMERA_STATE_ITEM2,
    CAMERA_STATE_ITEM3,
    CAMERA_STATE_NAVI,
    CAMERA_STATE_WARP0,
    CAMERA_STATE_DEATH,
    CAMERA_STATE_REBIRTH,
    CAMERA_STATE_TREASURE,
    CAMERA_STATE_TRANSFORM,
    CAMERA_STATE_ATTENTION,
    CAMERA_STATE_WARP1,
    CAMERA_STATE_DUNGEON1,
    CAMERA_STATE_FIXED1,
    CAMERA_STATE_FIXED2,
    CAMERA_STATE_MAZE,
    CAMERA_STATE_REMOTEBOMB,
    CAMERA_STATE_CIRCLE1,
    CAMERA_STATE_CIRCLE2,
    CAMERA_STATE_CIRCLE3,
    CAMERA_STATE_CIRCLE4,
    CAMERA_STATE_FIXED3,
    CAMERA_STATE_TOWER0,
    CAMERA_STATE_PARALLEL0,
    CAMERA_STATE_NORMALD,
    CAMERA_STATE_SUBJECTD,
    CAMERA_STATE_START0,
    CAMERA_STATE_START2,
    CAMERA_STATE_STOP0,
    CAMERA_STATE_JCRUISING,
    CAMERA_STATE_CLIMEMAZE,
    CAMERA_STATE_SIDED,
    CAMERA_STATE_DUNGEON2,
    CAMERA_STATE_BOSS_SHIGE,
    CAMERA_STATE_KEEPBACK,
    CAMERA_STATE_CIRCLE6,
    CAMERA_STATE_CIRCLE7,
    CAMERA_STATE_CHUBOSS,
    CAMERA_STATE_RFIXED1,
    CAMERA_STATE_TRESURE1,
    CAMERA_STATE_BOMBBASKET,
    CAMERA_STATE_CIRCLE8,
    CAMERA_STATE_FUKAN1,
    CAMERA_STATE_DUNGEON3,
    CAMERA_STATE_TELESCOPE,
    CAMERA_STATE_ROOM0,
    CAMERA_STATE_RCIRC0,
    CAMERA_STATE_CIRCLE9,
    CAMERA_STATE_ONTHEPOLE,
    CAMERA_STATE_INBUSH,
    CAMERA_STATE_BOSS_LAST,
    CAMERA_STATE_BOSS_INI,
    CAMERA_STATE_BOSS_HAK,
    CAMERA_STATE_BOSS_KON,
    CAMERA_STATE_CONNECT0,
    CAMERA_STATE_MORAY,
    CAMERA_STATE_NORMAL2,
    CAMERA_STATE_BOMBBOWL,
    CAMERA_STATE_CIRCLEa,
    CAMERA_STATE_WHIRLPOOL,
    CAMERA_STATE_KOKKOGAME,
    CAMERA_STATE_GIANT,
    CAMERA_STATE_SCENE0,
    CAMERA_STATE_ROOM1,
    CAMERA_STATE_WATER2,
    CAMERA_STATE_SOKONASI,
    CAMERA_STATE_FORCEKEEP,
    CAMERA_STATE_PARALLEL1,
    CAMERA_STATE_START1,
    CAMERA_STATE_ROOM2,
    CAMERA_STATE_NORMAL4,
    CAMERA_STATE_SHELL,
    CAMERA_STATE_DUNGEON4,
};

enum CameraMode {
    CAMERA_MODE_NORMAL,
    CAMERA_MODE_JUMP,
    CAMERA_MODE_GORONDASH,
    CAMERA_MODE_NUTSSHOT,
    CAMERA_MODE_BOWARROWZ,
    CAMERA_MODE_NUTSFLY,
    CAMERA_MODE_SUBJECT,
    CAMERA_MODE_BOOKEEPON,
    CAMERA_MODE_ZORAFIN,
    CAMERA_MODE_KEEPON,
    CAMERA_MODE_PARALLEL,
    CAMERA_MODE_TALK,
    CAMERA_MODE_PACHINCO,
    CAMERA_MODE_BOWARROW,
    CAMERA_MODE_BATTLE,
    CAMERA_MODE_NUTSHIDE,
    CAMERA_MODE_STILL,
    CAMERA_MODE_CHARGE,
    CAMERA_MODE_CLIMB,
    CAMERA_MODE_CLIMBZ,
    CAMERA_MODE_FOOKSHOT,
    CAMERA_MODE_FREEFALL,
    CAMERA_MODE_HANG,
    CAMERA_MODE_HANGZ,
    CAMERA_MODE_PUSHPULL,
    CAMERA_MODE_NUTSFLYZ,
    CAMERA_MODE_GORONJUMP,
    CAMERA_MODE_BOOMERANG,
    CAMERA_MODE_CHARGEZ,
    CAMERA_MODE_ZORAFINZ,
};

enum DamageEffect {
    DAMAGE_EFFECT_NORMAL,     // Damaged normally.
    DAMAGE_EFFECT_FLY_BACK,   // Flies backwards screaming.
    DAMAGE_EFFECT_FLY_BACK_2, // Flies backwards.
    DAMAGE_EFFECT_FREEZE,     // Freezes.
    DAMAGE_EFFECT_ELECTRIC,   // Electrocutes.
};

enum FloorPhysicsType {
    FLOOR_PHYSICS_NORMAL = 0,
    FLOOR_PHYSICS_ICE = 5,
    FLOOR_PHYSICS_SNOW = 0xE,
};

enum FloorType {
    FLOOR_TYPE_DIRT = 0,
    FLOOR_TYPE_SAND = 1,
    FLOOR_TYPE_STONE = 2,
    FLOOR_TYPE_WET1 = 4,
    FLOOR_TYPE_WET2 = 5,
    FLOOR_TYPE_PLANTS = 6,
    FLOOR_TYPE_GRASS = 8,
    FLOOR_TYPE_WOOD = 0xA,
    FLOOR_TYPE_SNOW = 0xE,
    FLOOR_TYPE_ICE = 0xF,
};

enum GraphicID {
    GRAPHIC_HEART_CONTAINER = 0x13,
    GRAPHIC_HEART_PIECE = 0x14,
    GRAPHIC_GI_GLASSES = 0x30,
    GRAPHIC_GI_SUTARU = 0x4B, // Skulltula Token used with OBJECT_GI_SUTARU
    GRAPHIC_ST_TOKEN = 0x57, // Skulltula Token used with OBJECT_ST
    GRAPHIC_MOONS_TEAR = 0x5A,
    GRAPHIC_ODOLWA_REMAINS = 0x5D,
    GRAPHIC_GOHT_REMAINS = 0x64,
    GRAPHIC_GYORG_REMAINS = 0x65,
    GRAPHIC_TWINMOLD_REMAINS = 0x66,
};

// Item inventory slots.
enum InventoryItemSlot {
    SLOT_OCARINA,
    SLOT_BOW,
    SLOT_FIRE_ARROW,
    SLOT_ICE_ARROW,
    SLOT_LIGHT_ARROW,
    SLOT_QUEST1,
    SLOT_BOMB,
    SLOT_BOMBCHU,
    SLOT_DEKU_STICK,
    SLOT_DEKU_NUT,
    SLOT_MAGIC_BEAN,
    SLOT_QUEST2,
    SLOT_POWDER_KEG,
    SLOT_PICTOGRAPH,
    SLOT_LENS,
    SLOT_HOOKSHOT,
    SLOT_FAIRY_SWORD,
    SLOT_QUEST3,
    SLOT_BOTTLE_1,
    SLOT_BOTTLE_2,
    SLOT_BOTTLE_3,
    SLOT_BOTTLE_4,
    SLOT_BOTTLE_5,
    SLOT_BOTTLE_6,
};

// Mask inventory slots.
enum InventoryMaskSlot {
    SLOT_POSTMAN_HAT,
    SLOT_ALL_NIGHT_MASK,
    SLOT_BLAST_MASK,
    SLOT_STONE_MASK,
    SLOT_GREAT_FAIRY_MASK,
    SLOT_DEKU_MASK,
    SLOT_KEATON_MASK,
    SLOT_BREMEN_MASK,
    SLOT_BUNNY_HOOD,
    SLOT_DON_GERO_MASK,
    SLOT_MASK_OF_SCENTS,
    SLOT_GORON_MASK,
    SLOT_ROMANI_MASK,
    SLOT_CIRCUS_LEADER_MASK,
    SLOT_KAFEI_MASK,
    SLOT_COUPLE_MASK,
    SLOT_MASK_OF_TRUTH,
    SLOT_ZORA_MASK,
    SLOT_KAMARO_MASK,
    SLOT_GIBDO_MASK,
    SLOT_GARO_MASK,
    SLOT_CAPTAIN_HAT,
    SLOT_GIANT_MASK,
    SLOT_FIERCE_DEITY_MASK,
};

enum ItemValue {
    // 0x00 (Inventory)
    ITEM_OCARINA,
    ITEM_BOW,
    ITEM_FIRE_ARROW,
    ITEM_ICE_ARROW,
    ITEM_LIGHT_ARROW,
    ITEM_FAIRY_OCARINA,
    ITEM_BOMB,
    ITEM_BOMBCHU,
    ITEM_DEKU_STICK,
    ITEM_DEKU_NUT,
    ITEM_MAGIC_BEAN,
    ITEM_SLINGSHOT,
    ITEM_POWDER_KEG,
    ITEM_PICTOGRAPH,
    ITEM_LENS,
    ITEM_HOOKSHOT,
    ITEM_FAIRY_SWORD,
    ITEM_HOOKSHOT_OOT,

    // 0x12 (Bottle)
    ITEM_EMPTY_BOTTLE,
    ITEM_RED_POTION,
    ITEM_GREEN_POTION,
    ITEM_BLUE_POTION,
    ITEM_FAIRY,
    ITEM_DEKU_PRINCESS,
    ITEM_MILK_BOTTLE,
    ITEM_MILK_HALF_BOTTLE,
    ITEM_FISH,
    ITEM_BUGS,
    ITEM_BLUE_FIRE,
    ITEM_POE,
    ITEM_BIG_POE,
    ITEM_WATER,
    ITEM_HOT_WATER,
    ITEM_ZORA_EGG,
    ITEM_GOLD_DUST_BOTTLE,
    ITEM_MUSHROOM,
    ITEM_SEAHORSE,
    ITEM_CHATEAU_ROMANI_BOTTLE,
    ITEM_EEL_BOTTLE,
    ITEM_EMPTY_BOTTLE_2,

    // 0x28 (Quest)
    ITEM_MOON_TEAR,
    ITEM_TOWN_DEED,
    ITEM_SWAMP_DEED,
    ITEM_MOUNTAIN_DEED,
    ITEM_OCEAN_DEED,
    ITEM_ROOM_KEY,
    ITEM_MAMA_LETTER,
    ITEM_KAFEI_LETTER,
    ITEM_PENDANT,
    ITEM_MAP,

    // 0x32 (Masks)
    ITEM_DEKU_MASK,
    ITEM_GORON_MASK,
    ITEM_ZORA_MASK,
    ITEM_FIERCE_DEITY_MASK,
    ITEM_MASK_OF_TRUTH,
    ITEM_KAFEI_MASK,
    ITEM_ALL_NIGHT_MASK,
    ITEM_BUNNY_HOOD,
    ITEM_KEATON_MASK,
    ITEM_GARO_MASK,
    ITEM_ROMANI_MASK,
    ITEM_CIRCUS_LEADER_MASK,
    ITEM_POSTMAN_HAT,
    ITEM_COUPLE_MASK,
    ITEM_GREAT_FAIRY_MASK,
    ITEM_GIBDO_MASK,
    ITEM_DON_GERO_MASK,
    ITEM_KAMARO_MASK,
    ITEM_CAPTAIN_HAT,
    ITEM_STONE_MASK,
    ITEM_BREMEN_MASK,
    ITEM_BLAST_MASK,
    ITEM_MASK_OF_SCENTS,
    ITEM_GIANT_MASK,

    // 0x4A (???)
    ITEM_BOW_FIRE_ARROW,
    ITEM_BOW_ICE_ARROW,
    ITEM_BOW_LIGHT_ARROW,

    // 0x4D (Equipment)
    ITEM_KOKIRI_SWORD,
    ITEM_RAZOR_SWORD,
    ITEM_GILDED_SWORD,
    ITEM_HELIX_SWORD,
    ITEM_HERO_SHIELD,
    ITEM_MIRROR_SHIELD,
    ITEM_QUIVER_30,
    ITEM_QUIVER_40,
    ITEM_QUIVER_50,
    ITEM_BOMB_BAG_20,
    ITEM_BOMB_BAG_30,
    ITEM_BOMB_BAG_40,
    ITEM_WALLET_UNUSED,
    ITEM_ADULT_WALLET,
    ITEM_GIANT_WALLET,
    ITEM_FISHING_ROD,

    // 0x5D (Remains)
    ITEM_ODOLWA_REMAINS,
    ITEM_GOHT_REMAINS,
    ITEM_GYORG_REMAINS,
    ITEM_TWINMOLD_REMAINS,

    // 0x61 (Songs)
    ITEM_SONATA_OF_AWAKENING,
    ITEM_GORON_LULLABY,
    ITEM_NEW_WAVE_BOSSA_NOVA,
    ITEM_ELEGY_OF_EMPTINESS,
    ITEM_OATH_TO_ORDER,
    ITEM_SARIA_SONG,
    ITEM_SONG_OF_TIME,
    ITEM_SONG_OF_HEALING,
    ITEM_EPONA_SONG,
    ITEM_SONG_OF_SOARING,
    ITEM_SONG_OF_STORMS,
    ITEM_SUN_SONG,

    // 0x6D (Misc Equipment?)
    ITEM_BOMBER_NOTEBOOK,
    ITEM_MAGIC_JAR = 0x79,
    ITEM_MAGIC_JAR_LARGE,
    ITEM_HEART_PIECE,

    // 0x83 (Pickups)
    ITEM_HEART = 0x83,
    ITEM_GREEN_RUPEE,
    ITEM_BLUE_RUPEE,
    ITEM_RED_RUPEE = 0x87,
    ITEM_PURPLE_RUPEE,
    ITEM_SILVER_RUPEE,
    ITEM_GOLD_RUPEE,
    ITEM_PICKUP_DEKU_STICKS_5,
    ITEM_PICKUP_DEKU_STICKS_10,
    ITEM_PICKUP_DEKU_NUTS_5,
    ITEM_PICKUP_DEKU_NUTS_10,
    ITEM_PICKUP_BOMBS_5,
    ITEM_PICKUP_BOMBS_10,
    ITEM_PICKUP_BOMBS_20,
    ITEM_PICKUP_BOMBS_30,
    ITEM_PICKUP_ARROWS_10,
    ITEM_PICKUP_ARROWS_30,
    ITEM_PICKUP_ARROWS_40,
    ITEM_PICKUP_ARROWS_50,
    ITEM_PICKUP_BOMBCHU_20,
    ITEM_PICKUP_BOMBCHU_10,
    ITEM_PICKUP_BOMBCHU_1,
    ITEM_PICKUP_BOMBCHU_5,

    ITEM_CHATEAU_ROMANI = 0x9F,
    ITEM_MILK = 0xA0,
    ITEM_GOLD_DUST = 0xA1,
    ITEM_EEL = 0xA2,
    ITEM_SEAHORSE2 = 0xA3,

    // 0xFF (None)
    ITEM_NONE = 0xFF,
};

enum PlayerForm {
    PLAYER_FORM_FIERCE_DEITY,
    PLAYER_FORM_GORON,
    PLAYER_FORM_ZORA,
    PLAYER_FORM_DEKU,
    PLAYER_FORM_HUMAN,
};

enum PlayerState1 {
    PLAYER_STATE1_TIME_STOP   = 0x20000000, // Time is stopped but Link & NPC animations continue.
    PLAYER_STATE1_SPECIAL_2   = 0x10000000, // Form transition, using ocarina.
    PLAYER_STATE1_SWIM        = 0x08000000, // Swimming.
    PLAYER_STATE1_DAMAGED     = 0x04000000, // Damaged.
    PLAYER_STATE1_ZORA_WEAPON = 0x01000000, // Zora fins are out, "Put Away" may be prompted.
    PLAYER_STATE1_EPONA       = 0x00800000, // On Epona.
    PLAYER_STATE1_SHIELD      = 0x00400000, // Shielding.
    PLAYER_STATE1_ZORA_FINS   = 0x00200000, // Using Zora fins.
    PLAYER_STATE1_AIM         = 0x00100000, // Aiming bow, hookshot, Zora fins, etc.
    PLAYER_STATE1_FALLING     = 0x00080000, // In the air (without jumping beforehand).
    PLAYER_STATE1_AIR         = 0x00040000, // In the air (with jumping beforehand).
    PLAYER_STATE1_Z_VIEW      = 0x00020000, // In Z-target view.
    PLAYER_STATE1_Z_CHECK     = 0x00010000, // Z-target check-able or speak-able.
    PLAYER_STATE1_Z_ON        = 0x00008000, // Z-target enabled.
    PLAYER_STATE1_LEDGE_HANG  = 0x00002000, // Hanging from ledge.
    PLAYER_STATE1_CHARGE_SPIN = 0x00001000, // Charging spin attack.
    PLAYER_STATE1_HOLD        = 0x00000800, // Hold above head.
    PLAYER_STATE1_GET_ITEM    = 0x00000400, // Hold new item over head.
    PLAYER_STATE1_TIME_STOP_2 = 0x00000200, // Time is stopped (does not affect Tatl, HUD animations).
    PLAYER_STATE1_DEAD        = 0x00000080, // Dead.
    PLAYER_STATE1_MOVE_SCENE  = 0x00000020, // When walking in a cutscene? Used during Postman's minigame.
    PLAYER_STATE1_BARRIER     = 0x00000010, // Zora electric barrier.
    PLAYER_STATE1_ITEM_OUT    = 0x00000008, // Item is out, may later prompt "Put Away." Relevant to Bow, Hookshot, not Great Fairy Sword.
    PLAYER_STATE1_LEDGE_CLIMB = 0x00000004, // Climbing ledge.
    PLAYER_STATE1_TIME_STOP_3 = 0x00000002, // Everything stopped and Link is stuck in place.
};

enum PlayerState2 {
    PLAYER_STATE2_IDLE        = 0x10000000, // Idle animation.
    PLAYER_STATE2_OCARINA     = 0x08000000, // Using ocarina? Maybe more.
    PLAYER_STATE2_KAMARO      = 0x02000000, // Kamaro mask dance.
    PLAYER_STATE2_CAN_DOWN    = 0x00400000, // Can get down from Epona.
    PLAYER_STATE2_TATL_BUTTON = 0x00200000, // Tatl C up button prompt.
    PLAYER_STATE2_TATL_OUT    = 0x00100000, // When tatl is out.
    PLAYER_STATE2_Z_JUMP      = 0x00080000, // Z-target jumping (sidehop, backflip).
    PLAYER_STATE2_SPIN_ATTACK = 0x00020000, // Spin attack.
    PLAYER_STATE2_FROZEN      = 0x00004000, // Frozen, ends once ice cracks.
    PLAYER_STATE2_CLIMB_STAY  = 0x00001000, // Stationary while climbing.
    PLAYER_STATE2_DIVING      = 0x00000800, // Diving.
    PLAYER_STATE2_DIVING_2    = 0x00000400, // Diving, swimming as Zora.
    PLAYER_STATE2_GRABBING    = 0x00000100, // Grabbing onto a block.
    PLAYER_STATE2_CLIMBING    = 0x00000040, // Climbing. Also occurs during: transforming, hanging from ledge, deku spinning, goron ball, sliding.
    PLAYER_STATE2_MOVING      = 0x00000020, // Running / moving.
    PLAYER_STATE2_PUSH_PULL   = 0x00000010, // Pushing or pulling a block.
    PLAYER_STATE2_MOVING_2    = 0x00000008, // Is set for some movement frames.
    PLAYER_STATE2_CHECK       = 0x00000002, // "Check" or "Speak" prompt may appear.
    PLAYER_STATE2_MAY_GRAB    = 0x00000001, // "Grab" prompt may appear.
};

enum PlayerState3 {
    PLAYER_STATE3_BREMEN      = 0x20000000, // Bremen mask march.
    PLAYER_STATE3_ROLLING     = 0x08000000, // Rolling (non-Goron).
    PLAYER_STATE3_ATTACK      = 0x02000000, // Attacking with sword, B button weapon.
    PLAYER_STATE3_DEKU_AIR_2  = 0x01000000, // Hover with flower petals? Maybe more.
    PLAYER_STATE3_DEKU_HOP    = 0x00200000, // Deku hopping on water.
    PLAYER_STATE3_GORON_SPIKE = 0x00080000, // Goron spike roll.
    PLAYER_STATE3_TRANS_PART  = 0x00020000, // Transforming (latter-half).
    PLAYER_STATE3_ZORA_SWIM   = 0x00008000, // Zora swimming/diving.
    PLAYER_STATE3_DEKU_AIR    = 0x00002000, // Hover with flower petals.
    PLAYER_STATE3_DEKU_RISE   = 0x00000200, // Jumping out of Deku flower.
    PLAYER_STATE3_DEKU_DIVE   = 0x00000100, // Deku flower dive.
    PLAYER_STATE3_PULL_BOW    = 0x00000040, // Pull back bow string.
    PLAYER_STATE3_ATTACK_2    = 0x00000008, // Post-attack.
    PLAYER_STATE3_JUMP_ATTACK = 0x00000002, // Beginning of jump attack.
};

enum SegmentID {
    SEGMENT_DIRECT_REFERENCE,
    SEGMENT_NINTENDO_LOGO,
    SEGMENT_CURRENT_SCENE,
    SEGMENT_CURRENT_ROOM,
    SEGMENT_GAMEPLAY_KEEP,
    SEGMENT_GAMEPLAY_DUNGEON_FIELD_KEEP,
    SEGMENT_CURRENT_OBJECT,
    SEGMENT_LINK_ANIMATION, // Unsure if used.
    SEGMENT_CURRENT_MASK = 10,
    SEGMENT_Z_BUFFER = 14,
    SEGMENT_FRAME_BUFFER = 15,
};

enum StoredSong {
    STORED_SONG_NONE1 = -1, // No stored song while not using ocarina.
    STORED_SONG_NONE2 = -2, // No stored song while using ocarina.
};

enum TimerIndex {
    TIMER_INDEX_POE_SISTERS = 1,         // Poe sisters fight.
    TIMER_INDEX_TREASURE_CHEST_GAME = 4, // Treasure Chest Shop game.
    TIMER_INDEX_DROWNING = 5,            // Drowning.
    TIMER_INDEX_SKULL_KID = 0x13,        // Clock tower skull kid encounter.
    TIMER_INDEX_HONEY_DARLING = 0x14,    // Honey & Darling game.
};

// Macro for checking if a timer state is visible.
#define IS_TIMER_VISIBLE(TState) (TIMER_STATE_INIT <= (TState) && (TState) < TIMER_STATE_FINISHED)

enum TimerState {
    TIMER_STATE_NONE,     // No timer.
    TIMER_STATE_PRE,      // Timer is not being displayed yet.
    TIMER_STATE_INIT,     // Timer is in middle of screen.
    TIMER_STATE_MOVING,   // Timer is moving into position.
    TIMER_STATE_SET,      // Timer is positioned.
    TIMER_STATE_FINISHED, // Timer is finished and no longer displaying.
};

enum TimerType {
    TIMER_TYPE_SKULL_KID = 3, // Skull kid on clock tower.
    TIMER_TYPE_TCG = 4,       // Treasure chest game, maybe others.
    TIMER_TYPE_DROWNING = 5,  // Drowning.
    TIMER_TYPE_NONE = 0x63,   // None.
};

enum TimeSpeed {
    TIME_SPEED_NORMAL,
    TIME_SPEED_INVERTED = -2,
    TIME_SPEED_STOPPED = -3,
};

#endif
