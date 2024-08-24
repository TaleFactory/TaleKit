using TaleKit.Extension;
using TaleKit.Game.Registry;
using TaleKit.Game;
using TaleKit.Game.Factory;
using TaleKit.Game.Storage;

namespace TaleKit.Network.Packet.Inventory;

public class Inv : IPacket
{
    public required int Index { get; init; }
    public required List<InvItem> Items { get; init; }
}

public class InvItem
{
    public int Rarity { get; init; }
    public required int Slot { get; init; }
    public required int VirtualNumber { get; init; }
    public required int Amount { get; init; }
}

public class InvBuilder : PacketBuilder<Inv>
{
    public override string Header { get; } = "inv";
    
    protected override Inv CreatePacket(string[] body)
    {
        var index = body[0].ToInt();
        var items = new List<InvItem>();

        if (body.Length > 1)
        {
            foreach (var item in body.Skip(1))
            {
                var split = item.Split('.');
                items.Add(new InvItem
                {
                    Rarity = index == 0 ? split[2].ToInt() : 0,
                    Slot = split[0].ToInt(),
                    VirtualNumber = split[1].ToInt(),
                    Amount = index != 0 ? split[2].ToInt() : 1
                });
            }
        }

        return new Inv
        {
            Index = body[0].ToInt(),
            Items = items
        };
    }
}

public class InvProcessor : PacketProcessor<Inv>
{
    protected override void Process(Session session, Inv packet)
    {
        foreach (var item in packet.Items)
        {
            var stack = ItemFactory.CreateItemStack(item.VirtualNumber, item.Amount, item.Rarity, item.Slot, (InventoryType)packet.Index);
            if (stack == null)
            {
                continue;
            }
            
            session.Character.Inventory.AddItem(stack);
        }
    }
}