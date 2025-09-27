
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services
{
    public class HomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClientVehicleListDto>> GetAllClientVehicleListViewModelAsync()
        {
            var vehicles =  await _httpClient.GetFromJsonAsync<List<ClientVehicleListDto>>("api/ClientVehicles");
            if (vehicles != null)
            {
                foreach (var vehicle in vehicles)
                {
                    vehicle.ImagePath = GetFullImagePath(vehicle.ImagePath);
                }
            }

            return vehicles ?? new List<ClientVehicleListDto>();
        }

        public async Task<ClientDetailedVehicleDto> GetClientDetailsViewModelAsync(int id)
        {
            var vehicle = await _httpClient.GetFromJsonAsync<ClientDetailedVehicleDto>($"api/ClientVehicles/{id}");

            vehicle.ImagePath = GetFullImagePath(vehicle.ImagePath);

            return vehicle;
        }

        private string GetFullImagePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return null!;
            return $"{_httpClient.BaseAddress}{relativePath.Replace("\\", "/")}";
        }
    }
}
