namespace VkLib.VkApi.Interfaces;

public interface ICommand
{
    string CommandText { get; }
    void Execute(long? userId, string message);
}