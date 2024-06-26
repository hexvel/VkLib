using Newtonsoft.Json;
using VkLib.Api.Methods;
using VkLib.Models;

namespace VkLib;

public class VkApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;
    private const string ApiVersion = "5.131";

    public VkApiClient(string accessToken)
    {
        _accessToken = accessToken;
        _httpClient = new HttpClient();
        Messages = new Messages(this);
    }

    public Messages Messages { get; }

    public async Task<string> CallMethodAsync(string methodName, Dictionary<string, string> parameters)
    {
        const string baseUrl = "https://api.vk.com/method/";
        var url = $"{baseUrl}{methodName}?access_token={_accessToken}&v={ApiVersion}&";
        
        url = parameters.Aggregate(url, (current, param) => current + $"{param.Key}={param.Value}&");

        var response = await _httpClient.GetStringAsync(url);
        return response;
    }
}