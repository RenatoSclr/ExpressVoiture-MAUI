using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;
using System.Net.Http.Json;

namespace ExpressVoiture.MAUI.Services
{
    public class VehicleAdminService : IVehicleAdminService
    {
        private readonly HttpClient _httpClient;
        public VehicleAdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AdminVehicleListDto>> GetVehiclesAsync()
        {
            var vehicles = await _httpClient.GetFromJsonAsync<List<AdminVehicleListDto>>("api/Vehicles");
            return vehicles;
        }

        public async Task<bool> AddVehicleAsync(AddOrUpdateVehicleDto vehicle)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Vehicles", vehicle);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Vehicles/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
