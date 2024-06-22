using VkLib.VkApi.Interfaces;

namespace VkLib.VkApi.Commands;

public class CommandHandler
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandHandler()
    {
        _commands = new Dictionary<string, ICommand>();
    }

    public void RegisterCommand(ICommand command)
    {
        if (!_commands.TryAdd(command.CommandText, command))
        {
            Console.WriteLine($"Command {command.CommandText} is already registered.");
        }
    }

    public void HandleCommand(long? userId, string message)
    {
        foreach (var command in _commands.Values.Where(command => message.StartsWith(command.CommandText)))
        {
            command.Execute(userId, message);
            return;
        }

        Console.WriteLine($"No matching command found for message: {message}");
    }
}