using Newtonsoft.Json;

namespace VkLib.VkApi.Models;

public class Message
{
    [JsonProperty("from_id")]
    public long FromId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}