using VkLib.VkApi.Interfaces;
using VkLib.VkApi.Methods.Messages;

namespace VkLib.VkApi.Commands;

public class StartCommand : ICommand
{
    private readonly VkApi _vkApi;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommand"/> class.
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    public StartCommand(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public string CommandText => "/start";

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
    public async void Execute(long? userId, string message)
    {
        var sendMessageMethod = new SendMessageMethod(_vkApi, userId!.Value, "Приветствую!");
        await sendMessageMethod.ExecuteAsync();
    }
}