namespace HousePlans.Services.House
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Data;
    using HousePlans.Models.Floor;
    using HousePlans.Models.House;
    using HousePlans.Models.Photo;
    using HousePlans.Models.Room;

    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext dbContext;

        public HouseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<HouseDetailsViewModel> GetById(int houseId)
        {

            var house = this.dbContext.Houses.Where(x => x.Id == houseId && !x.IsDeleted)
                   .Select(x => new HouseDetailsViewModel
                   {
                       Id = x.Id,
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
    }
}
