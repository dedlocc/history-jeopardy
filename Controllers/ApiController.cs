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

        return Json(new {
            Text = game.CurrentQuestion.Text,
            Picture = game.CurrentQuestion.PictureUri,
        });
    }

    [HttpPost("/answer")]
    public IActionResult Answer([FromForm] string? answer)
    {
        if (!PlayerService.TryGet(HttpContext, out var player)) {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var game = player.Game;

        if (game is null || !game.TryCompleteQuestion(out var question)) {
            return BadRequest();
        }

        var isCorrect = answer is not null && question.Answer.Match(answer.Trim());

        if (answer is not null) {
            player.Points += isCorrect ? question.Price : -question.Price;
        }

        return Json(new {
            IsCorrect = isCorrect,
            CorrectAnswer = question.Answer.Full,
            PlayerPoints = player.Points,
        });
    }
}