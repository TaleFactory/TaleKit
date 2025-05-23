using TaleKit.Game.Entities;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class DropFactory
{
    public static Drop CreateDrop(int id, int vnum, int amount)
    {
        var data = ItemRegistry.GetItemData(vnum);
        
        return new Drop
        {
            Id = id,
            VirtualNumber = vnum,
            Amount = amount,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Items, TaleKitSettings.Language, data?.NameKey ?? "") ?? "Undefined"
        };
    }
}