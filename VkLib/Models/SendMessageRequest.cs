namespace VkLib.Models;

public class SendMessageRequest
{
    public long PeerId { get; set; }
    public string Message { get; set; }
}