using VkLib.VkApi.Commands;
using VkLib.VkApi.Methods.Messages;

namespace VkLib.VkApi.Utils;

public static class BotRunner
{
    public static async Task RunAsync()
    {
        var vk = new VkApi();

        var initializeLongPollServerMethod = new InitializeLongPollServerMethod(vk);
        await initializeLongPollServerMethod.ExecuteAsync();

        var commandFactory = new CommandFactory();
        commandFactory.RegisterCommand("/start", () => new StartCommand(vk));
        commandFactory.RegisterCommand("/help", () => new HelpCommand(vk));

        var commandHandler = new CommandHandler(commandFactory);
        vk.SetCommandHandler(commandHandler);
        await vk.StartListeningAsync();
    }

    public static void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }
}