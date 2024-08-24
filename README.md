# TaleKit

TaleKit contains a working **packet based*** system for creating NosTale bot, tools etc...  
This project is useless as is since it's just some kind of abstraction of the game.
###### * Depend of the implementation, some memory reading, writing can be done too
## Project implementing TaleKit

### Private
- [TaleKit.Spark](https://github.com/TaleFactory/TaleKit.Spark) (Clientless)
- [TaleKit.Storm](https://github.com/TaleFactory/TaleKit.Storm) (Local)

### Public
- [TaleKit.Phoenix](https://github.com/TaleFactory/TaleKit.Phoenix) (Local using Phoenix Bot)

### How to create you own implementation
You need two major things to create your own implementation

<details>
  <summary>INetwork implementation</summary>
  
```csharp
public class SampleNetwork : INetwork
{
    public event Action<string>? PacketSend;
    public event Action<string>? PacketReceived;
    public event Action? Disconnected;
    
    public void SendPacket(string packet)
    {
        throw new NotImplementedException();
    }

    public void RecvPacket(string packet)
    {
        throw new NotImplementedException();
    }

    public void Disconnect()
    {
        throw new NotImplementedException();
    }
}
```
</details>

<details>
  <summary>IActionBridge implementation</summary>
  
```csharp
public class SampleBridge : IActionBridge
{
    public Session? Session { get; set; }
    
    public void Walk(Position position, int speed)
    {
        throw new NotImplementedException();
    }

    public void Attack(LivingEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Attack(LivingEntity entity, Skill skill)
    {
        throw new NotImplementedException();
    }

    public void PickUp(Drop drop)
    {
        throw new NotImplementedException();
    }
}
```
</details>

<details>
  <summary>Session Factory</summary>
  
```csharp
public static class SampleFactory
{
    public static Session CreateSession()
    {
        // Connect to remote server with clientless, bind to local method to capture packets etc ...

        var network = new SampleNetwork();
        var bridge = new SampleBridge();

        return TaleKitFactory.CreateSession(new SessionConfiguration
        {
            Network = network,
            ActionBridge = bridge
        });
    }
}
```
</details>


