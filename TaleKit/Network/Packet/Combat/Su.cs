using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Game.Event.Combat;

namespace TaleKit.Network.Packet.Combat;

public class Su : IPacket
{
    public EntityType EntityType { get; init; }
    public int EntityId { get; set; }
    public EntityType TargetEntityType { get; set; }
    public int TargetEntityId { get; set; }
    public int SkillVnum { get; set; }
    public bool TargetIsAlive { get; set; }
    public int TargetHpPercentage { get; set; }
    public int Damage { get; set; }
    
    public int TargetHp { get; set; }
    public int TargetMaxHp { get; set; }
}

public class SuBuilder : PacketBuilder<Su>
{
    public override string Header { get; } = "su";
    
    protected override Su CreatePacket(string[] body)
    {
        return new Su
        {
            EntityType = body[0].ToEnum<EntityType>(),
            EntityId = body[1].ToInt(),
            TargetEntityType = body[2].ToEnum<EntityType>(),
            TargetEntityId = body[3].ToInt(),
            SkillVnum = body[4].ToInt(),
            TargetIsAlive = body[10].ToBool(),
            TargetHpPercentage = body[11].ToInt(),
            Damage = body[12].ToInt(),
            TargetHp = body[15].ToInt(),
            TargetMaxHp = body[16].ToInt()
        };
    }
}

public class SuProcessor : PacketProcessor<Su>
{
    protected override void Process(Session session, Su packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }

        var caster = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
        var target = map.GetEntity<LivingEntity>(packet.TargetEntityType, packet.TargetEntityId);

        if (target is null || caster is null)
        {
            return;
        }
        
        target.HpPercentage = packet.TargetHpPercentage;

        session.Emit(new EntityDamageEvent
        {
            Target = target,
            Caster = caster,
            Damage = packet.Damage,
            Session = session
        });
        
        if (packet.TargetIsAlive)
        {
            return;
        }

        target.HpPercentage = 0;
        
        map.RemoveEntity(target);
        
        session.Emit(new EntityDieEvent
        {
            Entity = target,
            Killer = caster,
            Session = session
        });
    }
}