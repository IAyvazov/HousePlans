namespace HousePlans.Areas.Administration.Services.Instalation
{
    using HousePlans.Areas.Administration.Models.Instalation;

    public interface IInstalationAdministrationService
    {
        Task<int> CreateInstalation(InstalationFormViewModel model);

        Task<InstalationDetailsViewModel> GetById(int instalationId);

        Task<InstalationFormViewModel> GetByPlanId(int planId);
    }
}
