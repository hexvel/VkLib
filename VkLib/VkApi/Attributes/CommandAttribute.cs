namespace VkLib.VkApi.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class CommandAttribute : Attribute
{
    public string Name { get; }
    public CommandAttribute(string name) => Name = name;
}