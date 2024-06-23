using VkLib.VkApi.Interfaces;

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
        var parameters = new Dictionary<string, string?>
        {
            { "group_id", _vkApi.GroupId },
            { "access_token", _vkApi.AccessToken },
            { "v", "5.131" }
        };

        var response = await _vkApi.CallMethodAsync("groups.getLongPollServer", parameters);
        var server = response["response"]?["server"]?.ToString();
        var key = response["response"]?["key"]?.ToString();
        var ts = response["response"]?["ts"]?.ToString();

        if (server == null || key == null || ts == null)
        {
            throw new Exception("Failed to initialize long poll server");
        }

        _vkApi.SetLongPollServerInfo(server, key, ts);
    }
}