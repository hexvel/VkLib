using VkLib.VkApi.Commands;
using VkLib.VkApi.Methods.Messages;

namespace VkLib;

class Program
{
    private static async Task Main(string[] args)
    {
        var vkApi = new VkApi.VkApi();
        
        var initializeLongPollServerMethod = new InitializeLongPollServerMethod(vkApi);
        await initializeLongPollServerMethod.ExecuteAsync();
        
        var commandRegistry = new CommandRegistry();
        var botCommands = new BotCommands(vkApi);

        commandRegistry.RegisterCommands(botCommands);
        vkApi.SetCommandHandler(new CommandHandler(commandRegistry));

        await vkApi.StartListeningAsync();
    }
}