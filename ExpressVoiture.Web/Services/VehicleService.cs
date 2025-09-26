using ExpressVoiture.Services.IService;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IFileService _fileService;
        private readonly HttpClient _httpClient = new HttpClient();

        public VehicleService(IFileService fileService, HttpClient httpClient)
        {
            _fileService = fileService;
            _httpClient = httpClient;
        }

        public async Task<List<AdminVehicleListDto>> GetListAdminVehicleViewModel()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AdminVehicleListDto>>("api/vehicles");
            return result ?? new List<AdminVehicleListDto>();
        }

        public async Task<AddOrUpdateVehicleDto?> GetAddOrUpdateVehicleViewModel(int? id)
        {
            if (id == null) return null;
            return await _httpClient.GetFromJsonAsync<AddOrUpdateVehicleDto>($"api/vehicles/{id}");
        }

        public async Task SaveVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter, IFormFile file)
        {
            voitureAAjouter = _fileService.CreateFile(voitureAAjouter, file);
            await _httpClient.PostAsJsonAsync("api/vehicles", voitureAAjouter);
        }

        public async Task UpdateVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter, IFormFile file)
        {
            voitureAAjouter = _fileService.CreateFile(voitureAAjouter, file);

            var response = await _httpClient.PutAsJsonAsync($"api/vehicles", voitureAAjouter);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erreur lors de la mise à jour du véhicule {voitureAAjouter.VoitureId} : {response.ReasonPhrase}");
            }
        }

        public async Task DeleteVoitureAVendre(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var response = await _httpClient.DeleteAsync($"api/vehicles/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erreur lors de la suppression du véhicule {id} : {response.ReasonPhrase}");
            }

            _fileService.DeleteFileByVehiculeId(id.Value);

        }

        public async Task<DeleteVehicleDto> GetDeleteVehicleViewModel(int? id)
        {
            var resposnse = await _httpClient.GetFromJsonAsync<DeleteVehicleDto>($"api/vehicles/delete/{id}");
            return resposnse;
        }
    }
}
