using HistoryJeopardy.Models.Answers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HistoryJeopardy.Util;

public class AnswerConverter : JsonConverter<Answer>
{
    public override void WriteJson(JsonWriter writer, Answer? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Answer ReadJson(JsonReader reader, Type objectType, Answer? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var type = obj.Property("type");

        if (type is null || type.Value.Type != JTokenType.String) {
            throw new JsonSerializationException("Answer type must be a string");
        }

        return (Answer?) (type.Value.ToObject<string>() switch {
            "exact" => obj.ToObject<ExactAnswer>(),
            "keywords" => obj.ToObject<KeywordsAnswer>(),
            "option" => obj.ToObject<OptionAnswer>(),
            "multiOption" => obj.ToObject<MultiOptionAnswer>(),
            _ => throw new JsonSerializationException("Unknown answer type"),
        }) ?? throw new JsonSerializationException("Unable to deserialize answer");
    }
}