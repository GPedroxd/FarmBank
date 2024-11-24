using System.Text.Json.Serialization;

namespace FarmBank.Integration.Communication;

public struct SentMessageResponseModel
{
    public object Code { get; set; }
    public string Message { get; set; }
    public Result Result { get; set; }
}

public struct Result
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; set; }
    public string Status { get; set; }
}
