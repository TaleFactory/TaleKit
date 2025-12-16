using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Entities;

namespace TaleKit.Network.Packet.Entities;

public class Mv : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int Speed { get; init; }
}

public class MvBuilder : PacketBuilder<Mv>
{
    public override string Header => "mv";

    protected override Mv CreatePacket(string[] body)
    {
        return new Mv
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            X = body[2].ToInt(),
            Y = body[3].ToInt(),
            Speed = body[4].ToInt()
        };
    }
}

public class MvProcessor : PacketProcessor<Mv>
{
    protected override void Process(Session session, Mv packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }
        
        var entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }

        var from = entity.Position;
        entity.Position = new Position(packet.X, packet.Y);

        session.Emit(new EntityMovedEvent
        {
            Session = session,
            Entity = entity,
            From = from,
            To = entity.Position
        });
    }
}