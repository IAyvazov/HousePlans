namespace HousePlans.Controllers
{
    using HousePlans.Areas.Administration.Services.Email;
    using HousePlans.Data.Email;
    using HousePlans.Models;
    using HousePlans.Models.Home;
    using HousePlans.Services.Plan;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlanService planService;
        private readonly IEmailSender emailSender;

        public HomeController(
            ILogger<HomeController> logger,
            IPlanService planService,
            IEmailSender emailSender)
        {
            this._logger = logger;
            this.planService = planService;
            this.emailSender = emailSender;
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

        [HttpPost]
        public IActionResult Contact(EmailModel model)
        {
            this.emailSender.SendEmail(model);

            return Redirect(nameof(Contact));
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchModel model)
        {
            return RedirectToAction("Search", "Plan", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}