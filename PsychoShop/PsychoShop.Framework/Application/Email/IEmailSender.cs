namespace PsychoShop.Framework.Application.Email
{
    public interface IEmailSender
    {
        public Task SendEmail(string toEmail, string subject, string message, bool isBodyHtml);
    }
}
