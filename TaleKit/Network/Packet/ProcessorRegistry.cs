using System.Reflection;

namespace TaleKit.Network.Packet;

public enum PacketDirection
{
    Send,
    Receive,
    Both
}

public class ExecuteOnlyOnAttribute : Attribute
{
    public required PacketDirection Direction { get; init; }
}

public class ProcessorRegistry
{
    private readonly Dictionary<PacketDirection, Dictionary<Type, IPacketProcessor>> processors;

    public ProcessorRegistry(IEnumerable<IPacketProcessor> processors)
    {
        this.processors = new Dictionary<PacketDirection, Dictionary<Type, IPacketProcessor>>();
        foreach (var processor in processors)
        {
            var direction = processor.GetType().GetCustomAttribute<ExecuteOnlyOnAttribute>()?.Direction ?? PacketDirection.Receive;
            
            if (!this.processors.ContainsKey(direction))
            {
                this.processors[direction] = new Dictionary<Type, IPacketProcessor>();
            }
            
            if (direction == PacketDirection.Both)
            {
                this.processors[PacketDirection.Send][processor.PacketType] = processor;
                this.processors[PacketDirection.Receive][processor.PacketType] = processor;
                continue;
            }
            
            this.processors[direction][processor.PacketType] = processor;
        }
    }
    
    public IPacketProcessor GetProcessor(PacketDirection direction, Type packetType)
    {
        if (!this.processors.TryGetValue(direction, out var processor))
        {
            return null;
        }
        
        return processor.GetValueOrDefault(packetType);
    }
}