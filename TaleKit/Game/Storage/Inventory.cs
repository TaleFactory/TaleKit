using TaleKit.Game.Entities;
using TaleKit.Network;

namespace TaleKit.Game.Storage;

public enum InventoryType
{
    Equipment = 0,
    Main = 1,
    Etc = 2,
}

public class Inventory
{
    public int Gold { get; set; }
    
    private readonly Character character;
    private readonly Dictionary<InventoryType, Dictionary<int, InventoryItem>> content = new();

    public Inventory(Character character)
    {
        this.character = character;
    }

    public InventoryItem GetItem(Func<InventoryItem, bool> selector)
    {
        return content.Values
            .SelectMany(x => x.Values)
            .FirstOrDefault(selector);
    }
    
    public IEnumerable<InventoryItem> GetItems(InventoryType inventory)
    {
        var storage = content.GetValueOrDefault(inventory);
        if (storage == null)
        {
            return Array.Empty<InventoryItem>();
        }

        return storage.Values;
    }

    internal void AddItem(InventoryItem inventoryItem)
    {
        var storage = content.GetValueOrDefault(inventoryItem.Inventory);
        if (storage == null)
        {
            content[inventoryItem.Inventory] = storage = new Dictionary<int, InventoryItem>();
        }

        storage[inventoryItem.Slot] = inventoryItem;
    }

    internal void Remove(InventoryType inventory, int slot)
    {
        content?.GetValueOrDefault(inventory)?.Remove(slot);
    }

    public InventoryItem GetItem(InventoryType inventory, int slot)
    {
        return content.GetValueOrDefault(inventory)?.GetValueOrDefault(slot);
    }

    public void Use(InventoryItem stack)
    {
        var exists = content.GetValueOrDefault(stack.Inventory)?.GetValueOrDefault(stack.Slot);
        if (exists is null)
        {
            return;
        }
        
        character.GetNetwork().SendPacket($"u_i {(int)character.EntityType} {character.Id} {(int)stack.Inventory} {stack.Slot} 0 0 ");
    }

    public async Task<InventoryItem> WaitForItem(Func<InventoryItem, bool> expression)
    {
        while (true)
        {
            var item = GetItem(expression);
            if (item != null)
            {
                return item;
            }

            await Task.Delay(100);
        }
    }

    public void Drop(InventoryItem stack, int amount = 0)
    {
        var exists = content.GetValueOrDefault(stack.Inventory)?.GetValueOrDefault(stack.Slot);
        if (exists is null)
        {
            return;
        }
        
        if (amount == 0)
        {
            amount = stack.Amount;
        }
        
        character.GetNetwork().SendPacket($"put {(int)stack.Inventory} {stack.Slot} {amount}");
    }
}