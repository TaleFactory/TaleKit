using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public class SkillData
{
    public string NameKey { get; set; }
    public int SkillType { get; set; }
    public int CastId { get; set; }
    public int CastTime { get; set; }
    public int Cooldown { get; set; }
    public int MpCost { get; set; }
    public int TargetType { get; set; }
    public int HitType { get; set; }
    public int Range { get; set; }
    public int ZoneRange { get; set; }
}

public class SkillRegistry
{
    private static Dictionary<int, SkillData> Cache;
    private static readonly string DirectoryPath = Path.Combine(NKitSettings.StorageDirectory, "Registry");
    
    public static SkillData GetSkillData(int virtualNumber)
    {
        if (Cache is null)
        {
            var path = Path.Combine(DirectoryPath, "Skills.json");
            var content = File.ReadAllText(path);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(content);

            Cache = deserialized;
        }

        return Cache.GetValueOrDefault(virtualNumber);
    }
}