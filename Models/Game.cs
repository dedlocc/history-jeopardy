namespace HistoryJeopardy.Models;

public class Game
{
    public readonly Guid Id;
    public readonly Player Host;
    public readonly List<Player> Players = new();
    public readonly GameOptions Options;

    public Game(Guid id, Player host, GameOptions options)
    {
        Id = id;
        Host = host;
        Options = options;
    }
}

public struct GameOptions
{}