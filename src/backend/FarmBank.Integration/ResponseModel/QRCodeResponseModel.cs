using System.Text.Json.Serialization;

namespace FarmBank.Integration.ResponseModel;

public struct QRCodeResponseModel
{
    [JsonPropertyName("id")]
    public string TransactionId { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("date_of_expiration")]
    public DateTime ExpirationDate { get; set; }
    public QRCodeDataResponseModel Data { get; set; }
}
