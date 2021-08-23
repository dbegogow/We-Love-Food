using System.IO;
using System.Linq;
using CloudinaryDotNet;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace WeLoveFood.Web.Infrastructure.UploadFiles
{
    public static class Clouding
    {
        private const string Cloud = "dfhlletuw";
        private const string ApiKey = "299434538297623";
        private const string ApiSecret = "hM0HeQYUnOMV0ByTn09uAN6VpB8";

        public static async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || !IsFileValid(file))
            {
                return null;
            }

            string url;
            byte[] fileBytes;
            await using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                fileBytes = stream.ToArray();
            }

            await using (var uploadStream = new MemoryStream(fileBytes))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, uploadStream),
                };

                var clouding = new Cloudinary(new Account
                {
                    Cloud = Cloud,
                    ApiKey = ApiKey,
                    ApiSecret = ApiSecret
                });

                var result = await clouding.UploadAsync(uploadParams);

                url = result.Uri.AbsoluteUri;
            }

            return url;
        }

        private static bool IsFileValid(IFormFile photoFile)
        {
            if (photoFile == null)
            {
                return true;
            }

            string[] validTypes =
            {
                "image/x-png",
                "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/png"
            };

            if (!validTypes.Contains(photoFile.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}
