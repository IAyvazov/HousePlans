namespace HousePlans.Data.Models
{
    public class House: BaseModel<int>
    {
        public HouseType Type { get; set; }

        public double Area { get; set; }

        public double BuiltUpArea { get; set; }

        public double StepOfTheBuilding { get; set; }

        public double LengthOfThePlot { get; set; }

        public double WidthOfThePlot { get; set; }

        public IEnumerable<Floor> Floors { get; set; }

        public IEnumerable<Photo> Photos { get; set; }

        public Garage Garage { get; set; }

        public Roof Roof { get; set; }

        public bool PassiveHouse { get; set; }
    }
}
