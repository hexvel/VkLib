using VkLib.Models;

namespace VkLib.Interfaces;

public interface ICommand
{
    string Name { get; }
    Task Execute(Message message);
}