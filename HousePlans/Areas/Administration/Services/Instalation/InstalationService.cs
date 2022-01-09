namespace HousePlans.Areas.Administration.Services.Instalation
{
    using HousePlans.Areas.Administration.Models.Instalation;
    using HousePlans.Data;
    using HousePlans.Data.Models;

    public class InstalationService : IInstalationService
    {
        private readonly ApplicationDbContext dbContext;

        public InstalationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateInstalation(InstalationFormViewModel model)
        {
            var instalation = new Instalation
            {
                CreatedOn = DateTime.UtcNow,
                Biomass = model.Biomass,
                Chimney = model.Chimney,
                EcoPalletCoal = model.EcoPalletCoal,
                Electrical = model.Electrical,
                EnergyRecoveryFan = model.EnergyRecoveryFan,
                FireplaceWJ = model.FireplaceWJ,
                FloorHeating = model.FloorHeating,
                Gas = model.Gas,
                HeatPump = model.HeatPump,
                PelletStove = model.PelletStove,
                Petrol = model.Petrol,
                PhotovoltaicPanelsForElectricity = model.PhotovoltaicPanelsForElectricity,
                SolarHotWaterSystems = model.SolarHotWaterSystems,
                SolidFuel = model.SolidFuel,
            };

            await this.dbContext.Instalations.AddAsync(instalation);
            await this.dbContext.SaveChangesAsync();

            return instalation.Id;
        }

        public async Task<InstalationDetailsViewModel> GetById(int instalationId)
         => this.dbContext.Instalations
                  .Where(x => x.Id == instalationId)
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
