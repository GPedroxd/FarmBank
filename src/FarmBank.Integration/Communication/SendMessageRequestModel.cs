using System.Text.Json.Serialization;

namespace FarmBank.Integration.Communication;

public struct SendMessageRequestModel
{
    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("reply_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
     public string? ReplyTo {  get; set; }
}
