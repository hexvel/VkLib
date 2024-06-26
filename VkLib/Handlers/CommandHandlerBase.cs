using VkLib.Models;

namespace VkLib.Handlers;

public abstract class CommandHandlerBase : ICommandHandler
{
    protected readonly VkApiClient Api;
    private ICommandHandler _commandHandlerImplementation;

    protected CommandHandlerBase(VkApiClient api)
    {
        Api = api;
    }

    public abstract Task HandleAsync(Message messageEvent);
}