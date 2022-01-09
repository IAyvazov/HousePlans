namespace HousePlans.Areas.Administration.Controllers
{
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Areas.Administration.Services.Plan;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GlobalConstant;

    public class PlanController : AdministrationController
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Create()
        {
            return View(new PlanFormViewModel());
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(PlanFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Error");
            }

           var id = await this.planService.CreatePlan(model);

            return Redirect("/");
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> All()
        {
            var plans = await this.planService.AllPlans();

            return View(plans);
        }
    }
}
