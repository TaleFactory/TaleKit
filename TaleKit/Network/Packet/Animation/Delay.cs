﻿using TaleKit.Extension;
using TaleKit.Game;

namespace TaleKit.Network.Packet.Animation;

public class Delay : IPacket
{
    public int Time { get; init; }
    public int Identifier { get; init; }
    public string Response { get; init; }
}

public class DelayBuilder : PacketBuilder<Delay>
{
    public override string Header { get; } = "delay";
    
    protected override Delay CreatePacket(string[] body)
    {
        return new Delay
        {
            Time = body[0].ToInt(),
            Identifier = body[1].ToInt(),
            Response = body[2]
        };
    }
}

public class DelayProcessor : PacketProcessor<Delay>
{
    protected override void Process(Session session, Delay packet)
    {
        session.Character.Dance();
        Task.Delay(packet.Time).Then(() =>
        {
            session.SendPacket(packet.Response);
        });
    }
}