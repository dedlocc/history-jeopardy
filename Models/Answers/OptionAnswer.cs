using Newtonsoft.Json;

namespace HistoryJeopardy.Models.Answers;

[JsonObject]
public abstract class BaseOptionalAnswer : Answer
{
    [JsonProperty("data")] public Options Options = null!;
}

[JsonObject]
public class OptionAnswer : BaseOptionalAnswer
{

    public override bool Match(string answer)
    {
        return Options.Correct.First() == answer;
    }
}

[JsonObject]
public class MultiOptionAnswer : BaseOptionalAnswer
{
    public override bool Match(string answer)
    {
        return answer.Split('|').Select(s => s.Trim()).ToHashSet(StringComparer.InvariantCultureIgnoreCase).SetEquals(Options.Correct);
    }
}

[JsonObject]
public record Options(
    HashSet<string> Correct,
    HashSet<string> Incorrect
);