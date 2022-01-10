namespace HousePlans.Areas.Administration.Controllers
{
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Areas.Administration.Services.House;
    using HousePlans.Areas.Administration.Services.Plan;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GlobalConstant;

    public class PlanController : AdministrationController
    {
        private readonly IPlanAdministrationService planService;
        private readonly IHouseAdministrationService houseService;

        public PlanController(IPlanAdministrationService planService, IHouseAdministrationService houseService)
        {
            this.planService = planService;
            this.houseService = houseService;
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

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Details(int houseId)
        {
            var house = await this.houseService.Details(houseId);

            return View(house);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Edit(int planId)
        {
            var plan = await this.planService.GetById(planId);

            return View(plan);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Delete(int planId)
        {
            var isDeleted = await this.planService.Delete(planId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
