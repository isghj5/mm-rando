#ifndef SAVE_FILE_H
#define SAVE_FILE_H

#include <stdbool.h>
#include "quest_item_storage.h"
#include "z2.h"

#define SAVE_FILE_CONFIG_MAGIC 0x53415645

#define SAVE_FILE_OFFSET_NEW   0x100C
#define SAVE_FILE_OFFSET_OWL   0x3CA0

struct save_file_config {
    u32                       magic;                 /* 0x0000 */
    u32                       version;               /* 0x0004 */
    struct quest_item_storage quest_storage;         /* 0x0008 */
};                                                   /* 0x001A */

extern struct save_file_config SAVE_FILE_CONFIG;

void save_file_clear(void);
u32 save_file_get_flash_section_offset(bool owl_save);
bool save_file_read(const void *src);
void save_file_write(void *dest);

#endif // SAVE_FILE_H
