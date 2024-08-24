using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Maps;

public class Get : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init;}
    public int DropId { get; init; }
}

public class GetBuilder : PacketBuilder<Get>
{
    public override string Header { get; } = "get";
    
    protected override Get CreatePacket(string[] body)
    {
        return new Get
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            DropId = body[2].ToInt()
        };
    }
}

public class GetProcessor : PacketProcessor<Get>
{
    protected override void Process(Session session, Get packet)
    {
        var drop = session.Character.Map.GetEntity<Game.Entities.Drop>(EntityType.Drop, packet.DropId);
        if (drop is null)
        {
            return;
        }
        
        session.Character.Map.RemoveEntity(drop);
    }
}