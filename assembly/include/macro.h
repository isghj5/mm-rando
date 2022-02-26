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

#endif
