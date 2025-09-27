using ExpressVoiture.Services.IService;
using ExpressVoiture.Shared.ViewModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ExpressVoiture.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
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

            var vehicle = await _httpClient.GetFromJsonAsync<AddOrUpdateVehicleDto>($"api/vehicles/{id}");

            if (vehicle != null)
            {
                vehicle.ImagePath = GetFullImagePath(vehicle.ImagePath);
            }

            return vehicle;
        }

        public async Task SaveVoitureAVendre(AddOrUpdateVehicleDto voiture, IFormFile? file)
        {
            using var content = new MultipartFormDataContent();

            // Ajouter le fichier si présent
            if (file != null)
            {
                var fileStream = file.OpenReadStream();
                content.Add(new StreamContent(fileStream)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue(file.ContentType) }
                }, "file", file.FileName);
            }

            // Ajouter chaque champ du DTO
            content.Add(new StringContent(voiture.Marque ?? ""), "Marque");
            content.Add(new StringContent(voiture.Modele ?? ""), "Modele");
            content.Add(new StringContent(voiture.Finition ?? ""), "Finition");
            content.Add(new StringContent(voiture.CodeVIN ?? ""), "CodeVIN");
            content.Add(new StringContent(voiture.Annee.ToString()), "Annee");
            content.Add(new StringContent(voiture.PrixAchat.ToString(System.Globalization.CultureInfo.InvariantCulture)), "PrixAchat");
            content.Add(new StringContent(voiture.DateAchat.Value.ToString("o")), "DateAchat"); // format ISO
            if (voiture.DateVente.HasValue)
                content.Add(new StringContent(voiture.DateVente.Value.ToString("o")), "DateVente");
            if (voiture.DateDisponibiliteVente.HasValue)
                content.Add(new StringContent(voiture.DateDisponibiliteVente.Value.ToString("o")), "DateDisponibiliteVente");
            content.Add(new StringContent(voiture.CoutReparations.ToString(System.Globalization.CultureInfo.InvariantCulture)), "CoutReparations");
            content.Add(new StringContent(voiture.DescriptionReparations ?? ""), "DescriptionReparations");

            var response = await _httpClient.PostAsync("api/vehicles", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateVoitureAVendre(AddOrUpdateVehicleDto voiture, IFormFile? file)
        {
            using var content = new MultipartFormDataContent();

            // Ajout du fichier
            if (file != null)
            {
                var fileStream = file.OpenReadStream();
                content.Add(new StreamContent(fileStream)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue(file.ContentType) }
                }, "file", file.FileName);
            }

            // Ajout des champs du DTO (propriétés simples comme dans un formulaire)
            content.Add(new StringContent(voiture.VoitureId.ToString()), nameof(voiture.VoitureId));
            content.Add(new StringContent(voiture.Marque ?? ""), nameof(voiture.Marque));
            content.Add(new StringContent(voiture.Modele ?? ""), nameof(voiture.Modele));
            content.Add(new StringContent(voiture.Finition ?? ""), nameof(voiture.Finition));
            content.Add(new StringContent(voiture.CodeVIN ?? ""), nameof(voiture.CodeVIN));
            content.Add(new StringContent(voiture.Annee.ToString()), nameof(voiture.Annee));
            content.Add(new StringContent(voiture.PrixAchat.ToString()), nameof(voiture.PrixAchat));

            if (voiture.DateAchat != default)
                content.Add(new StringContent(voiture.DateAchat.Value.ToString("o")), nameof(voiture.DateAchat));

            if (voiture.DateDisponibiliteVente.HasValue)
                content.Add(new StringContent(voiture.DateDisponibiliteVente.Value.ToString("o")), nameof(voiture.DateDisponibiliteVente));

            if (voiture.DateVente.HasValue)
                content.Add(new StringContent(voiture.DateVente.Value.ToString("o")), nameof(voiture.DateVente));

            content.Add(new StringContent(voiture.CoutReparations.ToString()), nameof(voiture.CoutReparations));
            content.Add(new StringContent(voiture.DescriptionReparations ?? ""), nameof(voiture.DescriptionReparations));

            // Envoi au backend
            var response = await _httpClient.PutAsync("api/vehicles", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteVoitureAVendre(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var response = await _httpClient.DeleteAsync($"api/vehicles/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<DeleteVehicleDto> GetDeleteVehicleViewModel(int? id)
        {
            if (id is null) return null!;

            var vehicle = await _httpClient.GetFromJsonAsync<DeleteVehicleDto>($"api/vehicles/delete/{id}");

            return vehicle;
        }

        private string GetFullImagePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return null!;
            return $"{_httpClient.BaseAddress}{relativePath.Replace("\\", "/")}";
        }
    }
}
