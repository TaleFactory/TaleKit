using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Maps;

public class Shop : IPacket
{
    public int NpcId { get; init; }
    public string Name { get; init; }
}

public class ShopBuilder : PacketBuilder<Shop>
{
    public override string Header { get; } = "shop";
    
    protected override Shop CreatePacket(string[] body)
    {
        return new Shop
        {
            NpcId = body[1].ToInt(),
            Name = body[5]
        };
    }
}

public class ShopProcessor : PacketProcessor<Shop>
{
    protected override void Process(Session session, Shop packet)
    {
        var map = session.Character.Map;
        if (map is null)
            return;
        
        var npc = session.Character.Map.GetEntity<Npc>(EntityType.Npc, packet.NpcId);
        if (npc is null)
        {
            return;
        }
        
        session.Character.Map.AddShop(new Game.Maps.Shop
        {
            Name = packet.Name,
            Owner = npc
        });
    }
}