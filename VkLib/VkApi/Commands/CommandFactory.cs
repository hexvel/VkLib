using VkLib.VkApi.Interfaces;

namespace VkLib.VkApi.Commands;

public class CommandFactory
{
    private readonly Dictionary<string, Func<ICommand>> _commands;

    public CommandFactory()
    {
        _commands = new Dictionary<string, Func<ICommand>>();
    }

    public void RegisterCommand(string commandText, Func<ICommand> commandCreator)
    {
        if (!_commands.ContainsKey(commandText))
        {
            _commands.Add(commandText, commandCreator);
        }
        else
        {
            Console.WriteLine($"Command {commandText} is already registered.");
        }
    }

    public ICommand CreateCommand(string commandText)
    {
        if (_commands.TryGetValue(commandText, out var commandCreator))
        {
            return commandCreator();
        }

        throw new InvalidOperationException($"Command {commandText} is not registered.");
    }

    public bool IsCommandRegistered(string commandText)
    {
        return _commands.ContainsKey(commandText);
    }
}