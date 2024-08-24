namespace TaleKit.Network.Packet.Login;

public class CListEnd : IPacket
{
    
}

public class CListEndDeserializer : PacketBuilder<CListEnd>
{
    public override string Header => "clist_end";

    protected override CListEnd CreatePacket(string[] parameters)
    {
        return new CListEnd();
    }
}