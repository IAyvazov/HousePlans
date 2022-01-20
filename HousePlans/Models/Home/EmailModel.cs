using System.ComponentModel.DataAnnotations;

namespace HousePlans.Models.Home
{
    public class EmailModel
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 9)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 15)]
        public string Content { get; set; }
    }
}
