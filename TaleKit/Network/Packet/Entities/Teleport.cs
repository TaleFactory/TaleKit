using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Entities;

namespace TaleKit.Network.Packet.Entities;

public class Teleport : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
}

public class TeleportPacket : PacketBuilder<Teleport>
{
    public override string Header => "tp";

    protected override Teleport CreatePacket(string[] body)
    {
        return new Teleport
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            X = body[2].ToInt(),
            Y = body[3].ToInt()
        };
    }
}

public class TeleportProcessor : PacketProcessor<Teleport>
{
    protected override void Process(Session session, Teleport packet)
    {
        var entity = session.Character.Map.GetEntity<Entity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }
        
        entity.Position = new Position
        {
            X = packet.X,
            Y = packet.Y
        };
        
        session.Emit(new EntityTeleportedEvent
        {
            Entity = entity,
            Session = session,
            At = new Position(packet.X, packet.Y)
        });
    }
}