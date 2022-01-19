namespace HousePlans.Models.Plan
{
    using HousePlans.Models.Home;
    using HousePlans.Models.House;
    using HousePlans.Models.Instalation;
    using HousePlans.Models.Material;
    using HousePlans.Models.Photo;

    public class PlanDetailsViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CreatedOn { get; set; }

        public HouseDetailsViewModel House { get; set; }

        public InstalationDetailsViewModel Instalation { get; set; }

        public MaterialDetailsViewModel Material { get; set; }

        public SearchModel Search { get; set; }

        public HashSet<PhotoDetailsViewModel> Photos { get; set; }
    }
}
