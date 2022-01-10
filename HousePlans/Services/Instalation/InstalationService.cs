namespace HousePlans.Services.Instalation
{
    using HousePlans.Data;
    using HousePlans.Models.Instalation;

    public class InstalationService : IInstalationService
    {
        private readonly ApplicationDbContext dbContext;

        public InstalationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<InstalationDetailsViewModel> GetByHouseId(int houseId)
        => this.dbContext.Instalations
                  .Where(x => x.Plan.HouseId == houseId)
                  .Select(x => new InstalationDetailsViewModel
                  {
                      Biomass = x.Biomass,
                      Chimney = x.Chimney,
                      EcoPalletCoal = x.EcoPalletCoal,
                      Electrical = x.Electrical,
                      EnergyRecoveryFan = x.EnergyRecoveryFan,
                      FireplaceWJ = x.FireplaceWJ,
                      FloorHeating = x.FloorHeating,
                      Gas = x.Gas,
                      HeatPump = x.HeatPump,
                      PelletStove = x.PelletStove,
                      Petrol = x.Petrol,
                      PhotovoltaicPanelsForElectricity = x.PhotovoltaicPanelsForElectricity,
                      SolarHotWaterSystems = x.SolarHotWaterSystems,
                      SolidFuel = x.SolidFuel,
                  })
                  .FirstOrDefault();
    }
}
