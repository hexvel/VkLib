using VkBot.VkApi.Interfaces;

namespace VkLib.VkApi.Methods.Messages;

public class SendMessageMethod : IApiMethod
{
    private readonly VkApi _vkApi;
    private readonly long _userId;
    private readonly string _message;

    public SendMessageMethod(VkApi vkApi, long userId, string message)
    {
        _vkApi = vkApi;
        _userId = userId;
        _message = message;
    }

    public async Task ExecuteAsync()
    {
        var parameters = new Dictionary<string, string>
        {
            { "user_id", _userId.ToString() },
            { "message", _message },
            { "random_id", new System.Random().Next().ToString() },
            { "access_token", _vkApi.AccessToken },
            { "v", "5.131" }
        };

        await _vkApi.CallMethodAsync("messages.send", parameters);
    }
}