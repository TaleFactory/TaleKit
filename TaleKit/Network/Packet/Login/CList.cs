using TaleKit.Extension;

namespace TaleKit.Network.Packet.Login;

public class CList : IPacket
{
    public required int Index { get; init; }
    public required string Name { get; init; }
}

public class CListDeserializer : PacketBuilder<CList>
{
    public override string Header => "clist";

    protected override CList CreatePacket(string[] parameters)
    {
        return new CList
        {
            Index = parameters[0].ToInt(),
            Name = parameters[1]
        };
    }
}