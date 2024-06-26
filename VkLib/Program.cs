using DotNetEnv;

namespace VkLib;

class Program
{
    private static async Task Main(string[] args)
    {
        Env.Load();
        var accessToken = Env.GetString("ACCESS_TOKEN");

        var apiClient = new VkApiClient(accessToken);
        var bot = new VkBot(apiClient);

        await bot.StartBot();
    }
}