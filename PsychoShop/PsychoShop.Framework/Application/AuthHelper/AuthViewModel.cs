namespace PsychoShop.Framework.Application.AuthHelper
{
    public class AuthViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public bool RememberMe { get; set; }

        public AuthViewModel(int id, string fullName, string userName, string email, string mobilePhone, bool rememberMe)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            MobilePhone = mobilePhone;
            RememberMe = rememberMe;
        }
    }
}
