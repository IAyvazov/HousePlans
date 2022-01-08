namespace HousePlans.Areas.Administration.Models.Floor
{
    using HousePlans.Areas.Administration.Models.Room;
    using System.ComponentModel.DataAnnotations;

    public class FloorFormViewModel
    {
        public FloorFormViewModel()
        {
            this.Rooms = new HashSet<RoomFormViewModel>();
        }

        [DataType(DataType.Text)]
        [Display(Name = "Floor Numbers")]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        public int NumberOfRooms { get; set; }

        public HashSet<RoomFormViewModel> Rooms { get; set; }
    }
}
