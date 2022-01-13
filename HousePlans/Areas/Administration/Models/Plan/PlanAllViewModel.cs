namespace HousePlans.Areas.Administration.Models.Plan
{

    public class PlanAllViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int HouseId { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedOn { get; set; }

        public string DeletedOn { get; set; }

        public string ModifiedOn { get; set; }
    }
}
