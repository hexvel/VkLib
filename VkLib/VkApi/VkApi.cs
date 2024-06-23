using DotNetEnv;
using Newtonsoft.Json.Linq;
using VkLib.VkApi.Commands;
using VkLib.VkApi.Enums;
using VkLib.VkApi.Models;
using VkLib.VkApi.Utils;

namespace VkLib.VkApi;

public class VkApi
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://api.vk.com/method";
    public string? AccessToken { get; private set; }
    public string GroupId { get; private set; }
    private string _server;
    private string _key;
    private string? _ts;

    private CommandHandler _commandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="VkApi"/> class.
    /// </summary>
    public VkApi()
    {
        _httpClient = new HttpClient();

        // Load configuration from .env file
        Env.Load();
        AccessToken = Env.GetString("ACCESS_TOKEN");
        GroupId = Env.GetString("GROUP_ID");
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
            var pollUrl = $"{_server}?act=a_check&key={_key}&ts={_ts}&wait=25";
            var response = await _httpClient.GetStringAsync(pollUrl);
            var jsonResponse = JsonSerializer.Deserialize<LongPollServerResponse>(response);

            _ts = jsonResponse?.Ts;

            foreach (var update in jsonResponse!.Updates)
            {
                HandleUpdate(update);
            }
        }
    }

    /// <summary>
    /// Handles the update.
    /// </summary>
    /// <param name="update">The update object.</param>
    private void HandleUpdate(Update update)
    {
        var type = update.Type;

        if (type == EventType.MessageNew)
        {
            var message = update.Object?.Message;
            var userId = message?.FromId;
            var text = message?.Text;
            
            _commandHandler?.HandleCommand(userId, text!);
        }
    }

    /// <summary>
    /// Calls the method.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The <see cref="JObject"/>.</returns>
    public async Task<JObject> CallMethodAsync(string method, Dictionary<string, string?> parameters)
    {
        var queryString = $"{ApiBaseUrl}/{method}?";
        queryString = parameters.Aggregate(queryString, (current, param) => current + $"{param.Key}={Uri.EscapeDataString(param.Value!)}&");

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
    /// <param name="ts">The ts.</param>
    public void SetLongPollServerInfo(string server, string key, string ts)
    {
        _server = server;
        _key = key;
        _ts = ts;
    }
}