using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Entities;

namespace TaleKit.Network.Packet.Combat;

public class Us : IPacket
{
    public int CastId { get; set; }
    public EntityType EntityType { get; set; }
    public int EntityId { get; set; }
}

public class UsBuilder : PacketBuilder<Us>
{
    public override string Header { get; } = "u_s";
    
    protected override Us CreatePacket(string[] body)
    {
        return new Us
        {
            CastId = body[0].ToInt(),
            EntityType = body[1].ToEnum<EntityType>(),
            EntityId = body[2].ToInt()
        };
    }
}

public class UsProcessor : PacketProcessor<Us>
{
    protected override void Process(Session session, Us packet)
    {
        var skill = session.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
        if (skill is not null)
        {
            skill.IsOnCooldown = true;
        }
    }
}