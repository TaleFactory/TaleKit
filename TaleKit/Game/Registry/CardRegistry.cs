using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public class CardData
{
    public required int Id { get; init; }
    public required string NameKey { get; init; }
    public required int Duration { get; init; }
}

public class CardRegistry
{
    private static Dictionary<int, CardData> Cache;
    private static readonly string DirectoryPath = Path.Combine(TaleKitSettings.StorageDirectory, "Registry");
    
    public static CardData GetCardData(int virtualNumber)
    {
        if (Cache is null)
        {
            var path = Path.Combine(DirectoryPath, "Cards.json");
            var content = File.ReadAllText(path);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<int, CardData>>(content);

            Cache = deserialized;
        }

        return Cache.GetValueOrDefault(virtualNumber);
    }
}