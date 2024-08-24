namespace TaleKit.Game.Storage;

public class InventoryItem : IEquatable<InventoryItem>
{
    public required string Name { get; init; }
    public required int VirtualNumber { get; init; }
    public required int Slot { get; init; }
    public required int Amount { get; init; }
    public required InventoryType Inventory { get; init; }
    public int Rarity { get; init; }
    
    public bool Equals(InventoryItem other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return VirtualNumber == other.VirtualNumber && Slot == other.Slot && Inventory == other.Inventory;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((InventoryItem)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(VirtualNumber, Slot, (int)Inventory);
    }

    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(VirtualNumber)}: {VirtualNumber}, {nameof(Slot)}: {Slot}, {nameof(Amount)}: {Amount}, {nameof(Inventory)}: {Inventory}";
    }
}