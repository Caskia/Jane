using Jane.Reflection;
using System;

namespace Jane.Extensions
{
    public static class ReflectionExtensions
    {
        public static T GetFieldValue<T>(this object obj, string fieldName)
              where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return ReflectionHelper.GetInstanceField<T>(obj.GetType(), obj, fieldName);
        }
    }
}