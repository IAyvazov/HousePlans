using HousePlans.Models.Room;

namespace HousePlans.Models.Floor
{
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
