namespace FarmBank.Application.Dto;

public record ExternalInfos
{
    public string WppBotInstanceKey { get; set; }
    public string WppApiUrl { get; set; }
    public string FrontEndUrl { get; set; }
}
