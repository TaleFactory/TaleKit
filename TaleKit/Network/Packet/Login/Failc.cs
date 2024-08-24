using TaleKit.Extension;

namespace TaleKit.Network.Packet.Login;

public enum LoginFailReason
{
    OldClient = 1,
    UnhandledError = 2,
    Maintenance = 3,
    AlreadyConnected = 4,
    AccountOrPasswordWrong = 5,
    CantConnect = 6,
    Banned = 7,
    WrongCountry = 8,
    WrongCaps = 9
}

public class Failc : IPacket
{
    public LoginFailReason Reason { get; init; }
}

public class FailcDeserializer : PacketBuilder<Failc>
{
    public override string Header { get; } = "failc";
    
    protected override Failc CreatePacket(string[] body)
    {
        return new Failc
        {
            Reason = body[0].ToEnum<LoginFailReason>()
        };
    }
}