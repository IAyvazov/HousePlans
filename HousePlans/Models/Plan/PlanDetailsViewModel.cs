namespace HousePlans.Models.Plan
{
    using HousePlans.Models.House;
    using HousePlans.Models.Instalation;

    public class PlanDetailsViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CreatedOn { get; set; }

        public HouseDetailsViewModel House { get; set; }

        public InstalationDetailsViewModel Instalation { get; set; }
    }
}
