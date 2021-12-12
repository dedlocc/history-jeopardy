using HistoryJeopardy.Util;
using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Pack(
    [JsonProperty("name")] string Name,
    [JsonProperty("rounds")] List<Round> Rounds
)
{
    public static Pack FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Pack>(json, new AnswerConverter())
            ?? throw new JsonSerializationException("Can't get Pack from Json");
    }

    public static Pack FromFile(string path)
    {
        return FromJson(File.ReadAllText(path));
    }
}