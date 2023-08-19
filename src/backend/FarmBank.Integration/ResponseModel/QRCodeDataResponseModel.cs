using System.Text.Json.Serialization;

namespace FarmBank.Integration.ResponseModel;

public struct QRCodeDataResponseModel
{
    [JsonPropertyName("qr_code")]
    public string QRCodeCopyPaste { get; set; }
    [JsonPropertyName("qr_code_base64")]
    public string QRCodeBase64 { get; set; }

}
