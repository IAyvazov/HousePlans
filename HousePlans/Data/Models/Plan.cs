namespace HousePlans.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Plan : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int HouseId { get; set; }

        public House House { get; set; }

        public int InstalationId { get; set; }

        public Instalation Instalation { get; set; }
    }
}
