namespace HistoryJeopardy.Models;

public class Player
{
    public readonly Guid Id = Guid.NewGuid();
    public readonly string Name;
    public int Points { get; private set; } = 0;
    public readonly List<Connection> Connections = new();
    public Game? Game;

    public Player(string name)
    {
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