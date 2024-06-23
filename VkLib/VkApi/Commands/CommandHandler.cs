namespace VkLib.VkApi.Commands;

public class CommandHandler
{
    private readonly CommandRegistry _commandRegistry;

    public CommandHandler(CommandRegistry commandRegistry)
    {
        _commandRegistry = commandRegistry;
    }

    public void HandleCommand(long? userId, string text)
    {
        if (userId == null || string.IsNullOrEmpty(text))
            return;

        var parts = text.Split(' ', 2);
        var command = parts[0].ToLower();
        var args = parts.Length > 1 ? parts[1] : string.Empty;
        
        _commandRegistry.ExecuteCommand(command, userId.Value, args);
    }
}