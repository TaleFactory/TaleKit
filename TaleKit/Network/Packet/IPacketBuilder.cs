namespace TaleKit.Network.Packet;

public interface IPacketBuilder
{
    string Header { get; }
    IPacket CreatePacket(string[] body);
}

public abstract class PacketBuilder<T> : IPacketBuilder where T : IPacket
{
    public abstract string Header { get; }
    
    IPacket IPacketBuilder.CreatePacket(string[] body)
    {
        return CreatePacket(body);
    }

    protected abstract T CreatePacket(string[] body);
}