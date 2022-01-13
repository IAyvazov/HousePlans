namespace HousePlans.Areas.Administration.Models.Room
{
    using System.ComponentModel.DataAnnotations;

    using static GlobalConstant;

    public class RoomFormViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Room name")]
        [StringLength(NameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Room area")]
        [Range(0, int.MaxValue)]
        public double Area { get; set; }
    }
}
