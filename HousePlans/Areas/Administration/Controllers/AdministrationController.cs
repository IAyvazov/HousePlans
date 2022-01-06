namespace HousePlans.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GlobalConstant;

    [Authorize(Roles = AdministratorRoleName)]
    [Area("Administration")]
    public abstract class AdministrationController : Controller
    {
    }
}
