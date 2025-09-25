using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Json;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7167/api/auth/login", model);
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Email ou mot de passe incorrect");
            return View(model);
        }

        var user = await response.Content.ReadFromJsonAsync<UserDto>();
        if (user == null) return View(model);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
        foreach (var role in user.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}

public class LoginViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class UserDto
{
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}
