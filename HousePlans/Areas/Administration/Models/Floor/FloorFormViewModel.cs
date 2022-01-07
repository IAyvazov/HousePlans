namespace HousePlans.Areas.Administration.Models.Floor
{
    using HousePlans.Areas.Administration.Models.Room;
    using System.ComponentModel.DataAnnotations;

    public class FloorFormViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Floor Numbers")]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        public RoomFormViewModel Rooms { get; set; }
    }
}
