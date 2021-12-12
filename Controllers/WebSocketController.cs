using HistoryJeopardy.Models;
using HistoryJeopardy.Services;
using HistoryJeopardy.Socket;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using StreamJsonRpc;

namespace HistoryJeopardy.Controllers;

public class WebSocketController : BaseController
{
    public WebSocketController(PlayerService playerService, GameService gameService) : base(playerService, gameService)
    {}

    [HttpGet("/ws")]
    public async Task ConnectAsync()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest) {
            if (!PlayerService.TryGet(HttpContext, out var player)) {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            var wsHandler = new WebSocketMessageHandler(webSocket);
            ((JsonMessageFormatter) wsHandler.Formatter).JsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            using var jsonRpc = new JsonRpc(wsHandler, new Server(GameService));
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