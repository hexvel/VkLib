using VkLib.VkApi.Commands;
using VkLib.VkApi.Methods.Messages;

namespace VkLib
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var vk = new VkApi.VkApi();

            var initializeLongPollServerMethod = new InitializeLongPollServerMethod(vk);
            await initializeLongPollServerMethod.ExecuteAsync();

            vk.RegisterCommand(new StartCommand(vk));
            vk.RegisterCommand(new HelpCommand(vk));

            await vk.StartListeningAsync();

            await Task.Delay(-1);
        }
    }
}