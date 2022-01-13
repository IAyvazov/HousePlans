namespace HousePlans.Models.Home
{
    using HousePlans.Areas.Administration.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class SearchModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "House Type")]
        public HouseTypeFormViewModel HouseType { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Build up area")]
        public double BuildUpArea { get; set; }

        [Display(Name = "Step of the building")]
        public double StepOfTheBuilding { get; set; }

        [Display(Name = "Length of the plot")]
        public double LengthOfThePlot { get; set; }

        [Display(Name = "Width of the plot")]
        public double WidthOfThePlot { get; set; }

        [Display(Name = "Number of floors")]
        public int NumberOfFloors { get; set; }

        [Display(Name = "Number of rooms")]
        public string NumberOfRooms { get; set; }

        [Display(Name = "Size of the plot")]
        public double SizeOfThePlot { get; set; }
       
        [Display(Name = "Garage type")]
        public GarageFromViewModel GarageType { get; set; }

        [Display(Name = "House style")]
        public StyleFormViewModel HouseStyle { get; set; }

        [Display(Name = "Roof type")]
        public RoofFormVIewModel RoofType { get; set; }

        [Display(Name = "Pasive house")]
        public bool IsPasive { get; set; }
    }
}
