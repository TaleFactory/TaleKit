using TaleKit.Game.Entities;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class MonsterFactory
{
    public static Monster CreateMonster(int id, int vnum)
    {
        var data = MonsterRegistry.GetMonsterData(vnum);
        
        return new Monster
        {
            Id = id,
            VirtualNumber = vnum,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Monsters, TaleKitSettings.Language, data.NameKey ?? "") ?? "Undefined"
        };
    }
    
}