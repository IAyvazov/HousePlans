namespace HousePlans.Areas.Administration.Models.Room
{
    using System.ComponentModel.DataAnnotations;

    public class RoomFormViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Room name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Room area")]
        [Range(0, int.MaxValue)]
        public double Area { get; set; }
    }
}
