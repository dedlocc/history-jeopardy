namespace HistoryJeopardy.Models;

public class Game
{
    public const string InviteCodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public const int InviteCodeLength = 6;

    private static readonly Random Random = new();

    public readonly Guid Id = Guid.NewGuid();
    public readonly string InviteCode = new(Enumerable.Repeat(InviteCodeChars, InviteCodeLength).Select(s => s[Random.Next(s.Length)]).ToArray());

    public readonly List<Player> Players = new();
    public readonly Player Host;
    public readonly GameOptions Options;

    public GameState GameState = GameState.AwaitingPlayers;

    public Game(Player host, GameOptions options)
    {
        Host = host;
        Options = options;
    }
}

public enum GameState
{
    AwaitingPlayers,
    InProcess,
}

public struct GameOptions
{}