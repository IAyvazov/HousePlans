namespace HousePlans.Services.Instalation
{
    using HousePlans.Models.Instalation;

    public interface IInstalationService
    {
        Task<InstalationDetailsViewModel> GetByHouseId(int instalationId);
    }
}
