namespace HousePlans.Areas.Administration.Services.Instalation
{
    using HousePlans.Areas.Administration.Models.Instalation;

    public interface IInstalationService
    {
        Task<int> CreateInstalation(InstalationFormViewModel model);
    }
}
