using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Combat;

public class Bfe : IPacket
{
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
    public int EffectVirtualNumber { get; set; }
    public int Duration { get; set; }
}

public class BfeBuilder : PacketBuilder<Bfe>
{
    public override string Header => "bf_e";

    protected override Bfe CreatePacket(string[] body)
    {
        return new Bfe
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            EffectVirtualNumber = body[2].ToInt(),
            Duration = body[3].ToInt()
        };
    }
}

public class BfeProcessor : PacketProcessor<Bfe>
{
    protected override void Process(Session session, Bfe packet)
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
        
        if (packet.Duration > 0)
        {
            entity.Buffs.Add(BuffFactory.CreateBuff(packet.EffectVirtualNumber));
        }
        else
        {
            entity.Buffs.RemoveWhere(x => x.VirtualNumber == packet.EffectVirtualNumber);
        }
    }
}