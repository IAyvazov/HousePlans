namespace HousePlans.Services.Plan
{
    using HousePlans.Models.Plan;

    public interface IPlanService
    {
        Task<IEnumerable<PlanAllViewModel>> All();
    }
}
