using ExpressVoiture.Services.IService;
using ExpressVoiture.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IVehicleService, VehicleService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7167/");
});

builder.Services.AddHttpClient<HomeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7167/");
});


builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = ".ExpressVoiture.Identity"; 
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/Account/Login"; 
    });

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
