using System.Net.WebSockets;
using StreamJsonRpc;

namespace HistoryJeopardy.Models;

public record Connection(
    WebSocket WebSocket,
    JsonRpc JsonRpc,
    HttpContext HttpContext
);