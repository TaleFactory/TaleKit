using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public class ItemData
{
    public int Id { get; set; }
    public string NameKey { get; set; }
    public int BagType { get; set; }
    public int Type { get; set; }
    public int SubType { get; set; }
}

public class ItemRegistry
{
    private static Dictionary<int, ItemData> Cache;
    private static readonly string DirectoryPath = Path.Combine(NKitSettings.StorageDirectory, "Registry");
    
    public static ItemData GetItemData(int virtualNumber)
    {
        if (Cache is null)
        {
            var path = Path.Combine(DirectoryPath, "Items.json");
            var content = File.ReadAllText(path);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(content);

            Cache = deserialized;
        }

        return Cache.GetValueOrDefault(virtualNumber);
    }
}