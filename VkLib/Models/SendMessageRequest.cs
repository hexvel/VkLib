namespace VkLib.Models;

public class SendMessageRequest
{
    public long UserId { get; set; }
    public string Message { get; set; }
}