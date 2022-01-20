namespace HousePlans.Areas.Administration.Services.House
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.Floor;
    using HousePlans.Areas.Administration.Models.House;
    using HousePlans.Areas.Administration.Models.Material;
    using HousePlans.Areas.Administration.Models.Photo;
    using HousePlans.Areas.Administration.Models.Room;
    using HousePlans.Areas.Administration.Services.Floor;
    using HousePlans.Areas.Administration.Services.Instalation;
    using HousePlans.Areas.Administration.Services.Material;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;

    public class HouseAdministrationService : IHouseAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IInstalationAdministrationService instalationService;
        private readonly IFloorAdministrationService floorService;
        private readonly IMateialAdministrationService materialService;

        public HouseAdministrationService(
            ApplicationDbContext dbContext,
            IInstalationAdministrationService instalationService,
            IFloorAdministrationService floorService,
            IMateialAdministrationService materialService)
        {
            this.dbContext = dbContext;
            this.instalationService = instalationService;
            this.floorService = floorService;
            this.materialService = materialService;
        }

        public async Task<int> CreateHouse(HouseFormVIewModel model)
        {
            string roof = model.Roof.ToString();
            string garage = model.Garage.ToString();
            string style = model.Style.ToString();
            string type = model.Type.ToString();

            var house = new Building
            {
                CreatedOn = DateTime.UtcNow,
                Area = model.Area,
                BuildUpArea = model.BuiltUpArea,
                LengthOfThePlot = model.LengthOfThePlot,
                WidthOfThePlot = model.WidthOfThePlot,
                StepOfTheBuilding = model.StepOfTheBuilding,
                Roof = (Roof)Enum.Parse(typeof(Roof), roof),
                Garage = (Garage)Enum.Parse(typeof(Garage), garage),
                Style = (Style)Enum.Parse(typeof(Style), style),
                Type = (HouseType)Enum.Parse(typeof(HouseType), type),
                NumberOfRoom = model.Floors.Sum(x => x.Rooms.Count()),
            };

            await this.dbContext.Buildings.AddAsync(house);
            await this.dbContext.SaveChangesAsync();

            return house.Id;
        }

        public async Task<HouseDetailsViewModel> Details(int houseId)
        {
            var name = this.dbContext.Plans
                .Where(x => x.BuildingId == houseId)
                .Select(x => x.Name)
                .FirstOrDefault();

            var instalationId = this.dbContext.Plans
                .Where(x => x.BuildingId == houseId)
                .Select(x => x.InstalationId)
                .FirstOrDefault();

            var materialId = this.dbContext.Plans
                .Where(x => x.BuildingId == houseId)
                .Select(x => x.MaterialId)
                .FirstOrDefault();

            var material = await this.materialService.GetById(materialId);

            var instalations = await this.instalationService.GetById(instalationId);

            var house = this.dbContext.Buildings.Where(x => x.Id == houseId)
                   .Select(x => new HouseDetailsViewModel
                   {
                       Id = x.Id,
                       Name = name,
                       Area = x.Area,
                       BuiltUpArea = x.BuildUpArea,
                       CreatedOn = x.CreatedOn.ToString(),
                       LengthOfThePlot = x.LengthOfThePlot,
                       StepOfTheBuilding = x.StepOfTheBuilding,
                       WidthOfThePlot = x.WidthOfThePlot,
                       NumberOfFloors = x.Floors.Count(),
                       Roof = (RoofFormVIewModel)Enum.Parse<RoofFormVIewModel>(x.Roof.ToString()),
                       Style = (StyleFormViewModel)Enum.Parse<StyleFormViewModel>(x.Style.ToString()),
                       Type = (HouseTypeFormViewModel)Enum.Parse<HouseTypeFormViewModel>(x.Type.ToString()),
                       Garage = (GarageFormViewModel)Enum.Parse<GarageFormViewModel>(x.Garage.ToString()),
                       PassiveHouse = x.PassiveHouse,
                       Floors = x.Floors
                       .Select(x => new FloorDetailsViewModel
                       {
                           Number = x.Number,
                           NumberOfRooms = x.Rooms.Count(),
                           Rooms = x.Rooms.Select(x => new RoomDetailsViewModel
                           {
                               Name = x.Name,
                               Area = x.Area
                           })
                           .ToHashSet(),
                       })
                       .ToHashSet(),
                       Instalation = instalations,
                       Materials = material,
                       Photos = x.Photos
                       .Select(x => new PhotoDetailsViewModel
                       {
                           Url = x.Url,
                       })
                       .ToHashSet(),
                   })
                   .FirstOrDefault();

            return house;
        }

        public async Task<HouseFormVIewModel> GetById(int planId)
        {
            var instalation = await this.instalationService.GetByPlanId(planId);

            var material = await this.materialService.GetByPlanId(planId);

            var floor = await this.floorService.GetByPlanId(planId);

            var house = this.dbContext.Plans
                   .Where(x => x.Id == planId)
                   .Select(x => new HouseFormVIewModel
                   {
                       Area = x.Building.Area,
                       BuiltUpArea = x.Building.BuildUpArea,
                       LengthOfThePlot = x.Building.LengthOfThePlot,
                       StepOfTheBuilding = x.Building.StepOfTheBuilding,
                       WidthOfThePlot = x.Building.WidthOfThePlot,
                       NumberOfFloors = x.Building.Floors.Count(),
                       PassiveHouse = x.Building.PassiveHouse,
                       Garage = (GarageFormViewModel)x.Building.Garage,
                       Roof = (RoofFormVIewModel)x.Building.Roof,
                       Style = (StyleFormViewModel)x.Building.Style,
                       Type = (HouseTypeFormViewModel)x.Building.Type,
                       Instalation = instalation,
                       Floors = floor.ToHashSet(),
                       Material = material,
                   })
                   .FirstOrDefault();

            return house;
        }
    }
}
