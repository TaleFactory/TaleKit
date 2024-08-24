using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TaleKit.Configuration;

public static class ConfigurationFactory
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .Build();
    
    public static T LoadFromYaml<T>(string path)
    {
        var content = File.ReadAllText(path);
        return Deserializer.Deserialize<T>(content);
    }
}