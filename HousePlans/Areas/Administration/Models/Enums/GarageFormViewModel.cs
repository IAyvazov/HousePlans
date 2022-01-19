namespace HousePlans.Areas.Administration.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum GarageFormViewModel
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Single")]
        Single = 1,

        [Display(Name = "Double")]
        Double = 2,

        [Display(Name = "Many cars")]
        ManyCars = 3,
    }
}
