using HistoryJeopardy.Models;

namespace HistoryJeopardy.Services;

public class GameService
{
    public readonly Dictionary<Guid, Game> Games = new();

    public void Create(Player host, GameOptions options = new())
    {
        var id = Guid.NewGuid();
        Games.Add(id, new Game(id, host, options));
    }
}