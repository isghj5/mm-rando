Changelog (Majora's Mask)
=========================

## Version 1.1.0.0

### Changes

- Payload has been moved to the tail end of the heap and currently resides at `0x80750000`.
- D-Pad configuration has been moved into a versioned structure.
- All versioned configuration structures now have a 32-bit magic number for verification and easy recognition.
- Revamp of `z64.h` into `z2.h`, alongside using the `z2` prefix to differentiate from Ocarina of Time's `z64` prefix.

### Features

- Adds a `misc_config` structure with the option to enable using the ocarina underwater.
- Adds a D-Pad display option which lets you choose how to display the D-Pad (left side of screen, right side of screen, or hide).

### Fixes

- No longer crashes when exiting from Bombers' Notebook due to moving the payload to the heap.
- Properly adjusts the D-Pad display when a timer is on the screen in the following situations:
  - Poe sisters fight
  - Treasure Chest Shop game
  - When drowning
- Improve previous fix for B & C button text: text on the B button is no longer green before obtaining magic.

## Version 1.0.0.0

### Features

- D-Pad functionality which is customizable from external sources. Primarily supports ocarina and transformation masks.
- Large selection of customizable HUD colors.
- Implements some experimental "External Effects" which can be triggered by an outside source, such as Crowd Control.

External Effects currently provided (not guaranteed stable):

| Effect           | Description                                            |
| ---------------- | ------------------------------------------------------ |
| Camera Overlook  | Top-down camera (in most cases).                       |
| Chateau          | Grants infinite magic.                                 |
| Fairy            | Spawns a fairy on top of Link.                         |
| Freeze           | Freezes Link.                                          |
| Ice Physics      | Treats all floors as ice.                              |
| Jinx             | Jinxes Link and cannot be removed via Song of Storms.  |
| No Z             | Disables the Z button.                                 |
| Reverse Controls | Reverses analog controls.                              |

### Fixes

- (Original Game) Ammo counts on B & C buttons are no longer green before obtaining magic.
