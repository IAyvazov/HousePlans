namespace HousePlans.Areas.Administration.Services.Room
{
    using HousePlans.Areas.Administration.Models.Room;

    public interface IRoomService
    {
        Task CreateRoom(IEnumerable<RoomFormViewModel> model, int floorId);
    }
}
