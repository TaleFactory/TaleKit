using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Extension;

public static class CharacterExtensions
{
    public static bool IsOnMap(this Character character, int mapId)
    {
        return character.Map?.Id == mapId;
    }
    
    public static void WalkInRange(this Character character, Position target, int range)
    {
        var distance = (double)character.Position.GetDistance(target);

        var x = character.Position.X;
        var y = character.Position.Y;
        
        if (distance > range)
        {
            var scale = (distance - range) / distance;

            x = character.Position.X + (int)((target.X - character.Position.X) * scale);
            y = character.Position.Y + (int)((target.Y - character.Position.Y) * scale);
        }
                    
        WalkTo(character, new Position(x, y));
    }

    public static void WalkTo(this Character character, Position destination)
    {
        character.Walk(destination);

        var count = 0;
        var stuckCount = 0;
        Position previous = default;
        while (character.Position != destination)
        {
            if (count > 5)
            {
                character.Walk(destination);
                count = 0;
            }
            
            if (previous == character.Position)
            {
                stuckCount++;
                if (stuckCount > 20)
                {
                    break;
                }
            }
            else
            {
                stuckCount = 0;
            }
            
            previous = character.Position;
            
            Thread.Sleep(100);

            count++;
        }
    }
}