namespace VkLib.Interfaces;

public interface IMessageActionService
{
    Task SendMessage(long peerId, string message);
    Task EditMessage(long peerId, long messageId, string message);
}