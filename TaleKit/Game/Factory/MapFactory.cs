using TaleKit.Game.Maps;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public static class MapFactory
{
    public static Map CreateMap(int mapId)
    {
        var data = MapRegistry.GetMapData(mapId > 4599 ? 15000 : mapId);

        var grid = data == null
            ? []
            : data.Grid;
        
        return new Map
        {
            Id = mapId,
            Name = data == null 
                ? "Undefined" 
                : TranslationRegistry.GetTranslation(TranslationGroup.Maps, NKitSettings.Language, data.NameKey) ?? "Undefined",
            Grid = grid,
            Width = grid.Length == 0 
                ? 0 
                : BitConverter.ToInt16(grid.Take(2).ToArray(), 0),
            Height = grid.Length == 0 
                ? 0 
                : BitConverter.ToInt16(grid.Skip(2).Take(2).ToArray(), 0)
        };
    }

    public static string GetMapName(int mapId)
    {
        var data = MapRegistry.GetMapData(mapId);
        if (data is null)
        {
            return "Undefined";
        }

        return TranslationRegistry.GetTranslation(TranslationGroup.Maps, NKitSettings.Language, data.NameKey);
    }
}