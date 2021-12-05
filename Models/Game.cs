namespace HistoryJeopardy.Models;

public class Game
{
    // public const string InviteCodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    // public const int InviteCodeLength = 6;

    // private static readonly Random Random = new();

    public readonly Guid Id = Guid.NewGuid();
    // public readonly string InviteCode = new(Enumerable.Repeat(InviteCodeChars, InviteCodeLength).Select(s => s[Random.Next(s.Length)]).ToArray());

    public readonly Pack Pack;

    public readonly int CurrentRoundId = 0;
    public Round CurrentRound => Pack.Rounds[CurrentRoundId];
    public Question? CurrentQuestion = null;

    public readonly HashSet<Question> CompletedQuestions = new();

    // public readonly List<Player> Players = new();
    public readonly Player Host;

    // public GameState GameState = GameState.AwaitingPlayers;

    public Game(Player host, GameOptions options)
    {
        Host = host;
        Pack = options.Pack;
    }
}

public enum GameState
{
    AwaitingPlayers,
    InProcess,
}

public struct GameOptions
{
    public Pack Pack { get; init; }
}