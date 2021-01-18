using System.ComponentModel;

namespace Monitor.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T enumValue)
            where T : struct
        {
            if (!typeof(T).IsEnum)
                return null;

            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return enumValue.ToString();
        }
    }
}
