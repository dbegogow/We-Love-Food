using Microsoft.AspNetCore.Http;

namespace WeLoveFood.Services.Images
{
    public interface IImagesService
    {
        string UploadImage(IFormFile image, string path);
    }
}