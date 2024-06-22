using VkLib.VkApi.Interfaces;
using VkLib.VkApi.Methods.Messages;

namespace VkLib.VkApi.Commands;

public class HelpCommand : ICommand
{
    private readonly VkApi _vkApi;

    public HelpCommand(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public string CommandText => "/help";

    public async void Execute(long? userId, string message)
    {
        var sendMessageMethod =
            new SendMessageMethod(_vkApi, userId!.Value, "/start, /help");
        await sendMessageMethod.ExecuteAsync();
    }
}