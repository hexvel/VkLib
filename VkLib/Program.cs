using VkLib.VkApi.Commands;
using VkLib.VkApi.Methods.Messages;

namespace VkLib
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var vk = new VkApi.VkApi();

            // Авторизация и инициализация Long Poll сервера
            var initializeLongPollServerMethod = new InitializeLongPollServerMethod(vk);
            await initializeLongPollServerMethod.ExecuteAsync();

            // Создание фабрики команд
            var commandFactory = new CommandFactory();
            commandFactory.RegisterCommand("/start", () => new StartCommand(vk));
            commandFactory.RegisterCommand("/help", () => new HelpCommand(vk));

            // Создание и передача CommandHandler
            var commandHandler = new CommandHandler(commandFactory);
            vk.SetCommandHandler(commandHandler);

            // Запуск прослушивания событий
            await vk.StartListeningAsync();

            // Бесконечное ожидание, чтобы приложение не завершилось сразу
            await Task.Delay(-1);
        }
    }
}