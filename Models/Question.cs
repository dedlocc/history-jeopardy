using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Question(
    [JsonProperty("price")] uint Price,
    [JsonProperty("question")] string Text,
    [JsonProperty("answer")] IAnswer Answer
);