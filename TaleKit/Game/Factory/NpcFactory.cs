using TaleKit.Game.Entities;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class NpcFactory
{
    public static Npc CreateNpc(int id, int vnum)
    {
        var data = MonsterRegistry.GetMonsterData(vnum);
        
        return new Npc
        {
            Id = id,
            VirtualNumber = vnum,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Monsters, TaleKitSettings.Language, data?.NameKey ?? "") ?? "Undefined"
        };
    }
}