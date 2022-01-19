namespace HousePlans.Areas.Administration.Services.Material
{
    using HousePlans.Areas.Administration.Models.Material;

    public interface IMateialAdministrationService
    {
        Task<int> CreateMaterial(MaterialFormViewModel model);

        Task<MaterialDetailsViewModel> GetById(int materialId);

        Task<MaterialFormViewModel> GetByPlanId(int planId);
    }
}
