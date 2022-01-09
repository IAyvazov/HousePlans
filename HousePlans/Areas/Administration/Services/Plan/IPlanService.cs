namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Plan;

    public interface IPlanService
    {
        Task<int> CreatePlan(PlanFormViewModel model);

        Task<IEnumerable<PlanAllViewModel>> AllPlans();
    }
}
