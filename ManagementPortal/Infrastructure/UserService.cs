using Azure.Identity;
using ManagementPortal.Contracts;
using Microsoft.Graph;

namespace ManagementPortal.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Create(UserProfile userProfile, string name)
        {
            var config = _configuration.GetSection(name).Get<TenantConfiguration>();
            var client = GetClient(name);
            var user = await client.Users.Request().AddAsync(new User
            {
                GivenName = userProfile.GivenName,
                Surname = userProfile.Surname,
                DisplayName = $"{userProfile.GivenName} {userProfile.Surname}",
                Identities = new List<ObjectIdentity>
                {
                    new()
                    {
                        SignInType = "userName",
                        Issuer = config.DomainName,
                        IssuerAssignedId = userProfile.UserName
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    Password = userProfile.Password
                },
                PasswordPolicies = "DisablePasswordExpiration",
                
                
                AdditionalData = new Dictionary<string, object>
                {
                    { CustomAttributeName("UserRole", config.B2CExtensionAppClientId), userProfile.Role }
                }
            });
            return user.Id;
        }

        #region Supported Methods

        private GraphServiceClient GetClient(string name)
        {
            var config = _configuration.GetSection(name).Get<TenantConfiguration>();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var clientSecretCredential = new ClientSecretCredential(config.TenantId, config.AppId, config.ClientSecret);
            return new GraphServiceClient(clientSecretCredential, scopes);
        }

        private string CustomAttributeName(string attributeName, string b2CExtensionAppClientId)
        {
            return $"extension_{b2CExtensionAppClientId.Replace("-","")}_{attributeName}";
        }

        #endregion

    }

    public interface IUserService
    {
        Task<string> Create(UserProfile userProfile, string name);
    }
}
