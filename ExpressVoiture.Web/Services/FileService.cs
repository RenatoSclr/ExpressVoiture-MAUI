using ExpressVoiture.Domain.Models;
using ExpressVoiture.Services.IService;
using ExpressVoiture.ViewModel;

namespace ExpressVoiture.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public AddOrUpdateVehicleViewModel CreateFile(AddOrUpdateVehicleViewModel vehicle, IFormFile file)
        {
            if (file == null)
            {
                return vehicle;
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string voiturePath = Path.Combine(wwwRootPath, @"images\voitures");

            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                var oldImagePath = Path.Combine(wwwRootPath, vehicle.ImagePath.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            using (var fileStream = new FileStream(Path.Combine(voiturePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            vehicle.ImagePath = @"images\voitures\" + fileName;

            return vehicle;
        }

        VoitureAVendre IFileService.DeleteFile(VoitureAVendre vehicle)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                var oldImagePath = Path.Combine(wwwRootPath, vehicle.ImagePath.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            return vehicle;
        }
    }
}
