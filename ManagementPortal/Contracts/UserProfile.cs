namespace ManagementPortal.Contracts
{
    public record UserProfile
    {
        public string GivenName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
