using Newtonsoft.Json.Linq;
using VkBot.VkApi.Interfaces;

namespace VkLib.VkApi.Methods.Messages;

public class InitializeLongPollServerMethod : IApiMethod
{
    private readonly VkApi _vkApi;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="InitializeLongPollServerMethod"/> class.
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    public InitializeLongPollServerMethod(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public async Task ExecuteAsync()
    {
        var parameters = new Dictionary<string, string>
        {
            { "group_id", _vkApi.GroupId ?? "" },
            { "access_token", _vkApi.AccessToken ?? "" },
            { "v", "5.131" }
        };

        var response = await _vkApi.CallMethodAsync("groups.getLongPollServer", parameters);
        var server = response["response"]?["server"]?.Value<string>();
        var key = response["response"]?["key"]?.Value<string>();
        var pts = response["response"]?["key"]?.Value<string>();
        var ts = response["response"]?["ts"]?.Value<string>();

        if (server == null || key == null || ts == null)
        {
            throw new System.Exception("Failed to get Long Poll server info.");
        }

        _vkApi.SetLongPollServerInfo(server, key, pts, ts);
    }
}