namespace FarmBank.Application.Dto;

public class GeneralConfigs
{
    public GeneralConfigs(string groupId, string instanceKey, string frontendUrl)
    {
        GroupId = groupId;
        InstanceKey = instanceKey;
        FrontendUrl = frontendUrl;
    }

    public string GroupId { get; init; }
    public string InstanceKey { get; init; }
    public string FrontendUrl { get; init; }
}
