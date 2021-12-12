using Newtonsoft.Json;

namespace HistoryJeopardy.Models.Answers;

[JsonObject]
public class ExactAnswer : Answer
{
    [JsonProperty("data")] private string _answer = null!;

    public override bool Match(string answer)
    {
        return string.Equals(answer, _answer, StringComparison.InvariantCultureIgnoreCase);
    }
}