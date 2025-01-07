using TaleKit.Game.Entities;

namespace TaleKit.Game.Interaction;

public class TradeSide
{
    public int Gold { get; set; }
    public int BankGold { get; set; }
}

public class Trade(Session session)
{
    public Player Other { get; init; }

    public TradeSide OwnSide { get; } = new();
    public TradeSide OtherSide { get; set; }
    
    public void Register()
    {
        session.SendPacket($"exc_list {OwnSide.Gold} {OwnSide.BankGold}");   
    }

    public void Complete()
    {
        session.SendPacket("req_exc 3");
    }
}