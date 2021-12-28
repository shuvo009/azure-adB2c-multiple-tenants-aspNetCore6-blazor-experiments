using ManagementPortal.Contracts;
using ManagementPortal.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace ManagementPortal.Pages
{
    public partial class Index
    {
        [Inject] public IUserService UserService { get; set; } = null!;

        private List<UserView> Users { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public async Task LoadUsers(string tenantName)
        {
            Users = await UserService.GetAllUsers(tenantName);
        }
    }
}
