namespace HousePlans.Areas.Administration.Services.Plan
{
    using HousePlans.Areas.Administration.Models.Plan;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using HousePlans.Data.Models.Enums;

    public class PlanService : IPlanService
    {
        private readonly ApplicationDbContext dbContext;

        public PlanService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreatePlan(PlanFormViewModel model)
        {

            string roof = model.House.Roof.ToString();
            string garage = model.House.Garage.ToString();
            string style = model.House.Style.ToString();
            string type = model.House.Type.ToString();

            var house = new House
            {
                CreatedOn = DateTime.UtcNow,
                Area = model.House.Area,
                BuiltUpArea = model.House.BuiltUpArea,
                LengthOfThePlot = model.House.LengthOfThePlot,
                WidthOfThePlot = model.House.WidthOfThePlot,
                StepOfTheBuilding = model.House.StepOfTheBuilding,
                Roof = (Roof)Enum.Parse(typeof(Roof), roof),
                Garage = (Garage)Enum.Parse(typeof(Garage), garage),
                Style = (Style)Enum.Parse(typeof(Style), style),
                Type = (HouseType)Enum.Parse(typeof(HouseType), type),
            };

            await dbContext.Houses.AddAsync(house);

            var count = 0;

            foreach (var floor in model.House.Floors)
            {
                var newFloor = new Floor
                {
                    CreatedOn = DateTime.UtcNow,
                    Number = count,
                    HouseId= house.Id,
                };

               await dbContext.Floors.AddAsync(newFloor);

                foreach (var room in floor.Rooms)
                {
                    var newRoom = new Room
                    {
                        CreatedOn= DateTime.UtcNow,
                        Name = room.Name,
                        Area = room.Area,
                        FloorId =newFloor.Id,
                    };

                    await dbContext.Rooms.AddAsync(newRoom);
                }

                count++;
            }


            var plan = new Plan
            {
                CreatedOn = DateTime.UtcNow,
                Name = model.Name,
                Price = model.Price,
                HouseId = house.Id,
            };

            await this.dbContext.AddAsync(plan);
            await this.dbContext.SaveChangesAsync();

            return plan.Id;
        }
    }
}
