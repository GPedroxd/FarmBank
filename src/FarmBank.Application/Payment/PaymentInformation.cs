using System.Text.Json.Serialization;

namespace FarmBank.Application.Payment;

public class PaymentInformation
{
    [JsonPropertyName("date_approved")]
    public string Approved { get; set; }

    [JsonPropertyName("date_last_updated")]
    public string LastUpdated { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("payer")]
    public Payer Payer { get; set; }
}

public struct Payer
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}