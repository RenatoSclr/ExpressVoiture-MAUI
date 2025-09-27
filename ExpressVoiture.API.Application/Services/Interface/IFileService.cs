

using Microsoft.AspNetCore.Http;

namespace ExpressVoiture.API.Application.Services.Interface
{
    public interface IFileService
    {
        string SaveFile(IFormFile file);
        void DeleteFile(string relativePath);
        string GetVehicleImagesPath();
    }
}
