using Newtonsoft.Json;

namespace VkLib.VkApi.Utils;

public static class JsonSerializer
{
    public static T? Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}