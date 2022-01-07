using System.ComponentModel.DataAnnotations;

namespace HousePlans.Areas.Administration.Models.Photo
{
    public class PhotoFormViewModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Photo")]
        public string Url { get; set; }
    }
}
