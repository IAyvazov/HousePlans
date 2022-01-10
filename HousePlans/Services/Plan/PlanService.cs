namespace HousePlans.Services.Plan
{
    using HousePlans.Data;
    using HousePlans.Models.Plan;
    using HousePlans.Services.House;
    using HousePlans.Services.Instalation;

    public class PlanService : IPlanService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IInstalationService instalationService;
        private readonly IHouseService houseService;

        public PlanService(
            ApplicationDbContext dbContext,
            IInstalationService instalationService,
            IHouseService houseService)
        {
            this.dbContext = dbContext;
            this.instalationService = instalationService;
            this.houseService = houseService;   
        }

        public async Task<IEnumerable<PlanAllViewModel>> All()
        => this.dbContext.Plans
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Price)
            .Select(x => new PlanAllViewModel
            {
                HouseId = x.HouseId,
                Name = x.Name,
                Price = x.Price,
                CreatedOn = x.CreatedOn.ToString("g"),
                PictureUrl = x.House.Photos.FirstOrDefault().Url,
            })
            .ToHashSet();

        public async Task<PlanDetailsViewModel> GetByHouseId(int houseId)
        {
            var house = await this.houseService.GetById(houseId);

            var instalation = await this.instalationService.GetByHouseId(houseId);

            var plan = this.dbContext.Plans
                 .Where(x => x.HouseId == houseId && !x.IsDeleted)
                 .Select( x => new PlanDetailsViewModel
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Instalation = instalation,
                     House = house,
                     CreatedOn = x.CreatedOn.ToString("g"),
                 })
                 .FirstOrDefault();

            return plan;
        }
    }
}
