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

        public string GetCurrentUserAccountId()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}