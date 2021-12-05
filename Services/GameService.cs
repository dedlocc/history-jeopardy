using HistoryJeopardy.Models;

namespace HistoryJeopardy.Services;

public class GameService
{
    public readonly Dictionary<Guid, Game> Games = new();
    // public readonly Dictionary<string, Game> InviteCodes = new();

    public Game Create(Player host, GameOptions options = new())
    {
        var game = new Game(host, options);
        Games.Add(game.Id, game);
        // InviteCodes.Add(game.InviteCode, game);
        return host.Game = game;
    }
}