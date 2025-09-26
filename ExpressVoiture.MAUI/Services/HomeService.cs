using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;
using System.Net.Http.Json;

namespace ExpressVoiture.MAUI.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClientVehicleListDto>> GetAllClientVehicleListViewModelAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ClientVehicles");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<ClientVehicleListDto>>()
                       ?? new List<ClientVehicleListDto>();
            }
            catch (HttpRequestException)
            {
                return new List<ClientVehicleListDto>();
            }
        }

        public async Task<ClientDetailedVehicleDto?> GetClientDetailsViewModelAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ClientVehicles/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ClientDetailedVehicleDto>();
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}