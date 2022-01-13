namespace HousePlans.Areas.Administration.Models.Material
{
    using System.ComponentModel.DataAnnotations;

    public class MaterialFormViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Technology")]
        public string Technology { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Types Of Walls")]
        public string TypesOfWalls { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Overlapping Types")]
        public string OverlappingTypes { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Types Of Roof")]
        public string TypesOfRoof { get; set; }
    }
}
