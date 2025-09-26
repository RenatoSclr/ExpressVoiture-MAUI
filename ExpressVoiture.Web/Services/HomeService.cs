
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
            return await _httpClient.GetFromJsonAsync<List<ClientVehicleListDto>>("api/ClientVehicles");
        }

        public async Task<ClientDetailedVehicleDto> GetClientDetailsViewModelAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClientDetailedVehicleDto>($"api/ClientVehicles/{id}");
        }
    }
}
