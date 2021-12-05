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


    public Player? Get(HttpContext httpContext)
    {
        // TODO remove from here
        var id = httpContext.Session.GetString("playerId");

        return id is null ? null : Players.GetValueOrDefault(new Guid(id));
    }

    public bool TryGet(HttpContext httpContext, [MaybeNullWhen(false)] out Player player)
    {
        player = Get(httpContext);
        return player is not null;
    }
}