namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.House;
    using HousePlans.Areas.Administration.Models.Instalation;
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Areas.Administration.Services.Floor;
    using HousePlans.Areas.Administration.Services.House;
    using HousePlans.Areas.Administration.Services.Instalation;
    using HousePlans.Areas.Administration.Services.Photo;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class PlanAdministrationService : IPlanAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFloorAdministrationService floorService;
        private readonly IHouseAdministrationService houseService;
        private readonly IInstalationAdministrationService instalationService;
        private readonly IPhotoAdministrationService photoService;

        public PlanAdministrationService(
            ApplicationDbContext dbContext,
            IFloorAdministrationService floorService,
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
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    HouseId = p.HouseId,
                    CreatedOn = p.CreatedOn.ToString("g"),
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

        public async Task<bool> Delete(int planId)
        {
            var plan = this.dbContext.Plans
                .Where(x => x.Id == planId)
                .Include(x => x.House)
                .ThenInclude(x => x.Floors)
                .ThenInclude(x => x.Rooms)
                .Include(x => x.Instalation)
                .FirstOrDefault();

            if (plan == null)
            {
                return false;
            }

            plan.IsDeleted = true;
            plan.DeletedOn = DateTime.UtcNow;
            plan.ModifiedOn = DateTime.UtcNow;

            plan.House.IsDeleted = true;
            plan.House.DeletedOn = DateTime.UtcNow;
            plan.House.ModifiedOn = DateTime.UtcNow;

            plan.Instalation.IsDeleted = true;
            plan.Instalation.DeletedOn = DateTime.UtcNow;
            plan.Instalation.ModifiedOn = DateTime.UtcNow;

            foreach (var floor in plan.House.Floors)
            {
                floor.IsDeleted = true;
                floor.DeletedOn = DateTime.UtcNow;
                floor.ModifiedOn = DateTime.UtcNow;

                foreach (var room in floor.Rooms)
                {
                    room.IsDeleted = true;
                    room.DeletedOn = DateTime.UtcNow;
                    room.ModifiedOn = DateTime.UtcNow;
                }
            }

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Restore(int planId)
        {
            var plan = this.dbContext.Plans
                .Where(x => x.Id == planId)
                .Include(x => x.House)
                .ThenInclude(x => x.Floors)
                .ThenInclude(x => x.Rooms)
                .Include(x => x.Instalation)
                .FirstOrDefault();

            if (plan == null)
            {
                return false;
            }

            plan.IsDeleted = false;
            plan.ModifiedOn = DateTime.UtcNow;

            plan.House.IsDeleted = false;
            plan.House.ModifiedOn = DateTime.UtcNow;

            plan.Instalation.IsDeleted = false;
            plan.Instalation.ModifiedOn = DateTime.UtcNow;

            foreach (var floor in plan.House.Floors)
            {
                floor.IsDeleted = false;
                floor.ModifiedOn = DateTime.UtcNow;

                foreach (var room in floor.Rooms)
                {
                    room.IsDeleted = false;
                    room.ModifiedOn = DateTime.UtcNow;
                }
            }

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PlanFormViewModel> GetById(int planId)
        {
            var house = await this.houseService.GetById(planId);

            var plan = this.dbContext.Plans
                .Where(x => x.Id == planId)
                .Select(x => new PlanFormViewModel
                {
                    Name = x.Name,
                    Price = x.Price,
                    House = house,
                })
                .FirstOrDefault();

            return plan;
        }
    }
}
