#include <z64.h>

bool EnHorse_IsHorseBlocked(CollisionContext* colCtx, BgPolygon* poly, s32 bgId, Actor* actor) {
    if (actor->child != NULL && actor->child->id == ACTOR_PLAYER && actor->child->parent != actor && (actor->bgcheckFlags & 1)) { // BGCHECKFLAG_GROUND
        return true;
    }

    return z2_SurfaceType_IsHorseBlocked(colCtx, poly, bgId) || z2_SurfaceType_GetFloorType(colCtx, poly, bgId) == 7;
}
