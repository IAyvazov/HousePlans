namespace HousePlans.Services.Plan
{
    using HousePlans.Data;
    using HousePlans.Models.Plan;

    public class PlanService : IPlanService
    {
        private readonly ApplicationDbContext dbContext;

        public PlanService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PlanAllViewModel>> All()
        => this.dbContext.Plans
            .Where(x => !x.IsDeleted)
            .Select(x => new PlanAllViewModel
            {
                HouseId = x.HouseId,
                Name = x.Name,
                Price = x.Price,
                CreatedOn = x.CreatedOn.ToString("g"),
                PictureUrl = x.House.Photos.FirstOrDefault().Url,
            })
            .ToHashSet();
    }
}
