namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Plan;

    public interface IPlanAdministrationService
    {
        Task<int> CreatePlan(PlanFormViewModel model);

        Task<bool> Edit(PlanFormViewModel model, int planId);

        Task<PlanFormViewModel> GetById(int planId);

        Task<IEnumerable<PlanAllViewModel>> AllPlans();

        Task<bool> Delete(int planId);

        Task<bool> Restore(int planId);
    }
}
