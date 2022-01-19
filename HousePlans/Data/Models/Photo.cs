namespace HousePlans.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Photo : BaseModel<int>
    {
        [Required]
        public string Url { get; set; }

        public int? BuildingId { get; set; }

        public Building Building { get; set; }

        public int? PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}