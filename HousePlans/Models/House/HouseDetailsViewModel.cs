using HousePlans.Areas.Administration.Models.Enums;
using HousePlans.Models.Floor;
using HousePlans.Models.Photo;

namespace HousePlans.Models.House
{
    public class HouseDetailsViewModel
    {
        public HouseDetailsViewModel()
        {
            this.Floors = new HashSet<FloorDetailsViewModel>();
            this.Photos = new HashSet<PhotoDetailsViewModel>();
        }

        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double BuiltUpArea { get; set; }

        public double StepOfTheBuilding { get; set; }

        public double LengthOfThePlot { get; set; }

        public double WidthOfThePlot { get; set; }

        public bool PassiveHouse { get; set; }

        public HouseTypeFormViewModel Type { get; set; }

        public GarageFormViewModel Garage { get; set; }

        public RoofFormVIewModel Roof { get; set; }

        public StyleFormViewModel Style { get; set; }

        public int NumberOfFloors { get; set; } = 0;

        public HashSet<FloorDetailsViewModel> Floors { get; set; }

        public HashSet<PhotoDetailsViewModel> Photos { get; set; }
    }
}
