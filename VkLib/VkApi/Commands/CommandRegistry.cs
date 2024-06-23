using System.Reflection;
using VkLib.VkApi.Attributes;

namespace VkLib.VkApi.Commands;

public class CommandRegistry
{
    private readonly Dictionary<string, Action<long, string>> _commands = new();

    public void RegisterCommands(object target)
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
            .ToArray();

        foreach (var method in methods)
        {
            var attribute = (CommandAttribute)method.GetCustomAttributes(typeof(CommandAttribute), false).First();
            var commandName = attribute.Name;

            if (method.ReturnType != typeof(void) || method.GetParameters().Length != 2 ||
                method.GetParameters()[0].ParameterType != typeof(long) ||
                method.GetParameters()[1].ParameterType != typeof(string))
            {
                throw new InvalidOperationException(
                    "Command methods must have the signature 'void MethodName(long userId, string args)'");
            }

            var action = (Action<long, string>)Delegate.CreateDelegate(typeof(Action<long, string>), target, method);
            _commands[commandName] = action;
        }
    }

    public bool ExecuteCommand(string commandName, long userId, string args)
    {
        if (!_commands.TryGetValue(commandName, out var action)) return false;
        action(userId, args);
        return true;
    }
}