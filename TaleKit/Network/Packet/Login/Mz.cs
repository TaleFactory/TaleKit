using TaleKit.Extension;

namespace TaleKit.Network.Packet.Login;

public class Mz : IPacket
{
    public required string Host { get; init; }
    public required int Port { get; init; }
}

public class MzBuilder : PacketBuilder<Mz>
{
    public override string Header { get; } = "mz";
    
    protected override Mz CreatePacket(string[] body)
    {
        return new Mz
        {
            Host = body[0],
            Port = body[1].ToInt()
        };
    }
}