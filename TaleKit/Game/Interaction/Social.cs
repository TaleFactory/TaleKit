using TaleKit.Network;

namespace TaleKit.Game.Interaction;

public class Social
{
    public HashSet<Friend> Friends { get; set; } = new();

    private readonly INetwork network;

    public Social(INetwork network)
    {
        this.network = network;
    }

    public void JoinMiniland(Friend friend)
    {
        if (!Friends.Contains(friend))
        {
            return;
        }
        
        network.SendPacket($"mjoin 1 {friend.Id}");
    }
}