using TaleKit.Extension;

namespace TaleKit.Network.Packet.Login;

public class NsTeST : IPacket
{
    public required int EncryptionKey { get; init; }
    public required string Username { get; init; }
    public required IEnumerable<NsTeSTServer> Servers { get; init; }
}

public class NsTeSTServer
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Name { get; init; }
    public required int Server { get; init; }
    public required int Channel { get; init; }
}

public class NsTeSTBuilder : PacketBuilder<NsTeST>
{
    public override string Header => "NsTeST";

    protected override NsTeST CreatePacket(string[] body)
    {
        var filtered = body
            .Where(x => x != "1" && x != "0" && x != "3" && x != "-99" && x != "2" && x != "4" && !string.IsNullOrEmpty(x))
            .ToArray();
        
        IEnumerable<NsTeSTServer> servers = filtered.Skip(2).Select(x =>
        {
            var split = x.Split(':');
            var definition = split[^1].Split('.');

            var host = split[0];
            var port = split[1];
            var name = definition[2];
            var channel = definition[1];
            var server = definition[0];
            
            return new NsTeSTServer
            {
                Host = host,
                Port = port.ToInt(),
                Name = name,
                Channel = channel.ToInt(),
                Server = server.ToInt()
            };
        }).ToList();
        
        return new NsTeST
        {
            Username = filtered[0],
            EncryptionKey = filtered[1].ToInt(),
            Servers = servers
        };
    }
}