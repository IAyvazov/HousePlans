namespace HousePlans.Services.Plan
{
    using HousePlans.Models.Home;
    using HousePlans.Models.Plan;

    public interface IPlanService
    {
        Task<IEnumerable<PlanAllViewModel>> All();

        Task<IEnumerable<PlanAllViewModel>> All(string categoryType);

        Task<PlanDetailsViewModel> GetByHouseId(int houseId);

        Task<IEnumerable<PlanAllViewModel>> AreaRange(int fromArea, int toArea);

        Task<HomeViewModel> HomeInfo();

        Task<IEnumerable<PlanAllViewModel>> Search(SearchModel searchModel);
    }
}
