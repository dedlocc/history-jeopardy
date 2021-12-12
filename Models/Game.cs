using System.Diagnostics.CodeAnalysis;

namespace HistoryJeopardy.Models;

public class Game
{
    // public const string InviteCodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    // public const int InviteCodeLength = 6;

    // private static readonly Random Random = new();

    public readonly Guid Id = Guid.NewGuid();
    // public readonly string InviteCode = new(Enumerable.Repeat(InviteCodeChars, InviteCodeLength).Select(s => s[Random.Next(s.Length)]).ToArray());

    public readonly Pack Pack;

    public int CurrentRoundId { get; private set; }
    public Round CurrentRound => Pack.Rounds[CurrentRoundId];
    public Question? CurrentQuestion;

    public readonly HashSet<Question> CompletedQuestions = new();

    // public readonly List<Player> Players = new();
    public readonly Player Host;

    // public GameState GameState = GameState.AwaitingPlayers;

    public Game(Player host, GameOptions options)
    {
        Host = host;
        Pack = options.Pack;
    }

    public bool TryCompleteQuestion([MaybeNullWhen(false)] out Question question)
    {
        if (CurrentQuestion is null) {
            question = null;
            return false;
        }

        question = CurrentQuestion;

        CompletedQuestions.Add(question);
        if (CompletedQuestions.Count == CurrentRound.Questions.Count) {
            ++CurrentRoundId;
            CompletedQuestions.Clear();
        }

        CurrentQuestion = null;
        return true;
    }

    public bool IsFinished()
    {
        return CurrentRoundId == Pack.Rounds.Count;
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