using HistoryJeopardy.Services;
using Microsoft.AspNetCore.Mvc;

namespace HistoryJeopardy.Controllers;

public abstract class BaseController : Controller
{
    protected readonly PlayerService PlayerService;
    protected readonly GameService GameService;

    protected BaseController(PlayerService playerService, GameService gameService)
    {
        PlayerService = playerService;
        GameService = gameService;
    }
}