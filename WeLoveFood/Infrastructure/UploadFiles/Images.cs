using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WeLoveFood.Web.Web.Infrastructure.UploadFiles
{
    public class Images : IImages
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Images(IWebHostEnvironment hostEnvironment)
            => this._webHostEnvironment = hostEnvironment;

        public string Upload(IFormFile image, string path)
        {
            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, path);
                uniqueFileName = Guid.NewGuid() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
