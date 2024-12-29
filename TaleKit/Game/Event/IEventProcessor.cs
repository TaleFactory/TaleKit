namespace TaleKit.Game.Event;

public interface IEventProcessor
{
    public Type EventType { get; }
    void Process(IEvent e);
}

public abstract class EventProcessor<T> : IEventProcessor where T : IEvent
{
    public Type EventType { get; } = typeof(T);
    
    public void Process(IEvent e)
    {
        Process((T)e);
    }

    protected abstract void Process(T e);
}

public class SimpleProcessor<T> : EventProcessor<T> where T : IEvent
{
    private readonly Action<T> processor;
    
    public SimpleProcessor(Action<T> processor)
    {
        this.processor = processor;
    }
    
    protected override void Process(T e)
    {
        processor(e);
    }
}