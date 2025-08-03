using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Combat;

public class Uas : IPacket
{
    public int CastId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public class UasBuilder : PacketBuilder<Uas>
{
    public override string Header { get; } = "u_as";
    
    protected override Uas CreatePacket(string[] body)
    {
        return new Uas
        {
            CastId = body[0].ToInt(),
            X = body[1].ToInt(),
            Y = body[2].ToInt()
        };
    }
}

[ExecuteOnlyOn(Direction = PacketDirection.Send)]
public class UasProcessor : PacketProcessor<Uas>
{
    protected override void Process(Session session, Uas packet)
    {
        var skill = session.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
        if (skill is not null)
        {
            skill.IsOnCooldown = true;
        }
    }
}