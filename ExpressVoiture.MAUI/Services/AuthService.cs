using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.AuthDto;
using System.Net.Http.Json;

namespace ExpressVoiture.MAUI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var loginRequest = new LoginRequest { Email = email, Password = password };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            if (!response.IsSuccessStatusCode) return false;

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user is not null;
        }
        catch
        {
            return false;
        }
    }

    public Task LogoutAsync() => Task.CompletedTask;
}
