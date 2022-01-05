namespace HousePlans.Data.Models
{
    public class Instalation : BaseModel<int>
    {
        public bool Gas { get; set; }

        public bool EcoPalletCoal { get; set; }

        public bool SolidFuel { get; set; }

        public bool Petrol { get; set; }

        public bool Electrical { get; set; }

        public bool Chimney { get; set; }

        public bool SolarHotWaterSystems { get; set; }

        public bool HeatPump { get; set; }

        public bool PelletStove { get; set; }

        public bool EnergyRecoveryFan { get; set; }

        public bool Biomass { get; set; }

        public bool FloorHeating { get; set; }

        public bool FireplaceWJ { get; set; }

        public bool PhotovoltaicPanelsForElectricity { get; set; }
    }
}
