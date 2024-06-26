using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using VkLib;
using VkLib.Enums;
using VkLib.Models;
using VkLib.Handlers;


public class VkBot
{
    private readonly VkApiClient _apiClient;
    private readonly CommandDispatcher _commandDispatcher;

    public VkBot(VkApiClient apiClient)
    {
        _apiClient = apiClient;
        _commandDispatcher = new CommandDispatcher();

        RegisterCommands();
    }

    private void RegisterCommands()
    {
        _commandDispatcher.RegisterCommand("/test", async message =>
        {
            await _apiClient.Messages.SendMessage(new SendMessageRequest
            {
                UserId = message.PeerId,
                Message = "Прив :)"
            });
        });
    }

    public async Task StartBot()
    {
        Console.WriteLine("Bot started.");
        await ListenEvents();
    }

    private async Task ListenEvents()
    {
        while (true)
            try
            {
                var longPollServer = await _apiClient.Messages.GetLongPollServer();
                var updates = await GetUpdates(longPollServer);

                foreach (var update in updates)
                    switch (update.Type)
                    {
                        case EventType.MessageNew:
                            var messageNewEvent = (MessageNewEvent)update;
                            await HandleMessageEvent(messageNewEvent);
                            break;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while listening events: {ex.Message}");
            }
        // ReSharper disable once FunctionNeverReturns
    }

    private List<Event> ProcessLongPollResponse(string longPollResponse)
    {
        var events = new List<Event>();

        try
        {
            var responseJson = JObject.Parse(longPollResponse);
            var updates = responseJson["updates"];

            Debug.Assert(updates != null, nameof(updates) + " != null");

            foreach (var update in updates)
            {
                var eventType = update[0]?.ToObject<int>();

                switch (eventType)
                {
                    case 4:
                        var messageId = update[1]!.ToObject<long>();
                        var message = _apiClient.Messages.GetById(messageId).Result;

                        events.Add(new MessageNewEvent
                        {
                            Type = EventType.MessageNew,
                            Message = message
                        });
                        break;
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing Long Poll response: {ex.Message}");
        }

        return events;
    }

    private async Task<List<Event>> GetUpdates(LongPollServer longPollServer)
    {
        var url =
            $"https://{longPollServer.Server}?act=a_check&key={longPollServer.Key}&ts={longPollServer.Ts}&wait=25";
        var response = await new HttpClient().GetStringAsync(url);

        return ProcessLongPollResponse(response);
    }

    private async Task HandleMessageEvent(MessageNewEvent messageNewEvent)
    {
        var message = messageNewEvent.Message;
        if (IsCommand(message.Text, out var commandName))
            await _commandDispatcher.TryExecuteCommand(commandName, message);
    }

    private static bool IsCommand(string text, out string? commandName)
    {
        commandName = null;

        if (string.IsNullOrWhiteSpace(text))
            return false;

        if (!text.StartsWith("/")) return false;

        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        commandName = parts[0].ToLower();

        return true;
    }
}