using VkLib.Models;

namespace VkLib.Handlers;

public interface ICommandHandler
{
    Task HandleAsync(Message messageEvent);
}