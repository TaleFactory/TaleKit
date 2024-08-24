using TaleKit.Network;

namespace TaleKit.Game.Storage;

public class Equipment
{
    private readonly INetwork network;
    private readonly Dictionary<EquipmentSlot, EquipmentItem> content = new();

    public Equipment(INetwork network)
    {
        this.network = network;
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
        
        network.SendPacket("sl 0");
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
        
        network.SendPacket($"wear {stack.Slot} 0");
    }

    public void Remove(EquipmentItem item)
    {
        if (!content.ContainsKey(item.Slot))
        {
            return;
        }
        
        network.SendPacket($"remove {item.Slot} 0");
    }
}