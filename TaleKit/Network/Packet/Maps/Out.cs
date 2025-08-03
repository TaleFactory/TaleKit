using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Entities;

namespace TaleKit.Network.Packet.Maps;

public class Out : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
}

public class OutBuilder : PacketBuilder<Out>
{
    public override string Header { get; } = "out";
    
    protected override Out CreatePacket(string[] body)
    {
        return new Out
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt()
        };
    }
}

public class OutProcessor : PacketProcessor<Out>
{
    protected override void Process(Session session, Out packet)
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

        if (entity is Nosmate nosmate)
        {
            nosmate.Owner?.Nosmates.Remove(nosmate);
        }
        
        map.RemoveEntity(entity);
        
        session.Emit(new EntityLeftEvent
        {
            Session = session,
            Entity = entity
        });
    }
}