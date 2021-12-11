using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Round(
    [JsonProperty("title")] string Title,
    [JsonProperty("categories")] List<Category> Categories
)
{
    public List<Question> Questions => Categories.SelectMany(c => c.Questions).ToList();
};