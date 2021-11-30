using HistoryJeopardy.Models;
using HistoryJeopardy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HistoryJeopardy.Controllers;

public class HomeController : BaseController
{
    public HomeController(PlayerService playerService, GameService gameService) : base(playerService, gameService)
    {}
    
    public IActionResult Index()
    {
        return View();
    }

    private Player Auth(string name)
    {
        var player = PlayerService.Register(name);
        HttpContext.Session.SetString("playerId", player.Id.ToString());
        return player;
    }

    [HttpGet("/create")]
    public IActionResult Create([BindRequired] string name)
    {
        // TODO validate name
        if (!ModelState.IsValid) {
            return ValidationProblem();
        }
        
        var inviteCode = GameService.Create(Auth(name)).InviteCode;
        
        if (GameService.InviteCodes.TryGetValue(inviteCode, out _)) {
            return Content(inviteCode);
        }

        return NotFound();
    }

    [HttpGet("/join")]
    public IActionResult Join([BindRequired] string name, [BindRequired] string inviteCode)
    {
        // TODO validate name
        if (!ModelState.IsValid) {
            return ValidationProblem();
        }
        
        var player = Auth(name);
        
        // TODO forbid adding the same player twice
        if (GameService.InviteCodes.TryGetValue(inviteCode, out var game)) {
            game.Players.Add(player);
            return Content(inviteCode);
        }

        return NotFound();
    }

    [HttpGet("/room/{invite}")]
    public IActionResult Play(string invite)
    {
        if (!PlayerService.TryGet(HttpContext, out var player)) {
            return Forbid();
        }
        
        return View();
    }
}