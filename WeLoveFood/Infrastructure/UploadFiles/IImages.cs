using Microsoft.AspNetCore.Http;

namespace WeLoveFood.Infrastructure.UploadFiles
{
    public interface IImages
    {
        string Upload(IFormFile image, string path);
    }
}
