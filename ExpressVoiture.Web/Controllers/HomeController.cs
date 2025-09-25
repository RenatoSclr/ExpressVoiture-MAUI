using ExpressVoiture.Services;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpressVoiture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService _homeService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            List<ClientVehicleListViewModel> voitureList = await _homeService.GetAllClientVehicleListViewModelAsync();
            return View(voitureList);
        }

        public async Task<IActionResult> Details(int id)
        {
            ClientDetailedVehicleViewModel voitureDetailed = await _homeService.GetClientDetailsViewModelAsync(id);
            return View(voitureDetailed);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
