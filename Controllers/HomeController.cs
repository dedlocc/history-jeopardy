using System.Diagnostics;
using HistoryJeopardy.Services;
using HistoryJeopardy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HistoryJeopardy.Controllers;

public class HomeController : Controller
{
    private readonly PlayerService _playerService;

    public HomeController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    public IActionResult Index()
    {
        var playerId = HttpContext.Session.GetString("playerId");

        if (playerId is not null) {
            ViewData["player"] = _playerService.Get(new Guid(playerId));
        }

        return View();
    }

    // TODO remove when async page loading is complete
    [HttpGet("/table")]
    public IActionResult Table()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}