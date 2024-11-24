namespace FarmBank.Integration.Communication;

public class WppConfigs
{
    public WppConfigs(string groupId, string instanceKey, string frontendUrl, string userName, string password)
    {
        GroupId = groupId;
        InstanceKey = instanceKey;
        FrontendUrl = frontendUrl;
        UserName = userName;
        Password = password;
    }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string GroupId { get; init; }
    public string InstanceKey { get; init; }
    public string FrontendUrl { get; init; }
}
