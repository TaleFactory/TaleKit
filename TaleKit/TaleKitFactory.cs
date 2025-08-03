using System.Runtime.CompilerServices;
using TaleKit.Game;
using TaleKit.Game.Entities;
using TaleKit.Network;

[assembly:InternalsVisibleTo("TaleKit.Phoenix")]
[assembly:InternalsVisibleTo("TaleKit.Spark")]

namespace TaleKit;

public class SessionConfiguration
{
    public required INetwork Network { get; init; }
    public required IExecutor Executor { get; init; }
}

public class TaleKitFactory
{
    public static Session CreateSession(SessionConfiguration configuration)
    {
        return new Session(configuration.Network, configuration.Executor);
    }
}