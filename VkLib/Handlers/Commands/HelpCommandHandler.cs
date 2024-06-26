using VkLib.Interfaces;
using VkLib.Models;

namespace VkLib.Handlers.Commands;

public class HelpCommand : ICommand
{
    private readonly IMessageActionService _messageAction;

    public HelpCommand(IMessageActionService messageAction)
    {
        _messageAction = messageAction;
    }

    public string Name => "/help";

    public async Task Execute(Message message)
    {
        await _messageAction.EditMessage(message.PeerId, message.Id,
            "*(Типа список команд)*");
    }
}