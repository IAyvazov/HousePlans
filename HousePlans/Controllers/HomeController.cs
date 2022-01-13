namespace HousePlans.Controllers
{
    using HousePlans.Models;
    using HousePlans.Services.Plan;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlanService planService;


        public HomeController(ILogger<HomeController> logger, IPlanService planService)
        {
            _logger = logger;
            this.planService = planService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var home = await this.planService.HomeInfo();

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Information()
        {
            return View();
        }

        public IActionResult Contact()
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