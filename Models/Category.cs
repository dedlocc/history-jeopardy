namespace HistoryJeopardy.Models;

public record Category(
    string Name,
    List<Question> Questions
);