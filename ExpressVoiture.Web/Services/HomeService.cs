
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

        public async Task<List<ClientVehicleListViewModel>> GetAllClientVehicleListViewModelAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ClientVehicleListViewModel>>("api/ClientVehicles");
        }

        public async Task<ClientDetailedVehicleViewModel> GetClientDetailsViewModelAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClientDetailedVehicleViewModel>($"api/ClientVehicles/{id}");
        }
    }
}
