using Newtonsoft.Json.Linq;
using VkLib.VkApi.Commands;
using VkLib.VkApi.Interfaces;
using VkLib.VkApi.Utils;

namespace VkLib.VkApi;

public class VkApi
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "https://api.vk.com/method";
    public string AccessToken { get; private set; }
    public string GroupId { get; private set; }
    private string _server;
    private string _key;
    private string? _pts;
    private string? _ts;

    private readonly Dictionary<string, Action<long?, string>> _commands;
    private CommandHandler _commandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="VkApi"/> class.
    /// </summary>
    public VkApi()
    {
        _httpClient = new HttpClient();
        _commands = new Dictionary<string, Action<long?, string>>();

        // Load configuration from .env file
        EnvLoader.Load();
        AccessToken = EnvLoader.Get("ACCESS_TOKEN");
        GroupId = EnvLoader.Get("GROUP_ID");
    }

    /// <summary>
    /// Sets the command handler.
    /// </summary>
    /// <param name="commandHandler">The command handler.</param>
    public void SetCommandHandler(CommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }
    
    /// <summary>
    /// Starts the listening.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task StartListeningAsync()
    {
        if (string.IsNullOrEmpty(_server) || string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_ts))
        {
            throw new InvalidOperationException("You must initialize the Long Poll server first.");
        }

        while (true)
        {
            var pollUrl = $"{_server}?act=a_check&key={_key}&pts={_pts}&ts={_ts}&wait=25";
            var response = await _httpClient.GetStringAsync(pollUrl);
            var jsonResponse = JObject.Parse(response);

            _pts = jsonResponse["pts"]?.Value<string>();
            _ts = jsonResponse["ts"]?.Value<string>();

            if (jsonResponse["updates"] is not JArray updates) continue;
            foreach (var update in updates)
            {
                HandleUpdate(update);
            }
        }
    }

    /// <summary>
    /// Handles the update.
    /// </summary>
    /// <param name="update">The update object.</param>
    private void HandleUpdate(JToken update)
    {
        var type = update["type"]?.Value<string>();

        if (type != "message_new") return;
        var message = update["object"]?["message"];
        var userId = message?["from_id"]?.Value<long>();
        var text = message?["text"]?.Value<string>();

        _commandHandler?.HandleCommand(userId, text!);
    }
    
    /// <summary>
    /// Calls the method.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The <see cref="JObject"/>.</returns>
    public async Task<JObject> CallMethodAsync(string method, Dictionary<string, string> parameters)
    {
        var queryString = $"{_apiBaseUrl}/{method}?";

        queryString = parameters.Aggregate(queryString,
            (current, param) => current + $"{param.Key}={Uri.EscapeDataString(param.Value)}&");

        var response = await _httpClient.GetStringAsync(queryString);
        var jsonResponse = JObject.Parse(response);

        if (jsonResponse["error"] != null)
        {
            throw new Exception($"Error: {jsonResponse["error"]?["error_msg"]}");
        }

        return jsonResponse;
    }

    /// <summary>
    /// Sets the long poll server info.
    /// </summary>
    /// <param name="server">The server.</param>
    /// <param name="key">The key.</param>
    /// <param name="pts">The pts.</param>
    /// <param name="ts">The ts.</param>
    public void SetLongPollServerInfo(string server, string key, string? pts, string? ts)
    {
        _server = server;
        _key = key;
        _pts = pts;
        _ts = ts;
    }
}