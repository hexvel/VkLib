using VkLib.VkApi.Interfaces;

namespace VkLib.VkApi.Methods.Messages;

public class SendMessageMethod : IApiMethod
{
    private readonly VkApi _vkApi;
    private readonly long _userId;
    private readonly string? _message;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendMessageMethod"/> class.
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="message">The message.</param>
    public SendMessageMethod(VkApi vkApi, long userId, string? message)
    {
        _vkApi = vkApi;
        _userId = userId;
        _message = message;
    }

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public async Task ExecuteAsync()
    {
        var parameters = new Dictionary<string, string?>
        {
            { "user_id", _userId.ToString() },
            { "message", _message },
            { "random_id", new Random().Next().ToString() },
            { "access_token", _vkApi.AccessToken },
            { "v", "5.131" }
        };

        await _vkApi.CallMethodAsync("messages.send", parameters);
    }
}