namespace ManagementPortal.Contracts;

public record UserView(string Id, string DisplayName, string Role, string TenantName)
{
    public bool IsDeleting { get; set; }
}