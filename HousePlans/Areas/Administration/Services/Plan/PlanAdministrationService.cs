namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Areas.Administration.Services.Floor;
    using HousePlans.Areas.Administration.Services.House;
    using HousePlans.Areas.Administration.Services.Instalation;
    using HousePlans.Areas.Administration.Services.Photo;
    using HousePlans.Data;
    using HousePlans.Data.Models;

    public class PlanAdministrationService : IPlanAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFloorService floorService;
        private readonly IHouseAdministrationService houseService;
        private readonly IInstalationAdministrationService instalationService;
        private readonly IPhotoAdministrationService photoService;

        public PlanAdministrationService(
            ApplicationDbContext dbContext,
            IFloorService floorService,
            IHouseAdministrationService houseService,
            IInstalationAdministrationService instalationService,
            IPhotoAdministrationService photoService)
        {
            this.dbContext = dbContext;
            this.floorService = floorService;
            this.houseService = houseService;
            this.instalationService = instalationService;
            this.photoService = photoService;
        }

        public async Task<IEnumerable<PlanAllViewModel>> AllPlans()
        {
            var allPlans = this.dbContext.Plans
                .Select(p => new PlanAllViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    HouseId = p.HouseId,
                    CreatedOn = p.CreatedOn.ToString(),
                    IsDeleted = p.IsDeleted,
                    DeletedOn = p.DeletedOn.ToString(),
                    ModifiedOn = p.ModifiedOn.ToString(),
                })
                .ToList();

            return allPlans;
        }

        public async Task<int> CreatePlan(PlanFormViewModel model)
        {
            var houseId = await this.houseService.CreateHouse(model.House);

            if (houseId == 0)
            {
                return -1;
            }

            await this.floorService.CreateFloor(model.House.Floors, houseId);
            await this.photoService.Upload(model.House.Photos, houseId);

            var instalationId = await this.instalationService.CreateInstalation(model.House.Instalation);

            if (instalationId == 0)
            {
                return -1;
            }

            var plan = new Plan
            {
                CreatedOn = DateTime.UtcNow,
                Name = model.Name,
                Price = model.Price,
                HouseId = houseId,
                InstalationId = instalationId,
            };

            await this.dbContext.Plans.AddAsync(plan);
            await this.dbContext.SaveChangesAsync();

            return plan.Id;
        }
    }
}
