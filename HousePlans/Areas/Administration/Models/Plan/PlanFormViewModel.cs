namespace HousePlans.Areas.Administration.Models.Plan
{
    using HousePlans.Areas.Administration.Models.House;
    using System.ComponentModel.DataAnnotations;

    using static GlobalConstant;

    public class PlanFormViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Project Name")]
        [StringLength(Plan.NameMaxLength, ErrorMessage = Plan.ErrorMessage, MinimumLength = Plan.NameMinLength)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public HouseFormVIewModel House { get; set; }
    }
}
