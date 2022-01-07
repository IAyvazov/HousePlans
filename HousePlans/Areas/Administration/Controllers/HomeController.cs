namespace HousePlans.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GlobalConstant;

    public class HomeController : AdministrationController
    {
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
