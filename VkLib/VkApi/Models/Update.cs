using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VkLib.VkApi.Enums;

namespace VkLib.VkApi.Models;

public class Update
{
    [JsonProperty("type")]
    [JsonConverter(typeof(EventTypeConverter))]
    public EventType Type { get; set; }

    [JsonProperty("object")]
    public UpdateObject Object { get; set; }
}

public class UpdateObject
{
    [JsonProperty("message")]
    public Message Message { get; set; }
}