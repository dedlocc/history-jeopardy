using HistoryJeopardy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HistoryJeopardy.Util;

public class AnswerConverter : JsonConverter<IAnswer>
{
    public override void WriteJson(JsonWriter writer, IAnswer? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override IAnswer ReadJson(JsonReader reader, Type objectType, IAnswer? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return new ExactAnswer(serializer.Deserialize<string>(reader) ?? throw new JsonSerializationException("No answer provided"));
    }
}