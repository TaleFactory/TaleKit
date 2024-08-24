namespace TaleKit.Network.Packet;

public class ProcessorRegistry
{
    private readonly Dictionary<Type, IPacketProcessor> processors;

    public ProcessorRegistry(IEnumerable<IPacketProcessor> processors)
    {
        this.processors = processors.ToDictionary(x => x.PacketType);
    }
    
    public IPacketProcessor GetProcessor(Type packetType)
    {
        return processors.GetValueOrDefault(packetType);
    }
}