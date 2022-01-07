namespace HousePlans.Areas.Administration.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum RoofFormVIewModel
    {
        [Display(Name = "Villa")]
        Villa = 0,

        [Display(Name = "Sloping")]
        Sloping = 1,

        [Display(Name = "Pitched")]
        Pitched = 2,

        [Display(Name = "Flat")]
        Flat = 3,
    }
}
