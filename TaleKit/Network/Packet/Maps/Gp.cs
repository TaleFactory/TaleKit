using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Factory;
using TaleKit.Game.Maps;

namespace TaleKit.Network.Packet.Maps;

public class Gp : IPacket
{
    public int X { get; init; }
    public int Y { get; init; }
    public int DestinationId { get; init; }
    public PortalType PortalType { get; init; }
    public int Id { get; init; }
}

public class GpBuilder : PacketBuilder<Gp>
{
    public override string Header { get; } = "gp";
    
    protected override Gp CreatePacket(string[] body)
    {
        return new Gp
        {
            X = body[0].ToInt(),
            Y = body[1].ToInt(),
            DestinationId = body[2].ToInt(),
            PortalType = body[3].ToEnum<PortalType>(),
            Id = body[4].ToInt()
        };
    }
}

public class GpProcessor : PacketProcessor<Gp>
{
    protected override void Process(Session session, Gp packet)
    {
        var map = session.Character.Map;
        if (map is null)
        {
            return;
        }

        var portal = PortalFactory.CreatePortal(packet.Id, packet.DestinationId, packet.PortalType, packet.X, packet.Y);
        if (portal is null)
        {
            return;
        }
        
        map.AddPortal(portal);
    }
}