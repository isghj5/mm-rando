#ifndef _MACRO_H_
#define _MACRO_H_

#define FIELD_OFFSET(Type, FieldName) ((u32)(&((Type*)0)->FieldName))
#define FIELD_TO_STRUCT(Type, FieldName, FieldPtr) ((Type*)((u32)(FieldPtr) - FIELD_OFFSET(Type, FieldName)))
#define FIELD_TO_STRUCT_CONST(Type, FieldName, FieldPtr) ((const Type*)(FIELD_TO_STRUCT(Type, FieldName, FieldPtr)))

#define VALUE_WITHIN_RANGE(value, min, max) (min <= value && value < max)
#define IS_VALID_PTR(ptr) VALUE_WITHIN_RANGE((u32)ptr, 0x80000000, 0x80800000)

#endif
