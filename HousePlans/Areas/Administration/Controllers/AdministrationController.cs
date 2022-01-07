namespace HousePlans.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GlobalConstant;

    [Area("Administration")]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdministrationController : Controller
    {
    }
}
