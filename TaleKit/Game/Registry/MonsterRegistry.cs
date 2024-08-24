using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public class MonsterData
{
    public int Level { get; init; }
    public string NameKey { get; init; }
}

public class MonsterRegistry
{
    private static Dictionary<int, MonsterData> Cache;
    private static readonly string DirectoryPath = Path.Combine(TaleKitSettings.StorageDirectory, "Registry");
    
    public static MonsterData GetMonsterData(int virtualNumber)
    {
        if (Cache is null)
        {
            var path = Path.Combine(DirectoryPath, "Monsters.json");
            var content = File.ReadAllText(path);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<int, MonsterData>>(content);

            Cache = deserialized;
        }

        return Cache.GetValueOrDefault(virtualNumber);
    }
}