#ifndef _MACRO_H_
#define _MACRO_H_

#define FIELD_OFFSET(Type, FieldName) ((u32)(&((Type*)0)->FieldName))
#define FIELD_TO_STRUCT(Type, FieldName, FieldPtr) ((Type*)((u32)(FieldPtr) - FIELD_OFFSET(Type, FieldName)))
#define FIELD_TO_STRUCT_CONST(Type, FieldName, FieldPtr) ((const Type*)(FIELD_TO_STRUCT(Type, FieldName, FieldPtr)))

#define VALUE_WITHIN_RANGE(value, min, max) (min <= value && value < max)
#define IS_VALID_PTR(ptr) VALUE_WITHIN_RANGE((u32)ptr, 0x80000000, 0x80800000)

#define WEEKEVENTREG(index) (gSaveContext.perm.weekEventReg.bytes[(index)])
#define GET_WEEKEVENTREG(index) ((void)0, WEEKEVENTREG(index))
#define CHECK_WEEKEVENTREG(flag) (WEEKEVENTREG((flag) >> 8) & ((flag) & 0xFF))
#define SET_WEEKEVENTREG(flag) (WEEKEVENTREG((flag) >> 8) = GET_WEEKEVENTREG((flag) >> 8) | ((flag) & 0xFF))

#define SLOT(item) gItemSlots[item]
#define AMMO(item) gSaveContext.perm.inv.quantities[SLOT(item)]
#define INV_CONTENT(item) gSaveContext.perm.inv.items[SLOT(item)]
#define GET_INV_CONTENT(item) ((void)0, gSaveContext.perm.inv.items)[SLOT(item)]

#endif
