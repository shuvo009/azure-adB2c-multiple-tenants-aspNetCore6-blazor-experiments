using ManagementPortal.Contracts;
using ManagementPortal.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace ManagementPortal.Pages
{
    public partial class Index
    {
        [Inject] public IUserService UserService { get; set; } = null!;

        private bool IsLoading { get; set; }
        private List<UserView> Users { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task LoadUsers(string tenantName)
        {
            IsLoading = true;
            try
            {
                Users = await UserService.GetAllUsers(tenantName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteUser(UserView userView)
        {
            userView.IsDeleting = true;
            try
            {
                await UserService.Delete(userView.Id, userView.TenantName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                userView.IsDeleting = false;
            }

            await LoadUsers(userView.TenantName);
        }
    }
}
