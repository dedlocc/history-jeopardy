using Newtonsoft.Json;

namespace HistoryJeopardy.Models;

[JsonObject]
public record Round(
    [JsonProperty("number")] uint Number,
    [JsonProperty("title")] string Title,
    [JsonProperty("categories")] List<Category> Categories
);