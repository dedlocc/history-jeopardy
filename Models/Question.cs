using HistoryJeopardy.Models.Answers;
using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Question(
    [JsonProperty("price")] int Price,
    [JsonProperty("question")] string Text,
    [JsonProperty("answer")] Answer Answer
);