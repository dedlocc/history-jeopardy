using HistoryJeopardy.Services;

namespace HistoryJeopardy.Controllers;

public class ApiController : BaseController
{
    public ApiController(PlayerService playerService, GameService gameService) : base(playerService, gameService)
    {}
}