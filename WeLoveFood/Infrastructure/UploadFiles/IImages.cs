using Microsoft.AspNetCore.Http;

namespace WeLoveFood.Web.Web.Infrastructure.UploadFiles
{
    public interface IImages
    {
        string Upload(IFormFile image, string path);
    }
}
