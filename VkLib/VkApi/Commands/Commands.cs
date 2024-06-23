using VkLib.VkApi.Attributes;
using VkLib.VkApi.Methods.Messages;
using VkLib.VkApi.Utils;

namespace VkLib.VkApi.Commands;

public class BotCommands
{
    private readonly VkApi _vkApi;

    public BotCommands(VkApi vkApi)
    {
        _vkApi = vkApi;
    }
    
    [Command("/test")]
    public async void TestCommand(long userId, string args)
    {
        var sendMessageMethod = new SendMessageMethod(_vkApi, userId, $"Привет, {userId}!");
        await sendMessageMethod.ExecuteAsync();
    }
}