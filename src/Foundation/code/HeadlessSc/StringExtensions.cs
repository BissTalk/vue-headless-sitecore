using System.Linq;

namespace HeadlessSc
{
    public static class StringExtensions
    {
        public static string ToJsPropertyName(this string value)
        {
            value = value?.Trim();
            if ((value?.Length ?? 0) == 0)
                return value;
            if (char.IsUpper(value.First()))
                value = string.Concat(value.First().ToString().ToLowerInvariant(), value.Substring(1));
            if (value.Contains(" "))
                value.Replace(' ', '_');
            return value;
        }

        public static string ToJsonValueString(this string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : null;

        }
    }
}