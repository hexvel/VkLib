using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using VkLib.Interfaces;
using VkLib.Services;

namespace VkLib;

class Program
{
    static async Task Main(string[] args)
    {
        Env.Load();
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var bot = serviceProvider.GetRequiredService<VkBot>();

        await bot.StartAsync();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<VkApiClient, VkApiClient>();

        // Регистрация HttpClient
        services.AddSingleton(new HttpClient());

        // Регистрация VkApiClient с передачей accessToken
        var accessToken = Env.GetString("ACCESS_TOKEN"); // Замените на ваш токен доступа
        services.AddSingleton(provider => new VkApiClient(accessToken, provider.GetRequiredService<HttpClient>()));

        // Регистрация IMessageSenderService
        services.AddSingleton<IMessageActionService, VkMessageActionService>();

        // Регистрация VkBot
        services.AddSingleton<VkBot>();
    }
}