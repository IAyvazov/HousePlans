namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Plan;

    public interface IPlanAdministrationService
    {
        Task<int> CreatePlan(PlanFormViewModel model);

        Task<PlanFormViewModel> GetById(int planId);

        Task<IEnumerable<PlanAllViewModel>> AllPlans();

        Task<bool> Delete(int planId);

        Task<bool> Restore(int planId);
    }
}
