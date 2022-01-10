﻿namespace HousePlans.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using HousePlans.Services.Plan;

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

        public async Task<IActionResult> Details(int houseId)
        {
            var plansDetails = await this.planService.GetByHouseId(houseId);

            return View(plansDetails);
        }
    }
}