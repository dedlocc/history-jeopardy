using Newtonsoft.Json;

namespace HistoryJeopardy.Models.Answers;

[JsonObject]
public class ExactAnswer : Answer
{
    [JsonProperty("data")] public List<string> Answers = null!;

    public override bool Match(string answer)
    {
        return Answers.Any(kw => kw.Equals(answer, StringComparison.InvariantCultureIgnoreCase));
    }
}