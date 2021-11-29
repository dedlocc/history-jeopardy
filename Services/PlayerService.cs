using HistoryJeopardy.Models;

namespace HistoryJeopardy.Services;

public class PlayerService
{
    public readonly Dictionary<Guid, Player> Players = new();

    public Player Register(string name)
    {
        Player player = new(Guid.NewGuid(), name);
        Players.Add(player.Id, player);
        return player;
    }

    public Player? Get(Guid id)
    {
        return Players.GetValueOrDefault(id);
    }
}