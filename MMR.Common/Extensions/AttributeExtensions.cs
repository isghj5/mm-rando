using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MMR.Common.Extensions
{
    public static class AttributeExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            return type.GetField(name)
                .GetCustomAttributes(typeof(TAttribute), false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>();
        }

        public static bool HasAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return false;
            }
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .Any();
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false)
                .OfType<TAttribute>();
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(false)
                .OfType<TAttribute>()
                .Any();
        }
    }
}
