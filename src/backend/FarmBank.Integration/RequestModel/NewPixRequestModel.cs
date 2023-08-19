using System.Text.Json.Serialization;

namespace FarmBank.Integration.RequestModel;

public struct NewPixRequestModel
{
    [JsonPropertyName("transaction_amount")]
    public double TransactionAmount { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("payment_method_id")]
    public string PaymentMethodId { get =>"pix"; }
    [JsonPropertyName("payer")]
    public Payer Payer { get; set; }
    [JsonPropertyName("date_of_expiration")]
    public DateTime ExpirationDate { get; set;}
}
public struct Payer 
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
}
