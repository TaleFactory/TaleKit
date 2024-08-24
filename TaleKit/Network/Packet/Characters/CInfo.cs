using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Characters;

public class CInfo : IPacket
{
    public string Name { get; init; }
    public int Id { get; init; }
}

public class CInfoBuilder : PacketBuilder<CInfo>
{
    public override string Header { get; } = "c_info";
    
    protected override CInfo CreatePacket(string[] body)
    {
        return new CInfo
        {
            Name = body[0],
            Id = body[5].ToInt()
        };
    }
}

public class CInfoProcessor : PacketProcessor<CInfo>
{
    protected override void Process(Session session, CInfo packet)
    {
        session.Character.Id = packet.Id;
        session.Character.Name = packet.Name;
    }
}