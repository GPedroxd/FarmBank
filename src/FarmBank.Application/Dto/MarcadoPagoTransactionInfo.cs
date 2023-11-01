using System.Text.Json.Serialization;

namespace FarmBank.Application.Dto;

public struct MarcadoPagoTransactionInfo
{
    [JsonPropertyName("date_approved")]
    public DateTime Approved { get; set; }

    [JsonPropertyName("date_last_updated")]
    public DateTime LastUpdated { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("payer")]
    public Payer Payer { get; set; }
}

public struct Payer 
{   
    [JsonPropertyName("id")]
    public string Id { get; set;}

}
