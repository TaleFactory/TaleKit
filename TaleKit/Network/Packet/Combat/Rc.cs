using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Combat;

namespace TaleKit.Network.Packet.Combat;

public class Rc : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; init; }
    public int Amount { get; init; }
}

public class RcBuilder : PacketBuilder<Rc>
{
    public override string Header => "rc";

    protected override Rc CreatePacket(string[] body)
    {
        return new Rc
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            Amount = body[2].ToInt()
        };
    }
}

public class RcProcessor : PacketProcessor<Rc>
{
    protected override void Process(Session session, Rc packet)
    {
        var entity = session.Character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }
        
        session.Emit(new EntityHealEvent
        {
            Entity = entity,
            Amount = packet.Amount,
            Session = session
        });
    }
}