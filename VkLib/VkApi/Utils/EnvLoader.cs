using DotNetEnv;

namespace VkLib.VkApi.Utils;

public static class EnvLoader
{
    static EnvLoader()
    {
        Load();
    }

    public static void Load()
    {
        Env.Load();
    }

    public static string Get(string key)
    {
        return Env.GetString(key);
    }
}
