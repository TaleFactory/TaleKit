using TaleKit.Game.Entities;

namespace TaleKit.Extension;

public static class CharacterExtensions
{
    public static bool IsOnMap(this Character character, int mapId)
    {
        return character.Map?.Id == mapId;
    }
}