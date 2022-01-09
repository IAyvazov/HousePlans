namespace HousePlans.Areas.Administration.Models.House
{
    using System.ComponentModel.DataAnnotations;
    using HousePlans.Areas.Administration.Models.Enums;
    using HousePlans.Areas.Administration.Models.Floor;
    using HousePlans.Areas.Administration.Models.Instalation;
    using HousePlans.Areas.Administration.Models.Photo;

    public class HouseFormVIewModel
    {
        public HouseFormVIewModel()
        {
            this.Floors = new HashSet<FloorFormViewModel>();
        }

        [DataType(DataType.Text)]
        [Display(Name = "Area")]
        [Range(0, int.MaxValue)]
        public double Area { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Built Up Area")]
        [Range(0, int.MaxValue)]
        public double BuiltUpArea { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Step Of The Building")]
        [Range(0, int.MaxValue)]
        public double StepOfTheBuilding { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Length Of The Plot")]
        [Range(0, int.MaxValue)]
        public double LengthOfThePlot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Width Of The Plot")]
        [Range(0, int.MaxValue)]
        public double WidthOfThePlot { get; set; }

        [Display(Name = "Passive House")]
        public bool PassiveHouse { get; set; }

        public HouseTypeFormViewModel Type { get; set; }

        public GarageFromViewModel Garage { get; set; }

        public RoofFormVIewModel Roof { get; set; }

        public StyleFormViewModel Style { get; set; }

        public int NumberOfFloors { get; set; } = 0;

        public HashSet<FloorFormViewModel> Floors { get; set; }

        //public InstalationFormViewModel Instalation { get; set; }

        //public PhotoFormViewModel Photos { get; set; }
    }
}
