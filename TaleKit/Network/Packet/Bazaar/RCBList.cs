using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Bazaar;

public class RCBList : IPacket
{
    public required int Page { get; init; }
    public List<RCBListEntry> Entries { get; init; } = new();
}

public class RCBListEntry
{
    public required string Owner { get; init; }
    public required int ItemId { get; init; }
    public required int Amount { get; init; }
    public required int Price { get; init; }
}

public class RCBListBuilder : PacketBuilder<RCBList>
{
    public override string Header => "rc_blist";
    
    protected override RCBList CreatePacket(string[] body)
    {
        var entries = new List<RCBListEntry>();
        var values = body.Skip(1).ToArray();
        foreach (var value in values)
        {
            var split = value.Split('|');
            if (split.Length == 0) 
                continue;

            var name = split[2];
            var itemId = split[3].ToInt();
            var amount = split[4].ToInt();
            var price = split[6].ToInt();  
            
            entries.Add(new RCBListEntry
            {
                Owner = name,
                ItemId = itemId,
                Amount = amount,
                Price = price
            });
        }

        return new RCBList
        {
            Page = body[0].ToInt(),
            Entries = entries
        };
    }
}