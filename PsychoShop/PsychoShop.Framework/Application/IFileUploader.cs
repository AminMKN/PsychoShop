using Microsoft.AspNetCore.Http;

namespace PsychoShop.Framework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}