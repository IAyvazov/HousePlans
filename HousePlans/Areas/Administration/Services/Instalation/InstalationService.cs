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
    }
}
