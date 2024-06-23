using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VkLib.VkApi.Enums;

namespace VkLib.VkApi.Models;

public class EventTypeConverter : StringEnumConverter
{
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var enumText = reader.Value?.ToString();

            return Enum.TryParse(typeof(EventType), enumText?.Replace("_", ""), true, out var enumValue)
                ? enumValue
                : EventType.Unknown;
        }

        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enum.");
    }
}