using Newtonsoft.Json;

namespace HistoryJeopardy.Models.Answers;

[JsonObject]
public abstract class Answer
{
    [JsonProperty("full")] public readonly string Full = null!;

    public abstract bool Match(string answer);
}