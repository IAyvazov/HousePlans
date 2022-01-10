namespace HousePlans.Areas.Administration.Services.Room
{
    using HousePlans.Areas.Administration.Models.Room;
    using HousePlans.Data;
    using HousePlans.Data.Models;

    public class RoomAdministrationService : IRoomAdministrationService
    {
        private readonly ApplicationDbContext dbContext;

        public RoomAdministrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateRoom(IEnumerable<RoomFormViewModel> model,int floorId)
        {
            foreach (var room in model)
            {
                var newRoom = new Room
                {
                    CreatedOn = DateTime.UtcNow,
                    Name = room.Name,
                    Area = room.Area,
                    FloorId = floorId,
                };

                await this.dbContext.Rooms.AddAsync(newRoom);
            }
            await this.dbContext.SaveChangesAsync();
        }
    }
}
