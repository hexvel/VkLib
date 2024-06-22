using VkLib.VkApi.Interfaces;

namespace VkLib.VkApi.Utils;

public abstract class CommandBase : ICommand
{
    protected readonly VkApi VkApi;
    public abstract string CommandText { get; }

    protected CommandBase(VkApi vkApi)
    {
        VkApi = vkApi;
    }

    public abstract void Execute(long? userId, string message);
}