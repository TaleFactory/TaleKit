using TaleKit.Game.Registry;

namespace TaleKit;

public static class NKitSettings
{
    public static Language Language { get; set; }
    public static string StorageDirectory { get; set; }

    static NKitSettings()
    {
        Language = Language.French;
        StorageDirectory = AppDomain.CurrentDomain.BaseDirectory;
    }
}