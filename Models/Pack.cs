using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Pack(
    [JsonProperty("name")]
    string Name,
    [JsonProperty("rounds")]
    List<Round> Rounds
)
{
    public static Pack FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Pack>(json) ?? throw new JsonException("json huina");
    }
}