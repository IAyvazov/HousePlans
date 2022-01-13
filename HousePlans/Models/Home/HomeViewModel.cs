namespace HousePlans.Models.Home
{
    using HousePlans.Models.Plan;

    public class HomeViewModel
    {
        public IEnumerable<PlanHomeViewModel> Plans { get; set; }

        public string[] Urls { get; set; }
    }
}
