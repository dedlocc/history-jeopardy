using HistoryJeopardy.Services;
using StreamJsonRpc;

namespace HistoryJeopardy.Socket;

public class Server
{
    private readonly Guid _guid;
    private readonly GameService _gameService;

    public Server(Guid guid, GameService gameService)
    {
        _guid = guid;
        _gameService = gameService;
    }

    [JsonRpcMethod("hello")]
    public string SayHello(string name)
    {
        return "Hello, " + name;
    }
}