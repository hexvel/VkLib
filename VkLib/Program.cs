namespace VkLib
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var vk = new VkApi();
            await vk.InitializeLongPollServerAsync();

            async void StartAction(long? userId, string message)
            {
                Console.WriteLine($"Received /start command from {userId}");
                if (userId != null) await vk.SendMessageAsync(userId.Value, "Ну чо ты, мальчик, здарова");
            }

            async void HelpAction(long? userId, string message)
            {
                Console.WriteLine($"Received /help command from {userId}");
                if (userId != null)
                    await vk.SendMessageAsync(userId.Value, "Вот те доступные команды: /start, /help");
            }
            
            vk.RegisterCommand("/start", StartAction);
            vk.RegisterCommand("/help", HelpAction);

            Console.WriteLine("Starting to listen for events...");
            _ = vk.StartListeningAsync();

            var groupInfo = await vk.GetGroupInfoAsync("223349108");
            Console.WriteLine($"Group Name: {groupInfo["response"]?[0]?["name"]}");

            await Task.Delay(-1);
        }
    }
}