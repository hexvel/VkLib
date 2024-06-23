using Newtonsoft.Json;

namespace VkLib.VkApi.Models;

public class Update
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("object")]
    public UpdateObject Object { get; set; }
}


public class UpdateObject
{
    [JsonProperty("message")]
    public Message Message { get; set; }
}