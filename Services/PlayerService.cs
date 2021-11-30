using System.Diagnostics.CodeAnalysis;
using HistoryJeopardy.Models;

namespace HistoryJeopardy.Services;

public class PlayerService
{
    public readonly Dictionary<Guid, Player> Players = new();

    public Player Register(string name)
    {
        Player player = new(name);
        Players.Add(player.Id, player);
        return player;
    }

    public Player? Get(Guid id)
    {
        return Players.GetValueOrDefault(id);
    }

    public bool TryGet(HttpContext httpContext, [MaybeNullWhen(false)] out Player player)
    {
        // TODO remove from here
        var id = httpContext.Session.GetString("playerId");

        if (id is null) {
            player = null;
            return false;
        }

        return Players.TryGetValue(new Guid(id), out player);
    }
}