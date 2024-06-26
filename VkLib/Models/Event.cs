using VkLib.Enums;

namespace VkLib.Models;

public abstract class Event
{
    public EventType Type { get; set; }
}