using TaleKit.Extension;
using TaleKit.Game.Maps;
using TaleKit.Game;
using TaleKit.Game.Factory;

namespace TaleKit.Network.Packet.Maps;

public class Wp : IPacket
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Level { get; init; }
    public int Id { get; init; }
}

public class WpBuilder : PacketBuilder<Wp>
{
    public override string Header { get; } = "wp";
    
    protected override Wp CreatePacket(string[] body)
    {
        return new Wp
        {
            X = body[0].ToInt(),
            Y = body[1].ToInt(),
            Level = body[4].ToInt(),
            Id = body[2].ToInt()
        };
    }
}

public class WpProcessor : PacketProcessor<Wp>
{
    protected override void Process(Session session, Wp packet)
    {
        var map = session.Character.Map;
        if (map is null)
            return;
        
        var timespace = TimespaceFactory.CreateTimespace(packet.Id, packet.Level, packet.X, packet.Y);
        if (timespace is null)
        {
            return;
        }
        
        session.Character.Map.AddTimespace(timespace);
    }
}