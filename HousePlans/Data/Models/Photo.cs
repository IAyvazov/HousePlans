namespace HousePlans.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Photo : BaseModel<int>
    {
        [Required]
        public string Url { get; set; }

        public int HouseId { get; set; }

        public House House { get; set; }
    }
}