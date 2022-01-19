namespace HousePlans.Data.Models
{
    using HousePlans.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static GlobalConstant;

    public class Plan : BaseModel<int>
    {
        public Plan()
        {
            this.Photos =new HashSet<Photo>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }

        public int InstalationId { get; set; }

        public Instalation Instalation { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
    }
}
