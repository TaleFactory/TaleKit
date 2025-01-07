using Serilog;

namespace TaleKit.Network.Packet;



public class PacketFactory
{
    private readonly Dictionary<string, IPacketBuilder> mappings;

    public PacketFactory()
    {
        mappings = typeof(IPacketBuilder).Assembly.GetTypes()
            .Where(x => typeof(IPacketBuilder).IsAssignableFrom(x))
            .Where(x => !x.IsAbstract && !x.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IPacketBuilder>()
            .ToDictionary(x => x.Header);
    }

    public IPacket CreatePacket(string packet)
    {
        var split = packet.Split(" ");
        if (split.Length == 0)
        {
            return default;
        }

        var header = split[0];
        var content = split.Skip(1).ToArray();

        var builder = mappings.GetValueOrDefault(header);
        if (builder is null)
        {
            return default;
        }

        IPacket typedPacket = null;
        try
        {
            typedPacket = builder.CreatePacket(content);
        }
        catch (Exception e)
        {
            Log.Error(e, "Unable to deserialized packet");
        }

        return typedPacket;
    }
}