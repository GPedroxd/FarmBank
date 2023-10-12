using System.Text.Json.Serialization;

namespace FarmBank.Integration.ResponseModel;

public struct QRCodeResponseModel
{
    [JsonPropertyName("id")]
    public long TransactionId { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("date_of_expiration")]
    public DateTime ExpirationDate { get; set; }
    [JsonPropertyName("point_of_interaction")]
    public PointOfInteractionResponseModel PointOfInteraction { get; set; }
}

public struct PointOfInteractionResponseModel {
    [JsonPropertyName("transaction_data")]
    public TransactionDataResponseModel TransactionData { get; set; }
}

public struct TransactionDataResponseModel
{
    [JsonPropertyName("qr_code")]
    public string QRCodeCopyPaste { get; set; }
    [JsonPropertyName("qr_code_base64")]
    public string QRCodeBase64 { get; set; }

}