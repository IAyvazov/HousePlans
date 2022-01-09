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
                Biomass = false,
                Chimney = false,
                CreatedOn = DateTime.UtcNow,
                EcoPalletCoal = false,
                Electrical = false,
                EnergyRecoveryFan = false,
                FireplaceWJ = false,
                FloorHeating = false,
                Gas = false,
                HeatPump = false,
                PelletStove = false,
                Petrol = false,
                PhotovoltaicPanelsForElectricity = false,
                SolarHotWaterSystems = false,
                SolidFuel = false,
            };

            await this.dbContext.Instalations.AddAsync(instalation);
            await this.dbContext.SaveChangesAsync();

            return instalation.Id;
        }
    }
}
