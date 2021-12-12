using HistoryJeopardy.Models;
using HistoryJeopardy.Services;
using Microsoft.AspNetCore.Mvc;

namespace HistoryJeopardy.Controllers;

public class HomeController : BaseController
{
    public HomeController(PlayerService playerService, GameService gameService) : base(playerService, gameService)
    {}

    public IActionResult Index()
    {
        if (!PlayerService.TryGet(HttpContext, out var player)) {
            player = PlayerService.Register("");
            HttpContext.Session.SetString("playerId", player.Id.ToString());

            GameService.Create(player, new GameOptions {
                Pack = Pack.FromFile("Pack.json"),
            });
        }

        return View("Play", player);
    }

    /*private Player Auth(string name)
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

        // TODO forbid joining a game while in another
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
        // TODO [no rush] add support for joining directly via link
        if (
            !PlayerService.TryGet(HttpContext, out var player) ||
            !GameService.InviteCodes.TryGetValue(invite, out var game)
        ) {
            return Forbid();
        }

        return View();
    }*/
}