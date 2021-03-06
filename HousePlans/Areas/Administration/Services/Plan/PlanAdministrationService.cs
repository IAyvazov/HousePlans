namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Areas.Administration.Services.Floor;
    using HousePlans.Areas.Administration.Services.House;
    using HousePlans.Areas.Administration.Services.Instalation;
    using HousePlans.Areas.Administration.Services.Material;
    using HousePlans.Areas.Administration.Services.Photo;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class PlanAdministrationService : IPlanAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFloorAdministrationService floorService;
        private readonly IHouseAdministrationService houseService;
        private readonly IInstalationAdministrationService instalationService;
        private readonly IPhotoAdministrationService photoService;
        private readonly IMateialAdministrationService materialService;

        public PlanAdministrationService(
            ApplicationDbContext dbContext,
            IFloorAdministrationService floorService,
            IHouseAdministrationService houseService,
            IInstalationAdministrationService instalationService,
            IPhotoAdministrationService photoService,
            IMateialAdministrationService materialService)
        {
            this.dbContext = dbContext;
            this.floorService = floorService;
            this.houseService = houseService;
            this.instalationService = instalationService;
            this.photoService = photoService;
            this.materialService = materialService;
        }

        public async Task<IEnumerable<PlanAllViewModel>> AllPlans()
        {
            var allPlans = this.dbContext.Plans
                .OrderByDescending(x => x.CreatedOn)
                .Select(p => new PlanAllViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    HouseId = p.BuildingId,
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
            await this.photoService.UploadHousePhotos(model.House.Photos, houseId);

            var instalationId = await this.instalationService.CreateInstalation(model.House.Instalation);

            var materialId = await this.materialService.CreateMaterial(model.House.Material);

            if (instalationId == 0)
            {
                return -1;
            }

            var plan = new Plan
            {
                CreatedOn = DateTime.UtcNow,
                Name = model.Name,
                Price = model.Price,
                BuildingId = houseId,
                InstalationId = instalationId,
                MaterialId = materialId,
                Category = (Category)model.Category,
            };


            await this.dbContext.Plans.AddAsync(plan);
            await this.dbContext.SaveChangesAsync();

            await this.photoService.UploadPlanPhotos(model.Photos, plan.Id);

            return plan.Id;
        }

        public async Task<bool> Edit(PlanFormViewModel model, int planId)
        {
            var plan = this.dbContext.Plans
                 .Where(x => x.Id == planId)
                 .Include(x => x.Building)
                 .ThenInclude(x => x.Floors)
                 .ThenInclude(x => x.Rooms)
                 .Include(x => x.Instalation)
                 .Include(x => x.Material)
                 .FirstOrDefault();

            plan.Name = model.Name;
            plan.Price = model.Price;
            plan.Building.Area = model.House.Area;
            plan.Building.BuildUpArea = model.House.BuiltUpArea;
            plan.Building.LengthOfThePlot = model.House.LengthOfThePlot;
            plan.Building.WidthOfThePlot = model.House.WidthOfThePlot;
            plan.Building.StepOfTheBuilding = model.House.StepOfTheBuilding;
            plan.Building.PassiveHouse = model.House.PassiveHouse;
            plan.Building.Type = (HouseType)model.House.Type;
            plan.Building.Garage = (Garage)model.House.Garage;
            plan.Building.Roof = (Roof)model.House.Roof;
            plan.Building.Style = (Style)model.House.Style;
            plan.ModifiedOn = DateTime.UtcNow;

            plan.Instalation.Biomass = model.House.Instalation.Biomass;
            plan.Instalation.Gas = model.House.Instalation.Gas;
            plan.Instalation.Chimney = model.House.Instalation.Chimney;
            plan.Instalation.FloorHeating = model.House.Instalation.FloorHeating;
            plan.Instalation.EnergyRecoveryFan = model.House.Instalation.EnergyRecoveryFan;
            plan.Instalation.EcoPalletCoal = model.House.Instalation.EcoPalletCoal;
            plan.Instalation.Electrical = model.House.Instalation.Electrical;
            plan.Instalation.FireplaceWJ = model.House.Instalation.FireplaceWJ;
            plan.Instalation.HeatPump = model.House.Instalation.HeatPump;
            plan.Instalation.PelletStove = model.House.Instalation.PelletStove;
            plan.Instalation.Petrol = model.House.Instalation.Petrol;
            plan.Instalation.PhotovoltaicPanelsForElectricity = model.House.Instalation.PhotovoltaicPanelsForElectricity;
            plan.Instalation.SolarHotWaterSystems = model.House.Instalation.SolarHotWaterSystems;
            plan.Instalation.SolidFuel = model.House.Instalation.SolidFuel;
            plan.Instalation.ModifiedOn = DateTime.UtcNow;

            plan.Material.Technology = model.House.Material.Technology;
            plan.Material.TypesOfRoof = model.House.Material.TypesOfRoof;
            plan.Material.OverlappingTypes = model.House.Material.OverlappingTypes;
            plan.Material.TypesOfWalls = model.House.Material.TypesOfWalls;
            plan.Material.ModifiedOn = DateTime.UtcNow;

            plan.Category = (Category)model.Category;

            var floors = new HashSet<Floor>();

            var count = 0;
            foreach (var floor in model.House.Floors)
            {
                var rooms = new HashSet<Room>();

                var newFloor = new Floor
                {
                    HouseId = plan.BuildingId,
                    Number = count++,
                    ModifiedOn = DateTime.UtcNow,
                };

                foreach (var room in floor.Rooms)
                {
                    var newRoom = new Room
                    {
                        Name = room.Name,
                        Area = room.Area,
                        FloorId = newFloor.Id,
                    };

                    rooms.Add(newRoom);
                }

                newFloor.Rooms = rooms;
                floors.Add(newFloor);
            }

            plan.Building.Floors = floors;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int planId)
        {
            var plan = this.dbContext.Plans
                .Where(x => x.Id == planId)
                .Include(x => x.Building)
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

            plan.Building.IsDeleted = true;
            plan.Building.DeletedOn = DateTime.UtcNow;
            plan.Building.ModifiedOn = DateTime.UtcNow;

            plan.Instalation.IsDeleted = true;
            plan.Instalation.DeletedOn = DateTime.UtcNow;
            plan.Instalation.ModifiedOn = DateTime.UtcNow;

            foreach (var floor in plan.Building.Floors)
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
                .Include(x => x.Building)
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

            plan.Building.IsDeleted = false;
            plan.Building.ModifiedOn = DateTime.UtcNow;

            plan.Instalation.IsDeleted = false;
            plan.Instalation.ModifiedOn = DateTime.UtcNow;

            foreach (var floor in plan.Building.Floors)
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
                    Category = (CategoryFormViewModel)x.Category,

                })
                .FirstOrDefault();

            return plan;
        }
    }
}
