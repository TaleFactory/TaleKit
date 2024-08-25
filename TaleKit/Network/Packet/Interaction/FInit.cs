using TaleKit.Extension;
using TaleKit.Game;
using TaleKit.Game.Interaction;

namespace TaleKit.Network.Packet.Interaction;

public class FInit : IPacket
{
    public required List<FInitEntry> Entries { get; init; }
}

public class FInitEntry
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}

public class FInitBuilder : PacketBuilder<FInit>
{
    public override string Header { get; } = "finit";
    
    protected override FInit CreatePacket(string[] body)
    {
        var entries = new List<FInitEntry>();
        foreach (var content in body)
        {
            var split = content.Split('|');
            if (split.Length == 0)
            {
                continue;
            }
            
            entries.Add(new FInitEntry
            {
                Id = split[0].ToInt(),
                Name = split[3]
            });
        }

        return new FInit
        {
            Entries = entries
        };
    }
}

public class FInitProcessor : PacketProcessor<FInit>
{
    protected override void Process(Session session, FInit packet)
    {
        session.Character.Social.Friends = packet.Entries
            .Select(x => new Friend 
            { 
                Id = x.Id, 
                Name = x.Name 
            }).ToHashSet();
    }
}