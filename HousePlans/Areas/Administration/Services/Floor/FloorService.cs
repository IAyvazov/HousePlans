namespace HousePlans.Areas.Administration.Services.Floor
{
    using HousePlans.Areas.Administration.Models.Floor;
    using HousePlans.Areas.Administration.Services.Room;
    using HousePlans.Data;
    using HousePlans.Data.Models;
    using System.Collections.Generic;

    public class FloorService : IFloorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRoomService roomService;

        public FloorService(ApplicationDbContext dbContext, IRoomService roomService)
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
                    HouseId= houseId,
                };

                await this.dbContext.Floors.AddAsync(newFloor);
                await this.dbContext.SaveChangesAsync();

                await this.roomService.CreateRoom(floor.Rooms, newFloor.Id);

                count++;
            }

        }
    }
}
