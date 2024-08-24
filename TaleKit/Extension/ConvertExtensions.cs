namespace TaleKit.Extension;

internal static class ConvertExtensions
{
    public static int ToInt(this string value)
    {
        return int.Parse(value);
    }
    
    public static T ToEnum<T>(this string value) where T : System.Enum
    {
        if (Enum.TryParse(typeof(T), value, out var output))
        {
            return (T)output;
        }

        return default;
    }

    public static bool ToBool(this string value)
    {
        return value == "1";
    }
}