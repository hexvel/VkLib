using VkLib.VkApi.Interfaces;
using VkLib.VkApi.Methods.Messages;

namespace VkLib.VkApi.Commands;

public class HelpCommand : ICommand
{
    private readonly VkApi _vkApi;
    
    /// <summary>
    /// The help command
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    public HelpCommand(VkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public string CommandText => "/help";

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
    public async void Execute(long? userId, string message)
    {
        var sendMessageMethod =
            new SendMessageMethod(_vkApi, userId!.Value, "/start, /help");
        await sendMessageMethod.ExecuteAsync();
    }
}