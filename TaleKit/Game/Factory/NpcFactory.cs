using TaleKit.Game.Entities;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class NpcFactory
{
    public static Npc CreateNpc(int id, int vnum)
    {
        var data = MonsterRegistry.GetMonsterData(vnum);
        if (data is null)
        {
            throw new InvalidOperationException();
        }

        return new Npc
        {
            Id = id,
            VirtualNumber = vnum,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Monsters, NKitSettings.Language, data.NameKey) ?? "Undefined"
        };
    }
}