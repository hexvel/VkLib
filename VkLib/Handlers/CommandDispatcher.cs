using VkLib.Models;

namespace VkLib.Handlers;

public class CommandDispatcher
{
    private readonly Dictionary<string, Func<Message, Task>> _commandHandlers;

    public CommandDispatcher()
    {
        _commandHandlers = new Dictionary<string, Func<Message, Task>>();
    }

    public void RegisterCommand(string commandName, Func<Message, Task> handler)
    {
        _commandHandlers[commandName.ToLower()] = handler;
    }

    public async Task<bool> TryExecuteCommand(string? commandName, Message message)
    {
        if (!_commandHandlers.TryGetValue(commandName.ToLower(), out var handler))
        {
            return false;
        }

        await handler(message);
        return true;
    }
}