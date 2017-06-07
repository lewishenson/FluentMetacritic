using System;
using System.Reflection;

namespace FluentMetacritic.Scraping
{
    public static class Converter
    {
        public static T SafeConvert<T>(string value)
        {
            return SafeConvert(value, default(T));
        }

        public static T SafeConvert<T>(string value, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            var targetType = typeof(T);
            var targetTypeInfo = targetType.GetTypeInfo();
            if (targetTypeInfo.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                try
                {
                    return (T)Convert.ChangeType(value, targetTypeInfo.GetGenericArguments()[0]);
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}