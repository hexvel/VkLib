namespace VkLib.VkApi.Interfaces;

/// <summary>
/// Interface ICommand
/// </summary>
public interface ICommand
{
    string CommandText { get; }
    void Execute(long? userId, string message);
}