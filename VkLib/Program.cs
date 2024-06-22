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

            // Регистрация команд
            var commandHandler = new CommandHandler();
            commandHandler.RegisterCommand(new StartCommand(vk));
            commandHandler.RegisterCommand(new HelpCommand(vk));

            // Передача commandHandler в VkApi для обработки команд
            vk.SetCommandHandler(commandHandler);

            // Запуск прослушивания событий
            await vk.StartListeningAsync();

            // Бесконечное ожидание, чтобы приложение не завершилось сразу
            await Task.Delay(-1);
        }
    }
}