using TaleKit.Game;

namespace TaleKit.Network.Packet;

public interface IPacketProcessor
{
    Type PacketType { get; }
    void Process(Session session, IPacket packet);
}

public abstract class PacketProcessor<T> : IPacketProcessor where T : IPacket
{
    public Type PacketType { get; } = typeof(T);
    
    public void Process(Session session, IPacket packet)
    {
        Process(session, (T) packet);
    }

    protected abstract void Process(Session session, T packet);
}