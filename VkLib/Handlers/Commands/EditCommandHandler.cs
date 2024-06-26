using VkLib.Models;

namespace VkLib.Handlers.Commands;

public class EditCommandHandler : CommandHandlerBase
{
    public EditCommandHandler(VkApiClient api) : base(api)
    {
    }

    public override async Task HandleAsync(Message messageEvent)
    {
        await Api.Messages.EditMessage(new EditMessageRequest
        {
            PeerId = messageEvent.PeerId,
            MessageId = messageEvent.Id,
            Message = "Edited message!"
        });
    }
}