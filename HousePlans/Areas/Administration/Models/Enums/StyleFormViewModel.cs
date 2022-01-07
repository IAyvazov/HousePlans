namespace HousePlans.Areas.Administration.Models.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum StyleFormViewModel
    {
        [Display(Name = "Modern")]
        Modern = 0,

        [Display(Name = "Classic")]
        Classic = 1,

        [Display(Name = "Contemporary")]
        Contemporary = 2,

        [Display(Name = "Old Polish")]
        OldPolish = 3,
    }
}
