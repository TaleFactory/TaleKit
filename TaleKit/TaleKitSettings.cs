using TaleKit.Game.Registry;

namespace TaleKit;

public static class TaleKitSettings
{
    public static Language Language { get; set; }
    public static string StorageDirectory { get; set; }

    static TaleKitSettings()
    {
        Language = Language.French;
        StorageDirectory = AppDomain.CurrentDomain.BaseDirectory;
    }
}