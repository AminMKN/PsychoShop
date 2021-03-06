using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using PsychoShop.Application.Contracts.UserAccount;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;
using PsychoShop.Framework.Application.Email;

namespace PsychoShop.Application
{
    public class UserAccountApplication : IUserAccountApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IEmailSender _emailSender;
        private readonly IFileUploader _fileUploader;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<UserAccount> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountApplication(IAuthHelper authHelper, IEmailSender emailSender, IFileUploader fileUploader, LinkGenerator linkGenerator, UserManager<UserAccount> userManager, IHttpContextAccessor contextAccessor, SignInManager<UserAccount> signInManager, IUserAccountRepository userAccountRepository)
        {
            _authHelper = authHelper;
            _emailSender = emailSender;
            _userManager = userManager;
            _fileUploader = fileUploader;
            _linkGenerator = linkGenerator;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _userAccountRepository = userAccountRepository;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> SignIn(SignInUserAccount command)
        {
            return await _signInManager.PasswordSignInAsync(command.UserName, command.Password, command.RememberMe, false);
        }

        public async Task<IdentityResult> SignUp(SignUpUserAccount command)
        {
            var userAccount = new UserAccount()
            {
                Email = command.Email,
                UserName = command.UserName,
                FullName = command.FullName,
                PhoneNumber = command.PhoneNumber
            };

            return await _userManager.CreateAsync(userAccount, command.Password);
        }

        public async Task<IdentityResult> Edit(EditUserAccount command)
        {
            var userAccount = await _userManager.FindByIdAsync(command.Id);
            userAccount.Email = command.Email;
            userAccount.UserName = command.UserName;
            userAccount.FullName = command.FullName;
            userAccount.PhoneNumber = command.PhoneNumber;
            if (command.ProfilePhoto != null)
            {
                var profilePhotoPath = $"{"Users"}/{userAccount.UserName}";
                var profilePhoto = _fileUploader.Upload(command.ProfilePhoto, profilePhotoPath);
                userAccount.ProfilePhoto = profilePhoto;
            }

            return await _userManager.UpdateAsync(userAccount);
        }

        public async Task<IdentityResult> ResetPassword(ResetPassword command)
        {
            var userAccount = await _userManager.FindByEmailAsync(command.Email);
            return await _userManager.ResetPasswordAsync(userAccount, command.Token, command.Password);
        }

        public async Task<OperationResult> ForgotPassword(ForgotPassword command)
        {
            var operation = new OperationResult();
            var userAccount = await _userManager.FindByEmailAsync(command.Email);
            if (userAccount == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userAccount);
            var emailMessage = _linkGenerator.GetUriByAction(_contextAccessor.HttpContext, "ResetPassword", "UserAccount",
                 new { email = userAccount.Email, token = resetPasswordToken }, _contextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmail(userAccount.Email, "سایکو شاپ-تغییر کلمه عبور",
                $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>تغییر کلمه عبور</a>", true);

            return operation.Success(ApplicationMessages.SentForgotPasswordEmail);
        }

        public async Task<OperationResult> ConfirmEmail(string userName, string token)
        {
            var operation = new OperationResult();
            if (userName == null || token == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var userAccount = await _userManager.FindByNameAsync(userName);
            if (userAccount == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var result = await _userManager.ConfirmEmailAsync(userAccount, token);
            if (result.Succeeded)
                return operation.Success(ApplicationMessages.EmailConfirmed);

            return operation.Failed(ApplicationMessages.RequestedInfoNotFound);
        }

        public async Task<OperationResult> SendEmailConfirmation()
        {
            var operation = new OperationResult();
            var userAccount = await _userManager.FindByIdAsync(_authHelper.GetCurrentUserAccountId());
            if (userAccount == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount);
            var emailMessage = _linkGenerator.GetUriByAction(_contextAccessor.HttpContext, "ConfirmEmail", "UserProfile",
                 new { userName = userAccount.UserName, token = emailConfirmToken }, _contextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmail(userAccount.Email, "سایکو شاپ-فعال سازی",
                $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>فعال سازی</a>", true);

            return operation.Success(ApplicationMessages.SentConfirmationEmail);
        }

        public async Task<EditUserAccount> GetDetails(string id)
        {
            return await _userAccountRepository.GetDetails(id);
        }

        public async Task<UserAccountViewModel> GetCurrentUserAccountInfo()
        {
            return await _userAccountRepository.GetCurrentUserAccountInfo();
        }

        public async Task<List<UserAccountViewModel>> Search(UserAccountSearchModel searchModel)
        {
            return await _userAccountRepository.Search(searchModel);
        }
    }
}
