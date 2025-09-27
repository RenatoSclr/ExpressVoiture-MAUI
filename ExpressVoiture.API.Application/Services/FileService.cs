using ExpressVoiture.API.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ExpressVoiture.API.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _hostEnvironment;

        public FileService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string GetVehicleImagesPath()
        {
            string wwwRootPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot");
            string imagesPath = Path.Combine(wwwRootPath, "images", "vehicles");

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            return imagesPath;
        }


        public string SaveFile(IFormFile file)
        {
            if (file is null) return null;

            string imagesPath = GetVehicleImagesPath();
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(imagesPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("images", "vehicles", fileName).Replace("\\", "/");
        }

        public void DeleteFile(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            string fullPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", relativePath.Replace("/", "\\"));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
