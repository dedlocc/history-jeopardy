using HistoryJeopardy.Services;
using StreamJsonRpc;

namespace HistoryJeopardy.Socket;

public class Server
{
    private readonly GameService _gameService;

    public Server(GameService gameService)
    {
        _gameService = gameService;
    }

    [JsonRpcMethod("hello")]
    public string SayHello(string name)
    {
        return "Hello, " + name;
    }
}