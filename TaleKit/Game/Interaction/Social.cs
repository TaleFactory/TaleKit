using TaleKit.Game.Entities;
using TaleKit.Network;

namespace TaleKit.Game.Interaction;

public class Social
{
    public HashSet<Friend> Friends { get; set; } = new();

    private readonly Character character;

    public Social(Character character)
    {
        this.character = character;
    }

    public void JoinMiniland(Friend friend)
    {
        if (!Friends.Contains(friend))
        {
            return;
        }
        
        character.GetNetwork().SendPacket($"mjoin 1 {friend.Id}");
    }

    public void UseWingsOfFriendship(Friend friend)
    {
        if (!Friends.Contains(friend))
            return;
        
        character.GetNetwork().SendPacket($"guri 199 1 {friend.Id}");
    }
}