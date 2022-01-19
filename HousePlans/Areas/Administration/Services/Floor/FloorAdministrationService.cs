namespace HousePlans.Areas.Administration.Services.Floor
{
    using HousePlans.Areas.Administration.Models.Floor;
    using HousePlans.Areas.Administration.Models.Room;
    using HousePlans.Areas.Administration.Services.Room;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using System.Collections.Generic;

    public class FloorAdministrationService : IFloorAdministrationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRoomAdministrationService roomService;

        public FloorAdministrationService(ApplicationDbContext dbContext, IRoomAdministrationService roomService)
        {
            this.dbContext = dbContext;
            this.roomService = roomService;
        }

        public async Task CreateFloor(IEnumerable<FloorFormViewModel> model, int houseId)
        {
            var count = 0;

            foreach (var floor in model)
            {
                var newFloor = new Floor
                {
                    CreatedOn = DateTime.UtcNow,
                    Number = count,
                    HouseId = houseId,
                };

                await this.dbContext.Floors.AddAsync(newFloor);
                await this.dbContext.SaveChangesAsync();

                await this.roomService.CreateRoom(floor.Rooms, newFloor.Id);

                count++;
            }

        }

        public async Task<IEnumerable<FloorFormViewModel>> GetByPlanId(int planId)
        {
            var floors = this.dbContext.Plans
                .Where(x => x.Id == planId)
                .Select(x => x.Building.Floors
                .Select(x => new FloorFormViewModel
                {
                    Number = x.Number,
                    NumberOfRooms = x.Rooms.Count(),
                    Rooms = x.Rooms.Select(x => new RoomFormViewModel
                    {
                        Area = x.Area,
                        Name = x.Name,
                    }).ToHashSet(),
                }).ToHashSet())
                .FirstOrDefault();

            return floors;
        }
    }
}
