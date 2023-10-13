using System.Text.Json.Serialization;

namespace FarmBank.Integration.RequestModel;

public struct NewPixRequestModel
{
    [JsonPropertyName("transaction_amount")]
    public decimal TransactionAmount { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("payment_method_id")]
    public string PaymentMethodId { get => "pix"; }
    [JsonPropertyName("payer")]
    public Payer Payer { get; set; }
    [JsonPropertyName("date_of_expiration")]
    public string ExpirationDate
    {
        get => DateTime.Now.AddHours(1).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
    }
}
public struct Payer
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
}
