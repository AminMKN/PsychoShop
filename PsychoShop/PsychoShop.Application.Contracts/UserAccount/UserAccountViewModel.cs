namespace PsychoShop.Application.Contracts.UserAccount
{
    public class UserAccountViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
