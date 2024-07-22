using ExpressVoiture.Domain.Models;
using ExpressVoiture.Services;
using ExpressVoiture.ViewModel;
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

        public IActionResult Index()
        {
            List<ClientVehicleListViewModel> voitureList = _homeService.GetAllClientVehicleListViewModel();
            return View(voitureList);
        }

        public IActionResult Details(int id)
        {
            ClientDetailedVehicleViewModel voitureDetailed = _homeService.GetClientDetailsViewModel(id);
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
