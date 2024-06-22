using DotNetEnv;
using Newtonsoft.Json.Linq;

namespace VkLib;

public class VkApi
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://api.vk.com/method";
    private readonly string _accessToken;
    private readonly string _groupId;
    private string? _server;
    private string? _key;
    private string? _pts;
    private string? _ts;
    private readonly Dictionary<string, Action<long?, string>> _commands;

    public VkApi()
    {
        _httpClient = new HttpClient();
        _commands = new Dictionary<string, Action<long?, string>>();

        Env.Load(".env");
        _accessToken = Env.GetString("ACCESS_TOKEN");
        _groupId = Env.GetString("GROUP_ID");
    }

    public async Task InitializeLongPollServerAsync()
    {
        var parameters = new Dictionary<string, string>
        {
            { "group_id", _groupId },
            { "access_token", _accessToken },
            { "v", "5.131" }
        };

        var response = await CallMethodAsync("groups.getLongPollServer", parameters);
        _server = response["response"]?["server"]?.Value<string>();
        _key = response["response"]?["key"]?.Value<string>();
        _pts = response["response"]?["pts"]?.Value<string>();
        _ts = response["response"]?["ts"]?.Value<string>();

        if (_server == null || _key == null || _ts == null) throw new Exception("Failed to get Long Poll server info.");
    }

    public async Task StartListeningAsync()
    {
        if (string.IsNullOrEmpty(_server) || string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_ts))
            throw new InvalidOperationException("You must initialize the Long Poll server first.");

        while (true)
        {
            var pollUrl = $"{_server}?act=a_check&key={_key}&pts={_pts}&ts={_ts}&wait=25";
            var response = await _httpClient.GetStringAsync(pollUrl);
            var jsonResponse = JObject.Parse(response);

            _ts = jsonResponse["ts"]?.Value<string>();

            if (jsonResponse["updates"] is JArray updates)
                foreach (var update in updates)
                    HandleUpdate(update);
        }
    }

    private void HandleUpdate(JToken update)
    {
        var type = update["type"]?.Value<string>();

        if (type != "message_new") return;
        var message = update["object"]?["message"];
        var userId = message?["from_id"]?.Value<long>();
        var text = message?["text"]?.Value<string>();

        Console.WriteLine($"New message from {userId}: {text}");

        if (text != null) OnMessageNew(userId, text);
    }

    private void OnMessageNew(long? userId, string text)
    {
        foreach (var command in _commands.Where(command =>
                     text.StartsWith(command.Key)))
        {
            command.Value.Invoke(userId, text);
            return;
        }

        // Default handling if no command matched
        Console.WriteLine($"Handling new message from {userId}: {text}");
    }

    public void RegisterCommand(string command, Action<long?, string> action)
    {
        if (!_commands.TryAdd(command, action)) Console.WriteLine($"Command {command} is already registered.");
    }

    public async Task<JObject> CallMethodAsync(string method, Dictionary<string, string> parameters)
    {
        var queryString = $"{ApiBaseUrl}/{method}?";

        foreach (var param in parameters) queryString += $"{param.Key}={Uri.EscapeDataString(param.Value)}&";

        var response = await _httpClient.GetStringAsync(queryString);
        var jsonResponse = JObject.Parse(response);

        if (jsonResponse["error"] != null) throw new Exception($"Error: {jsonResponse["error"]?["error_msg"]}");

        return jsonResponse;
    }

    public async Task<JObject> GetGroupInfoAsync(string groupId)
    {
        var parameters = new Dictionary<string, string>
        {
            { "group_id", groupId },
            { "fields", "description,members_count" },
            { "access_token", _accessToken },
            { "v", "5.131" }
        };

        return await CallMethodAsync("groups.getById", parameters);
    }

    public async Task SendMessageAsync(long userId, string message)
    {
        var parameters = new Dictionary<string, string>
        {
            { "user_id", userId.ToString() },
            { "message", message },
            { "random_id", new Random().Next().ToString() },
            { "access_token", _accessToken },
            { "v", "5.131" }
        };

        await CallMethodAsync("messages.send", parameters);
    }
}