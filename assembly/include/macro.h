#ifndef _MACRO_H_
#define _MACRO_H_

#define FIELD_OFFSET(Type, FieldName) ((u32)(&((Type*)0)->FieldName))
#define FIELD_TO_STRUCT(Type, FieldName, FieldPtr) ((Type*)((u32)(FieldPtr) - FIELD_OFFSET(Type, FieldName)))
#define FIELD_TO_STRUCT_CONST(Type, FieldName, FieldPtr) ((const Type*)(FIELD_TO_STRUCT(Type, FieldName, FieldPtr)))

#define CONTROLLER1(globalCtx) (&(globalCtx)->state.input[0])
#define CONTROLLER2(globalCtx) (&(globalCtx)->state.input[1])
#define CONTROLLER3(globalCtx) (&(globalCtx)->state.input[2])
#define CONTROLLER4(globalCtx) (&(globalCtx)->state.input[3])

#define CHECK_BTN_ALL(state, combo) (~((state) | ~(combo)) == 0)
#define CHECK_BTN_ANY(state, combo) (((state) & (combo)) != 0)

#define WEEKEVENTREG(index) (gSaveContext.perm.weekEventReg.bytes[(index)])
#define GET_WEEKEVENTREG(index) ((void)0, WEEKEVENTREG(index))
#define CHECK_WEEKEVENTREG(flag) (WEEKEVENTREG((flag) >> 8) & ((flag) & 0xFF))
#define SET_WEEKEVENTREG(flag) (WEEKEVENTREG((flag) >> 8) = GET_WEEKEVENTREG((flag) >> 8) | ((flag) & 0xFF))

#define CHECK_EVENTINF(flag) (gSaveContext.owl.eventInf[(flag) >> 4] & (1 << ((flag) & 0xF)))
#define SET_EVENTINF(flag) (gSaveContext.owl.eventInf[(flag) >> 4] |= (1 << ((flag) & 0xF)))
#define CLEAR_EVENTINF(flag) (gSaveContext.owl.eventInf[(flag) >> 4] &= (u8)~(1 << ((flag) & 0xF)))
#define CLEAR_EVENTINF_ALT(flag) (gSaveContext.owl.eventInf[(flag) >> 4] &= ~(1 << ((flag) & 0xF)))

#define SLOT(item) gItemSlots[item]
#define AMMO(item) gSaveContext.perm.inv.quantities[SLOT(item)]
#define INV_CONTENT(item) gSaveContext.perm.inv.items[SLOT(item)]
#define GET_INV_CONTENT(item) ((void)0, gSaveContext.perm.inv.items)[SLOT(item)]

#define SQ(x) ((x) * (x))

#endif
