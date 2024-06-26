using Newtonsoft.Json;

namespace VkLib.Models;

public class LongPollServer
{
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("server")]
    public string Server { get; set; }

    [JsonProperty("ts")]
    public string Ts { get; set; }
}
