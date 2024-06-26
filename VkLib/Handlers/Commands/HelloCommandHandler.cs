using VkLib.Models;

namespace VkLib.Handlers.Commands;

public class HelloCommandHandler : CommandHandlerBase
{
    public HelloCommandHandler(VkApiClient api) : base(api)
    {
    }

    public override async Task HandleAsync(Message messageEvent)
    {
        await Api.Messages.SendMessage(new SendMessageRequest
        {
            UserId = messageEvent.FromId,
            Message = "Hello! How can I help you today?"
        });
    }
}