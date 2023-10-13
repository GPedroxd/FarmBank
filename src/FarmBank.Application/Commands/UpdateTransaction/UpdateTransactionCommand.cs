using System.Text.Json.Serialization;
using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.UpdateTransaction;

public class UpdateTransactionCommand : ICommand
{
    [JsonPropertyName("action")]
    public string Action { get; set; }
    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; }
    [JsonPropertyName("data")]
    public Data Data{ get; set; }
    [JsonPropertyName("date_created")]
    public string CreatedAt { get; set; }
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
}

public class Data 
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
