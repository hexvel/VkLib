using VkLib.VkApi.Methods.Messages;
using VkLib.VkApi.Utils;

namespace VkLib.VkApi.Commands;

public class StartCommand : CommandBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommand"/> class.
    /// </summary>
    /// <param name="vkApi">The vk api.</param>
    public StartCommand(VkApi vkApi) : base(vkApi)
    {
    }

    public override string CommandText => "/start";

    /// <summary>
    /// Executes the specified user identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
    public override async void Execute(long? userId, string message)
    {
        var sendMessageMethod = new SendMessageMethod(VkApi, userId!.Value, "Приветствую!");
        await sendMessageMethod.ExecuteAsync();
    }
}