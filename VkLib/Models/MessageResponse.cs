using Newtonsoft.Json;

namespace VkLib.Models;

public class MessageResponse
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("items")]
    public List<Message> Items { get; set; }
}