#include <stdbool.h>
#include <z64.h>
#include "ActorExt.h"
#include "Util.h"

// Number of ActorExt entries in the heap.
static const size_t gCount = 0x80;

// Actor extended "heap."
static struct ActorExt* gHeap = NULL;

// Size of the allocated size.
static size_t gHeapSize = 0;

// Index of the next expected-free entry.
static size_t gNextFreeIdx = 0;

static bool CheckIsHeap(void* ptr) {
    u8* ext = (u8*)ptr;
    u8* heap = (u8*)gHeap;
    return (heap <= ext && ext < (heap + gHeapSize));
}

static size_t GetHeapAlignedEntrySize(void) {
    const size_t align = 8;
    size_t ssize = sizeof(struct ActorExt);
    size_t extra = ssize % align;
    if (extra > 0) {
        return ssize + (align - ssize);
    } else {
        return ssize;
    }
};

static size_t GetHeapSize(size_t count) {
    size_t entry = GetHeapAlignedEntrySize();
    return entry * count;
}

/**
 * Decode the ActorExt pointer in an actor instance and mark it as unused.
 **/
void ActorExt_AfterActorDtor(Actor* actor) {
    struct ActorExt* ext = ActorExt_GenericDecode(actor);
    if (ext != NULL) {
        // Mark table entry as free, encode pointer as NULL
        ActorExt_SetFree(ext);
        ActorExt_GenericEncode(actor, NULL);
    }
}

void ActorExt_Clear(void) {
    gNextFreeIdx = 0;
}

/**
 * Find the next unused ActorExt entry in the array.
 **/
struct ActorExt* ActorExt_FindFree(void) {
    if (gHeap == NULL)
        return NULL;

    // Start from index of next expected-free entry
    size_t expected = gNextFreeIdx;

    // Iterate through heap array to find an entry without USED bit set
    for (size_t i = 0; i < gCount; i++) {
        size_t idx = (expected + i) % gCount;
        struct ActorExt* ext = &gHeap[idx];
        if ((ext->flags & ACTOR_EXT_USED) == 0) {
            gNextFreeIdx = (idx + 1) % gCount;
            ext->flags |= ACTOR_EXT_USED;
            return ext;
        }
    }

    return NULL;
}

/**
 * Encode a pointer into unused fields in the actor header.
 **/
void ActorExt_GenericEncode(Actor* actor, const void* ext) {
    u32 pval = (u32)ext;
    actor->pad22[0] = (u8)(pval >> 24);
    actor->pad22[1] = (u8)(pval >> 16);
    actor->pad3A[0] = (u8)(pval >> 8);
    actor->pad3A[1] = (u8)(pval);
}

/**
 * Decode a pointer from the unused fields in the actor header.
 **/
void* ActorExt_GenericDecode(const Actor* actor) {
    u32 pval = 0;
    pval |= (actor->pad22[0] << 24);
    pval |= (actor->pad22[1] << 16);
    pval |= (actor->pad3A[0] << 8);
    pval |= (actor->pad3A[1]);
    return (void*)pval;
}

/**
 * Mark an ActorExt entry as unused.
 **/
void ActorExt_SetFree(struct ActorExt* ext) {
    // Sanitization check to ensure ext points into the heap somewhere.
    if (CheckIsHeap(ext)) {
        ext->flags &= ~ACTOR_EXT_USED;
    } else {
        // Mess with health to indicate an issue.
        // gSaveContext.perm.unk24.maxLife = 0x140;
        // gSaveContext.perm.unk24.currentLife = 4;
    }
}

/**
 * Setup an ActorExt entry for a specific actor instance and return it.
 **/
struct ActorExt* ActorExt_Setup(Actor* actor, bool* created) {
    struct ActorExt* ext = (struct ActorExt*)ActorExt_GenericDecode(actor);
    if (ext != NULL) {
        if (created != NULL)
            *created = false;
        return ext;
    } else {
        if (created != NULL)
            *created = true;
        ext = ActorExt_FindFree();
        ActorExt_GenericEncode(actor, ext);
        return ext;
    }
}

/**
 * Allocate and initialize the ActorExt array.
 **/
void ActorExt_Init(void) {
    // Allocate the ActorExt array
    gHeapSize = GetHeapSize(gCount);
    void* heap = Util_HeapAlloc((int)gHeapSize);
    gHeap = (struct ActorExt*)heap;
}
