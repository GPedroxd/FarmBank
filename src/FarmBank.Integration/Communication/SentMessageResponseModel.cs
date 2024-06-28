namespace FarmBank.Integration.Communication;

public struct SentMessageResponseModel
{
    public bool Error { get; set; }
    public object Data { get; set; }
}
