#ifndef ITEMS_H
#define ITEMS_H

enum custom_item {
    // Non-progressive upgrades
    Z2_TYCOONS_WALLET     = 0xA4,
    Z2_STICK_UPGRADE_20   = 0xA5,
    Z2_STICK_UPGRADE_30   = 0xA6,
    Z2_NUTS_UPGRADE_30    = 0xA7,
    Z2_NUTS_UPGRADE_40    = 0xA8,
    // Progressive Upgrades
    Z2_PROGRESSIVE_BOW    = 0xA9,
    Z2_PROGRESSIVE_BOMBS  = 0xAA,
    Z2_PROGRESSIVE_STICKS = 0xAB,
    Z2_PROGRESSIVE_NUTS   = 0xAC,
    Z2_PROGRESSIVE_SWORD  = 0xAD,
    Z2_PROGRESSIVE_MAGIC  = 0xAE,
    Z2_PROGRESSIVE_WALLET = 0xAF,
    // Traps
    Z2_ICE_TRAP           = 0xB0,
};

#endif // ITEMS_H
