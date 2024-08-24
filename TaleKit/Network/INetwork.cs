namespace TaleKit.Network;

/// <summary>
/// Abstraction used to send/receive packets and bind processors
/// </summary>
public interface INetwork
{
    event Action<string> PacketSend;
    event Action<string> PacketReceived;
    event Action Disconnected;

    void SendPacket(string packet);
    void RecvPacket(string packet);

    void Disconnect();
}