namespace HousePlans.Areas.Administration.Models.Plan
{
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.House;
    using System.ComponentModel.DataAnnotations;

    using static GlobalConstant;

    public class PlanFormViewModel 
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Project Name")]
        [StringLength(NameMaxLength, ErrorMessage = ErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public CategoryFormViewModel Category { get; set; }

        public HouseFormVIewModel House { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
