using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Category(
    [JsonProperty("name")] string Name,
    [JsonProperty("questions")] List<Question> Questions
);