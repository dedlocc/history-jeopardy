using HistoryJeopardy.Services;
using Microsoft.AspNetCore.Mvc;

namespace HistoryJeopardy.Controllers;

public class ApiController : BaseController
{
    public ApiController(PlayerService playerService, GameService gameService) : base(playerService, gameService)
    {}

    [HttpPost("/question")]
    public IActionResult Question([FromForm] string questionId)
    {
        if (!PlayerService.TryGet(HttpContext, out var player)) {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        int rId, qId;

        try {
            (rId, qId) = questionId.Split('-', 2).Select(int.Parse).ToList() switch {
                var ids => (ids[0], ids[1])
            };
        } catch (SystemException e) when (e is FormatException or OverflowException or ArgumentNullException) {
            return BadRequest();
        }

        var game = player.Game;
        if (game is null) {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var question = game.CurrentRound.Categories.ElementAtOrDefault(rId)?.Questions.ElementAtOrDefault(qId);

        if (question is null || game.CompletedQuestions.Contains(question)) {
            return BadRequest();
        }

        game.CurrentQuestion = question;

        return Content(game.CurrentQuestion.Text);
    }

    [HttpPost("/answer")]
    public IActionResult Answer([FromForm] string answer)
    {
        if (!PlayerService.TryGet(HttpContext, out var player)) {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var game = player.Game;
        var question = game?.CurrentQuestion;
        if (game is null || !game.TryGetAnswer(out var correctAnswer)) {
            return BadRequest();
        }

        var isCorrect = correctAnswer.Match(answer);
        player.Points += isCorrect ? question!.Price : -question!.Price;

        return Json(new {
            IsCorrect = isCorrect,
            CorrectAnswer = correctAnswer.Get(),
        });
    }
}