using Newtonsoft.Json;

namespace VkLib.Models;

public class ResponseWrapper<T>
{
    [JsonProperty("response")]
    public T Response { get; set; }
}