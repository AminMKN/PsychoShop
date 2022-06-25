using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PsychoShop.Framework.Application.AuthHelper
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetCurrentAccountId()
        {
            return int.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }

        public string GetCurrentAccountFullName()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
        }

        public string GetCurrentAccountUserName()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public string GetCurrentAccountEmail()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public string GetCurrentAccountMobilePhone()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
        }

        public void SignIn(AuthViewModel command)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, command.Id.ToString()),
                new Claim(ClaimTypes.Surname, command.FullName),
                new Claim(ClaimTypes.Name, command.UserName),
                new Claim(ClaimTypes.Email, command.Email),
                new Claim(ClaimTypes.MobilePhone, command.MobilePhone),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = command.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}