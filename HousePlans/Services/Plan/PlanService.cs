namespace HousePlans.Services.Plan
{
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;
    using HousePlans.Models.Home;
    using HousePlans.Models.Photo;
    using HousePlans.Models.Plan;
    using HousePlans.Services.House;
    using HousePlans.Services.Instalation;
    using HousePlans.Services.Material;
    using Microsoft.EntityFrameworkCore;

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
                HouseId = x.BuildingId,
                Name = x.Name,
                Price = x.Price,
                CreatedOn = x.CreatedOn.ToString("g"),
                PictureUrl = x.Building.Photos
                .Select(x => x.Url)
                .FirstOrDefault(),
            })
            .ToHashSet();

        public async Task<IEnumerable<PlanAllViewModel>> All(string categoryType)
        {
            _ = Enum.TryParse(categoryType, out Category category);

            var result = this.dbContext.Plans
                 .Where(x => !x.IsDeleted && x.Category == category)
                 .OrderBy(x => x.Price)
                 .Select(x => new PlanAllViewModel
                 {
                     HouseId = x.BuildingId,
                     Name = x.Name,
                     Price = x.Price,
                     CreatedOn = x.CreatedOn.ToString("g"),
                     PictureUrl = x.Building.Photos
                     .Select(x => x.Url)
                     .FirstOrDefault(),
                 })
                 .ToHashSet();

            return result;
        }

        public async Task<PlanDetailsViewModel> GetByHouseId(int houseId)
        {
            var house = await this.houseService.GetById(houseId);

            var instalation = await this.instalationService.GetByHouseId(houseId);

            var material = await this.materialService.GetByHouseId(houseId);

            var plan = this.dbContext.Plans
                 .Where(x => x.BuildingId == houseId && !x.IsDeleted)
                 .Select(x => new PlanDetailsViewModel
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Instalation = instalation,
                     House = house,
                     Material = material,
                     CreatedOn = x.CreatedOn.ToString("g"),
                     Photos = x.Photos.Select(x => new PhotoDetailsViewModel
                     {
                         Url = x.Url,
                     })
                     .ToHashSet(),
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
                    HouseId = x.BuildingId,
                    Name = x.Name,
                    Url = x.Building.Photos
                    .Select(x => x.Url)
                    .FirstOrDefault(),
                })
                .ToHashSet();

            var home = new HomeViewModel
            {
                Plans = plans,
                Urls = this.dbContext.Photos
                .Where(x => x.BuildingId != null && x.PlanId == null)
                .Take(3)
                .Select(x => x.Url)
                .ToArray(),
            };

            return home;
        }

        public async Task<IEnumerable<PlanAllViewModel>> AreaRange(int fromArea, int toArea)
        {
            if (toArea == 0)
            {
                toArea = int.MaxValue;
            }

            var plans = this.dbContext.Plans
                .Where(x => !x.IsDeleted && (x.Building.Area >= fromArea && x.Building.Area <= toArea))
                .Select(x => new PlanAllViewModel
                {
                    HouseId = x.BuildingId,
                    Name = x.Name,
                    Price = x.Price,
                    CreatedOn = x.CreatedOn.ToString("g"),
                    PictureUrl = x.Building.Photos.FirstOrDefault().Url,
                })
            .ToHashSet();

            return plans;
        }

        public async Task<IEnumerable<PlanAllViewModel>> Search(SearchModel searchModel)
        {
            Enum.TryParse<Garage>(searchModel.GarageType.ToString(), out Garage garage);
            Enum.TryParse<Roof>(searchModel.RoofType.ToString(), out Roof roof);
            Enum.TryParse<Style>(searchModel.Style.ToString(), out Style style);
            Enum.TryParse<HouseType>(searchModel.HouseType.ToString(), out HouseType houseType);

            var plans = this.dbContext.Plans
                .Where(x => !x.IsDeleted)
                .Include(x=>x.Building)
                .ThenInclude(x=>x.Floors)
                .ThenInclude(x=>x.Rooms);

            List<PlanAllViewModel> result = new();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                var curr = plans
                    .Where(x => x.Name == searchModel.Name);

                AddResult(result, curr);
            }

            if (searchModel.Price > 0)
            {
                var curr = plans
                    .Where(x => x.Price <= searchModel.Price);

                AddResult(result, curr);
            }

            if (searchModel.BuildUpArea > 0)
            {
                var curr = plans
                    .Where(x => x.Building.BuildUpArea <= searchModel.BuildUpArea);

                AddResult(result, curr);
            }

            if (searchModel.LengthOfThePlot > 0)
            {
                var curr = plans
                    .Where(x => x.Building.LengthOfThePlot <= searchModel.LengthOfThePlot);

                AddResult(result, curr);
            }

            if (searchModel.StepOfTheBuilding > 0)
            {
                var curr = plans
                    .Where(x => x.Building.StepOfTheBuilding <= searchModel.StepOfTheBuilding);

                AddResult(result, curr);
            }

            if (searchModel.WidthOfThePlot > 0)
            {
                var curr = plans
                    .Where(x => x.Building.WidthOfThePlot <= searchModel.WidthOfThePlot);

                AddResult(result, curr);
            }

            if (searchModel.NumberOfFloors > 0)
            {
                var curr = plans
                    .Where(x => x.Building.Floors.Count() == searchModel.NumberOfFloors);

                AddResult(result, curr);
            }

            if (searchModel.NumberOfRooms > 0)
            {
                var curr = plans
                    .Where(x => x.Building.NumberOfRoom == searchModel.NumberOfRooms);

                AddResult(result, curr);
            }

            if (garage.ToString() != "None")
            {
                var curr = plans
                    .Where(x => x.Building.Garage == garage);

                AddResult(result, curr);
            }

            if (roof.ToString() != "None")
            {
                var curr = plans
                    .Where(x => x.Building.Roof == roof);

                AddResult(result, curr);
            }

            if (style.ToString() != "None")
            {
                var curr = plans
                    .Where(x => x.Building.Style == style);

                AddResult(result, curr);
            }

            if (houseType.ToString() != "None")
            {
                var curr = plans
                    .Where(x => x.Building.Type == houseType);

                AddResult(result, curr);
            }

            return result;
        }

        private static void AddResult(List<PlanAllViewModel> result, IQueryable<Plan> plans)
        {
            var res = plans.Select(x => new PlanAllViewModel
            {
                Name = x.Name,
                CreatedOn = x.CreatedOn.ToString("g"),
                HouseId = x.BuildingId,
                PictureUrl = x.Building.Photos
                .Select(x => x.Url)
                .FirstOrDefault(),
                Price = x.Price,
            })
               .ToHashSet();

            result.AddRange(res);
        }
    }
}
