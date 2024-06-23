using Newtonsoft.Json;

namespace VkLib.VkApi.Models;

public class LongPollServerResponse
{
    [JsonProperty("ts")]
    public string Ts { get; set; }

    [JsonProperty("updates")]
    public Update[] Updates { get; set; }
}