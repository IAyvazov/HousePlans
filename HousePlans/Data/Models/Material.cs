using System.ComponentModel.DataAnnotations;

namespace HousePlans.Data.Models
{
    public class Material : BaseModel<int>
    {
        [Required]
        public string Technology { get; set; }

        [Required]
        public string TypesOfWalls { get; set; }

        [Required]
        public string OverlappingTypes { get; set; }

        [Required]
        public string TypesOfRoof { get; set; }

        public Plan Plan { get; set; }
    }
}
