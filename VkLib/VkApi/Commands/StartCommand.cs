using VkLib.VkApi.Interfaces;
using VkLib.VkApi.Methods.Messages;

namespace VkLib.VkApi.Commands;

public class StartCommand : ICommand
{
    private readonly VkApi _vkApi;

    public StartCommand(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public string CommandText => "/start";

    public async void Execute(long? userId, string message)
    {
        var sendMessageMethod = new SendMessageMethod(_vkApi, userId!.Value, "Приветствую!");
        await sendMessageMethod.ExecuteAsync();
    }
}