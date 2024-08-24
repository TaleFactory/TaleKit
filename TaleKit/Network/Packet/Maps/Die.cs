using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Maps;

public class Die : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
}

public class DieBuilder : PacketBuilder<Die>
{
    public override string Header { get; } = "die";
    
    protected override Die CreatePacket(string[] body)
    {
        return new Die
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt()
        };
    }
}

public class DieProcessor : PacketProcessor<Die>
{
    protected override void Process(Session session, Die packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }

        var entity = map.GetEntity<Entity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }
        
        map.RemoveEntity(entity);
    }
}