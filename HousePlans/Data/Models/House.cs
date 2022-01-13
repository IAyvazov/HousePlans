namespace HousePlans.Data.Models
{
    using HousePlans.Data.Models.Enums;

    public class House : BaseModel<int>
    {
        public House()
        {
            this.Floors = new HashSet<Floor>();
            this.Photos = new HashSet<Photo>();
        }

        public HouseType Type { get; set; }

        public double Area { get; set; }

        public double BuildUpArea { get; set; }

        public double StepOfTheBuilding { get; set; }

        public double LengthOfThePlot { get; set; }

        public double WidthOfThePlot { get; set; }

        public IEnumerable<Floor> Floors { get; set; }

        public IEnumerable<Photo> Photos { get; set; }

        public Garage Garage { get; set; }

        public Roof Roof { get; set; }

        public Style Style { get; set; }

        public bool PassiveHouse { get; set; }
    }
}
