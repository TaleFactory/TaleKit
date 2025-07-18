namespace TaleKit.Network.Packet.Login;

public class SrvList : IPacket
{
    
}

public class SrvListBuilder : PacketBuilder<SrvList>
{
    public override string Header { get; } = "svrlist";
    protected override SrvList CreatePacket(string[] body)
    {
        return new SrvList();
    }
}