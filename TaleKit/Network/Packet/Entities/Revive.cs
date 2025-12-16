using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Entities;

namespace TaleKit.Network.Packet.Entities;

public class Revive : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; set; }
}

public class RevivePacket : PacketBuilder<Revive>
{
    public override string Header => "revive";

    protected override Revive CreatePacket(string[] body)
    {
        return new Revive
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt()
        };
    }
}

public class ReviveProcessor : PacketProcessor<Revive>
{
    protected override void Process(Session session, Revive packet)
    {
        var map = session.Character.Map;
        if (map is null)
            return;
        
        var entity = session.Character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }
        
        session.Emit(new EntityReviveEvent
        {
            Entity = entity,
            Session = session
        });
    }
}