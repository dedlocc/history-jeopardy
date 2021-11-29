using HistoryJeopardy.Models;
using HistoryJeopardy.Services;
using HistoryJeopardy.Socket;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using StreamJsonRpc;

namespace HistoryJeopardy.Controllers;

public class WebSocketController : ControllerBase
{
    private readonly PlayerService _playerService;
    private readonly GameService _gameService;

    public WebSocketController(PlayerService playerService, GameService gameService)
    {
        _playerService = playerService;
        _gameService = gameService;
    }

    [HttpGet("/ws")]
    public async Task ConnectAsync()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest) {
            var guid = Guid.NewGuid();
            var playerId = HttpContext.Session.GetString("playerId");

            if (playerId is null || !_playerService.Players.TryGetValue(new Guid(playerId), out var player)) {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            var wsHandler = new WebSocketMessageHandler(webSocket);
            ((JsonMessageFormatter) wsHandler.Formatter).JsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            using var jsonRpc = new JsonRpc(wsHandler, new Server(guid, _gameService));
            jsonRpc.StartListening();

            var connection = new Connection(webSocket, jsonRpc, HttpContext);
            player.Connect(connection);

            try {
                await jsonRpc.Completion;
            } finally {
                player.Disconnect(connection);
            }
        } else {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}