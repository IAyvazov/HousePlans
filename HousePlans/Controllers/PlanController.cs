namespace HousePlans.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using HousePlans.Services.Plan;
    using HousePlans.Models.Home;

    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public async Task<IActionResult> All()
        {
            var plans = await this.planService.All();

            return View(plans);
        }

        public async Task<IActionResult> Residential()
        {
            var plans = await this.planService.All(nameof(Residential));

            return View(plans);
        }

        public async Task<IActionResult> Administrative()
        {
            var plans = await this.planService.All(nameof(Administrative));

            return View(plans);
        }

        public async Task<IActionResult> Industrial()
        {
            var plans = await this.planService.All(nameof(Industrial));

            return View(plans);
        }

        public async Task<IActionResult> Typical()
        {
            var plans = await this.planService.All(nameof(Typical));

            return View(plans);
        }

        public async Task<IActionResult> Execution()
        {
            return View();
        }

        public async Task<IActionResult> Details(int houseId)
        {
            var plansDetails = await this.planService.GetByHouseId(houseId);

            return View(plansDetails);
        }

        public async Task<IActionResult> AreaRange(int fromArea, int toArea)
        {
            var plans = await this.planService.AreaRange(fromArea,toArea);

            return View("All",plans);
        }

        public async Task<IActionResult> Search(SearchModel model)
        {
            var plans = await this.planService.Search(model);

            return View("All", plans);
        }
    }
}
