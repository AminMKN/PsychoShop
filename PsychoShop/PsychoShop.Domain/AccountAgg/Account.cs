using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ProfilePhoto { get; set; }
        public bool EmailConfirmed { get; set; }

        public Account(string fullName, string userName, string email, string mobilePhone, string password, string token)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            MobilePhone = mobilePhone;
            Password = password;
            Token = token;
            EmailConfirmed = false;
        }

        public void Edit(string fullName, string userName, string email, string mobilePhone, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            MobilePhone = mobilePhone;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
        }
    }
}
