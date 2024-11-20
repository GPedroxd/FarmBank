using FarmBank.Application.Base;
using System.Text.Json.Serialization;

namespace FarmBank.Application.WppCommands;

public record WppInputMessage : ICommand
{
    [JsonPropertyName("message")]
    public WppCommandMessage Message { get; init; } = default!;

    [JsonPropertyName("pushname")]
    public string SenderName { get; init; } = default!;

    [JsonPropertyName("sender")]
    public string SenderId { get; init; } = default!;
}

public record WppCommandMessage
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("text")]
    public string Content { get; init; } = default!;

    [JsonPropertyName("replied_id")]
    public string RepliedId { get; init; } = default!;
}

