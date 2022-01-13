namespace HousePlans.Services.Plan
{
    using HousePlans.Data;
    using HousePlans.Models.Home;
    using HousePlans.Models.Plan;
    using HousePlans.Services.House;
    using HousePlans.Services.Instalation;
    using HousePlans.Services.Material;

    public class PlanService : IPlanService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IInstalationService instalationService;
        private readonly IHouseService houseService;
        private readonly IMaterialService materialService;

        public PlanService(
            ApplicationDbContext dbContext,
            IInstalationService instalationService,
            IHouseService houseService,
            IMaterialService materialService)
        {
            this.dbContext = dbContext;
            this.instalationService = instalationService;
            this.houseService = houseService;
            this.materialService = materialService;
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

            var material = await this.materialService.GetByHouseId(houseId);

            var plan = this.dbContext.Plans
                 .Where(x => x.HouseId == houseId && !x.IsDeleted)
                 .Select(x => new PlanDetailsViewModel
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Instalation = instalation,
                     House = house,
                     Material= material,
                     CreatedOn = x.CreatedOn.ToString("g"),
                 })
                 .FirstOrDefault();

            return plan;
        }

        public async Task<HomeViewModel> HomeInfo()
        {
            var plans = this.dbContext.Plans
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .Select(x => new PlanHomeViewModel
                {
                    Id = x.Id,
                    HouseId = x.HouseId,
                    Name = x.Name,
                    Url = x.House.Photos
                    .Select(x => x.Url)
                    .FirstOrDefault(),
                })
                .ToHashSet();

            var home = new HomeViewModel
            {
                Plans = plans,
                Urls =  this.dbContext.Photos
                .Take(3)
                .Select(x=>x.Url)
                .ToArray(),
        };
            return home;
        }

    public async Task<IEnumerable<PlanAllViewModel>> Search(int fromArea, int toArea)
    {
        if (toArea == 0)
        {
            toArea = int.MaxValue;
        }

        var plans = this.dbContext.Plans
            .Where(x => !x.IsDeleted && (x.House.Area >= fromArea && x.House.Area <= toArea))
            .Select(x => new PlanAllViewModel
            {
                HouseId = x.HouseId,
                Name = x.Name,
                Price = x.Price,
                CreatedOn = x.CreatedOn.ToString("g"),
                PictureUrl = x.House.Photos.FirstOrDefault().Url,
            })
        .ToHashSet();

        return plans;
    }
}
}
