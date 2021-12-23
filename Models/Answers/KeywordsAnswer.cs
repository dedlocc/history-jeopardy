using Newtonsoft.Json;

namespace HistoryJeopardy.Models.Answers;

[JsonObject]
public class KeywordsAnswer : Answer
{
    [JsonProperty("data")] public List<string> Keywords = null!;

    public override bool Match(string answer)
    {
        return Keywords.Any(kw => kw.Contains(answer, StringComparison.InvariantCultureIgnoreCase));
    }
}