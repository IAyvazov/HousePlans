namespace HousePlans.Services.Material
{
    using HousePlans.Models.Material;

    public interface IMaterialService
    {
        Task<MaterialDetailsViewModel> GetByHouseId(int houseId);

    }
}
