using System.Security.Cryptography;
using TaleKit.Extension;
using TaleKit.Game.Registry;
using TaleKit.Game;
using TaleKit.Game.Factory;
using TaleKit.Game.Storage;

namespace TaleKit.Network.Packet.Inventory;

public class Ivn : IPacket
{
    public required int Index { get; init; }
    public required InvItem Item { get; init; }
}

public class IvnBuilder : PacketBuilder<Ivn>
{
    public override string Header { get; } = "ivn";
    
    protected override Ivn CreatePacket(string[] body)
    {
        var index = body[0].ToInt();
        var split = body[1].Split('.');
        return new Ivn
        {
            Index = index,
            Item = new InvItem
            {
                Rarity = index == 0 ? split[2].ToInt() : 0,
                Slot = split[0].ToInt(),
                VirtualNumber = split[1].ToInt(),
                Amount = index != 0 ? split[2].ToInt() : 1
            }
        };
    }
}

public class IvnProcessor : PacketProcessor<Ivn>
{
    protected override void Process(Session session, Ivn packet)
    {
        if (packet.Item.VirtualNumber == -1)
        {
            session.Character.Inventory.Remove((InventoryType)packet.Index, packet.Item.Slot);
        }
        else
        {
            var stack = ItemFactory.CreateItemStack(packet.Item.VirtualNumber, packet.Item.Amount, packet.Item.Rarity, packet.Item.Slot, (InventoryType)packet.Index);
            if (stack == null)
            {
                return;
            }
            
            session.Character.Inventory.AddItem(stack);
        }
    }
}