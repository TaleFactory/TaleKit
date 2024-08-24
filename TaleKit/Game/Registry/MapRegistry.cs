using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public class MapData
{
    public required string NameKey { get; init; }
    public required byte[] Grid { get; init; }
}

public class MapRegistry
{
    private static Dictionary<int, MapData> Cache;
    private static readonly string DirectoryPath = Path.Combine(TaleKitSettings.StorageDirectory, "Registry");
    
    public static MapData GetMapData(int virtualNumber)
    {
        if (Cache is null)
        {
            var path = Path.Combine(DirectoryPath, "Maps.json");
            var content = File.ReadAllText(path);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<int, MapData>>(content);

            Cache = deserialized;
        }

        return Cache.GetValueOrDefault(virtualNumber);
    }
}