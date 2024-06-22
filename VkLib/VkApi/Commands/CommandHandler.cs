using VkLib.VkApi.Interfaces;

namespace VkLib.VkApi.Commands;

public class CommandHandler
{
    private readonly Dictionary<string, ICommand> _commands;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandler"/> class.
    /// </summary>
    public CommandHandler()
    {
        _commands = new Dictionary<string, ICommand>();
    }
    
    /// <summary>
    /// Registers a command.
    /// </summary>
    /// <param name="command">The command.</param>
    public void RegisterCommand(ICommand command)
    {
        if (!_commands.TryAdd(command.CommandText, command))
        {
            Console.WriteLine($"Command {command.CommandText} is already registered.");
        }
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
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