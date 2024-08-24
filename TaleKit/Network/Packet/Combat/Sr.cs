using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Combat;

public class Sr : IPacket
{
    public int CastId { get; set; }
}

public class SrBuilder : PacketBuilder<Sr>
{
    public override string Header { get; } = "sr";
    
    protected override Sr CreatePacket(string[] body)
    {
        return new Sr
        {
            CastId = body[0].ToInt()
        };
    }
}

public class SrProcessor : PacketProcessor<Sr>
{
    protected override void Process(Session session, Sr packet)
    {
        var skill = session.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
        if (skill is null)
        {
            return;
        }

        skill.IsOnCooldown = false;
    }
}