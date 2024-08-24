namespace TaleKit.Extension;

public static class RandomExtensions
{
    public static TimeSpan Seconds(this Random random, int minimum, int maximum)
    {
        return TimeSpan.FromSeconds(random.Next(minimum, maximum));
    }
    
    public static TimeSpan Milliseconds(this Random random, int minimum, int maximum)
    {
        return TimeSpan.FromMilliseconds(random.Next(minimum, maximum));
    }
}