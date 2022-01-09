namespace HousePlans.Areas.Administration.Services.House
{
    using HousePlans.Areas.Administration.Models.House;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;

    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext dbContext;

        public HouseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }
}
