using VkLib.Interfaces;
using VkLib.Models;

namespace VkLib.Services;

public class VkMessageActionService : IMessageActionService
{
    private readonly VkApiClient _vkApi;

    public VkMessageActionService(VkApiClient vkApi)
    {
        _vkApi = vkApi;
    }

    public async Task SendMessage(long peerId, string message)
    {
        await _vkApi.Messages.SendMessage(new SendMessageRequest
        {
            PeerId = peerId,
            Message = message
        });
    }

    public async Task EditMessage(long peerId, long messageId, string message)
    {
        await _vkApi.Messages.EditMessage(new EditMessageRequest
        {
            PeerId = peerId,
            MessageId = messageId,
            Message = message
        });
    }
}