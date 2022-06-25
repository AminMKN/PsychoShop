using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PsychoShop.Application.Contracts.Account;
using PsychoShop.Domain.AccountAgg;
using PsychoShop.Framework.Application;
using PsychoShop.Framework.Application.AuthHelper;
using PsychoShop.Framework.Application.Email;
using PsychoShop.Framework.Application.PasswordHasher;

namespace PsychoShop.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IEmailSender _emailSender;
        private readonly IFileUploader _fileUploader;
        private readonly LinkGenerator _linkGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IAuthHelper authHelper, IFileUploader fileUploader, IEmailSender emailSender, LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
        {
            _authHelper = authHelper;
            _emailSender = emailSender;
            _fileUploader = fileUploader;
            _linkGenerator = linkGenerator;
            _passwordHasher = passwordHasher;
            _contextAccessor = contextAccessor;
            _accountRepository = accountRepository;
        }

        public OperationResult SignUp(SignUpAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.UserName == command.UserName))
                return operation.Failed(ApplicationMessages.DuplicatedUserName);

            if (_accountRepository.Exists(x => x.Email == command.Email))
                return operation.Failed(ApplicationMessages.DuplicatedEmail);

            var token = CodeGenerator.GenerateToken();
            var password = _passwordHasher.Hash(command.Password);
            var account = new Account(command.FullName, command.UserName, command.Email, command.MobilePhone, password, token);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult SignIn(SignInAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByUserName(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMessages.UserNameOrPasswordNotValid);

            var password = _passwordHasher.Check(account.Password, command.Password);
            if (!password.Verified)
                return operation.Failed(ApplicationMessages.UserNameOrPasswordNotValid);

            var authViewModel = new AuthViewModel(account.Id, account.FullName, account.UserName, account.Email, account.MobilePhone, command.RememberMe);
            _authHelper.SignIn(authViewModel);
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (_accountRepository.Exists(x => x.UserName == command.UserName && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedUserName);

            if (_accountRepository.Exists(x => x.Email == command.Email && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedEmail);

            var profilePhotoPath = $"{"User"}/{command.UserName}";
            var profilePhoto = _fileUploader.Upload(command.ProfilePhoto, profilePhotoPath);
            account.Edit(command.FullName, command.UserName, command.Email, command.MobilePhone, profilePhoto);
            _accountRepository.SaveChanges();
            return operation.Success(ApplicationMessages.TaskSuccessful);
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByEmail(command.Email);
            if (account == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (command.Email == account.Email && command.Token == account.Token)
            {
                var password = _passwordHasher.Hash(command.Password);
                account.ChangePassword(password);
                _accountRepository.SaveChanges();
                return operation.Success(ApplicationMessages.ChangePasswordSuccessful);
            }

            return operation.Failed(ApplicationMessages.RequestedInfoNotFound);
        }

        public OperationResult ConfirmEmail(string userName, string token)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByUserName(userName);
            if (account == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            if (userName == account.UserName && token == account.Token)
            {
                account.ConfirmEmail();
                _accountRepository.SaveChanges();
                return operation.Success(ApplicationMessages.EmailConfirmed);
            }

            return operation.Failed(ApplicationMessages.RequestedInfoNotFound);
        }

        public async Task<OperationResult> ForgotPassword(ForgotPassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByEmail(command.Email);
            if (account == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var emailMessage = _linkGenerator.GetUriByAction(_contextAccessor.HttpContext, "/Account/ResetPassword", "",
                 new { email = account.Email, token = account.Token }, _contextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmail(account.Email, "سایکو شاپ-تغییر کلمه عبور",
                $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>تغییر کلمه عبور</a>", true);

            return operation.Success(ApplicationMessages.SentForgotPasswordEmail);
        }

        public async Task<OperationResult> SendEmailConfirmation()
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(_authHelper.GetCurrentAccountId());
            if (account == null)
                return operation.Failed(ApplicationMessages.RequestedInfoNotFound);

            var emailMessage = _linkGenerator.GetUriByAction(_contextAccessor.HttpContext, "/Index", "ConfirmEmail",
                new { userName = account.UserName, token = account.Token }, _contextAccessor.HttpContext.Request.Scheme);

            await _emailSender.SendEmail(account.Email, "سایکو شاپ-فعال سازی",
                $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>فعال سازی</a>", true);

            return operation.Success(ApplicationMessages.SentConfirmationEmail);
        }

        public EditAccount GetDetails(int id)
        {
            return _accountRepository.GetDetails(id);
        }

        public async Task<AccountViewModel> GetCurrentAccountInfo()
        {
            return await _accountRepository.GetCurrentAccountInfo();
        }

        public async Task<List<AccountViewModel>> GetAccountsList()
        {
            return await _accountRepository.GetAccountsList();
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            return await _accountRepository.Search(searchModel);
        }

        public void SignOut()
        {
            _authHelper.SignOut();
        }
    }
}
