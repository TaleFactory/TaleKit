using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Entities;

public class Cond : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    public int Speed { get; set; }
}

public class CondBuilder : PacketBuilder<Cond>
{
    public override string Header { get; } = "cond";
    
    protected override Cond CreatePacket(string[] body)
    {
        return new Cond
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            CanAttack = !body[2].ToBool(),
            CanMove = !body[3].ToBool(),
            Speed = body[4].ToInt()
        };
    }
}

public class CondProcessor : PacketProcessor<Cond>
{
    protected override void Process(Session session, Cond packet)
    {
        var entity = session.Character.Map?.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
        if (entity is null)
        {
            return;
        }

        entity.CanAttack = packet.CanAttack;
        entity.CanMove = packet.CanMove;
        entity.Speed = packet.Speed;
    }
}