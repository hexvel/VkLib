using System.Threading.Tasks;
using VkLib.Interfaces;
using VkLib.Models;

namespace VkLib.Handlers.Commands;

public class StartCommand : ICommand
{
    private readonly IMessageActionService _messageAction;

    public StartCommand(IMessageActionService messageAction)
    {
        _messageAction = messageAction;
    }

    public string Name => "/start";

    public async Task Execute(Message message)
    {
        await _messageAction.SendMessage(message.PeerId, "Каво?");
    }
}