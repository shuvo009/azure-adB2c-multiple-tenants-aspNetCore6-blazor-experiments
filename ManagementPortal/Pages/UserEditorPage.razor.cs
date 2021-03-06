using ManagementPortal.Contracts;
using ManagementPortal.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace ManagementPortal.Pages
{
    public partial class UserEditorPage
    {
        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "tn")]
        public string? TenantName { get; set; }

        [Inject] public IUserService UserService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        public UserProfile UserProfile { get; set; } = new();

        public bool IsSaving { get; set; }

        public async Task Update()
        {
            IsSaving = true;
            try
            {
                await UserService.Create(UserProfile, TenantName!);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsSaving = false;
            }
        }
    }
}
