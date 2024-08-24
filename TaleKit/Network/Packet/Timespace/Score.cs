using TaleKit.Game;
using TaleKit.Game.Event.Timespaces;

namespace TaleKit.Network.Packet.Timespace;

public class Score : IPacket
{
    
}

public class ScoreBuilder : PacketBuilder<Score>
{
    public override string Header { get; } = "score";
    
    protected override Score CreatePacket(string[] body)
    {
        return new Score();
    }
}

public class ScoreProcessor : PacketProcessor<Score>
{
    protected override void Process(Session session, Score packet)
    {
        session.Emit(new TimespaceCompletedEvent
        {
            Session = session
        });
    }
}