using ManagementPortal.Contracts;
using ManagementPortal.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace ManagementPortal.Pages
{
    public partial class UserEditorPage
    {
        [Parameter]
        public string? Id { get; set; }

        [Inject] public IUserService UserService { get; set; } = null!;

        public UserProfile UserProfile { get; set; } = new();

        public bool IsSaving { get; set; }

        public async Task Update()
        {
            IsSaving = true;
            try
            {
                await UserService.Create(UserProfile, "MobileTenant");
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
