using VkLib.Interfaces;
using VkLib.Models;

namespace VkLib.Handlers;

public class CommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandDispatcher()
    {
        _commands = new Dictionary<string, ICommand>();
    }

    public void RegisterCommand(ICommand command)
    {
        _commands[command.Name.ToLower()] = command;
    }

    public async Task<bool> TryExecuteCommand(string commandName, Message message)
    {
        if (!_commands.TryGetValue(commandName.ToLower(), out var command)) return false;
        
        await command.Execute(message);
        return true;

    }
}