namespace HistoryJeopardy.Models;

public class Player
{
    public readonly Guid Id;
    public readonly string Name;
    public readonly List<Connection> Connections = new();
    public Game? Game;

    public Player(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Connect(Connection connection)
    {
        Connections.Add(connection);
    }

    public void Disconnect(Connection connection)
    {
        Connections.Remove(connection);
    }
}