namespace HousePlans.Areas.Administration.Models.Enums
{
    using System.ComponentModel.DataAnnotations;
    
    public enum HouseTypeFormViewModel
    {
        [Display(Name = "House")]
        House = 0,

        [Display(Name = "Double House")]
        DoubleHouse = 1,

        [Display(Name = "Office")]
        Office = 2,

        [Display(Name = "Guest House")]
        GuestHouse = 3,
    }
}
