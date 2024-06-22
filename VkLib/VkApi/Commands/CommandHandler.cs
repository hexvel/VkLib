namespace VkLib.VkApi.Commands;

public class CommandHandler
{
    private readonly CommandFactory _commandFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandler"/> class.
    /// </summary>
    public CommandHandler(CommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }
    
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="message">The message.</param>
    public void HandleCommand(long? userId, string message)
    {
        var commandText = GetCommandText(message);
        
        if (_commandFactory.IsCommandRegistered(commandText))
        {
            var command = _commandFactory.CreateCommand(commandText);
            command.Execute(userId, message);
        }
        else
        {
            Console.WriteLine($"No matching command found for message: {message}");
        }
    }
    
    /// <summary>
    /// Gets the command text.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    private static string GetCommandText(string message)
    {
        var commandText = message.Split(' ')[0];
        return commandText;
    }
}