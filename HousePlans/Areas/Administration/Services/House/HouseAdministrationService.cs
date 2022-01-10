namespace HousePlans.Areas.Administration.Services.House
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.Floor;
    using HousePlans.Areas.Administration.Models.House;
    using HousePlans.Areas.Administration.Models.Photo;
    using HousePlans.Areas.Administration.Models.Room;
    using HousePlans.Areas.Administration.Services.Instalation;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;

    public class HouseAdministrationService : IHouseAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IInstalationAdministrationService instalationService;

        public HouseAdministrationService(ApplicationDbContext dbContext,IInstalationAdministrationService instalationService)
        {
            this.dbContext = dbContext;
            this.instalationService = instalationService;
        }

        public async Task<int> CreateHouse(HouseFormVIewModel model)
        {
            string roof = model.Roof.ToString();
            string garage = model.Garage.ToString();
            string style = model.Style.ToString();
            string type = model.Type.ToString();

            var house = new House
            {
                CreatedOn = DateTime.UtcNow,
                Area = model.Area,
                BuiltUpArea = model.BuiltUpArea,
                LengthOfThePlot = model.LengthOfThePlot,
                WidthOfThePlot = model.WidthOfThePlot,
                StepOfTheBuilding = model.StepOfTheBuilding,
                Roof = (Roof)Enum.Parse(typeof(Roof), roof),
                Garage = (Garage)Enum.Parse(typeof(Garage), garage),
                Style = (Style)Enum.Parse(typeof(Style), style),
                Type = (HouseType)Enum.Parse(typeof(HouseType), type),
            };

            await this.dbContext.Houses.AddAsync(house);
            await this.dbContext.SaveChangesAsync();

            return house.Id;
        }

        public async Task<HouseDetailsViewModel> Details(int houseId)
        {
            var name = this.dbContext.Plans.Where(x => x.HouseId == houseId).Select(x => x.Name).FirstOrDefault();

            var instalationId = this.dbContext.Plans
                .Where(x => x.HouseId == houseId)
                .Select(x => x.InstalationId)
                .FirstOrDefault();

            var instalations = await this.instalationService.GetById(instalationId);

            var house = this.dbContext.Houses.Where(x => x.Id == houseId)
                   .Select(x => new HouseDetailsViewModel
                   {
                       Id = x.Id,
                       Name = name,
                       Area = x.Area,
                       BuiltUpArea = x.BuiltUpArea,
                       CreatedOn = x.CreatedOn.ToString(),
                       LengthOfThePlot = x.LengthOfThePlot,
                       StepOfTheBuilding = x.StepOfTheBuilding,
                       WidthOfThePlot = x.WidthOfThePlot,
                       NumberOfFloors = x.Floors.Count(),
                       Roof = (RoofFormVIewModel)Enum.Parse<RoofFormVIewModel>(x.Roof.ToString()),
                       Style = (StyleFormViewModel)Enum.Parse<StyleFormViewModel>(x.Style.ToString()),
                       Type = (HouseTypeFormViewModel)Enum.Parse<HouseTypeFormViewModel>(x.Type.ToString()),
                       Garage = (GarageFromViewModel)Enum.Parse<GarageFromViewModel>(x.Garage.ToString()),
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
                       Photos = x.Photos
                       .Select(x=> new PhotoDetailsViewModel
                       {
                           Url = x.Url,
                       })
                       .ToHashSet(),
                   })
                   .FirstOrDefault();

            return house;
        }
    }
}
