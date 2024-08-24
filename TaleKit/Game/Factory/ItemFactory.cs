using TaleKit.Game.Registry;
using TaleKit.Game.Storage;

namespace TaleKit.Game.Factory;

public class ItemFactory
{
    public static InventoryItem CreateItemStack(int virtualNumber, int amount, int rarity, int slot, InventoryType type)
    {
        var data = ItemRegistry.GetItemData(virtualNumber);
        var stack = new InventoryItem
        {
            VirtualNumber = virtualNumber,
            Amount = amount,
            Name = data is not null
                ? TranslationRegistry.GetTranslation(TranslationGroup.Items,
                      Language.French,
                      data.NameKey) ??
                  "Undefined"
                : "Undefined",
            Slot = slot,
            Inventory = type,
            Rarity = rarity
        };

        return stack;
    }

    public static EquipmentItem CreateEquipmentItem(int virtualNumber, EquipmentSlot slot)
    {
        var data = ItemRegistry.GetItemData(virtualNumber);
        var equipment = new EquipmentItem
        {
            Slot = slot,
            VirtualNumber = virtualNumber,
            Name = data is not null
                ? TranslationRegistry.GetTranslation(TranslationGroup.Items,
                      Language.French,
                      data.NameKey) ??
                  "Undefined"
                : "Undefined",
        };

        return equipment;
    }
}