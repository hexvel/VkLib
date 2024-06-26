using VkLib.Enums;

namespace VkLib.Models;

public class MessageNewEvent : Event
{
    public EventType Type { get; set; }
    public Message Message { get; set; }
}