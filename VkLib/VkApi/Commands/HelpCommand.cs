using VkLib.VkApi.Methods.Messages;
using VkLib.VkApi.Utils;

namespace VkLib.VkApi.Commands;

public class HelpCommand : CommandBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HelpCommand"/> class.
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    public HelpCommand(VkApi vkApi) : base(vkApi)
    {
    }

    public override string CommandText => "/help";

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
    public override async void Execute(long? userId, string message)
    {
        var sendMessageMethod = new SendMessageMethod(VkApi, userId!.Value, "/start, /help");
        await sendMessageMethod.ExecuteAsync();
    }
}