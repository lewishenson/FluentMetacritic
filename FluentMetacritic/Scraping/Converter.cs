using System;

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
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return (T)Convert.ChangeType(value, targetType.GetGenericArguments()[0]);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}