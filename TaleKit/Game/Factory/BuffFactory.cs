using TaleKit.Game.Combat;
using TaleKit.Game.Registry;

namespace TaleKit.Game.Factory;

public class BuffFactory
{
    public static Buff CreateBuff(int virtualNumber)
    {
        var data = CardRegistry.GetCardData(virtualNumber);

        return new Buff
        {
            VirtualNumber = virtualNumber,
            Name = TranslationRegistry.GetTranslation(TranslationGroup.Cards, TaleKitSettings.Language,
                data?.NameKey ?? "") ?? "Undefined",
            Duration = data?.Duration ?? 0,
        };
    }
}