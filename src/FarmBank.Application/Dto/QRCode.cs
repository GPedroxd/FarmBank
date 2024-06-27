namespace FarmBank.Application.Dto;

public struct QRCode
{
    public string PixCopyPaste { get; init; }
    public string PixBase64 { get; init; }
    public DateTime ExpirationDate { get; init; }
    public decimal Amount { get; init; }
}
