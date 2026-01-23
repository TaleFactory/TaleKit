using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace TaleKit.Game.Registry;

public enum TranslationGroup
{
    Items,
    Monsters,
    Skills,
    Maps,
    Cards
}

public enum Language
{
    French
}

public class TranslationRegistry
{
    private static readonly ConcurrentDictionary<TranslationGroup, ConcurrentDictionary<Language, Dictionary<string, string>>> Cache = new();
    
    private static readonly string DirectoryPath = Path.Combine(TaleKitSettings.StorageDirectory, "Registry", "Translation");
    
    public static string GetTranslation(TranslationGroup group, Language language, string key)
    {
        var category = Cache.GetValueOrDefault(group);
        var translations = category?.GetValueOrDefault(language);
        
        if (translations is null)
        {
            var path = Path.Combine(DirectoryPath, group.ToString(), $"{language}.json");
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            
            if (category == null)
            {
                Cache[group] = category = new ConcurrentDictionary<Language, Dictionary<string, string>>();
            }

            category[language] = translations = Load(path);
        }

        return translations.GetValueOrDefault(key);
    }

    public static string GetTranslationKey(TranslationGroup group, Language language, string value)
    {
        var category = Cache.GetValueOrDefault(group);
        var translations = category?.GetValueOrDefault(language);
        
        if (translations is null)
        {
            var path = Path.Combine(DirectoryPath, group.ToString(), $"{language}.json");
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            
            if (category == null)
            {
                Cache[group] = category = new ConcurrentDictionary<Language, Dictionary<string, string>>();
            }

            category[language] = translations = Load(path);
        }
        
        return translations?.FirstOrDefault(pair => string.Equals(pair.Value, value, StringComparison.InvariantCultureIgnoreCase)).Key ?? string.Empty;
    }

    public static List<KeyValuePair<string, string>> GetTranslations(TranslationGroup group, Language language)
    {
        var category = Cache.GetValueOrDefault(group);
        var translations = category?.GetValueOrDefault(language);
        
        if (translations is null)
        {
            var path = Path.Combine(DirectoryPath, group.ToString(), $"{language}.json");
            if (!File.Exists(path))
            {
                return [];
            }
            
            if (category == null)
            {
                Cache[group] = category = new ConcurrentDictionary<Language, Dictionary<string, string>>();
            }

            category[language] = translations = Load(path);
        }
        
        return translations.ToList();
    }

    private static Dictionary<string, string> Load(string path)
    {
        var content = File.ReadAllText(path);
        var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

        return deserialized;
    }
}