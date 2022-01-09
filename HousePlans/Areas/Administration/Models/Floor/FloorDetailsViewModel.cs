namespace HousePlans.Areas.Administration.Models.Floor
{
    using HousePlans.Areas.Administration.Models.Room;

    public class FloorDetailsViewModel
    {
        public FloorDetailsViewModel()
        {
            this.Rooms = new HashSet<RoomDetailsViewModel>();
        }

        public int Number { get; set; }

        public int NumberOfRooms { get; set; }

        public HashSet<RoomDetailsViewModel> Rooms { get; set; }
    }
}
