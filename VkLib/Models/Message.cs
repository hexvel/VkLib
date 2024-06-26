using Newtonsoft.Json;

namespace VkLib.Models;

public class Message : Event
{
    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("from_id")]
    public long FromId { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("out")]
    public long Out { get; set; }

    [JsonProperty("version")]
    public long Version { get; set; }

    [JsonProperty("attachments")]
    public List<object> Attachments { get; set; }

    [JsonProperty("conversation_message_id")]
    public long ConversationMessageId { get; set; }

    [JsonProperty("fwd_messages")]
    public List<object> FwdMessages { get; set; }

    [JsonProperty("important")]
    public bool Important { get; set; }

    [JsonProperty("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonProperty("peer_id")]
    public long PeerId { get; set; }

    [JsonProperty("random_id")]
    public long RandomId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
