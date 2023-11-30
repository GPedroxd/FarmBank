using System.Text.Json.Serialization;
using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.WppCommand;

public record WppCommand : ICommand<ResponseResult<bool>>
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("body")]
    public Body Body { get; set; }
}

public struct Body 
{
    [JsonPropertyName("key")]
    public Key SenderInfo { get; set; }

    [JsonPropertyName("pushName")]
    public string PushName { get; set; }

    [JsonPropertyName("message")]
    public Message Message { get; set; }
}
public struct Key
{
    [JsonPropertyName("remoteJid")]
    public string GroupId { get; set; }
    [JsonPropertyName("participant")]
    public string Participant { get; set; }
}
public struct Message
{
    [JsonPropertyName("conversation")]
    public string Text { get; set; }
}