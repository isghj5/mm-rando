#include <stdbool.h>
#include "actor_ext.h"
#include "util.h"
#include "z2.h"

// Number of actor_ext entries in the heap.
static const size_t g_count = 0x80;

// Actor extended "heap."
static struct actor_ext * g_heap = NULL;

// Size of the allocated size.
static size_t g_heap_size = 0;

// Index of the next expected-free entry.
static size_t g_next_free_idx = 0;

static bool check_is_heap(void *ptr) {
    u8 *ext = (u8*)ptr;
    u8 *heap = (u8*)g_heap;
    return (heap <= ext && ext < (heap + g_heap_size));
}

static size_t get_heap_aligned_entry_size(void) {
    const size_t align = 8;
    size_t ssize = sizeof(struct actor_ext);
    size_t extra = ssize % align;
    if (extra > 0) {
        return ssize + (align - ssize);
    } else {
        return ssize;
    }
};

static size_t get_heap_size(size_t count) {
    size_t entry = get_heap_aligned_entry_size();
    return entry * count;
}

/**
 * Decode the actor_ext pointer in an actor instance and mark it as unused.
 **/
void actor_ext_after_actor_dtor(z2_actor_t *actor) {
    struct actor_ext *ext = actor_ext_generic_decode(actor);
    if (ext != NULL) {
        // Mark table entry as free, encode pointer as NULL
        actor_ext_set_free(ext);
        actor_ext_generic_encode(actor, NULL);
    }
}

void actor_ext_clear(void) {
    g_next_free_idx = 0;
}

/**
 * Find the next unused actor_ext entry in the array.
 **/
struct actor_ext * actor_ext_find_free(void) {
    if (g_heap == NULL)
        return NULL;

    // Start from index of next expected-free entry
    size_t expected = g_next_free_idx;

    // Iterate through heap array to find an entry without USED bit set
    for (size_t i = 0; i < g_count; i++) {
        size_t idx = (expected + i) % g_count;
        struct actor_ext *ext = &g_heap[idx];
        if ((ext->flags & ACTOR_EXT_USED) == 0) {
            g_next_free_idx = (idx + 1) % g_count;
            ext->flags |= ACTOR_EXT_USED;
            return ext;
        }
    }

    return NULL;
}

/**
 * Encode a pointer into unused fields in the actor header.
 **/
void actor_ext_generic_encode(z2_actor_t *actor, const void *ext) {
    u32 pval = (u32)ext;
    actor->unk_0x22[0] = (u8)(pval >> 24);
    actor->unk_0x22[1] = (u8)(pval >> 16);
    actor->unk_0x3A[0] = (u8)(pval >> 8);
    actor->unk_0x3A[1] = (u8)(pval);
}

/**
 * Decode a pointer from the unused fields in the actor header.
 **/
struct actor_ext * actor_ext_generic_decode(const z2_actor_t *actor) {
    u32 pval = 0;
    pval |= (actor->unk_0x22[0] << 24);
    pval |= (actor->unk_0x22[1] << 16);
    pval |= (actor->unk_0x3A[0] << 8);
    pval |= (actor->unk_0x3A[1]);
    return (struct actor_ext *)pval;
}

/**
 * Mark an actor_ext entry as unused.
 **/
void actor_ext_set_free(struct actor_ext *ext) {
    // Sanitization check to ensure ext points into the heap somewhere.
    if (check_is_heap(ext)) {
        ext->flags &= ~ACTOR_EXT_USED;
    } else {
        // Mess with health to indicate an issue.
        // z2_file.max_health = 0x140;
        // z2_file.current_health = 0x04;
    }
}

/**
 * Setup an actor_ext entry for a specific actor instance and return it.
 **/
struct actor_ext * actor_ext_setup(z2_actor_t *actor, bool *created) {
    struct actor_ext *ext = actor_ext_generic_decode(actor);
    if (ext != NULL) {
        if (created != NULL)
            *created = false;
        return ext;
    } else {
        if (created != NULL)
            *created = true;
        ext = actor_ext_find_free();
        actor_ext_generic_encode(actor, ext);
        return ext;
    }
}

/**
 * Allocate and initialize the actor_ext array.
 **/
void actor_ext_init(void) {
    // Allocate the actor_ext array
    g_heap_size = get_heap_size(g_count);
    void *heap = heap_alloc((int)g_heap_size);
    g_heap = (struct actor_ext *)heap;
}
