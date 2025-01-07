using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Characters;

public class Walk : IPacket
{
    public int X { get; init; }
    public int Y { get; init; }
    
    public int Checksum { get; init; }
    
    public int Speed { get; init; }
}

public class WalkBuilder : PacketBuilder<Walk>
{
    public override string Header { get; } = "walk";
    
    protected override Walk CreatePacket(string[] body)
    {
        return new Walk
        {
            X = body[0].ToInt(),
            Y = body[1].ToInt()
        };
    }
}

[ExecuteOnlyOn(Direction = PacketDirection.Send)]
public class WalkProcessor : PacketProcessor<Walk>
{
    protected override void Process(Session session, Walk packet)
    {
        var distance = session.Character.Position.GetDistance(new Position
        {
            X = packet.X,
            Y = packet.Y
        });
        
        Task.Delay(distance * 200).Then(() =>
        {
            session.Character.Position = new Position
            {
                X = packet.X,
                Y = packet.Y
            };
        });
    }
}