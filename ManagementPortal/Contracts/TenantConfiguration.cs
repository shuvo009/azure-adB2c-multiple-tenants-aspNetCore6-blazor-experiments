namespace ManagementPortal.Contracts;

public class TenantConfiguration
{
    public string TenantId { get; set; } = null!;
    public string AppId { get; set; } = null!;
    public string B2CExtensionAppClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string DomainName { get; set; } = null!;
}