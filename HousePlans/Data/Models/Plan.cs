namespace HousePlans.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Plan : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstant.Plan.NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int HouseId { get; set; }

        public House House { get; set; }

        public int InstalationId { get; set; }

        public Instalation Instalation { get; set; }
    }
}
