namespace HousePlans.Services.Plan
{
    using HousePlans.Models.Home;
    using HousePlans.Models.Plan;

    public interface IPlanService
    {
        Task<IEnumerable<PlanAllViewModel>> All();

        Task<PlanDetailsViewModel> GetByHouseId(int houseId);

        Task<IEnumerable<PlanAllViewModel>> Search(int fromArea, int toArea);

        Task<HomeViewModel> HomeInfo();
    }
}
