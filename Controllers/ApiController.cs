using HistoryJeopardy.Services;
using Microsoft.AspNetCore.Mvc;

namespace HistoryJeopardy.Controllers;

public class ApiController : Controller
{
    private readonly PlayerService _playerService;

    public ApiController(PlayerService playerService)
    {
        _playerService = playerService;
    }
    
    [HttpPost]
    public IActionResult Auth([FromForm] string name)
    {
        var player = _playerService.Register(name);
        HttpContext.Session.SetString("playerId", player.Id.ToString());
        return Ok();
    }
}