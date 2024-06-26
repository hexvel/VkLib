namespace VkLib.Models;

public class EditMessageRequest
{
    public long PeerId { get; set; }
    public long MessageId { get; set; }
    public string Message { get; set; }
}