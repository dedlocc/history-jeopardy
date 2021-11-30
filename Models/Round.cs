namespace HistoryJeopardy.Models;

public record Round(
    uint Number,
    string Name,
    List<Category> Categories
);