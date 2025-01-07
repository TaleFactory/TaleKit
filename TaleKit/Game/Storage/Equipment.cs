using TaleKit.Game.Entities;
using TaleKit.Network;

namespace TaleKit.Game.Storage;

public class Equipment
{
    private readonly Character character;
    private readonly Dictionary<EquipmentSlot, EquipmentItem> content = new();

    public Equipment(Character character)
    {
        this.character = character;
    }

    public void Clear()
    {
        content.Clear();
    }

    public void WearSp()
    {
        if (!content.ContainsKey(EquipmentSlot.Specialist))
        {
            return;
        }
        
        character.GetNetwork().SendPacket("sl 0");
    }

    public EquipmentItem GetItem(EquipmentSlot slot)
    {
        return content.GetValueOrDefault(slot);
    }

    internal void SetItem(EquipmentItem item)
    {
        content[item.Slot] = item;
    }

    public void Wear(InventoryItem stack)
    {
        if (stack.Inventory != InventoryType.Equipment)
        {
            return;
        }
        
        character.GetNetwork().SendPacket($"wear {stack.Slot} 0");
    }

    public void Remove(EquipmentItem item)
    {
        if (!content.ContainsKey(item.Slot))
        {
            return;
        }
        
        character.GetNetwork().SendPacket($"remove {item.Slot} 0");
    }
}