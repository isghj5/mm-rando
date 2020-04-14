#include <stdbool.h>
#include "quest_item_storage.h"
#include "save_file.h"

struct save_file_config SAVE_FILE_CONFIG = {
    .magic = SAVE_FILE_CONFIG_MAGIC,
    .version = 0,
};

static void save_file_init_struct(struct save_file_config *config) {
    config->magic = SAVE_FILE_CONFIG_MAGIC;
    config->version = 0;
    quest_item_storage_clear(&config->quest_storage);
}

static bool save_file_try_copy_struct(struct save_file_config *dest, const void *src_bytes) {
    struct save_file_config *src = (struct save_file_config *)src_bytes;
    if (src->magic == SAVE_FILE_CONFIG_MAGIC) {
        // If magic matches, copy over struct data
        z2_memcpy(dest, src_bytes, sizeof(*dest));
        return true;
    } else {
        // Otherwise, initialize struct
        save_file_init_struct(dest);
        return false;
    }
}

/**
 * Clear save file config data.
 **/
void save_file_clear(void) {
    save_file_init_struct(&SAVE_FILE_CONFIG);
}

/**
 * Get the offset of custom save file struct in the save flash data section.
 **/
u32 save_file_get_flash_section_offset(bool owl_save) {
    if (owl_save) {
        return SAVE_FILE_OFFSET_OWL;
    } else {
        return SAVE_FILE_OFFSET_NEW;
    }
}

/**
 * Read existing save file struct data into our struct.
 **/
bool save_file_read(const void *src) {
    return save_file_try_copy_struct(&SAVE_FILE_CONFIG, src);
}

/**
 * Write save file config to a destination buffer.
 **/
void save_file_write(void *dest) {
    z2_memcpy(dest, &SAVE_FILE_CONFIG, sizeof(SAVE_FILE_CONFIG));
}
