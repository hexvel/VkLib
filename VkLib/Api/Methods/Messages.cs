using Newtonsoft.Json;
using VkLib.Models;

namespace VkLib.Api.Methods;

public class Messages
{
    private readonly VkApiClient _apiClient;

    public Messages(VkApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<string> SendMessage(SendMessageRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            { "peer_id", request.PeerId.ToString() },
            { "random_id", new Random().Next().ToString() },
            { "message", request.Message }
        };

        var a = await _apiClient.CallMethodAsync("messages.send", parameters);
        Console.WriteLine(a);
        return a;
    }

    public async Task<string> EditMessage(EditMessageRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            { "peer_id", request.PeerId.ToString() },
            { "message_id", request.MessageId.ToString() },
            { "message", request.Message }
        };

        return await _apiClient.CallMethodAsync("messages.edit", parameters);
    }

    public async Task<Message> GetById(long messageId)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                { "message_ids", messageId.ToString() }
            };

            var response = await _apiClient.CallMethodAsync("messages.getById", parameters);
            var responseWrapper = JsonConvert.DeserializeObject<ResponseWrapper<MessageResponse>>(response);

            if (responseWrapper?.Response?.Items?.Count > 0)
            {
                return responseWrapper.Response.Items[0];
            }
            else
            {
                Console.WriteLine($"Message with ID {messageId} not found or empty response.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving message by ID {messageId}: {ex.Message}");
            return null;
        }
    }

    public async Task<LongPollServer> GetLongPollServer()
    {
        var parameters = new Dictionary<string, string>();

        var response = await _apiClient.CallMethodAsync("messages.getLongPollServer", parameters);
        var responseWrapper = JsonConvert.DeserializeObject<ResponseWrapper<LongPollServer>>(response);

        return responseWrapper.Response;
    }
}